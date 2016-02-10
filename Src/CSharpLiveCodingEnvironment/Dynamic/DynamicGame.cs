using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CSharpLiveCodingEnvironment.CodeCompilation;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class DynamicGame
    {
        private readonly DynamicGameSimulator _dynamicGameSimulator;
        private readonly ManualResetEvent _evExit = new ManualResetEvent(false);

        private readonly Dictionary<char, bool> _input = new Dictionary<char, bool>();

        private readonly object _locker = new object();

        private readonly ToolStripStatusLabel _logLabel;
        public readonly GraphicsControl GraphicsControl;
        private CompiledData _compiledDataToBeReplaced;

        private bool _justPaused;

        private Task _sceneLoopTask;
        public int CurrentTrackBarValue;
        public bool NeedToSimulateTimelapseScene;

        public DynamicGame(GraphicsControl g, ToolStripStatusLabel logLabel)
        {
            g.DrawingFunc = DrawScene;
            GraphicsControl = g;
            _dynamicGameSimulator = new DynamicGameSimulator(this);

            for (int i = 'A'; i <= 'Z'; i += 1)
            {
                _input[(char) i] = false;
            }
            _logLabel = logLabel;
        }

        public bool Paused { get; private set; }

        public CompiledData CompiledData { get; private set; }

        public event FieldsChangedHandler FieldsChanged;
        public event EventHandler CurrentTrackBarValueChanged;
        public event EventHandler PausedChanged;

        public void ReplaceCompiledData(CompiledData cd)
        {
            _compiledDataToBeReplaced = cd;
        }

        public bool TickDelegateExists()
        {
            return CompiledData?.TickDelegate != null;
        }

        private void DrawScene(DrawingContext dc)
        {
            dc.DrawRectangle(Brushes.White, null,
                new Rect(0, 0, GraphicsControl.ActualWidth, GraphicsControl.ActualHeight));
            if (CompiledData != null)
            {
                CompiledData.TryInvokeDrawDelegate(dc);
                if (Paused && CompiledData.DrawTrackDelegates.Count > 0)
                {
                    dc.DrawImage(_dynamicGameSimulator.TimelapseScene,
                        new Rect(0, 0, GraphicsControl.ActualWidth, GraphicsControl.ActualHeight));
                }
            }
        }

        private void MoveScene(double dt, Dictionary<char, bool> input, int frame)
        {
            if (Paused)
            {
                CompiledData.SetGameState(_dynamicGameSimulator.Snapshots[frame].State);
            }
            else
            {
                CompiledData?.TryInvokeTickDelegate(dt, input);
            }
        }

        public void Start()
        {
            _sceneLoopTask?.Dispose();
            _sceneLoopTask = new Task(GameTick, TaskCreationOptions.LongRunning);
            _sceneLoopTask.Start();
        }

        public void Stop()
        {
            _evExit.Set();
            _sceneLoopTask.Wait();
            _evExit.Close();
        }

        public void Puase()
        {
            _justPaused = true;
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public void SetInput(char key, bool v)
        {
            _input[key] = v;
        }

        private bool FireFieldsChangedEvent()
        {
            if (CompiledData == null) return false;
            var handler = FieldsChanged;
            if (handler != null)
            {
                var fields = new Tuple<string, string>[CompiledData.Fields.Length];
                for (var i = 0; i < fields.Length; ++i)
                {
                    var val = CompiledData.Fields[i].GetValue(CompiledData.Instance);
                    fields[i] = new Tuple<string, string>(CompiledData.Fields[i].Name, val?.ToString() ?? "null");
                }
                handler.Invoke(this, new FieldsChangedEventArgs(fields));
                return true;
            }
            return false;
        }

        private void GameTick()
        {
            var dtStopwatch = new Stopwatch();
            var gameTickStopwatch = new Stopwatch();
            var fieldsChangedStopwacth = new Stopwatch();
            var immediateFieldsChanged = false;

            uint ii = 0;
            fieldsChangedStopwacth.Start();
            while (!_evExit.WaitOne(SettingsForm.Instance.DesiredDt))
            {
                double dt;
                lock (_locker)
                {
                    gameTickStopwatch.Restart();

                    // copy values to local variables in case they change during game tick
                    var localCurrentTrackBarValue = CurrentTrackBarValue;
                    var localNeedToSimulateTimelapseScene = NeedToSimulateTimelapseScene;
                    NeedToSimulateTimelapseScene = false;

                    // react to StoreLastFrames value change in pause mode
                    if (Paused && _dynamicGameSimulator.Snapshots.Count != SettingsForm.Instance.StoreLastFrames)
                    {
                        if (_dynamicGameSimulator.Snapshots.Count > SettingsForm.Instance.StoreLastFrames)
                        {
                            var count = _dynamicGameSimulator.Snapshots.Count - SettingsForm.Instance.StoreLastFrames;
                            _dynamicGameSimulator.RemoveSnapshotsFromEnd(count);
                            localCurrentTrackBarValue = Math.Min(CurrentTrackBarValue,
                                _dynamicGameSimulator.Snapshots.Count);
                            CurrentTrackBarValue = localCurrentTrackBarValue;
                        }
                        else
                        {
                            var count = SettingsForm.Instance.StoreLastFrames - _dynamicGameSimulator.Snapshots.Count;
                            if (_justPaused)
                            {
                                localCurrentTrackBarValue = _dynamicGameSimulator.Snapshots.Count;
                                CurrentTrackBarValue = localCurrentTrackBarValue;
                            }
                            _dynamicGameSimulator.AddDummySnapshots(count);
                            _dynamicGameSimulator.SimulateGame(_dynamicGameSimulator.Snapshots.Count - count - 1);
                            if (_justPaused)
                            {
                                CurrentTrackBarValueChanged?.Invoke(this, EventArgs.Empty);
                            }
                        }
                        GraphicsControl.Dispatcher.Invoke(_dynamicGameSimulator.RenderTimelapseScene);
                    }
                    _justPaused = false;

                    // perform hot code swapping
                    var localInput = new Dictionary<char, bool>(_input);
                    var copyOfCompiledDataToBeReplaced = _compiledDataToBeReplaced;
                    if (copyOfCompiledDataToBeReplaced != null)
                    {
                        var canGoOn = true;
                        if (SettingsForm.Instance.CheckInfiniteLoops)
                        {
                            var state = copyOfCompiledDataToBeReplaced.DumpGameState();
                            canGoOn = copyOfCompiledDataToBeReplaced.CheckWhileTrue(16, localInput);
                            copyOfCompiledDataToBeReplaced.SetGameState(state);
                        }
                        if (canGoOn)
                        {
                            if (copyOfCompiledDataToBeReplaced.NeedToSaveState)
                            {
                                var state = CompiledData.DumpGameState();
                                CompiledData = copyOfCompiledDataToBeReplaced;
                                CompiledData.SetGameState(state);
                            }
                            else
                            {
                                CompiledData = copyOfCompiledDataToBeReplaced;
                                CompiledData.TryInvokeInitDelegate();
                                Paused = false;
                                localNeedToSimulateTimelapseScene = false;
                                _dynamicGameSimulator.RemoveSnapshotsFromEnd(_dynamicGameSimulator.Snapshots.Count);
                                PausedChanged?.Invoke(this, EventArgs.Empty);
                            }
                        }
                        _compiledDataToBeReplaced = null;
                    }

                    // simulate game
                    if (localNeedToSimulateTimelapseScene)
                    {
                        _dynamicGameSimulator.SimulateGame(localCurrentTrackBarValue - 1);
                    }

                    // call move delegate
                    dtStopwatch.Stop();
                    dt = 1000.0*dtStopwatch.ElapsedTicks/Stopwatch.Frequency;
                    var b = localCurrentTrackBarValue < _dynamicGameSimulator.Snapshots.Count && !Paused;
                    MoveScene(b ? _dynamicGameSimulator.Snapshots[localCurrentTrackBarValue].Dt : dt,
                        b && SettingsForm.Instance.UseTrackedInput
                            ? _dynamicGameSimulator.Snapshots[localCurrentTrackBarValue].Input
                            : localInput, localCurrentTrackBarValue - 1);
                    dtStopwatch.Restart();

                    // take snapshot
                    if (!Paused)
                    {
                        if (localCurrentTrackBarValue < _dynamicGameSimulator.Snapshots.Count)
                        {
                            CurrentTrackBarValue = localCurrentTrackBarValue + 1;
                            CurrentTrackBarValueChanged?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            _dynamicGameSimulator.TakeSnapshot(new Dictionary<char, bool>(localInput), dt,
                                SettingsForm.Instance.StoreLastFrames);
                        }
                    }

                    // render TimelapseScene
                    if (localNeedToSimulateTimelapseScene)
                    {
                        GraphicsControl.Dispatcher.Invoke(_dynamicGameSimulator.RenderTimelapseScene);
                    }

                    // call draw delegate
                    if (!GraphicsControl.IsRendering)
                    {
                        GraphicsControl.Dispatcher.BeginInvoke(DispatcherPriority.Render,
                            (MethodInvoker) GraphicsControl.InvalidateVisual);
                    }

                    // fire FieldsChanged event
                    if (fieldsChangedStopwacth.ElapsedMilliseconds >= 100 || immediateFieldsChanged)
                    {
                        fieldsChangedStopwacth.Restart();
                        immediateFieldsChanged = !FireFieldsChangedEvent();
                    }
                }

                // wait
                if (SettingsForm.Instance.WaitAfterEachTick)
                {
                    dtStopwatch.Stop();
                    Thread.Sleep(SettingsForm.Instance.WaitAfterEachTickMsec);
                    dtStopwatch.Start();
                }

                gameTickStopwatch.Stop();

                // log
                if (ii++%40 == 0)
                {
                    _logLabel.Owner.BeginInvoke((MethodInvoker) (() => _logLabel.Text =
                        $"GameTick = {1000.0*gameTickStopwatch.ElapsedTicks/Stopwatch.Frequency:F3}; dt = {dt:F3}"));
                }
            }
        }

        public byte[] GetCurrentFramePngBytes()
        {
            lock (_locker)
            {
                var rtb = new RenderTargetBitmap((int) GraphicsControl.ActualWidth, (int) GraphicsControl.ActualHeight,
                    96,
                    96, PixelFormats.Default);

                var dv = new DrawingVisual();
                using (var dc = dv.RenderOpen())
                {
                    dc.DrawRectangle(new VisualBrush(GraphicsControl), null,
                        new Rect(new Point(), new Point(GraphicsControl.ActualWidth, GraphicsControl.ActualHeight)));
                }
                rtb.Render(dv);

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
                var ms = new MemoryStream();
                pngEncoder.Save(ms);
                ms.Close();
                return ms.ToArray();
            }
        }

        public byte[] GetStoredFramesGifBytes()
        {
            lock (_locker)
            {
                var gifEncoder = new GifBitmapEncoder();

                var state = CompiledData.DumpGameState();
                for (var i = 0; i < _dynamicGameSimulator.Snapshots.Count; ++i)
                {
                    var rtb = new RenderTargetBitmap((int) GraphicsControl.ActualWidth,
                        (int) GraphicsControl.ActualHeight, 96, 96, PixelFormats.Default);
                    var dv = new DrawingVisual();
                    CompiledData.SetGameState(_dynamicGameSimulator.Snapshots[i].State);
                    using (var dc = dv.RenderOpen())
                    {
                        CompiledData.TryInvokeDrawDelegate(dc);
                    }
                    rtb.Render(dv);
                    gifEncoder.Frames.Add(BitmapFrame.Create(rtb));
                }
                CompiledData.SetGameState(state);

                var metadataPtr = -1;
                byte[] data;
                using (var stream = new MemoryStream())
                {
                    gifEncoder.Save(stream);
                    data = stream.ToArray();
                }

                for (var i = 0; i < data.Length - 2; ++i)
                {
                    if (data[i] == 0)
                    {
                        if (data[i + 1] == 0x21)
                        {
                            if (data[i + 2] == 0xF9)
                            {
                                metadataPtr = i;
                                break;
                            }
                        }
                    }
                }

                var appExt = new byte[]
                {
                    0x21, 0xFF, 0xB, 0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45, 0x32, 0x2E, 0x30, 0x3, 0x1, 0x0,
                    0x0,
                    0x0
                };
                var temp = new byte[data.Length + appExt.Length];
                Array.Copy(data, temp, metadataPtr);
                Array.Copy(appExt, 0, temp, metadataPtr + 1, appExt.Length);
                Array.Copy(data, metadataPtr + 1, temp, metadataPtr + appExt.Length + 1, data.Length - metadataPtr - 1);
                data = temp;

                for (var x = 0; x < data.Length - 3; ++x)
                {
                    if (data[x] == 0)
                    {
                        if (data[x + 1] == 0x21)
                        {
                            if (data[x + 2] == 0xF9)
                            {
                                if (data[x + 3] == 4)
                                {
                                    var delay = BitConverter.GetBytes(Math.Max(SettingsForm.Instance.DesiredDt/10, 2));
                                    data[x + 5] = delay[0];
                                    data[x + 6] = delay[1];
                                }
                            }
                        }
                    }
                }
                return data;
            }
        }
    }
}