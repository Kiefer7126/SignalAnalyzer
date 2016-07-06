using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SignalAnalyzer
{
    enum Direction { Degree0, Degree45, Degree90, Degree135 };

    public partial class MainWindow : Form
    {
        WavFile wavFile;
        MetricalStructure metricalStruct;
        ChorusStructure chorusStruct;
        Graph gprGraph;

        private System.Media.SoundPlayer player = null;

        public MainWindow()
        {
            InitializeComponent();
            wavFile = new WavFile();
            metricalStruct = new MetricalStructure();
            chorusStruct = new ChorusStructure();

            allProgressBar.Minimum = 0;
            allProgressBar.Maximum = 3; //処理の数
            allProgressBar.Value = 0;

            gprGraph = new Graph();
        }

        /**
         * imMetricalMenu_Click
         * 概要：[File] -> [import] -> [Metrical]
         * @param  なし
         * @return なし
         */

        private void imMetricalMenu_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string beatFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            metricalStruct = importFile.ReadMetric(beatFileName);
        }

        /**
         * imAudioTextMenu_Click
         * 概要：[File] -> [import] -> [Audio] -> [text]
         * @param  なし
         * @return なし
         */

        private void imAudio_TextMenu_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            int[] originalData = importFile.ReadAudioText(fileName);
            var graph = new Graph();
            graph.draw(this.chartcontrol, originalData);

        }

        /**
         * imAudioWavMenu_Click
         * 概要：[File] -> [import] -> [Audio] -> [wav]
         * @param  なし
         * @return なし
         */

        private void imAudio_wavMenu_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);
            var graph = new Graph();
            graph.draw(this.chartcontrol, wavFile.RightData);
        }

        /**
         * exAudioTextMenu_Click
         * 概要：[File] -> [import] -> [Audio] -> [wav]
         * @param  なし
         * @return なし
         */

        private void exAudio_TextMenu_Click(object sender, EventArgs e)
        {
            var exportFile = new ExportFile();
            exportFile.WriteAudioText(wavFile);
        }

        /**
         * analyzeSTFTMenu_Click
         * 概要：[Analyze] -> [STFT]
         * @param  なし
         * @return なし
         */

        private void analyzeSTFTMenu_Click(object sender, EventArgs e)
        {
            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData, this.progressBar);

            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            //PictureBoxの大きさが変更させるようにする
            pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length / 4);

            var graph = new Graph();
            graph.DrawSpectrogram(this.pictureBox1, freqAnalyzer, null, null, 0, this.progressBar);
        }

        /**
         * testButton_Click
         * 概要：グラフとスペクトログラムの描画
         * @param  なし
         * @return なし
         */

        private void testButton_Click(object sender, EventArgs e)
        {

            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);
            var graph = new Graph();
            graph.draw(this.chartcontrol, wavFile.RightData);
            allProgressBar.Value = 1;

            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData, this.progressBar);
            allProgressBar.Value = 2;

            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            //PictureBoxの大きさが変更させるようにする
            pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length / 4);

            graph.DrawSpectrogram(this.pictureBox1, freqAnalyzer, null, null, 0, this.progressBar);
            allProgressBar.Value = 3;

        }

        /**
         * exAudioTextMenu_Click
         * 概要：[File] -> [export] -> [Metrical] -> [wav]
         * @param  なし
         * @return なし
         */

        private void exMetricalWavMenu_Click(object sender, EventArgs e)
        {
            var generateWave = new GenerateWave();
            var beatStructure = new double[wavFile.RightData.Length];
            beatStructure = generateWave.beat(wavFile, metricalStruct);
            var export = new ExportFile();
            string fileName = export.SaveFileDialog(ExportFile.Formats.Wav);
            export.WriteClick(beatStructure, fileName);
        }


        /**
         * beat_Click
         * 概要： ビートボタンを押したときの処理．ビートのwavファイルを出力．
         * @param  なし
         * @return なし
         */

        private void beat_Click(object sender, EventArgs e)
        {
            //importAudio
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);

            //importBeatStructure
            string beatFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            metricalStruct = importFile.ReadMetric(beatFileName);

            //generateClick
            var generateWave = new GenerateWave();
            var beatStructure = new double[wavFile.RightData.Length];
            beatStructure = generateWave.beat(wavFile, metricalStruct);

            //exportClick
            var export = new ExportFile();
            string expfileName = export.SaveFileDialog(ExportFile.Formats.Wav);
            export.WriteClick(beatStructure, expfileName);
        }

        /**
         * beatDetectionButton_Click
         * 概要： beatDetectionボタン押したときの処理．ビートトラッキングを行い，テキスト形式で出力．
         * @param  なし
         * @return なし
         */

        private void beatDetectionButton_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);

            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData, this.progressBar);

            var beatDetection = new BeatDetection();
            var beat = new double[freqAnalyzer.stftData.Length];
            beat = beatDetection.main(freqAnalyzer);

            var exportFile = new ExportFile();
            string exportFileName = exportFile.SaveFileDialog(ExportFile.Formats.Text);
            exportFile.WriteMetricalText(beat, exportFileName);
        }

        /**
         * experimentsButton_Click
         * 概要： experimentボタンを押したときの処理．指定ディレクトリ内の楽曲をビートトラッキング（txtとwavを出力）
         * @param  なし
         * @return なし
         */

        private void experimentsButton_Click(object sender, EventArgs e)
        {
            //ファイルをすべて取得する
            //string[] files = System.IO.Directory.GetFiles(@"C:\Users\b1012046\Music\V.A\RWC研究用音楽データベース Disc 1", "*", System.IO.SearchOption.AllDirectories);
            string[] files = System.IO.Directory.GetFiles(@"C:\Users\sawada\Music\Pops", "*", System.IO.SearchOption.AllDirectories);

            var importFile = new ImportFile();
            var freqAnalyzer = new FrequencyAnalyzer();
            var beatDetection = new BeatDetection();
            var exportFile = new ExportFile();
            var generateWave = new GenerateWave();


            int count = 1;
            foreach (var fileName in files)
            {
                wavFile = importFile.ReadAudioWav(fileName);

                freqAnalyzer.STFT(wavFile.RightData, this.progressBar);

                var beat = new double[freqAnalyzer.stftData.Length];
                beat = beatDetection.main(freqAnalyzer);

                string noExtFileName = fileName.Replace(".wav", "");
                noExtFileName = fileName.Replace("Pops", "20160128");
                exportFile.WriteMetricalText(beat, noExtFileName + "_beat.txt");

                var beatStructure = new double[wavFile.RightData.Length];
                metricalStruct = importFile.ReadMetric(noExtFileName + "_beat.txt");
                beatStructure = generateWave.beat(wavFile, metricalStruct);

                exportFile.WriteClick(beatStructure, noExtFileName + "_beat.wav");

                Console.WriteLine("100個中" + count + "個のデータを処理");
                count++;
            }
        }

        /**
         * buttonGPR_Click
         * 概要： グルーピング構造分析を行う．
         * @param  なし
         * @return なし
         */

        private void buttonGPR_Click(object sender, EventArgs e)
        {
            /* read metric */
            var importFile = new ImportFile();
            //ファイルを選ぶときに使用

            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);

            PlaySound(fileName);

            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData, this.progressBar);

            var beatDetection = new BeatDetection();
            var beat = new double[freqAnalyzer.stftData.Length];
            beat = beatDetection.main(freqAnalyzer);

            var exportFile = new ExportFile();
            string exportFileName = exportFile.SaveFileDialog(ExportFile.Formats.Text);
            exportFile.WriteMetricalText(beat, exportFileName);

            metricalStruct = importFile.ReadMetric(exportFileName);

            /*
            string beatFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            metricalStruct = importFile.ReadMetric(beatFileName);
             */

            /*
            metricalStruct = importFile.ReadMetric("C:/Users/sawada/Desktop/AIST.RWC-MDB-P-2001.BEAT/RM-P001.BEAT.txt");
            wavFile = importFile.ReadAudioWav("C:/Users/sawada/Desktop/01 永遠のレプリカedit2.wav");
            */
            /* 
             metricalStruct = importFile.ReadMetric("C:/Users/sawada/Desktop/AIST.RWC-MDB-P-2001.BEAT/RM-P002.BEAT.txt");
             wavFile = importFile.ReadAudioWav("C:/Users/sawada/Music/RWC研究用音楽データベース Disc 1/02 Magic in your eyes.wav");
             */
            /*
         metricalStruct = importFile.ReadMetric("C:/Users/sawada/Desktop/AIST.RWC-MDB-C-2001.BEAT/RM-C002.BEAT.txt");
         wavFile = importFile.ReadAudioWav("C:/Users/sawada/Desktop/02 Symphony no. 40 in G minor, K. 550. 1st mvmt-01.wav");
        */

            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            //PictureBoxの大きさが変更させるようにする
            pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length / 4);

            //スペクトログラムの描画

            gprGraph.DrawSpectrogram(this.pictureBox1, freqAnalyzer, metricalStruct.aistToBeat(wavFile), freqAnalyzer.sumSpectoroInterval(metricalStruct), metricalStruct.BeatInterval, this.progressBar);
        }

        /**
         * beatEval_Click
         * 概要： 正解データと比較する
         * @param  なし
         * @return なし
         */

        private void beatEval_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();

            /*
            string correctFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            var correctTime = importFile.ReadMetric(correctFileName);

            string beatFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            var beatTime = importFile.ReadMetric(beatFileName);
            */



            string[] correctFiles = System.IO.Directory.GetFiles(@"C:\Users\sawada\Desktop\AIST.RWC-MDB-P-2001.BEAT", "*", System.IO.SearchOption.AllDirectories);
            string[] beatFiles = System.IO.Directory.GetFiles(@"C:\Users\sawada\Music\20160128\DiscAll", "*", System.IO.SearchOption.AllDirectories);

            var beatEval = new List<List<double>>();

            var intervalList = new List<string>();

            for (int index = 0; index < correctFiles.Length; index++)
            {
                var correctTime = importFile.ReadMetric(correctFiles[index]);
                var beatTime = importFile.ReadMetric(beatFiles[index]);

                var pairTime = new List<double>();

                int correctInterval = 0;
                int beatInterval = 0;
                int i = 0;

                correctInterval = correctTime.RightTime[1] - correctTime.RightTime[0];
                beatInterval = beatTime.RightTime[1] - beatTime.RightTime[0];

                intervalList.Add(correctInterval + ":" + beatInterval);

                if (correctInterval > beatInterval)
                {
                    for (int j = 0; j < beatTime.RightTime.Length; j++)
                    {
                        if (correctTime.RightTime[i] - correctInterval / 2 <= beatTime.RightTime[j] && beatTime.RightTime[j] < correctTime.RightTime[i] + correctInterval / 2)
                        {
                            pairTime.Add(Math.Abs(beatTime.RightTime[j] - correctTime.RightTime[i]) * 2.0 / correctInterval);
                            i++;
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < beatTime.RightTime.Length; k++)
                    {
                        if (correctTime.RightTime[k] - correctInterval / 2 <= beatTime.RightTime[i] && beatTime.RightTime[i] < correctTime.RightTime[k] + correctInterval / 2)
                        {
                            pairTime.Add(Math.Abs(beatTime.RightTime[i] - correctTime.RightTime[k]) * 2.0 / correctInterval);
                            i++;
                        }
                    }
                }
                beatEval.Add(pairTime);
            }

            int count = 0;
            var beatTracking = new Boolean[beatEval.Count];
            for (int i = 0; i < beatEval.Count; i++)
            {
                beatTracking[i] = true;

                if (beatEval[i].Count == 0) beatTracking[i] = false;

                for (int j = 0; j < beatEval[i].Count; j++)
                {
                    if (beatEval[i][j] > 0.35) beatTracking[i] = false;
                }
            }
        }

        private void changeGPRValiable()
        {
            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            gprGraph.drawGroupingStructure(this.pictureBox1, trackBarGPR2a.Value, trackBarGPR2b.Value, trackBarGPR3.Value, trackBarGPR5.Value);
        }

        private void trackBarGPR2a_Scroll(object sender, EventArgs e)
        {
            changeGPRValiable();
            labelGPR2aParam.Text = "" + trackBarGPR2a.Value / 10.0;

        }

        private void trackBarGPR2b_Scroll(object sender, EventArgs e)
        {
            changeGPRValiable();
            labelGPR2bParam.Text = "" + trackBarGPR2b.Value / 10.0;
        }

        private void trackBarGPR3_Scroll(object sender, EventArgs e)
        {
            changeGPRValiable();
            labelGPR3Param.Text = "" + trackBarGPR3.Value / 10.0;
        }

        private void trackBarGPR5_Scroll(object sender, EventArgs e)
        {
            changeGPRValiable();
            labelGPR5Param.Text = "" + trackBarGPR5.Value / 10.0;
        }

        private void hierarchalButton_Click(object sender, EventArgs e)
        {
            int maxIndex = gprGraph.getMaxIndex();
            //gprGraph.functionGPR5(maxIndex);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void playButton_Click(object sender, EventArgs e)
        {

        }

        //WAVEファイルを再生する
        private void PlaySound(string waveFile)
        {
            //再生されているときは止める
            if (player != null)
                StopSound();

            //読み込む
            player = new System.Media.SoundPlayer(waveFile);

            player.PlayLooping();

        }

        //再生されている音を止める
        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }

        private void buttonGLCM_Click(object sender, EventArgs e)
        {
            string[] beatFileNames = System.IO.Directory.GetFiles(@"C:\Users\sawada\Desktop\AIST.RWC-MDB-P-2001.BEAT/", "*", System.IO.SearchOption.AllDirectories);

            string[] spectrogramNames1 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 1/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames2 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 2/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames3 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 3/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames4 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 4/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames5 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 5/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames6 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 6/", "*", System.IO.SearchOption.AllDirectories);
            string[] spectrogramNames7 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/実験データ/spectrogram/Disc 7/", "*", System.IO.SearchOption.AllDirectories);

            //wavFile = importFile.ReadAudioWav("C:/Users/sawada/Music/RWC研究用音楽データベース Disc 1/01 永遠のレプリカ.wav");

            exportGLCM(spectrogramNames1, beatFileNames, "Disc1", 0);
            exportGLCM(spectrogramNames2, beatFileNames, "Disc2", spectrogramNames1.Length);
            exportGLCM(spectrogramNames3, beatFileNames, "Disc3", spectrogramNames1.Length + spectrogramNames2.Length);
            exportGLCM(spectrogramNames4, beatFileNames, "Disc4", spectrogramNames1.Length + spectrogramNames2.Length + spectrogramNames3.Length);
            exportGLCM(spectrogramNames5, beatFileNames, "Disc5", spectrogramNames1.Length + spectrogramNames2.Length + spectrogramNames3.Length + spectrogramNames4.Length);
            exportGLCM(spectrogramNames6, beatFileNames, "Disc6", spectrogramNames1.Length + spectrogramNames2.Length + spectrogramNames3.Length + spectrogramNames4.Length + spectrogramNames5.Length);
            exportGLCM(spectrogramNames7, beatFileNames, "Disc7", spectrogramNames1.Length + spectrogramNames2.Length + spectrogramNames3.Length + spectrogramNames4.Length + spectrogramNames5.Length + spectrogramNames6.Length);

        }

        private void exportGLCM(string[] spectrogramName, string[] beatFileNames, string disc, int count)
        {
            for (int j = 0; j < spectrogramName.Length; j++)
            {
                var importFile = new ImportFile();

                // 画像読み込み
                var image = new ImageProcessing();
                byte[,] data = image.LoadByteImage(spectrogramName[j], this.progressBar);

                var metric = new MetricalStructure();
                metric = importFile.ReadMetric(beatFileNames[count + j]);

                var featureMatrixGLCM = new double[(metric.RightTime.Length / 5) - 1][];

                for (int i = 0; i < featureMatrixGLCM.Length; i++)
                {
                    int[,] degree0GLCM = image.CalcGLCM(data, 1, Direction.Degree0, metric.RightTime[i], metric.RightTime[i + 1]);
                    int[,] degree45GLCM = image.CalcGLCM(data, 1, Direction.Degree45, metric.RightTime[i], metric.RightTime[i + 1]);
                    int[,] degree90GLCM = image.CalcGLCM(data, 1, Direction.Degree90, metric.RightTime[i], metric.RightTime[i + 1]);
                    int[,] degree135GLCM = image.CalcGLCM(data, 1, Direction.Degree135, metric.RightTime[i], metric.RightTime[i + 1]);

                    double[,] probabilityGLCM = image.NormalizeGLCM(degree0GLCM, degree45GLCM, degree90GLCM, degree135GLCM);

                    var featureVectorGLCM = new double[] {
             image.AngularSecondMoment(probabilityGLCM),
            image.Contrast(probabilityGLCM),
            image.Mean(probabilityGLCM),
            image.StandardDeviation(probabilityGLCM),
            image.Entropy(probabilityGLCM),
            image.InverseDifferentMoment(probabilityGLCM)
                    };

                    featureMatrixGLCM[i] = featureVectorGLCM;

                    //graph.DrawGLCM(this.pictureBox1, degree0GLCM);
                }

                var exportFile = new ExportFile();
                // string fileName = exportFile.SaveFileDialog(ExportFile.Formats.Text);
                exportFile.WriteGLCMText(featureMatrixGLCM, "C:/Users/sawada/Desktop/GLCM2/" + disc + "/" + j + ".txt");

                // 画像保存
                //image.SaveByteImage(filterdata, "C:/Users/sawada/Desktop/out.bmp");
            }

        }
        private void drawSpectrogramAllButton_Click(object sender, EventArgs e)
        {

            string[] beatFileNames = System.IO.Directory.GetFiles(@"C:\Users\sawada\Desktop\AIST.RWC-MDB-P-2001.BEAT/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames1 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 1/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames2 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 2/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames3 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 3/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames4 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 4/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames5 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 5/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames6 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 6/", "*", System.IO.SearchOption.AllDirectories);
            string[] wavFileNames7 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Music/RWC研究用音楽データベース Disc 7/", "*", System.IO.SearchOption.AllDirectories);

            //storeSpectrogram(wavFileNames1, "disc 1/");
            //storeSpectrogram(wavFileNames2, "disc 2/");
            //storeSpectrogram(wavFileNames3, "disc 3/");
            //storeSpectrogram(wavFileNames4, "disc 4/");
            //storeSpectrogram(wavFileNames5, "disc 5/");
            //storeSpectrogram(wavFileNames6, "disc 6/");
            storeSpectrogram(wavFileNames7, "disc 7/");

        }

        private void storeSpectrogram(string[] fileNames, string disc)
        {

            for (int i = 0; i < fileNames.Length - 1; i++)
            {
                var importFile = new ImportFile();
                var graph = new Graph();
                var freqAnalyzer = new FrequencyAnalyzer();
                var wav = new WavFile();

                wav = importFile.ReadAudioWav(fileNames[i]);
                //metricalStruct = importFile.ReadMetric(beatFileNames[i]);

                freqAnalyzer.STFT(wav.RightData, this.progressBar);

                //スクロールバーが表示されるようにする
                this.panel1.AutoScroll = true;

                //PictureBoxの大きさが変更させるようにする
                pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length / 4);

                graph.ExportSpectrogram(this.pictureBox1, freqAnalyzer, this.progressBar, disc + (i + 1));

            }
        }

        private void clusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var import = new ImportFile();
            chorusStruct = import.ReadTruthCluster("C:/Users/sawada/Desktop/AIST.RWC-MDB-P-2001.CHORUS/RM-P001.CHORUS.txt");
            chorusStruct.chorusToBeat(metricalStruct);
        }

        private void truthChorusbutton_Click(object sender, EventArgs e)
        {

            string[] beatFileNames = System.IO.Directory.GetFiles(@"C:\Users\sawada\Desktop\AIST.RWC-MDB-P-2001.BEAT/", "*", System.IO.SearchOption.AllDirectories);
            string[] clusterFileNames = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/AIST.RWC-MDB-P-2001.CHORUS/", "*", System.IO.SearchOption.AllDirectories);

            for (int i = 0; i < beatFileNames.Length; i++)
            {
                var import = new ImportFile();
                var metric = new MetricalStructure();
                metric = import.ReadMetric(beatFileNames[i]);
                chorusStruct = import.ReadTruthCluster(clusterFileNames[i]);
                var export = new ExportFile();
                export.WriteChorusTruthText(chorusStruct.chorusToBeat(metric), "C:/Users/sawada/Desktop/chorusTruth2/" + i + ".txt");
            }

        }

        private void fMeasureButton_Click(object sender, EventArgs e)
        {
            var import = new ImportFile();

            string[] systemOutputNames3 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/3/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames4 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/4/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames5 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/5/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames6 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/6/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames7 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/7/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames8 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/8/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames9 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/9/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames10 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/output/10/", "*", System.IO.SearchOption.AllDirectories);
            /*string[] systemOutputNames11 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/11/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames12 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/12/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames13 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/13/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames14 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/14/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames15 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/15/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames16 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/16/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames17 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/17/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames18 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/18/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames19 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/19/", "*", System.IO.SearchOption.AllDirectories);
            string[] systemOutputNames20 = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/systemOutput/20/", "*", System.IO.SearchOption.AllDirectories);
            */
            string[] correctNames = System.IO.Directory.GetFiles(@"C:/Users/sawada/Desktop/chorusTruth2/", "*", System.IO.SearchOption.AllDirectories);
            var correctRateArray = new double[systemOutputNames3.Length];

            for (int j = 0; j < systemOutputNames3.Length; j++)
            {
                var systemOutput3 = import.ReadGroupingStructure(systemOutputNames3[j], "system");
                var systemOutput4 = import.ReadGroupingStructure(systemOutputNames4[j], "system");
                var systemOutput5 = import.ReadGroupingStructure(systemOutputNames5[j], "system");
                var systemOutput6 = import.ReadGroupingStructure(systemOutputNames6[j], "system");
                var systemOutput7 = import.ReadGroupingStructure(systemOutputNames7[j], "system");
                var systemOutput8 = import.ReadGroupingStructure(systemOutputNames8[j], "system");
                var systemOutput9 = import.ReadGroupingStructure(systemOutputNames9[j], "system");
                var systemOutput10 = import.ReadGroupingStructure(systemOutputNames10[j], "system");
                /*
                var systemOutput11 = import.ReadGroupingStructure(systemOutputNames11[j], "system");
                var systemOutput12 = import.ReadGroupingStructure(systemOutputNames12[j], "system");
                var systemOutput13 = import.ReadGroupingStructure(systemOutputNames13[j], "system");
                var systemOutput14 = import.ReadGroupingStructure(systemOutputNames14[j], "system");
                var systemOutput15 = import.ReadGroupingStructure(systemOutputNames15[j], "system");
                var systemOutput16 = import.ReadGroupingStructure(systemOutputNames16[j], "system");
                var systemOutput17 = import.ReadGroupingStructure(systemOutputNames17[j], "system");
                var systemOutput18 = import.ReadGroupingStructure(systemOutputNames18[j], "system");
                var systemOutput19 = import.ReadGroupingStructure(systemOutputNames19[j], "system");
                var systemOutput20 = import.ReadGroupingStructure(systemOutputNames20[j], "system");
                */
                var correct = import.ReadGroupingStructure(correctNames[j], "correct");

                correctRateArray[j] =
                    Math.Max(
                        Math.Max(
                            Math.Max(calcCorrectRate(systemOutput3, correct), calcCorrectRate(systemOutput4, correct)),
                            Math.Max(calcCorrectRate(systemOutput5, correct), calcCorrectRate(systemOutput6, correct))
                            ),
                        Math.Max(
                            Math.Max(calcCorrectRate(systemOutput7, correct), calcCorrectRate(systemOutput8, correct)),
                            Math.Max(calcCorrectRate(systemOutput9, correct), calcCorrectRate(systemOutput10, correct))
                            )
                        );

                //                correctRateArray[j] = Math.Max(Math.Max(Math.Max(Math.Max(calcCorrectRate(systemOutput3, correct), calcCorrectRate(systemOutput4, correct)), Math.Max(calcCorrectRate(systemOutput5, correct), calcCorrectRate(systemOutput8, correct))), Math.Max(Math.Max(calcCorrectRate(systemOutput9, correct), calcCorrectRate(systemOutput10, correct)), Math.Max(calcCorrectRate(systemOutput11, correct), calcCorrectRate(systemOutput12, correct)))), Math.Max(Math.Max(Math.Max(calcCorrectRate(systemOutput13, correct), calcCorrectRate(systemOutput14, correct)), Math.Max(calcCorrectRate(systemOutput15, correct), calcCorrectRate(systemOutput16, correct))), Math.Max(Math.Max(calcCorrectRate(systemOutput17, correct), calcCorrectRate(systemOutput18, correct)), Math.Max(calcCorrectRate(systemOutput19, correct), calcCorrectRate(systemOutput20, correct)))));

            }

            double average = 0;
            for (int i = 0; i < correctRateArray.Length; i++)
            {
                average += correctRateArray[i];
                Console.WriteLine(systemOutputNames3[i] + correctRateArray[i]);
            }
            Console.WriteLine("average : " + average / correctRateArray.Length);
            /*
            foreach (var txt in systemOutput)
                {
                    Console.WriteLine("system: " + txt);
                }

            foreach (var txt in correct)
            {
                Console.WriteLine("correct: " + txt);
            }
             */
        }

        private double calcCorrectRate(string[] systemOutput, string[] correct)
        {
            double correctNumber = 0;

            for (int i = 0; i < systemOutput.Length - 1; i++)
            {
                if (systemOutput[i] == correct[i]) correctNumber++;
            }
            return correctNumber / systemOutput.Length;
        }
    }
}
