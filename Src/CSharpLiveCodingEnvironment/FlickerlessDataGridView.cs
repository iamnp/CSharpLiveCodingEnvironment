using System;
using System.Data;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment
{
    /// <summary>
    ///     DataGridView for fast data updating.
    /// </summary>
    internal class FlickerlessDataGridView : DataGridView
    {
        private readonly DataTable _dt = new DataTable();

        public FlickerlessDataGridView()
        {
            _dt.Columns.AddRange(new[]
            {
                new DataColumn("Object", typeof (string)),
                new DataColumn("Value", typeof (string))
            });

            DataSource = _dt;

            // needed to get rid of flickerness
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            ShowCellToolTips = false;
            AllowUserToResizeColumns = true;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DefaultCellStyle.SelectionBackColor = DefaultCellStyle.BackColor;
            DefaultCellStyle.SelectionForeColor = DefaultCellStyle.ForeColor;
            RowHeadersVisible = false;

            ScrollBars = ScrollBars.None;
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler,
            DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs)
        {
        }

        public void ShowData(Tuple<string, string>[] list)
        {
            ClearSelection();
            var saveRow = 0;
            if (Rows.Count > 0 && FirstDisplayedCell != null) saveRow = FirstDisplayedCell.RowIndex;
            _dt.Rows.Clear();
            for (var i = 0; i < list.Length; ++i)
            {
                _dt.Rows.Add(list[i].Item1, list[i].Item2);
            }
            if (saveRow != 0 && saveRow < Rows.Count) FirstDisplayedScrollingRowIndex = saveRow;
        }
    }
}