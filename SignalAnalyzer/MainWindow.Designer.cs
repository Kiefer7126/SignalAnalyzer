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
            this.clusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exAudioMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exAudioTextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exMetricalMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exMetricalWavMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeSTFTMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chartcontrol = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.drawSpectrogramButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.generateAudioBeat = new System.Windows.Forms.Button();
            this.beatDetectionButton = new System.Windows.Forms.Button();
            this.generateBeatAllButton = new System.Windows.Forms.Button();
            this.buttonGPR = new System.Windows.Forms.Button();
            this.beatEvaluation = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.allProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelGPR5Param = new System.Windows.Forms.Label();
            this.labelGPR3Param = new System.Windows.Forms.Label();
            this.labelGPR2bParam = new System.Windows.Forms.Label();
            this.labelGPR2aParam = new System.Windows.Forms.Label();
            this.labelGPR5 = new System.Windows.Forms.Label();
            this.labelGPR3 = new System.Windows.Forms.Label();
            this.labelGPR2b = new System.Windows.Forms.Label();
            this.labelGPR2a = new System.Windows.Forms.Label();
            this.trackBarGPR5 = new System.Windows.Forms.TrackBar();
            this.trackBarGPR3 = new System.Windows.Forms.TrackBar();
            this.trackBarGPR2b = new System.Windows.Forms.TrackBar();
            this.trackBarGPR2a = new System.Windows.Forms.TrackBar();
            this.playButton = new System.Windows.Forms.Button();
            this.buttonGLCM = new System.Windows.Forms.Button();
            this.drawSpectrogramAllButton = new System.Windows.Forms.Button();
            this.truthChorusbutton = new System.Windows.Forms.Button();
            this.fMeasureButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartcontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR2b)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR2a)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.analyzeMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
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
            this.imMetricalMenu,
            this.clusterToolStripMenuItem});
            this.importMenu.Name = "importMenu";
            this.importMenu.Size = new System.Drawing.Size(152, 22);
            this.importMenu.Text = "Import";
            // 
            // imAudioMenu
            // 
            this.imAudioMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imAudioTextMenu,
            this.imAudiowavMenu});
            this.imAudioMenu.Name = "imAudioMenu";
            this.imAudioMenu.Size = new System.Drawing.Size(152, 22);
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
            this.imMetricalMenu.Size = new System.Drawing.Size(152, 22);
            this.imMetricalMenu.Text = "Metrical";
            this.imMetricalMenu.Click += new System.EventHandler(this.imMetricalMenu_Click);
            // 
            // clusterToolStripMenuItem
            // 
            this.clusterToolStripMenuItem.Name = "clusterToolStripMenuItem";
            this.clusterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clusterToolStripMenuItem.Text = "Cluster";
            this.clusterToolStripMenuItem.Click += new System.EventHandler(this.clusterToolStripMenuItem_Click);
            // 
            // exportMenu
            // 
            this.exportMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exAudioMenu,
            this.exMetricalMenu});
            this.exportMenu.Name = "exportMenu";
            this.exportMenu.Size = new System.Drawing.Size(152, 22);
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
            this.chartcontrol.Size = new System.Drawing.Size(802, 128);
            this.chartcontrol.TabIndex = 1;
            this.chartcontrol.Text = "chart1";
            // 
            // drawSpectrogramButton
            // 
            this.drawSpectrogramButton.Location = new System.Drawing.Point(698, 307);
            this.drawSpectrogramButton.Name = "drawSpectrogramButton";
            this.drawSpectrogramButton.Size = new System.Drawing.Size(116, 23);
            this.drawSpectrogramButton.TabIndex = 2;
            this.drawSpectrogramButton.Text = "DrawSpectrogram";
            this.drawSpectrogramButton.UseVisualStyleBackColor = true;
            this.drawSpectrogramButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(652, 504);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 504);
            this.panel1.TabIndex = 4;
            // 
            // generateAudioBeat
            // 
            this.generateAudioBeat.Location = new System.Drawing.Point(698, 336);
            this.generateAudioBeat.Name = "generateAudioBeat";
            this.generateAudioBeat.Size = new System.Drawing.Size(116, 23);
            this.generateAudioBeat.TabIndex = 5;
            this.generateAudioBeat.Text = "GenerateAudioBeat";
            this.generateAudioBeat.UseVisualStyleBackColor = true;
            this.generateAudioBeat.Click += new System.EventHandler(this.beat_Click);
            // 
            // beatDetectionButton
            // 
            this.beatDetectionButton.Location = new System.Drawing.Point(698, 365);
            this.beatDetectionButton.Name = "beatDetectionButton";
            this.beatDetectionButton.Size = new System.Drawing.Size(116, 23);
            this.beatDetectionButton.TabIndex = 6;
            this.beatDetectionButton.Text = "BeatDetection";
            this.beatDetectionButton.UseVisualStyleBackColor = true;
            this.beatDetectionButton.Click += new System.EventHandler(this.beatDetectionButton_Click);
            // 
            // generateBeatAllButton
            // 
            this.generateBeatAllButton.Location = new System.Drawing.Point(698, 520);
            this.generateBeatAllButton.Name = "generateBeatAllButton";
            this.generateBeatAllButton.Size = new System.Drawing.Size(117, 23);
            this.generateBeatAllButton.TabIndex = 7;
            this.generateBeatAllButton.Text = "GenerateBeatAll";
            this.generateBeatAllButton.UseVisualStyleBackColor = true;
            this.generateBeatAllButton.Click += new System.EventHandler(this.experimentsButton_Click);
            // 
            // buttonGPR
            // 
            this.buttonGPR.Location = new System.Drawing.Point(698, 394);
            this.buttonGPR.Name = "buttonGPR";
            this.buttonGPR.Size = new System.Drawing.Size(116, 23);
            this.buttonGPR.TabIndex = 8;
            this.buttonGPR.Text = "GPR";
            this.buttonGPR.UseVisualStyleBackColor = true;
            this.buttonGPR.Click += new System.EventHandler(this.buttonGPR_Click);
            // 
            // beatEvaluation
            // 
            this.beatEvaluation.Location = new System.Drawing.Point(698, 549);
            this.beatEvaluation.Name = "beatEvaluation";
            this.beatEvaluation.Size = new System.Drawing.Size(116, 23);
            this.beatEvaluation.TabIndex = 9;
            this.beatEvaluation.Text = "BeatEvaluation";
            this.beatEvaluation.UseVisualStyleBackColor = true;
            this.beatEvaluation.Click += new System.EventHandler(this.beatEval_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(258, 49);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(245, 12);
            this.progressBar.TabIndex = 10;
            // 
            // allProgressBar
            // 
            this.allProgressBar.Location = new System.Drawing.Point(258, 27);
            this.allProgressBar.Name = "allProgressBar";
            this.allProgressBar.Size = new System.Drawing.Size(245, 19);
            this.allProgressBar.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelGPR5Param);
            this.groupBox1.Controls.Add(this.labelGPR3Param);
            this.groupBox1.Controls.Add(this.labelGPR2bParam);
            this.groupBox1.Controls.Add(this.labelGPR2aParam);
            this.groupBox1.Controls.Add(this.labelGPR5);
            this.groupBox1.Controls.Add(this.labelGPR3);
            this.groupBox1.Controls.Add(this.labelGPR2b);
            this.groupBox1.Controls.Add(this.labelGPR2a);
            this.groupBox1.Controls.Add(this.trackBarGPR5);
            this.groupBox1.Controls.Add(this.trackBarGPR3);
            this.groupBox1.Controls.Add(this.trackBarGPR2b);
            this.groupBox1.Controls.Add(this.trackBarGPR2a);
            this.groupBox1.Location = new System.Drawing.Point(674, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 229);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GPR Parameter";
            // 
            // labelGPR5Param
            // 
            this.labelGPR5Param.AutoSize = true;
            this.labelGPR5Param.Location = new System.Drawing.Point(72, 161);
            this.labelGPR5Param.Name = "labelGPR5Param";
            this.labelGPR5Param.Size = new System.Drawing.Size(19, 12);
            this.labelGPR5Param.TabIndex = 19;
            this.labelGPR5Param.Text = "0.5";
            // 
            // labelGPR3Param
            // 
            this.labelGPR3Param.AutoSize = true;
            this.labelGPR3Param.Location = new System.Drawing.Point(72, 116);
            this.labelGPR3Param.Name = "labelGPR3Param";
            this.labelGPR3Param.Size = new System.Drawing.Size(19, 12);
            this.labelGPR3Param.TabIndex = 18;
            this.labelGPR3Param.Text = "0.5";
            // 
            // labelGPR2bParam
            // 
            this.labelGPR2bParam.AutoSize = true;
            this.labelGPR2bParam.Location = new System.Drawing.Point(72, 67);
            this.labelGPR2bParam.Name = "labelGPR2bParam";
            this.labelGPR2bParam.Size = new System.Drawing.Size(19, 12);
            this.labelGPR2bParam.TabIndex = 17;
            this.labelGPR2bParam.Text = "0.5";
            // 
            // labelGPR2aParam
            // 
            this.labelGPR2aParam.AutoSize = true;
            this.labelGPR2aParam.Location = new System.Drawing.Point(72, 19);
            this.labelGPR2aParam.Name = "labelGPR2aParam";
            this.labelGPR2aParam.Size = new System.Drawing.Size(19, 12);
            this.labelGPR2aParam.TabIndex = 16;
            this.labelGPR2aParam.Text = "0.5";
            // 
            // labelGPR5
            // 
            this.labelGPR5.AutoSize = true;
            this.labelGPR5.Location = new System.Drawing.Point(6, 161);
            this.labelGPR5.Name = "labelGPR5";
            this.labelGPR5.Size = new System.Drawing.Size(34, 12);
            this.labelGPR5.TabIndex = 15;
            this.labelGPR5.Text = "GPR5";
            // 
            // labelGPR3
            // 
            this.labelGPR3.AutoSize = true;
            this.labelGPR3.Location = new System.Drawing.Point(6, 116);
            this.labelGPR3.Name = "labelGPR3";
            this.labelGPR3.Size = new System.Drawing.Size(34, 12);
            this.labelGPR3.TabIndex = 14;
            this.labelGPR3.Text = "GPR3";
            // 
            // labelGPR2b
            // 
            this.labelGPR2b.AutoSize = true;
            this.labelGPR2b.Location = new System.Drawing.Point(6, 67);
            this.labelGPR2b.Name = "labelGPR2b";
            this.labelGPR2b.Size = new System.Drawing.Size(40, 12);
            this.labelGPR2b.TabIndex = 13;
            this.labelGPR2b.Text = "GPR2b";
            // 
            // labelGPR2a
            // 
            this.labelGPR2a.AutoSize = true;
            this.labelGPR2a.Location = new System.Drawing.Point(6, 19);
            this.labelGPR2a.Name = "labelGPR2a";
            this.labelGPR2a.Size = new System.Drawing.Size(40, 12);
            this.labelGPR2a.TabIndex = 12;
            this.labelGPR2a.Text = "GPR2a";
            // 
            // trackBarGPR5
            // 
            this.trackBarGPR5.Location = new System.Drawing.Point(7, 176);
            this.trackBarGPR5.Name = "trackBarGPR5";
            this.trackBarGPR5.Size = new System.Drawing.Size(148, 45);
            this.trackBarGPR5.TabIndex = 3;
            this.trackBarGPR5.Value = 5;
            this.trackBarGPR5.Scroll += new System.EventHandler(this.trackBarGPR5_Scroll);
            // 
            // trackBarGPR3
            // 
            this.trackBarGPR3.Location = new System.Drawing.Point(7, 128);
            this.trackBarGPR3.Name = "trackBarGPR3";
            this.trackBarGPR3.Size = new System.Drawing.Size(148, 45);
            this.trackBarGPR3.TabIndex = 2;
            this.trackBarGPR3.Value = 5;
            this.trackBarGPR3.Scroll += new System.EventHandler(this.trackBarGPR3_Scroll);
            // 
            // trackBarGPR2b
            // 
            this.trackBarGPR2b.Location = new System.Drawing.Point(7, 82);
            this.trackBarGPR2b.Name = "trackBarGPR2b";
            this.trackBarGPR2b.Size = new System.Drawing.Size(148, 45);
            this.trackBarGPR2b.TabIndex = 1;
            this.trackBarGPR2b.Value = 5;
            this.trackBarGPR2b.Scroll += new System.EventHandler(this.trackBarGPR2b_Scroll);
            // 
            // trackBarGPR2a
            // 
            this.trackBarGPR2a.Location = new System.Drawing.Point(8, 34);
            this.trackBarGPR2a.Name = "trackBarGPR2a";
            this.trackBarGPR2a.Size = new System.Drawing.Size(146, 45);
            this.trackBarGPR2a.TabIndex = 0;
            this.trackBarGPR2a.Value = 5;
            this.trackBarGPR2a.Scroll += new System.EventHandler(this.trackBarGPR2a_Scroll);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(13, 37);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 12;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // buttonGLCM
            // 
            this.buttonGLCM.Location = new System.Drawing.Point(698, 424);
            this.buttonGLCM.Name = "buttonGLCM";
            this.buttonGLCM.Size = new System.Drawing.Size(116, 23);
            this.buttonGLCM.TabIndex = 13;
            this.buttonGLCM.Text = "GLCM";
            this.buttonGLCM.UseVisualStyleBackColor = true;
            this.buttonGLCM.Click += new System.EventHandler(this.buttonGLCM_Click);
            // 
            // drawSpectrogramAllButton
            // 
            this.drawSpectrogramAllButton.Location = new System.Drawing.Point(682, 491);
            this.drawSpectrogramAllButton.Name = "drawSpectrogramAllButton";
            this.drawSpectrogramAllButton.Size = new System.Drawing.Size(146, 23);
            this.drawSpectrogramAllButton.TabIndex = 14;
            this.drawSpectrogramAllButton.Text = "DrawSpectrogramAll";
            this.drawSpectrogramAllButton.UseVisualStyleBackColor = true;
            this.drawSpectrogramAllButton.Click += new System.EventHandler(this.drawSpectrogramAllButton_Click);
            // 
            // truthChorusbutton
            // 
            this.truthChorusbutton.Location = new System.Drawing.Point(698, 462);
            this.truthChorusbutton.Name = "truthChorusbutton";
            this.truthChorusbutton.Size = new System.Drawing.Size(116, 23);
            this.truthChorusbutton.TabIndex = 4;
            this.truthChorusbutton.Text = "TruthChorus";
            this.truthChorusbutton.UseVisualStyleBackColor = true;
            this.truthChorusbutton.Click += new System.EventHandler(this.truthChorusbutton_Click);
            // 
            // fMeasureButton
            // 
            this.fMeasureButton.Location = new System.Drawing.Point(720, 37);
            this.fMeasureButton.Name = "fMeasureButton";
            this.fMeasureButton.Size = new System.Drawing.Size(75, 23);
            this.fMeasureButton.TabIndex = 15;
            this.fMeasureButton.Text = "F-Measure";
            this.fMeasureButton.UseVisualStyleBackColor = true;
            this.fMeasureButton.Click += new System.EventHandler(this.fMeasureButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 720);
            this.Controls.Add(this.fMeasureButton);
            this.Controls.Add(this.truthChorusbutton);
            this.Controls.Add(this.drawSpectrogramAllButton);
            this.Controls.Add(this.buttonGLCM);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.allProgressBar);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.beatEvaluation);
            this.Controls.Add(this.buttonGPR);
            this.Controls.Add(this.generateBeatAllButton);
            this.Controls.Add(this.beatDetectionButton);
            this.Controls.Add(this.generateAudioBeat);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.drawSpectrogramButton);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR2b)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPR2a)).EndInit();
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
        private System.Windows.Forms.Button drawSpectrogramButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exMetricalWavMenu;
        private System.Windows.Forms.Button generateAudioBeat;
        private System.Windows.Forms.Button beatDetectionButton;
        private System.Windows.Forms.Button generateBeatAllButton;
        private System.Windows.Forms.Button buttonGPR;
        private System.Windows.Forms.Button beatEvaluation;
        private System.Windows.Forms.ProgressBar allProgressBar;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarGPR2b;
        private System.Windows.Forms.TrackBar trackBarGPR2a;
        private System.Windows.Forms.TrackBar trackBarGPR3;
        private System.Windows.Forms.TrackBar trackBarGPR5;
        private System.Windows.Forms.Label labelGPR5;
        private System.Windows.Forms.Label labelGPR3;
        private System.Windows.Forms.Label labelGPR2b;
        private System.Windows.Forms.Label labelGPR2a;
        private System.Windows.Forms.Label labelGPR5Param;
        private System.Windows.Forms.Label labelGPR3Param;
        private System.Windows.Forms.Label labelGPR2bParam;
        private System.Windows.Forms.Label labelGPR2aParam;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button buttonGLCM;
        private System.Windows.Forms.Button drawSpectrogramAllButton;
        private System.Windows.Forms.ToolStripMenuItem clusterToolStripMenuItem;
        private System.Windows.Forms.Button truthChorusbutton;
        private System.Windows.Forms.Button fMeasureButton;
    }
}

