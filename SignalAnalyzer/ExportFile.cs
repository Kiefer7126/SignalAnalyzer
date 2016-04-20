using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SignalAnalyzer
{
    public class ExportFile
    {
        public enum Formats { Text, Wav }

        //RIFFチャンク
        private byte[] riffID = { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };
        private uint fileSize; 
        private byte[] waveID = { (byte)'W', (byte)'A', (byte)'V', (byte)'E' };
        //fmtチャンク
        private byte[] fmtID = { (byte)'f', (byte)'m', (byte)'t', (byte)' ' };
        private uint fmtSize = 16;  //リニアPCM→16(10 00 00 00)
        private ushort format = 1;     //リニアPCM(01 00)
        private ushort channels = 2;        //ステレオ
        private uint samplingRate = 44100;
        private uint bytePerSec = 176400;
        private ushort blockSize = 4; //ステレオ
        private ushort bitPerSampling = 16;
        //dataチャンク
        private byte[] dataID = { (byte)'d', (byte)'a', (byte)'t', (byte)'a' };
        private uint dataSize;


        /*
        * SaveFileDialog
        * 概要：ファイル保存ダイアログを表示する
        * @param flag DataRetention.TEXTDATA -> テキストファイル
        * 　　　　　  DataRetention.WAVDATA -> wavファイル
        * @return string 保存ファイル名
        *         ダイアログがキャンセルされた場合は空文字を返す
        */

        public string SaveFileDialog(Formats format)
        {
            string writeFileName = "";
            SaveFileDialog dialog = new SaveFileDialog();

            if (format == Formats.Text) dialog.Filter = "テキストファイル(*.txt)|*.txt";
            else if (format == Formats.Wav)  dialog.Filter = "wavファイル(*.wav)|*.wav";

            if (dialog.ShowDialog() == DialogResult.OK) writeFileName = dialog.FileName;

            return writeFileName;
        }
        
        /*
         * WriteAudioText
         * 概要：テキスト形式で書き込む
         * @param data 対象データ格納用
         * @param flag DataRetention.TIMEGRAPH -> 時間軸グラフのデータを書き込む
         * 　　　　　  DataRetention.FREQGRAPH -> 周波数軸グラフのデータを書き込む
         * @return なし
         */

        public Boolean WriteAudioText(WavFile wavFile)
        {
            string writeFileName = "";

            writeFileName = SaveFileDialog(Formats.Text);

            if (writeFileName == "" || writeFileName == null) return false;
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(writeFileName))
                    {
                        for (int i = 0; i < wavFile.RightData.Length; i++)
                        {
                            sw.WriteLine(wavFile.RightData[i]);
                            sw.WriteLine(wavFile.LeftData[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing data: {0}.", e.GetType().Name);
                }
                return true;
            }
        }

        /**
         * WriteClick
         * 概要：wavファイルに書き込む
         * @param samplingFreq サンプリング周波数
         * @param data wavデータ格納用
         * @return なし
         */

        public void WriteClick(double[] data, string fileName)
        {
            //書き込むファイル名
            string writeFileName = fileName;

            if (writeFileName == "" || writeFileName == null) { }//ファイル名が取得されていなければ、処理を続行しない
            else
            {
                try
                {
                    using (FileStream fs = new FileStream(writeFileName, FileMode.Create, FileAccess.Write))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            fileSize = (uint)(44 + (data.Length * 2) - 8);
                            dataSize = (uint)(data.Length * 2 * 2);

                            //RIFFチャンク
                            bw.Write(riffID);
                            bw.Write(fileSize);
                            bw.Write(waveID);
                            //fmtチャンク
                            bw.Write(fmtID);
                            bw.Write(fmtSize);
                            bw.Write(format);
                            bw.Write(channels);
                            bw.Write(samplingRate);
                            bw.Write(bytePerSec);
                            bw.Write(blockSize);
                            bw.Write(bitPerSampling);
                            //dataチャンク
                            bw.Write(dataID);
                            bw.Write(dataSize);

                            for (int i = 0; i < data.Length; i++)
                            {
                                bw.Write(Convert.ToInt16(data[i]));//R
                                bw.Write(Convert.ToInt16(data[i]));//L
                            }
                            bw.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    //例外処理
                }
            }
        }

        /*
         * WriteMetricalText
         * 概要：テキスト形式で書き込む
         * @param data 対象データ格納用
         * @param flag DataRetention.TIMEGRAPH -> 時間軸グラフのデータを書き込む
         * 　　　　　  DataRetention.FREQGRAPH -> 周波数軸グラフのデータを書き込む
         * @return なし
         */

        public Boolean WriteMetricalText(double[] beat, string fileName)
        {
            string writeFileName = fileName;

            if (writeFileName == "" || writeFileName == null) return false;
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(writeFileName))
                    {
                        for (int i = 0; i < beat.Length; i++)
                        {
                            if (beat[i] != 0)
                            {
                                sw.Write(i);
                                sw.Write("\t");
                                sw.Write(i);
                                sw.Write("\t");
                                sw.Write(384);
                                sw.Write("\n");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing data: {0}.", e.GetType().Name);
                }
                return true;
            }
        }

        public Boolean WriteGLCMText(double[][] data, string fileName)
        {
            string writeFileName = fileName;

            if (writeFileName == "" || writeFileName == null) return false;
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(writeFileName))
                    {
                        sw.Write("No.");
                        sw.Write("\t");
                        sw.Write("ASM");
                        sw.Write("\t");
                        sw.Write("Contrast");
                        sw.Write("\t");
                        sw.Write("Mean");
                        sw.Write("\t");
                        sw.Write("SD");
                        sw.Write("\t");
                        sw.Write("Entropy");
                        sw.Write("\t");
                        sw.Write("IDM");
                        sw.Write("\t");
                        sw.Write("\n");
                        for (int i = 1; i < data.Length; i++)
                        {
                            sw.Write(i*10);
                            sw.Write("\t");

                            for (int j = 0; j < data[i].Length; j++)
                            {
                                if (data[i][j] != 0)
                                {
                                    sw.Write(data[i][j]);
                                    sw.Write("\t");
                                }
                            }
                            sw.Write("\n");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing data: {0}.", e.GetType().Name);
                }
                return true;
            }
        }

    }
}
