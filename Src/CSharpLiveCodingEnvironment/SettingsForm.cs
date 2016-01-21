using System;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment
{
    internal partial class SettingsForm : Form
    {
        private static SettingsForm _instance;

        private bool _storeLastFramesEditingEnabled;

        public SettingsForm()
        {
            InitializeComponent();

            CheckInfiniteLoops = checkBox2.Checked;
            UseTrackedInput = useTrackedInputCheckBox.Checked;
            TogglePauseOnQ = togglePauseOnQCheckBox.Checked;
            WaitAfterEachTick = waitAfterEachTIckCheckBox.Checked;
            WaitAfterEachTickMsec = int.Parse(waitAfterEachTIckTextBox.Text);
            DesiredDt = 1000/int.Parse(desiredFPStextBox.Text);
            StoreLastFrames = int.Parse(storeLastFramesTextBox.Text);
        }

        public static SettingsForm Instance => _instance ?? (_instance = new SettingsForm());

        public bool UseTrackedInput { get; private set; }
        public bool TogglePauseOnQ { get; private set; }
        public bool WaitAfterEachTick { get; private set; }
        public int WaitAfterEachTickMsec { get; private set; }
        public int DesiredDt { get; private set; }
        public int StoreLastFrames { get; private set; }
        public bool CheckInfiniteLoops { get; private set; }

        public bool StoreLastFramesEditingEnabled
        {
            get { return _storeLastFramesEditingEnabled; }
            set
            {
                _storeLastFramesEditingEnabled = value;
                storeLastFramesTextBox.Enabled = value;
            }
        }

        public event EventHandler StoreLastFramesParamChanged;

        private void useTrackedInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UseTrackedInput = useTrackedInputCheckBox.Checked;
        }

        private void togglePauseOnQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TogglePauseOnQ = togglePauseOnQCheckBox.Checked;
        }

        private void waitAfterEachTIckCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WaitAfterEachTick = waitAfterEachTIckCheckBox.Checked;
        }

        private void waitAfterEachTIckTextBox_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (int.TryParse(waitAfterEachTIckTextBox.Text, out a))
            {
                WaitAfterEachTickMsec = a;
            }
        }

        private void desiredFPStextBox_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (int.TryParse(desiredFPStextBox.Text, out a))
            {
                DesiredDt = 1000/a;
            }
        }

        private void storeLastFramesTextBox_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (int.TryParse(storeLastFramesTextBox.Text, out a))
            {
                StoreLastFrames = a;
                StoreLastFramesParamChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckInfiniteLoops = checkBox2.Checked;
        }
    }
}