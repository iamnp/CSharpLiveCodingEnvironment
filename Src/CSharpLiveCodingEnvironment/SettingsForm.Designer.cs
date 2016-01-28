namespace CSharpLiveCodingEnvironment
{
    partial class SettingsForm
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
            this.useTrackedInputCheckBox = new System.Windows.Forms.CheckBox();
            this.togglePauseOnQCheckBox = new System.Windows.Forms.CheckBox();
            this.waitAfterEachTIckCheckBox = new System.Windows.Forms.CheckBox();
            this.desiredFrameratelabel = new System.Windows.Forms.Label();
            this.storeLastFramesLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InfiniteLoopsCheckBox = new System.Windows.Forms.CheckBox();
            this.storeLastFramesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.desiredFramerateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.waitAfterEachTickNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.topMostCheckBox = new System.Windows.Forms.CheckBox();
            this.InfiniteLoopsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.InfiniteLoopsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.storeLastFramesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desiredFramerateNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitAfterEachTickNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfiniteLoopsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // useTrackedInputCheckBox
            // 
            this.useTrackedInputCheckBox.AutoSize = true;
            this.useTrackedInputCheckBox.Checked = true;
            this.useTrackedInputCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useTrackedInputCheckBox.Location = new System.Drawing.Point(12, 35);
            this.useTrackedInputCheckBox.Name = "useTrackedInputCheckBox";
            this.useTrackedInputCheckBox.Size = new System.Drawing.Size(246, 17);
            this.useTrackedInputCheckBox.TabIndex = 0;
            this.useTrackedInputCheckBox.Text = "Использовать записанные команды ввода";
            this.useTrackedInputCheckBox.UseVisualStyleBackColor = true;
            this.useTrackedInputCheckBox.CheckedChanged += new System.EventHandler(this.useTrackedInputCheckBox_CheckedChanged);
            // 
            // togglePauseOnQCheckBox
            // 
            this.togglePauseOnQCheckBox.AutoSize = true;
            this.togglePauseOnQCheckBox.Checked = true;
            this.togglePauseOnQCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.togglePauseOnQCheckBox.Location = new System.Drawing.Point(12, 58);
            this.togglePauseOnQCheckBox.Name = "togglePauseOnQCheckBox";
            this.togglePauseOnQCheckBox.Size = new System.Drawing.Size(199, 17);
            this.togglePauseOnQCheckBox.TabIndex = 1;
            this.togglePauseOnQCheckBox.Text = "Переключать паузу по нажатию Q";
            this.togglePauseOnQCheckBox.UseVisualStyleBackColor = true;
            this.togglePauseOnQCheckBox.CheckedChanged += new System.EventHandler(this.togglePauseOnQCheckBox_CheckedChanged);
            // 
            // waitAfterEachTIckCheckBox
            // 
            this.waitAfterEachTIckCheckBox.AutoSize = true;
            this.waitAfterEachTIckCheckBox.Location = new System.Drawing.Point(12, 81);
            this.waitAfterEachTIckCheckBox.Name = "waitAfterEachTIckCheckBox";
            this.waitAfterEachTIckCheckBox.Size = new System.Drawing.Size(175, 17);
            this.waitAfterEachTIckCheckBox.TabIndex = 2;
            this.waitAfterEachTIckCheckBox.Text = "Ждать после каждого кадра:";
            this.waitAfterEachTIckCheckBox.UseVisualStyleBackColor = true;
            this.waitAfterEachTIckCheckBox.CheckedChanged += new System.EventHandler(this.waitAfterEachTIckCheckBox_CheckedChanged);
            // 
            // desiredFrameratelabel
            // 
            this.desiredFrameratelabel.AutoSize = true;
            this.desiredFrameratelabel.Location = new System.Drawing.Point(13, 105);
            this.desiredFrameratelabel.Name = "desiredFrameratelabel";
            this.desiredFrameratelabel.Size = new System.Drawing.Size(91, 13);
            this.desiredFrameratelabel.TabIndex = 4;
            this.desiredFrameratelabel.Text = "Требуемый FPS:";
            // 
            // storeLastFramesLabel
            // 
            this.storeLastFramesLabel.AutoSize = true;
            this.storeLastFramesLabel.Location = new System.Drawing.Point(16, 131);
            this.storeLastFramesLabel.Name = "storeLastFramesLabel";
            this.storeLastFramesLabel.Size = new System.Drawing.Size(158, 13);
            this.storeLastFramesLabel.TabIndex = 5;
            this.storeLastFramesLabel.Text = "Сохранять последних кадров:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "мс";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "шт";
            // 
            // InfiniteLoopsCheckBox
            // 
            this.InfiniteLoopsCheckBox.AutoSize = true;
            this.InfiniteLoopsCheckBox.Location = new System.Drawing.Point(12, 154);
            this.InfiniteLoopsCheckBox.Name = "InfiniteLoopsCheckBox";
            this.InfiniteLoopsCheckBox.Size = new System.Drawing.Size(168, 17);
            this.InfiniteLoopsCheckBox.TabIndex = 4;
            this.InfiniteLoopsCheckBox.Text = "Искать бесконечные циклы";
            this.InfiniteLoopsCheckBox.UseVisualStyleBackColor = true;
            this.InfiniteLoopsCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // storeLastFramesNumericUpDown
            // 
            this.storeLastFramesNumericUpDown.Location = new System.Drawing.Point(194, 129);
            this.storeLastFramesNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.storeLastFramesNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.storeLastFramesNumericUpDown.Name = "storeLastFramesNumericUpDown";
            this.storeLastFramesNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.storeLastFramesNumericUpDown.TabIndex = 10;
            this.storeLastFramesNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.storeLastFramesNumericUpDown.ValueChanged += new System.EventHandler(this.storeLastFramesNumericUpDown_ValueChanged);
            // 
            // desiredFramerateNumericUpDown
            // 
            this.desiredFramerateNumericUpDown.Location = new System.Drawing.Point(194, 103);
            this.desiredFramerateNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.desiredFramerateNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.desiredFramerateNumericUpDown.Name = "desiredFramerateNumericUpDown";
            this.desiredFramerateNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.desiredFramerateNumericUpDown.TabIndex = 11;
            this.desiredFramerateNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.desiredFramerateNumericUpDown.ValueChanged += new System.EventHandler(this.desiredFramerateNumericUpDown_ValueChanged);
            // 
            // waitAfterEachTickNumericUpDown
            // 
            this.waitAfterEachTickNumericUpDown.Location = new System.Drawing.Point(194, 80);
            this.waitAfterEachTickNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.waitAfterEachTickNumericUpDown.Name = "waitAfterEachTickNumericUpDown";
            this.waitAfterEachTickNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.waitAfterEachTickNumericUpDown.TabIndex = 12;
            this.waitAfterEachTickNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.waitAfterEachTickNumericUpDown.ValueChanged += new System.EventHandler(this.waitAfterEachTickNumericUpDown_ValueChanged);
            // 
            // topMostCheckBox
            // 
            this.topMostCheckBox.AutoSize = true;
            this.topMostCheckBox.Location = new System.Drawing.Point(12, 12);
            this.topMostCheckBox.Name = "topMostCheckBox";
            this.topMostCheckBox.Size = new System.Drawing.Size(116, 17);
            this.topMostCheckBox.TabIndex = 13;
            this.topMostCheckBox.Text = "Поверх всех окон";
            this.topMostCheckBox.UseVisualStyleBackColor = true;
            this.topMostCheckBox.CheckedChanged += new System.EventHandler(this.topMostCheckBox_CheckedChanged);
            // 
            // InfiniteLoopsNumericUpDown
            // 
            this.InfiniteLoopsNumericUpDown.Enabled = false;
            this.InfiniteLoopsNumericUpDown.Location = new System.Drawing.Point(194, 153);
            this.InfiniteLoopsNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.InfiniteLoopsNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.InfiniteLoopsNumericUpDown.Name = "InfiniteLoopsNumericUpDown";
            this.InfiniteLoopsNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.InfiniteLoopsNumericUpDown.TabIndex = 14;
            this.InfiniteLoopsNumericUpDown.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.InfiniteLoopsNumericUpDown.ValueChanged += new System.EventHandler(this.InfiniteLoopsNumericUpDown_ValueChanged);
            // 
            // InfiniteLoopsLabel
            // 
            this.InfiniteLoopsLabel.AutoSize = true;
            this.InfiniteLoopsLabel.Location = new System.Drawing.Point(250, 155);
            this.InfiniteLoopsLabel.Name = "InfiniteLoopsLabel";
            this.InfiniteLoopsLabel.Size = new System.Drawing.Size(21, 13);
            this.InfiniteLoopsLabel.TabIndex = 15;
            this.InfiniteLoopsLabel.Text = "мс";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 174);
            this.Controls.Add(this.InfiniteLoopsLabel);
            this.Controls.Add(this.InfiniteLoopsNumericUpDown);
            this.Controls.Add(this.topMostCheckBox);
            this.Controls.Add(this.waitAfterEachTickNumericUpDown);
            this.Controls.Add(this.desiredFramerateNumericUpDown);
            this.Controls.Add(this.storeLastFramesNumericUpDown);
            this.Controls.Add(this.InfiniteLoopsCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storeLastFramesLabel);
            this.Controls.Add(this.desiredFrameratelabel);
            this.Controls.Add(this.waitAfterEachTIckCheckBox);
            this.Controls.Add(this.togglePauseOnQCheckBox);
            this.Controls.Add(this.useTrackedInputCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.storeLastFramesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desiredFramerateNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitAfterEachTickNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InfiniteLoopsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox useTrackedInputCheckBox;
        private System.Windows.Forms.CheckBox togglePauseOnQCheckBox;
        private System.Windows.Forms.CheckBox waitAfterEachTIckCheckBox;
        private System.Windows.Forms.Label desiredFrameratelabel;
        private System.Windows.Forms.Label storeLastFramesLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox InfiniteLoopsCheckBox;
        private System.Windows.Forms.NumericUpDown storeLastFramesNumericUpDown;
        private System.Windows.Forms.NumericUpDown desiredFramerateNumericUpDown;
        private System.Windows.Forms.NumericUpDown waitAfterEachTickNumericUpDown;
        private System.Windows.Forms.CheckBox topMostCheckBox;
        private System.Windows.Forms.NumericUpDown InfiniteLoopsNumericUpDown;
        private System.Windows.Forms.Label InfiniteLoopsLabel;
    }
}