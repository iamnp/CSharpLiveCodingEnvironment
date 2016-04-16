using CSharpLiveCodingEnvironment.CodeEditing;

namespace CSharpLiveCodingEnvironment
{
    partial class MainForm
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportGifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportExeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawEllipsePictureBox = new System.Windows.Forms.PictureBox();
            this.drawEllipseWithStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawEllipseStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawRectPictureBox = new System.Windows.Forms.PictureBox();
            this.drawRectWithStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawRectStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawRoundedRectPictureBox = new System.Windows.Forms.PictureBox();
            this.drawRoundedRectWithStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawRoundedRectStrokePictureBox = new System.Windows.Forms.PictureBox();
            this.drawLinePictureBox = new System.Windows.Forms.PictureBox();
            this.drawTextPictureBox = new System.Windows.Forms.PictureBox();
            this.codeEditor = new CSharpLiveCodingEnvironment.CodeEditing.CodeEditor();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipsePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipseWithStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipseStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectWithStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectWithStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectStrokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawLinePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawTextPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 28);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(500, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 100;
            this.trackBar1.Visible = false;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(6, 76);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(500, 500);
            this.elementHost1.TabIndex = 10;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1150, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator3,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newFileToolStripMenuItem.Text = "Новый";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.openFileToolStripMenuItem.Text = "Открыть";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveFileToolStripMenuItem.Text = "Сохранить";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportPngToolStripMenuItem,
            this.exportGifToolStripMenuItem,
            this.exportExeToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exportToolStripMenuItem.Text = "Экспорт";
            // 
            // exportPngToolStripMenuItem
            // 
            this.exportPngToolStripMenuItem.Enabled = false;
            this.exportPngToolStripMenuItem.Name = "exportPngToolStripMenuItem";
            this.exportPngToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.exportPngToolStripMenuItem.Text = "Текущего кадра в PNG";
            this.exportPngToolStripMenuItem.Click += new System.EventHandler(this.exportPngToolStripMenuItem_Click);
            // 
            // exportGifToolStripMenuItem
            // 
            this.exportGifToolStripMenuItem.Enabled = false;
            this.exportGifToolStripMenuItem.Name = "exportGifToolStripMenuItem";
            this.exportGifToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.exportGifToolStripMenuItem.Text = "Сохраненных кадров в GIF";
            this.exportGifToolStripMenuItem.Click += new System.EventHandler(this.exportGifToolStripMenuItem_Click);
            // 
            // exportExeToolStripMenuItem
            // 
            this.exportExeToolStripMenuItem.Name = "exportExeToolStripMenuItem";
            this.exportExeToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.exportExeToolStripMenuItem.Text = "В исполняемый файл";
            this.exportExeToolStripMenuItem.Click += new System.EventHandler(this.exportExeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.settingsToolStripMenuItem.Text = "Параметры";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.quitToolStripMenuItem.Text = "Выход";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.watchListToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.viewToolStripMenuItem.Text = "Вид";
            // 
            // watchListToolStripMenuItem
            // 
            this.watchListToolStripMenuItem.Name = "watchListToolStripMenuItem";
            this.watchListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.watchListToolStripMenuItem.Text = "Watch List";
            this.watchListToolStripMenuItem.Click += new System.EventHandler(this.watchListToolStripMenuItem_Click_1);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.actionsToolStripMenuItem.Text = "Действия";
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.pauseToolStripMenuItem.Text = "Пауза";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.resetToolStripMenuItem.Text = "Сброс";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.helpToolStripMenuItem.Text = "Помощь";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 583);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1150, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // drawEllipsePictureBox
            // 
            this.drawEllipsePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawEllipsePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.ellipse;
            this.drawEllipsePictureBox.Location = new System.Drawing.Point(1112, 28);
            this.drawEllipsePictureBox.Name = "drawEllipsePictureBox";
            this.drawEllipsePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawEllipsePictureBox.TabIndex = 19;
            this.drawEllipsePictureBox.TabStop = false;
            this.drawEllipsePictureBox.Click += new System.EventHandler(this.drawEllipsePictureBox_Click);
            // 
            // drawEllipseWithStrokePictureBox
            // 
            this.drawEllipseWithStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawEllipseWithStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.ellipse_with_stroke;
            this.drawEllipseWithStrokePictureBox.Location = new System.Drawing.Point(1112, 66);
            this.drawEllipseWithStrokePictureBox.Name = "drawEllipseWithStrokePictureBox";
            this.drawEllipseWithStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawEllipseWithStrokePictureBox.TabIndex = 20;
            this.drawEllipseWithStrokePictureBox.TabStop = false;
            this.drawEllipseWithStrokePictureBox.Click += new System.EventHandler(this.drawEllipseWithStrokePictureBox_Click);
            // 
            // drawEllipseStrokePictureBox
            // 
            this.drawEllipseStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawEllipseStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.ellipse_stroke;
            this.drawEllipseStrokePictureBox.Location = new System.Drawing.Point(1112, 104);
            this.drawEllipseStrokePictureBox.Name = "drawEllipseStrokePictureBox";
            this.drawEllipseStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawEllipseStrokePictureBox.TabIndex = 21;
            this.drawEllipseStrokePictureBox.TabStop = false;
            this.drawEllipseStrokePictureBox.Click += new System.EventHandler(this.drawEllipseStrokePictureBox_Click);
            // 
            // drawRectPictureBox
            // 
            this.drawRectPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRectPictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rect;
            this.drawRectPictureBox.Location = new System.Drawing.Point(1112, 152);
            this.drawRectPictureBox.Name = "drawRectPictureBox";
            this.drawRectPictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRectPictureBox.TabIndex = 22;
            this.drawRectPictureBox.TabStop = false;
            this.drawRectPictureBox.Click += new System.EventHandler(this.drawRectPictureBox_Click);
            // 
            // drawRectWithStrokePictureBox
            // 
            this.drawRectWithStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRectWithStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rect_with_stroke;
            this.drawRectWithStrokePictureBox.Location = new System.Drawing.Point(1112, 190);
            this.drawRectWithStrokePictureBox.Name = "drawRectWithStrokePictureBox";
            this.drawRectWithStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRectWithStrokePictureBox.TabIndex = 23;
            this.drawRectWithStrokePictureBox.TabStop = false;
            this.drawRectWithStrokePictureBox.Click += new System.EventHandler(this.drawRectWithStrokePictureBox_Click);
            // 
            // drawRectStrokePictureBox
            // 
            this.drawRectStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRectStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rect_stroke;
            this.drawRectStrokePictureBox.Location = new System.Drawing.Point(1112, 228);
            this.drawRectStrokePictureBox.Name = "drawRectStrokePictureBox";
            this.drawRectStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRectStrokePictureBox.TabIndex = 24;
            this.drawRectStrokePictureBox.TabStop = false;
            this.drawRectStrokePictureBox.Click += new System.EventHandler(this.drawRectStrokePictureBox_Click);
            // 
            // drawRoundedRectPictureBox
            // 
            this.drawRoundedRectPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRoundedRectPictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rounded_rect;
            this.drawRoundedRectPictureBox.Location = new System.Drawing.Point(1112, 278);
            this.drawRoundedRectPictureBox.Name = "drawRoundedRectPictureBox";
            this.drawRoundedRectPictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRoundedRectPictureBox.TabIndex = 25;
            this.drawRoundedRectPictureBox.TabStop = false;
            this.drawRoundedRectPictureBox.Click += new System.EventHandler(this.drawRoundedRectPictureBox_Click);
            // 
            // drawRoundedRectWithStrokePictureBox
            // 
            this.drawRoundedRectWithStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRoundedRectWithStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rounded_rect_with_stroke;
            this.drawRoundedRectWithStrokePictureBox.Location = new System.Drawing.Point(1112, 316);
            this.drawRoundedRectWithStrokePictureBox.Name = "drawRoundedRectWithStrokePictureBox";
            this.drawRoundedRectWithStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRoundedRectWithStrokePictureBox.TabIndex = 26;
            this.drawRoundedRectWithStrokePictureBox.TabStop = false;
            this.drawRoundedRectWithStrokePictureBox.Click += new System.EventHandler(this.drawRoundedRectWithStrokePictureBox_Click);
            // 
            // drawRoundedRectStrokePictureBox
            // 
            this.drawRoundedRectStrokePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawRoundedRectStrokePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.rounded_rect_stroke;
            this.drawRoundedRectStrokePictureBox.Location = new System.Drawing.Point(1112, 354);
            this.drawRoundedRectStrokePictureBox.Name = "drawRoundedRectStrokePictureBox";
            this.drawRoundedRectStrokePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawRoundedRectStrokePictureBox.TabIndex = 27;
            this.drawRoundedRectStrokePictureBox.TabStop = false;
            this.drawRoundedRectStrokePictureBox.Click += new System.EventHandler(this.drawRoundedRectStrokePictureBox_Click);
            // 
            // drawLinePictureBox
            // 
            this.drawLinePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawLinePictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.line;
            this.drawLinePictureBox.Location = new System.Drawing.Point(1112, 404);
            this.drawLinePictureBox.Name = "drawLinePictureBox";
            this.drawLinePictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawLinePictureBox.TabIndex = 28;
            this.drawLinePictureBox.TabStop = false;
            this.drawLinePictureBox.Click += new System.EventHandler(this.drawLinePictureBox_Click);
            // 
            // drawTextPictureBox
            // 
            this.drawTextPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawTextPictureBox.Image = global::CSharpLiveCodingEnvironment.Properties.Resources.text;
            this.drawTextPictureBox.Location = new System.Drawing.Point(1112, 442);
            this.drawTextPictureBox.Name = "drawTextPictureBox";
            this.drawTextPictureBox.Size = new System.Drawing.Size(32, 32);
            this.drawTextPictureBox.TabIndex = 29;
            this.drawTextPictureBox.TabStop = false;
            this.drawTextPictureBox.Click += new System.EventHandler(this.drawTextPictureBox_Click);
            // 
            // codeEditor
            // 
            this.codeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditor.AutoScroll = true;
            this.codeEditor.AutoScrollMinSize = new System.Drawing.Size(21, 16);
            this.codeEditor.Location = new System.Drawing.Point(512, 28);
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.SelectedText = "";
            this.codeEditor.Size = new System.Drawing.Size(594, 548);
            this.codeEditor.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 605);
            this.Controls.Add(this.drawTextPictureBox);
            this.Controls.Add(this.drawLinePictureBox);
            this.Controls.Add(this.drawRoundedRectStrokePictureBox);
            this.Controls.Add(this.drawRoundedRectWithStrokePictureBox);
            this.Controls.Add(this.drawRoundedRectPictureBox);
            this.Controls.Add(this.drawRectStrokePictureBox);
            this.Controls.Add(this.drawRectWithStrokePictureBox);
            this.Controls.Add(this.drawRectPictureBox);
            this.Controls.Add(this.drawEllipseStrokePictureBox);
            this.Controls.Add(this.drawEllipseWithStrokePictureBox);
            this.Controls.Add(this.drawEllipsePictureBox);
            this.Controls.Add(this.codeEditor);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1000, 644);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Live Coding Environment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipsePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipseWithStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawEllipseStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectWithStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRectStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectWithStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawRoundedRectStrokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawLinePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawTextPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private CodeEditor codeEditor;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.PictureBox drawEllipsePictureBox;
        private System.Windows.Forms.PictureBox drawEllipseWithStrokePictureBox;
        private System.Windows.Forms.PictureBox drawEllipseStrokePictureBox;
        private System.Windows.Forms.PictureBox drawRectPictureBox;
        private System.Windows.Forms.PictureBox drawRectWithStrokePictureBox;
        private System.Windows.Forms.PictureBox drawRectStrokePictureBox;
        private System.Windows.Forms.PictureBox drawRoundedRectPictureBox;
        private System.Windows.Forms.PictureBox drawRoundedRectWithStrokePictureBox;
        private System.Windows.Forms.PictureBox drawRoundedRectStrokePictureBox;
        private System.Windows.Forms.PictureBox drawLinePictureBox;
        private System.Windows.Forms.PictureBox drawTextPictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportGifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportExeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem watchListToolStripMenuItem;
    }
}

