using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using CSharpLiveCodingEnvironment.CodeCompilation;
using CSharpLiveCodingEnvironment.CodeEditing;
using CSharpLiveCodingEnvironment.Dynamic;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace CSharpLiveCodingEnvironment
{
    internal partial class MainForm : Form
    {
        private const string HelloWorldSourceCode = @"[MainClass]
class Game
{
  [StateField]
  double x;

  [InitMethod]
  void Init() {
    x = 0.0;
  }

  [TickMethod]
  void Tick(double dt, Dictionary<char, bool> input) {
    x += 0.05 * dt;
    if (x > 450) x = 0;
  }

  [DrawMethod]
  void DrawScene(DrawingContext dc) {
    dc.DrawRectangle(Brushes.White, null, new Rect(0, 0, 500, 500));
    
    DrawCircle(dc);
    
    dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(255, 100, 100, 250)),
      null, new Rect(x - 25, 300, 50, 50));
  }

  void DrawCircle(DrawingContext dc) {
    dc.DrawEllipse(new SolidColorBrush(Color.FromArgb(255, 120, 120, 120)),
      null, new Point(x, 200), 10, 10);
  }
}";
        private readonly CodeCompiler _codeCompiler;
        private readonly DynamicGame _dynamicGame;
        private readonly GraphicsControl _graphics;
        private bool _currentFileSaved;
        private bool _forceReset;
        private string _openedFilePath;

        public MainForm()
        {
            InitializeComponent();
            WinApi.TimeBeginPeriod(1);

            _codeCompiler = new CodeCompiler(SynchronizationContext.Current);
            _codeCompiler.Compiled += CodeCompilerOnCompiled;
            _codeCompiler.CompilationError += CodeCompilerOnCompilationError;

            SettingsForm.Instance.StoreLastFramesParamChanged += SettingsFormOnStoreLastFramesParamChanged;

            _graphics = new GraphicsControl();
            elementHost1.Child = _graphics;
            _graphics.KeyDown += GraphicsOnKeyDown;
            _graphics.KeyUp += GraphicsOnKeyUp;

            codeEditor.Text = HelloWorldSourceCode;
            codeEditor.TextChanged += CodeEditorOnTextChanged;

            _dynamicGame = new DynamicGame(_graphics, toolStripStatusLabel1)
            {
                CurrentTrackBarValue = trackBar1.Value
            };
            _dynamicGame.CurrentTrackBarValueChanged += DynamicGameOnCurrentTrackBarValueChanged;
            _dynamicGame.FieldsChanged += DynamicGameOnFieldsChanged;

            new TmpBlocksClass(_graphics, _dynamicGame);

            UpdateFormTitle();
        }

        private void DynamicGameOnCurrentTrackBarValueChanged(object sender, EventArgs eventArgs)
        {
            if (_dynamicGame.CurrentTrackBarValue >= trackBar1.Minimum &&
                _dynamicGame.CurrentTrackBarValue <= trackBar1.Maximum)
            {
                trackBar1.Value = _dynamicGame.CurrentTrackBarValue;
            }
        }

        private string OpenedFilePath
        {
            get { return _openedFilePath; }
            set
            {
                _openedFilePath = value;
                UpdateFormTitle();
            }
        }

        private bool CurrentFileSaved
        {
            get { return _currentFileSaved; }
            set
            {
                _currentFileSaved = value;
                UpdateFormTitle();
            }
        }

        private void SettingsFormOnStoreLastFramesParamChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = SettingsForm.Instance.StoreLastFrames;
            if (!_dynamicGame.Paused) trackBar1.Value = trackBar1.Maximum;
        }

        private void DynamicGameOnFieldsChanged(object sender, FieldsChangedEventArgs e)
        {
            if (WatchForm.Instance.Visible)
            {
                WatchForm.Instance.BeginInvoke((MethodInvoker) (() => WatchForm.Instance.PopulateData(e.Fields)));
            }
        }

        private void GraphicsOnKeyUp(object sender, KeyEventArgs e)
        {
            if (_graphics.IsFocused)
            {
                _dynamicGame.SetInput(e.Key.ToString()[0], false);
            }
            if (SettingsForm.Instance.TogglePauseOnQ && e.Key == Key.Q)
            {
                TogglePause();
            }
        }

        private void GraphicsOnKeyDown(object sender, KeyEventArgs e)
        {
            if (_graphics.IsFocused)
            {
                _dynamicGame.SetInput(e.Key.ToString()[0], true);
            }
        }

        private void CodeCompilerOnCompilationError(object sender, CompilationErrorEventArgs e)
        {
            codeEditor.RemoveAllMarkers();
            codeEditor.AddMarker(new LineMarker {Color = Color.Red, Line = e.Line, Text = e.ErrorText});
        }

        private void CodeCompilerOnCompiled(object sender, CompiledEventArgs e)
        {
            _dynamicGame.ReplaceCompiledData(e.CompiledData);
            codeEditor.RemoveAllMarkers();
            if (_dynamicGame.Paused)
            {
                _dynamicGame.NeedToSimulateTimelapseScene = true;
            }
        }

        private void CodeEditorOnTextChanged(object sender, EventArgs eventArgs)
        {
            CurrentFileSaved = false;
            _codeCompiler.CompileGameClass(codeEditor.Text, !_forceReset && _dynamicGame.TickDelegateExists());
        }

        private void TogglePause()
        {
            _dynamicGame.Paused = !_dynamicGame.Paused;
            if (!_dynamicGame.Paused)
            {
                pauseToolStripMenuItem.Text = "Пауза";
                trackBar1.Value = trackBar1.Maximum;
                trackBar1.Visible = false;
            }
            else
            {
                _dynamicGame.NeedToSimulateTimelapseScene = true;
                pauseToolStripMenuItem.Text = "Продолжить";
                trackBar1.Visible = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _dynamicGame.Start();
            _codeCompiler.CompileGameClass(codeEditor.Text, false);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            _dynamicGame.CurrentTrackBarValue = trackBar1.Value;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SettingsForm.Instance.Visible)
            {
                SettingsForm.Instance.Focus();
            }
            else
            {
                SettingsForm.Instance.Show();
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePause();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _codeCompiler.CompileGameClass(codeEditor.Text, false);
        }

        private void watchListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WatchForm.Instance.Visible)
            {
                WatchForm.Instance.Focus();
            }
            else
            {
                WatchForm.Instance.Show();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CurrentFileSaved && codeEditor.Text != HelloWorldSourceCode &&
                MessageBox.Show("Сохранить текущий файл?", "Сохранение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveCurrentFile();
            }

            var dialog = new OpenFileDialog {Filter = "C# Source Code|*.cs"};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _forceReset = true;
                OpenedFilePath = dialog.FileName;
                codeEditor.Text = File.ReadAllText(OpenedFilePath, Encoding.UTF8);
                CurrentFileSaved = true;
                _forceReset = false;
            }
        }

        private void SaveCurrentFile()
        {
            if (CurrentFileSaved) return;

            if (OpenedFilePath == null)
            {
                var dialog = new SaveFileDialog {Filter = "C# Source Code|*.cs"};
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OpenedFilePath = dialog.FileName;
                }
            }

            if (OpenedFilePath != null)
            {
                File.WriteAllText(OpenedFilePath, codeEditor.Text, Encoding.UTF8);
                CurrentFileSaved = true;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private string GetNameFromPath(string path)
        {
            var pos = path.LastIndexOf('\\');
            if (pos == -1) return path;
            return path.Substring(pos + 1);
        }

        private void UpdateFormTitle()
        {
            var text = OpenedFilePath == null ? "NewFile.cs" : GetNameFromPath(OpenedFilePath);
            if (!CurrentFileSaved) text += "*";
            text += " - C# Live Coding Environment";
            Text = text;
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CurrentFileSaved && codeEditor.Text != HelloWorldSourceCode &&
                MessageBox.Show("Сохранить текущий файл?", "Сохранение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveCurrentFile();
            }

            OpenedFilePath = null;
            CurrentFileSaved = false;

            _forceReset = true;
            codeEditor.Text = HelloWorldSourceCode;
            _forceReset = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var cancel = false;
            if (!CurrentFileSaved && codeEditor.Text != HelloWorldSourceCode)
            {
                var r = MessageBox.Show("Сохранить текущий файл?", "Сохранение", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.Yes)
                {
                    SaveCurrentFile();
                }
                if (r == DialogResult.Cancel)
                {
                    cancel = true;
                }
            }

            if (cancel)
            {
                e.Cancel = true;
            }
            else
            {
                _dynamicGame.Stop();
                WinApi.TimeEndPeriod(1);
            }
        }
    }
}