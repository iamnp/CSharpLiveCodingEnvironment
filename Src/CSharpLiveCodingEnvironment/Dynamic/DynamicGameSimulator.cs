using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    /// <summary>
    ///     Class that simulates game.
    /// </summary>
    internal class DynamicGameSimulator
    {
        private readonly DynamicGame _game;
        public List<Snapshot> Snapshots = new List<Snapshot>();

        /// <summary>
        ///     Initializes a new instance of the DynamicGameSimulator class.
        /// </summary>
        public DynamicGameSimulator(DynamicGame game)
        {
            _game = game;
        }

        public BitmapSource TimelapseScene { get; private set; }

        /// <summary>
        ///     Takes snapshot of current game state.
        /// </summary>
        public void TakeSnapshot(Dictionary<char, bool> input, double dt, int limit)
        {
            if (_game.CompiledData == null) return;
            Snapshots.RemoveRange(0, Snapshots.Count - (limit - 1) < 0 ? 0 : Snapshots.Count - (limit - 1));
            Snapshots.Add(new Snapshot {Input = input, State = _game.CompiledData.DumpGameState(), Dt = dt});
        }

        /// <summary>
        ///     Adds dummy (same) snapshots.
        /// </summary>
        public void AddDummySnapshots(int count)
        {
            var lastSnapshot = Snapshots.Count - 1;
            for (var i = 0; i < count; ++i)
            {
                Snapshots.Add(new Snapshot
                {
                    Dt = Snapshots[lastSnapshot].Dt,
                    State = Snapshots[lastSnapshot].State,
                    Input = Snapshots[lastSnapshot].Input
                });
            }
        }

        /// <summary>
        ///     Removes snapshots from end.
        /// </summary>
        /// <param name="count"></param>
        public void RemoveSnapshotsFromEnd(int count)
        {
            Snapshots.RemoveRange(Snapshots.Count - count, count);
        }

        /// <summary>
        ///     Renders timelapse scene.
        /// </summary>
        public void RenderTimelapseScene()
        {
            var state = _game.CompiledData.DumpGameState();

            var drawingVisual = new DrawingVisual();
            using (var dc = drawingVisual.RenderOpen())
            {
                dc.PushOpacity(0.2);
                for (var i = 0; i < Snapshots.Count; ++i)
                {
                    _game.CompiledData.SetGameState(Snapshots[i].State);
                    _game.CompiledData.TryInvokeDrawTrackDelegates(dc);
                }
            }
            var bmp = new RenderTargetBitmap((int) _game.GraphicsControl.ActualWidth,
                (int) _game.GraphicsControl.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(drawingVisual);
            TimelapseScene = bmp;

            _game.CompiledData.SetGameState(state);
        }

        /// <summary>
        ///     Simulates game starting from nth screenshot. 
        /// </summary>
        public void SimulateGame(int startSnapshot)
        {
            var state = Snapshots[startSnapshot].State;
            for (var i = startSnapshot; i < Snapshots.Count - 1; ++i)
            {
                Snapshots[i].State = state;
                _game.CompiledData.SetGameState(state);
                _game.CompiledData.TryInvokeTickDelegate(Snapshots[i + 1].Dt, Snapshots[i + 1].Input);
                state = _game.CompiledData.DumpGameState();
            }
            Snapshots[Snapshots.Count - 1].State = state;
        }
    }
}