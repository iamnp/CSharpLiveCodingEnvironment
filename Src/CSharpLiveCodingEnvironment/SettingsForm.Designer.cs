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
            this.waitAfterEachTIckTextBox = new System.Windows.Forms.TextBox();
            this.desiredFPSlabel = new System.Windows.Forms.Label();
            this.storeLastFramesLabel = new System.Windows.Forms.Label();
            this.desiredFPStextBox = new System.Windows.Forms.TextBox();
            this.storeLastFramesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // useTrackedInputCheckBox
            // 
            this.useTrackedInputCheckBox.AutoSize = true;
            this.useTrackedInputCheckBox.Checked = true;
            this.useTrackedInputCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useTrackedInputCheckBox.Location = new System.Drawing.Point(12, 12);
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
            this.togglePauseOnQCheckBox.Location = new System.Drawing.Point(12, 35);
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
            this.waitAfterEachTIckCheckBox.Location = new System.Drawing.Point(12, 58);
            this.waitAfterEachTIckCheckBox.Name = "waitAfterEachTIckCheckBox";
            this.waitAfterEachTIckCheckBox.Size = new System.Drawing.Size(175, 17);
            this.waitAfterEachTIckCheckBox.TabIndex = 2;
            this.waitAfterEachTIckCheckBox.Text = "Ждать после каждого кадра:";
            this.waitAfterEachTIckCheckBox.UseVisualStyleBackColor = true;
            this.waitAfterEachTIckCheckBox.CheckedChanged += new System.EventHandler(this.waitAfterEachTIckCheckBox_CheckedChanged);
            // 
            // waitAfterEachTIckTextBox
            // 
            this.waitAfterEachTIckTextBox.Location = new System.Drawing.Point(193, 56);
            this.waitAfterEachTIckTextBox.Name = "waitAfterEachTIckTextBox";
            this.waitAfterEachTIckTextBox.Size = new System.Drawing.Size(50, 20);
            this.waitAfterEachTIckTextBox.TabIndex = 3;
            this.waitAfterEachTIckTextBox.Text = "10";
            this.waitAfterEachTIckTextBox.TextChanged += new System.EventHandler(this.waitAfterEachTIckTextBox_TextChanged);
            // 
            // desiredFPSlabel
            // 
            this.desiredFPSlabel.AutoSize = true;
            this.desiredFPSlabel.Location = new System.Drawing.Point(13, 82);
            this.desiredFPSlabel.Name = "desiredFPSlabel";
            this.desiredFPSlabel.Size = new System.Drawing.Size(91, 13);
            this.desiredFPSlabel.TabIndex = 4;
            this.desiredFPSlabel.Text = "Требуемый FPS:";
            // 
            // storeLastFramesLabel
            // 
            this.storeLastFramesLabel.AutoSize = true;
            this.storeLastFramesLabel.Location = new System.Drawing.Point(16, 108);
            this.storeLastFramesLabel.Name = "storeLastFramesLabel";
            this.storeLastFramesLabel.Size = new System.Drawing.Size(158, 13);
            this.storeLastFramesLabel.TabIndex = 5;
            this.storeLastFramesLabel.Text = "Сохранять последних кадров:";
            // 
            // desiredFPStextBox
            // 
            this.desiredFPStextBox.Location = new System.Drawing.Point(193, 79);
            this.desiredFPStextBox.Name = "desiredFPStextBox";
            this.desiredFPStextBox.Size = new System.Drawing.Size(50, 20);
            this.desiredFPStextBox.TabIndex = 6;
            this.desiredFPStextBox.Text = "60";
            this.desiredFPStextBox.TextChanged += new System.EventHandler(this.desiredFPStextBox_TextChanged);
            // 
            // storeLastFramesTextBox
            // 
            this.storeLastFramesTextBox.Location = new System.Drawing.Point(193, 105);
            this.storeLastFramesTextBox.Name = "storeLastFramesTextBox";
            this.storeLastFramesTextBox.Size = new System.Drawing.Size(50, 20);
            this.storeLastFramesTextBox.TabIndex = 7;
            this.storeLastFramesTextBox.Text = "100";
            this.storeLastFramesTextBox.TextChanged += new System.EventHandler(this.storeLastFramesTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "мс";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "шт";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 131);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(168, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Искать бесконечные циклы";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 157);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storeLastFramesTextBox);
            this.Controls.Add(this.desiredFPStextBox);
            this.Controls.Add(this.storeLastFramesLabel);
            this.Controls.Add(this.desiredFPSlabel);
            this.Controls.Add(this.waitAfterEachTIckTextBox);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox useTrackedInputCheckBox;
        private System.Windows.Forms.CheckBox togglePauseOnQCheckBox;
        private System.Windows.Forms.CheckBox waitAfterEachTIckCheckBox;
        private System.Windows.Forms.TextBox waitAfterEachTIckTextBox;
        private System.Windows.Forms.Label desiredFPSlabel;
        private System.Windows.Forms.Label storeLastFramesLabel;
        private System.Windows.Forms.TextBox desiredFPStextBox;
        private System.Windows.Forms.TextBox storeLastFramesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}