namespace CSharpLiveCodingEnvironment
{
    partial class WatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._flickerlessDataGridView = new CSharpLiveCodingEnvironment.FlickerlessDataGridView();
            this.objectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._flickerlessDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FlickerlessDataGridView
            // 
            this._flickerlessDataGridView.AllowUserToAddRows = false;
            this._flickerlessDataGridView.AllowUserToDeleteRows = false;
            this._flickerlessDataGridView.AutoGenerateColumns = false;
            this._flickerlessDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._flickerlessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._flickerlessDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.objectDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._flickerlessDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this._flickerlessDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flickerlessDataGridView.Location = new System.Drawing.Point(0, 0);
            this._flickerlessDataGridView.Name = "_flickerlessDataGridView";
            this._flickerlessDataGridView.RowHeadersVisible = false;
            this._flickerlessDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._flickerlessDataGridView.ShowCellToolTips = false;
            this._flickerlessDataGridView.Size = new System.Drawing.Size(434, 281);
            this._flickerlessDataGridView.TabIndex = 0;
            // 
            // objectDataGridViewTextBoxColumn
            // 
            this.objectDataGridViewTextBoxColumn.DataPropertyName = "Object";
            this.objectDataGridViewTextBoxColumn.HeaderText = "Object";
            this.objectDataGridViewTextBoxColumn.Name = "objectDataGridViewTextBoxColumn";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // WatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 281);
            this.Controls.Add(this._flickerlessDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "WatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Объекты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WatchForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._flickerlessDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlickerlessDataGridView _flickerlessDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn objectDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
    }
}