using System;
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
    public partial class MainWindow : Form
    {
        WavFile wavFile;
        MetricalStructure metricalStruct;

        public MainWindow()
        {
            InitializeComponent();
            wavFile = new WavFile();
            metricalStruct = new MetricalStructure();
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

        private void analyzeSTFTMenu_Click(object sender, EventArgs e)
        {
            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);
            var graph = new Graph();
            graph.draw(this.chartcontrol, wavFile.RightData);

            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData);

            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            //PictureBoxの大きさが変更させるようにする
            pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length/4);

            graph.DrawSpectrogram(this.pictureBox1, freqAnalyzer, null, null, 0);
        }

        private void exMetricalWavMenu_Click(object sender, EventArgs e)
        {
            var generateWave = new GenerateWave();
            var beatStructure = new double[wavFile.RightData.Length];
            beatStructure = generateWave.beat(wavFile, metricalStruct);
            var export = new ExportFile();
            string fileName = export.SaveFileDialog(ExportFile.Formats.Wav);
            export.WriteClick(beatStructure, fileName);
        }

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

        private void beatDetectionButton_Click(object sender, EventArgs e)
        {
            var importFile = new ImportFile();
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);

            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData);

            var beatDetection = new BeatDetection();
            var beat = new double[freqAnalyzer.stftData.Length];
            beat = beatDetection.main(freqAnalyzer);

            var graph = new Graph();
            graph.drawBeat(this.chartcontrol, beat);

            var exportFile = new ExportFile();
            string exportFileName = exportFile.SaveFileDialog(ExportFile.Formats.Text);
            exportFile.WriteMetricalText(beat, exportFileName);
        }

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

                freqAnalyzer.STFT(wavFile.RightData);

                var beat = new double[freqAnalyzer.stftData.Length];
                beat = beatDetection.main(freqAnalyzer);

                string noExtFileName = fileName.Replace(".wav", "");
                noExtFileName = fileName.Replace("Pops", "20160111");
                exportFile.WriteMetricalText(beat, noExtFileName + "_beat.txt");
                
                var beatStructure = new double[wavFile.RightData.Length];
                metricalStruct = importFile.ReadMetric(noExtFileName + "_beat.txt");
                beatStructure = generateWave.beat(wavFile, metricalStruct);

                exportFile.WriteClick(beatStructure, noExtFileName + "_beat.wav");

                Console.WriteLine("100個中"+ count + "個のデータを処理");
                count++;
            }
        }

        private void buttonGPR_Click(object sender, EventArgs e)
        {
            /* read metric */
            var importFile = new ImportFile();
            //ファイルを選ぶときに使用
            /*
            string beatFileName = importFile.OpenFileDialog(ImportFile.Formats.Text);
            metricalStruct = importFile.ReadMetric(beatFileName);
            
            string fileName = importFile.OpenFileDialog(ImportFile.Formats.Wav);
            wavFile = importFile.ReadAudioWav(fileName);
            */

            metricalStruct = importFile.ReadMetric("C:/Users/sawada/Desktop/AIST.RWC-MDB-C-2001.BEAT/RM-C002.BEAT.txt");
            wavFile = importFile.ReadAudioWav("C:/Users/sawada/Desktop/02 Symphony no. 40 in G minor, K. 550. 1st mvmt-01.wav");
            var freqAnalyzer = new FrequencyAnalyzer();
            freqAnalyzer.STFT(wavFile.RightData);
         

            //スクロールバーが表示されるようにする
            this.panel1.AutoScroll = true;

            //PictureBoxの大きさが変更させるようにする
            pictureBox1.Size = new Size(freqAnalyzer.stftData.Length, freqAnalyzer.stftData[0].Length / 4);

            //スペクトログラムの描画
            var graph = new Graph();

            graph.DrawSpectrogram(this.pictureBox1, freqAnalyzer, metricalStruct.aistToBeat(wavFile), freqAnalyzer.sumSpectoroInterval(metricalStruct), metricalStruct.BeatInterval);
        }
    }
}
