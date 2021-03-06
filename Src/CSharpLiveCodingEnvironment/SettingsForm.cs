﻿using System;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment
{
    /// <summary>
    ///     Form that contains application settings.
    /// </summary>
    internal partial class SettingsForm : Form
    {
        private static SettingsForm _instance;

        public SettingsForm()
        {
            InitializeComponent();

            CheckInfiniteLoops = InfiniteLoopsCheckBox.Checked;
            UseTrackedInput = useTrackedInputCheckBox.Checked;
            TogglePauseOnQ = togglePauseOnQCheckBox.Checked;
            WaitAfterEachTick = waitAfterEachTIckCheckBox.Checked;
            WaitAfterEachTickMsec = (int) waitAfterEachTickNumericUpDown.Value;
            DesiredDt = 1000/(int) desiredFramerateNumericUpDown.Value;
            StoreLastFrames = (int) storeLastFramesNumericUpDown.Value;
            InfiniteLoopsTimeout = (int) InfiniteLoopsNumericUpDown.Value;
        }

        public static SettingsForm Instance => _instance ?? (_instance = new SettingsForm());

        public bool UseTrackedInput { get; private set; }
        public bool TogglePauseOnQ { get; private set; }
        public bool WaitAfterEachTick { get; private set; }
        public int WaitAfterEachTickMsec { get; private set; }
        public int DesiredDt { get; private set; }
        public int StoreLastFrames { get; private set; }
        public bool CheckInfiniteLoops { get; private set; }

        public int InfiniteLoopsTimeout { get; private set; }

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

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckInfiniteLoops = InfiniteLoopsCheckBox.Checked;
            InfiniteLoopsNumericUpDown.Enabled = InfiniteLoopsCheckBox.Checked;
        }

        private void storeLastFramesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            StoreLastFrames = (int) storeLastFramesNumericUpDown.Value;
            StoreLastFramesParamChanged?.Invoke(this, EventArgs.Empty);
        }

        private void desiredFramerateNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DesiredDt = 1000/(int) desiredFramerateNumericUpDown.Value;
        }

        private void waitAfterEachTickNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            WaitAfterEachTickMsec = (int) waitAfterEachTickNumericUpDown.Value;
        }

        private void topMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostCheckBox.Checked;
        }

        private void InfiniteLoopsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            InfiniteLoopsTimeout = (int) InfiniteLoopsNumericUpDown.Value;
        }
    }
}