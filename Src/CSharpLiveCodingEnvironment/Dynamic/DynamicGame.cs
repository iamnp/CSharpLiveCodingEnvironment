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

        private readonly object _locker = new object();

        private readonly ToolStripStatusLabel _logLabel;
        public readonly GraphicsControl GraphicsControl;
        private CompiledData _compiledDataToBeReplaced;
        private int _currentPausedFrame = -1;

        private Task _sceneLoopTask;
        public int CurrentTrackBarValue;
        public bool NeedToSimulateTimelapseScene;
        public bool Paused;

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

        public CompiledData CompiledData { get; private set; }

        public event FieldsChangedHandler FieldsChanged;

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

        public void SetInput(char key, bool v)
        {
            lock (_locker)
            {
                _input[key] = v;
            }
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

                var localCurrentTrackBarValue = CurrentTrackBarValue;
                if (localCurrentTrackBarValue - 1 > _dynamicGameSimulator.Snapshots.Count - 1)
                    localCurrentTrackBarValue = _dynamicGameSimulator.Snapshots.Count;

                //hot replace code
                var localNeedToSimulateTimelapseScene = NeedToSimulateTimelapseScene;
                Dictionary<char, bool> localInput;
                lock (_locker)
                {
                    localInput = new Dictionary<char, bool>(_input);
                }
                var copyOfCompiledDataToBeReplaced = _compiledDataToBeReplaced;
                if (copyOfCompiledDataToBeReplaced != null)
                {
                    var cont = true;
                    if (SettingsForm.Instance.CheckInfiniteLoops)
                    {
                        var state = copyOfCompiledDataToBeReplaced.DumpGameState();
                        cont = copyOfCompiledDataToBeReplaced.CheckWhileTrue(16, localInput);
                        copyOfCompiledDataToBeReplaced.SetGameState(state);
                    }
                    if (cont)
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
                        }
                    }
                    _compiledDataToBeReplaced = null;
                }

                // simulate game if needed
                if (localNeedToSimulateTimelapseScene)
                {
                    _dynamicGameSimulator.SimulateGame(localCurrentTrackBarValue - 1);
                }

                //move
                dtStopwatch.Stop();
                var dt = 1000.0*dtStopwatch.ElapsedTicks/Stopwatch.Frequency;
                var b = _currentPausedFrame != -1 && _currentPausedFrame < _dynamicGameSimulator.Snapshots.Count - 1 &&
                        !Paused;
                MoveScene(b ? _dynamicGameSimulator.Snapshots[_currentPausedFrame + 1].Dt : dt,
                    b && SettingsForm.Instance.UseTrackedInput
                        ? _dynamicGameSimulator.Snapshots[_currentPausedFrame + 1].Input
                        : localInput, localCurrentTrackBarValue - 1);
                dtStopwatch.Restart();

                //take snapshot
                if (Paused)
                {
                    _currentPausedFrame = localCurrentTrackBarValue - 1;
                }
                else
                {
                    if (_currentPausedFrame != -1 && _currentPausedFrame < _dynamicGameSimulator.Snapshots.Count - 1)
                    {
                        _currentPausedFrame += 1;
                    }
                    else
                    {
                        _dynamicGameSimulator.TakeSnapshot(new Dictionary<char, bool>(localInput), dt,
                            SettingsForm.Instance.StoreLastFrames);
                    }
                }

                //simulate if needed
                if (Paused && localNeedToSimulateTimelapseScene)
                {
                    GraphicsControl.Dispatcher.Invoke(_dynamicGameSimulator.RenderTimelapseScene);
                }

                //draw
                GraphicsControl.Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (MethodInvoker) GraphicsControl.InvalidateVisual);
                if (localNeedToSimulateTimelapseScene) NeedToSimulateTimelapseScene = false;

                //wait if needed
                if (SettingsForm.Instance.WaitAfterEachTick)
                {
                    dtStopwatch.Stop();
                    Thread.Sleep(SettingsForm.Instance.WaitAfterEachTickMsec);
                    dtStopwatch.Start();
                }

                gameTickStopwatch.Stop();

                //log
                if (ii++%40 == 0)
                {
                    _logLabel.Owner.BeginInvoke((MethodInvoker) (() => _logLabel.Text =
                        $"GameTick = {1000.0*gameTickStopwatch.ElapsedTicks/Stopwatch.Frequency:F3}; dt = {dt:F3}"));
                }

                //fire event if needed
                if (fieldsChangedStopwacth.ElapsedMilliseconds >= 100 || immediateFieldsChanged)
                {
                    fieldsChangedStopwacth.Restart();
                    immediateFieldsChanged = !FireFieldsChangedEvent();
                }
            }
        }
    }
}