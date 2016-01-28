using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using CSharpLiveCodingEnvironment.CodeCompilation;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class DynamicGame
    {
        private readonly DynamicGameSimulator _dynamicGameSimulator;
        private readonly ManualResetEvent _evExit = new ManualResetEvent(false);

        private readonly Dictionary<char, bool> _input = new Dictionary<char, bool>();

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
                        localCurrentTrackBarValue = Math.Min(CurrentTrackBarValue, _dynamicGameSimulator.Snapshots.Count);
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
                var dt = 1000.0*dtStopwatch.ElapsedTicks/Stopwatch.Frequency;
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

                // fire FieldsChanged event
                if (fieldsChangedStopwacth.ElapsedMilliseconds >= 100 || immediateFieldsChanged)
                {
                    fieldsChangedStopwacth.Restart();
                    immediateFieldsChanged = !FireFieldsChangedEvent();
                }
            }
        }
    }
}