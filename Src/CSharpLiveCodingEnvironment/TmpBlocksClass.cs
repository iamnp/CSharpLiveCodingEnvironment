using System.Drawing;
using System.Windows.Input;
using CSharpLiveCodingEnvironment.Dynamic;

namespace CSharpLiveCodingEnvironment
{
    internal class TmpBlocksClass
    {
        private readonly DynamicGame _dynamicGame;
        private readonly GraphicsControl _graph;
        private Point _lastBlockToggled = new Point(-1, -1);
        private bool _leftMouseuttonDown;
        private bool _setBlockState;

        public TmpBlocksClass(GraphicsControl a, DynamicGame b)
        {
            _graph = a;
            _dynamicGame = b;
            _graph.MouseDown += GOnMouseDown;
            _graph.MouseUp += GOnMouseUp;
            _graph.MouseMove += GOnMouseMove;
            _graph.MouseLeave += GOnMouseLeave;
        }


        private void GOnMouseLeave(object sender, MouseEventArgs e)
        {
            _leftMouseuttonDown = false;
        }

        private int GetBlockSize()
        {
            for (var i = 0; i < _dynamicGame.CompiledData.Fields.Length; ++i)
            {
                if (_dynamicGame.CompiledData.Fields[i].Name == "blockSize")
                {
                    return (int) _dynamicGame.CompiledData.Fields[i].GetValue(_dynamicGame.CompiledData.Instance);
                }
            }
            return -1;
        }

        private bool ToggleBlock(int mouseX, int mouseY)
        {
            var blockSize = GetBlockSize();

            if (blockSize != -1)
            {
                var blockX = mouseX/blockSize;
                var blockY = mouseY/blockSize;

                var res = false;
                if (_lastBlockToggled.X != blockX || _lastBlockToggled.Y != blockY)
                {
                    for (var i = 0; i < _dynamicGame.CompiledData.Fields.Length; ++i)
                    {
                        if (_dynamicGame.CompiledData.Fields[i].Name == "blocks")
                        {
                            var blocks =
                                (int[,])
                                    _dynamicGame.CompiledData.Fields[i].GetValue(_dynamicGame.CompiledData.Instance);
                            var blockState = blocks[blockX, blockY];
                            blocks[blockX, blockY] = blockState == 1 ? 0 : 1;
                            _dynamicGame.CompiledData.Fields[i].SetValue(_dynamicGame.CompiledData.Instance, blocks);
                            res = blockState == 0;
                        }
                    }
                }

                _lastBlockToggled.X = blockX;
                _lastBlockToggled.Y = blockY;
                if (_dynamicGame.Paused)
                {
                    _dynamicGame.NeedToSimulateTimelapseScene = true;
                }
                return res;
            }
            return false;
        }

        private void SetBlock(int mouseX, int mouseY, bool state)
        {
            var blockSize = GetBlockSize();

            if (blockSize != -1)
            {
                var blockX = mouseX/blockSize;
                var blockY = mouseY/blockSize;

                if (_lastBlockToggled.X != blockX || _lastBlockToggled.Y != blockY)
                {
                    for (var i = 0; i < _dynamicGame.CompiledData.Fields.Length; ++i)
                    {
                        if (_dynamicGame.CompiledData.Fields[i].Name == "blocks")
                        {
                            var blocks =
                                (int[,])
                                    _dynamicGame.CompiledData.Fields[i].GetValue(_dynamicGame.CompiledData.Instance);
                            blocks[blockX, blockY] = state ? 1 : 0;
                            _dynamicGame.CompiledData.Fields[i].SetValue(_dynamicGame.CompiledData.Instance, blocks);
                        }
                    }
                }

                _lastBlockToggled.X = blockX;
                _lastBlockToggled.Y = blockY;
                if (_dynamicGame.Paused)
                {
                    _dynamicGame.NeedToSimulateTimelapseScene = true;
                }
            }
        }

        private void GOnMouseMove(object sender, MouseEventArgs e)
        {
            if (_leftMouseuttonDown)
            {
                SetBlock((int) e.GetPosition(_graph).X, (int) e.GetPosition(_graph).Y, _setBlockState);
            }
        }

        private void GOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _leftMouseuttonDown = false;
            _lastBlockToggled.X = -1;
            _lastBlockToggled.Y = -1;
        }

        private void GOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _graph.Focus();
            if (e.ChangedButton == MouseButton.Left)
            {
                _leftMouseuttonDown = true;
                _setBlockState = ToggleBlock((int) e.GetPosition(_graph).X, (int) e.GetPosition(_graph).Y);
            }
        }
    }
}