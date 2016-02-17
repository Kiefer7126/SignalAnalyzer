namespace SignalAnalyzer
{
    partial class MainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imAudioMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imAudioTextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imAudiowavMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imMetricalMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exAudioMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exAudioTextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exMetricalMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exMetricalWavMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeSTFTMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chartcontrol = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.testButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.beat = new System.Windows.Forms.Button();
            this.beatDetectionButton = new System.Windows.Forms.Button();
            this.experimentsButton = new System.Windows.Forms.Button();
            this.buttonGPR = new System.Windows.Forms.Button();
            this.beatEval = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartcontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.analyzeMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1254, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMenu,
            this.exportMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importMenu
            // 
            this.importMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imAudioMenu,
            this.imMetricalMenu});
            this.importMenu.Name = "importMenu";
            this.importMenu.Size = new System.Drawing.Size(115, 22);
            this.importMenu.Text = "Import";
            // 
            // imAudioMenu
            // 
            this.imAudioMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imAudioTextMenu,
            this.imAudiowavMenu});
            this.imAudioMenu.Name = "imAudioMenu";
            this.imAudioMenu.Size = new System.Drawing.Size(120, 22);
            this.imAudioMenu.Text = "Audio";
            // 
            // imAudioTextMenu
            // 
            this.imAudioTextMenu.Name = "imAudioTextMenu";
            this.imAudioTextMenu.Size = new System.Drawing.Size(98, 22);
            this.imAudioTextMenu.Text = "text";
            this.imAudioTextMenu.Click += new System.EventHandler(this.imAudio_TextMenu_Click);
            // 
            // imAudiowavMenu
            // 
            this.imAudiowavMenu.Name = "imAudiowavMenu";
            this.imAudiowavMenu.Size = new System.Drawing.Size(98, 22);
            this.imAudiowavMenu.Text = "wav";
            this.imAudiowavMenu.Click += new System.EventHandler(this.imAudio_wavMenu_Click);
            // 
            // imMetricalMenu
            // 
            this.imMetricalMenu.Name = "imMetricalMenu";
            this.imMetricalMenu.Size = new System.Drawing.Size(120, 22);
            this.imMetricalMenu.Text = "Metrical";
            this.imMetricalMenu.Click += new System.EventHandler(this.imMetricalMenu_Click);
            // 
            // exportMenu
            // 
            this.exportMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exAudioMenu,
            this.exMetricalMenu});
            this.exportMenu.Name = "exportMenu";
            this.exportMenu.Size = new System.Drawing.Size(115, 22);
            this.exportMenu.Text = "Export";
            // 
            // exAudioMenu
            // 
            this.exAudioMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exAudioTextMenu});
            this.exAudioMenu.Name = "exAudioMenu";
            this.exAudioMenu.Size = new System.Drawing.Size(120, 22);
            this.exAudioMenu.Text = "Audio";
            // 
            // exAudioTextMenu
            // 
            this.exAudioTextMenu.Name = "exAudioTextMenu";
            this.exAudioTextMenu.Size = new System.Drawing.Size(98, 22);
            this.exAudioTextMenu.Text = "text";
            this.exAudioTextMenu.Click += new System.EventHandler(this.exAudio_TextMenu_Click);
            // 
            // exMetricalMenu
            // 
            this.exMetricalMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToolStripMenuItem,
            this.exMetricalWavMenu});
            this.exMetricalMenu.Name = "exMetricalMenu";
            this.exMetricalMenu.Size = new System.Drawing.Size(120, 22);
            this.exMetricalMenu.Text = "Metrical";
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.textToolStripMenuItem.Text = "text";
            // 
            // exMetricalWavMenu
            // 
            this.exMetricalWavMenu.Name = "exMetricalWavMenu";
            this.exMetricalWavMenu.Size = new System.Drawing.Size(98, 22);
            this.exMetricalWavMenu.Text = "wav";
            this.exMetricalWavMenu.Click += new System.EventHandler(this.exMetricalWavMenu_Click);
            // 
            // analyzeMenu
            // 
            this.analyzeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeSTFTMenu});
            this.analyzeMenu.Name = "analyzeMenu";
            this.analyzeMenu.Size = new System.Drawing.Size(64, 20);
            this.analyzeMenu.Text = "Analyze";
            // 
            // analyzeSTFTMenu
            // 
            this.analyzeSTFTMenu.Name = "analyzeSTFTMenu";
            this.analyzeSTFTMenu.Size = new System.Drawing.Size(105, 22);
            this.analyzeSTFTMenu.Text = "STFT";
            this.analyzeSTFTMenu.Click += new System.EventHandler(this.analyzeSTFTMenu_Click);
            // 
            // chartcontrol
            // 
            chartArea1.Name = "ChartArea1";
            this.chartcontrol.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartcontrol.Legends.Add(legend1);
            this.chartcontrol.Location = new System.Drawing.Point(12, 580);
            this.chartcontrol.Name = "chartcontrol";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartcontrol.Series.Add(series1);
            this.chartcontrol.Size = new System.Drawing.Size(1230, 128);
            this.chartcontrol.TabIndex = 1;
            this.chartcontrol.Text = "chart1";
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(24, 41);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 2;
            this.testButton.Text = "test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1230, 504);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 504);
            this.panel1.TabIndex = 4;
            // 
            // beat
            // 
            this.beat.Location = new System.Drawing.Point(106, 41);
            this.beat.Name = "beat";
            this.beat.Size = new System.Drawing.Size(75, 23);
            this.beat.TabIndex = 5;
            this.beat.Text = "Metrical";
            this.beat.UseVisualStyleBackColor = true;
            this.beat.Click += new System.EventHandler(this.beat_Click);
            // 
            // beatDetectionButton
            // 
            this.beatDetectionButton.Location = new System.Drawing.Point(188, 41);
            this.beatDetectionButton.Name = "beatDetectionButton";
            this.beatDetectionButton.Size = new System.Drawing.Size(75, 23);
            this.beatDetectionButton.TabIndex = 6;
            this.beatDetectionButton.Text = "Beat";
            this.beatDetectionButton.UseVisualStyleBackColor = true;
            this.beatDetectionButton.Click += new System.EventHandler(this.beatDetectionButton_Click);
            // 
            // experimentsButton
            // 
            this.experimentsButton.Location = new System.Drawing.Point(270, 41);
            this.experimentsButton.Name = "experimentsButton";
            this.experimentsButton.Size = new System.Drawing.Size(75, 23);
            this.experimentsButton.TabIndex = 7;
            this.experimentsButton.Text = "experiments";
            this.experimentsButton.UseVisualStyleBackColor = true;
            this.experimentsButton.Click += new System.EventHandler(this.experimentsButton_Click);
            // 
            // buttonGPR
            // 
            this.buttonGPR.Location = new System.Drawing.Point(351, 41);
            this.buttonGPR.Name = "buttonGPR";
            this.buttonGPR.Size = new System.Drawing.Size(75, 23);
            this.buttonGPR.TabIndex = 8;
            this.buttonGPR.Text = "GPR";
            this.buttonGPR.UseVisualStyleBackColor = true;
            this.buttonGPR.Click += new System.EventHandler(this.buttonGPR_Click);
            // 
            // beatEval
            // 
            this.beatEval.Location = new System.Drawing.Point(433, 41);
            this.beatEval.Name = "beatEval";
            this.beatEval.Size = new System.Drawing.Size(75, 23);
            this.beatEval.TabIndex = 9;
            this.beatEval.Text = "beatEval";
            this.beatEval.UseVisualStyleBackColor = true;
            this.beatEval.Click += new System.EventHandler(this.beatEval_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 720);
            this.Controls.Add(this.beatEval);
            this.Controls.Add(this.buttonGPR);
            this.Controls.Add(this.experimentsButton);
            this.Controls.Add(this.beatDetectionButton);
            this.Controls.Add(this.beat);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.chartcontrol);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartcontrol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMenu;
        private System.Windows.Forms.ToolStripMenuItem exportMenu;
        private System.Windows.Forms.ToolStripMenuItem imAudioMenu;
        private System.Windows.Forms.ToolStripMenuItem imMetricalMenu;
        private System.Windows.Forms.ToolStripMenuItem exAudioMenu;
        private System.Windows.Forms.ToolStripMenuItem exMetricalMenu;
        private System.Windows.Forms.ToolStripMenuItem imAudioTextMenu;
        private System.Windows.Forms.ToolStripMenuItem imAudiowavMenu;
        private System.Windows.Forms.ToolStripMenuItem exAudioTextMenu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartcontrol;
        private System.Windows.Forms.ToolStripMenuItem analyzeMenu;
        private System.Windows.Forms.ToolStripMenuItem analyzeSTFTMenu;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exMetricalWavMenu;
        private System.Windows.Forms.Button beat;
        private System.Windows.Forms.Button beatDetectionButton;
        private System.Windows.Forms.Button experimentsButton;
        private System.Windows.Forms.Button buttonGPR;
        private System.Windows.Forms.Button beatEval;
    }
}

