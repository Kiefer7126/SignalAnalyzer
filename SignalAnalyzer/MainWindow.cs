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
        Graph gprGraph;

        private System.Media.SoundPlayer player = null;

        public MainWindow()
        {
            InitializeComponent();
            wavFile = new WavFile();
            metricalStruct = new MetricalStructure();

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
            var image = new ImageProcessing();

            // 画像読み込み
            byte[,] data = image.LoadByteImage("C:/Users/sawada/Desktop/test.bmp", this.progressBar);

            // フィルタ処理
            //byte[,] filterdata = image.ReverseColor(data);
            
            /* test data
            byte[,] data = {{ 0, 1, 2, 3, 4, 5 },
                           { 1, 2, 3, 4, 5, 4 },
                           { 2, 4, 5, 6, 5, 4 },
                           { 1, 3, 4, 2, 3, 4 },
                           { 2, 3, 4, 5, 6, 2 }};
            */

            int[,] degree0GLCM = image.CalcGLCM(data, 1, Direction.Degree0);
            int[,] degree45GLCM = image.CalcGLCM(data, 1, Direction.Degree45);
            int[,] degree90GLCM = image.CalcGLCM(data, 1, Direction.Degree90);
            int[,] degree135GLCM = image.CalcGLCM(data, 1, Direction.Degree135);

            double[,] probabilityGLCM = image.NormalizeGLCM(degree0GLCM, degree45GLCM, degree90GLCM, degree135GLCM);


            /*
            for (int i = 0; i < probabilityGLCM.GetLength(0); i++)
            {
                for (int j = 0; j < probabilityGLCM.GetLength(1); j++)
                {
                    Console.Write(probabilityGLCM[j, i] + ",");
                }
                Console.Write("\n");
           }
           
            */

            image.AngularSecondMoment(probabilityGLCM);
            image.Contrast(probabilityGLCM);
            image.Mean(probabilityGLCM);
            image.StandardDeviation(probabilityGLCM);
            image.Entropy(probabilityGLCM);
            image.InverseDifferentMoment(probabilityGLCM);

            var graph = new Graph();
            graph.DrawGLCM(this.pictureBox1, degree0GLCM);

            // 画像保存
            //image.SaveByteImage(filterdata, "C:/Users/sawada/Desktop/out.bmp");

        }
    }
}
