using System;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment
{
    /// <summary>
    ///     Form that shows field's values.
    /// </summary>
    internal partial class WatchForm : Form
    {
        private static WatchForm _instance;

        public WatchForm()
        {
            InitializeComponent();
            _flickerlessDataGridView.MouseWheel += FlickerlessDataGridViewOnMouseWheel;
        }

        public static WatchForm Instance => _instance ?? (_instance = new WatchForm());

        private void FlickerlessDataGridViewOnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (_flickerlessDataGridView.FirstDisplayedCell.RowIndex < _flickerlessDataGridView.RowCount - 1)
                    _flickerlessDataGridView.FirstDisplayedScrollingRowIndex =
                        _flickerlessDataGridView.FirstDisplayedCell.RowIndex + 1;
            }
            else if (e.Delta > 0)
            {
                if (_flickerlessDataGridView.FirstDisplayedCell.RowIndex > 0)
                    _flickerlessDataGridView.FirstDisplayedScrollingRowIndex =
                        _flickerlessDataGridView.FirstDisplayedCell.RowIndex - 1;
            }
        }

        private void WatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void PopulateData(Tuple<string, string>[] list)
        {
            _flickerlessDataGridView.ShowData(list);
        }

        private void topMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = topMostCheckBox.Checked;
        }
    }
}