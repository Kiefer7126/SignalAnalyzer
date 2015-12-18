using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SignalAnalyzer
{
    public class ImportFile
    {
        public enum Formats {Text, Wav}

        /**
         * OpenFileDialog
         * 概要：ファイル読み込みダイアログを表示する
         * @param flag FORMAT_TEXT -> テキストファイル
         *             FORMAT_WAV  -> wavファイル
         * @return String 読み込むファイル名
         *                キャンセルされた場合は空文字
         */

        private string OpenFileDialog(Formats format)
        {
            string loadFileName = "";
            OpenFileDialog dialog = new OpenFileDialog();

            //この記述をしないとOSがハングアップする
            dialog.ShowHelp = true;

            if (format == Formats.Text)      dialog.Filter = "テキストファイル(*.txt)|*.txt";
            else if (format == Formats.Wav)  dialog.Filter = "wavファイル(*.wav)|*.wav";

            if (dialog.ShowDialog() == DialogResult.OK) loadFileName = dialog.FileName;
            return loadFileName;
        }

        /**
         * ReadTextFile
         * 概要：テキストファイルを読み込む
         * @param Format format
         * @return string[] textArray
         */

        public string[] ReadText(Formats format)
        {
            string loadFileName = "";
            string readText = "";
            string[] textArray;

            loadFileName = OpenFileDialog(format);

            if (loadFileName == "" || loadFileName == null) return null;
            else
            {
                using (StreamReader sr = new StreamReader(loadFileName))
                {
                    readText = sr.ReadToEnd();
                    textArray = readText.Split(System.Environment.NewLine.ToCharArray());
                    
                }
                return textArray;
            }
        }

        /**
         * ReadAudioText
         * 概要：音楽データ.txtを読み込む
         * @param なし
         * @return int[] originalData
         */

        public int[] ReadAudioText()
        {
            string[] textArray;
            string[] textArrayWithoutSpaces;
            int[] originalData;
            
            textArray = ReadText(Formats.Text);
            textArrayWithoutSpaces = textArray.Where(c => c != "").ToArray();
            originalData = new int[textArrayWithoutSpaces.Length];
            
            for (int i = 0; i < textArrayWithoutSpaces.Length; i++) originalData[i] = Convert.ToInt32(textArrayWithoutSpaces[i]);
            return originalData;
        }

        /**
         * ReadMetricalStructure
         * 概要：拍節構造.txtを読み込む
         * @param なし
         * @return MetricalStructure metricalstruct
         */

        public MetricalStructure ReadMetric()
        {
            Console.WriteLine("Read Metrical Structure...");
            var metricalStruct = new MetricalStructure();
            int[] rightTime, leftTime, ticks;
            string[] splitText, splitElement;

            splitText = ReadText(Formats.Text);
            
            rightTime = new int[splitText.Length-1];
            leftTime  = new int[splitText.Length-1];
            ticks     = new int[splitText.Length-1];

            for (int i = 0; i < splitText.Length - 1 /* 末尾の削除 */; i++)
            {
                splitElement = splitText[i].Split("\t".ToCharArray());
                rightTime[i] = Convert.ToInt32(splitElement[0]);
                leftTime[i] = Convert.ToInt32(splitElement[1]);
                ticks[i] = Convert.ToInt32(splitElement[2]);
            }
            metricalStruct.RightTime = rightTime;
            metricalStruct.LeftTime = leftTime;
            metricalStruct.Ticks = ticks;

            Console.WriteLine("Done!");
            return metricalStruct;
        }

        /**
         * ReadAudioWav
         * 概要：音楽データ.wavを読み込む
         * @param  なし
         * @return WavFile wavFile
         */

        public WavFile ReadAudioWav()
        {
            string loadFileName = "";
            WavFile wavFile = new WavFile();

            loadFileName = OpenFileDialog(Formats.Wav);

            if (loadFileName == "" || loadFileName == null) return null;
            else
            {
                try
                {
                    using (FileStream fs = new FileStream(loadFileName, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            Console.WriteLine("Read wavFile...");
                            wavFile.riffID = br.ReadBytes(4); // "riff"
                            wavFile.fileSize = br.ReadUInt32();  // ファイルサイズ-8
                            wavFile.waveID = br.ReadBytes(4);

                            string headerChar;
                            for (int j = 0; j < br.BaseStream.Length; j++)
                            {
                                headerChar = System.Text.Encoding.ASCII.GetString(br.ReadBytes(1));
                                if (headerChar.ToLower() == "f")
                                {
                                    headerChar = System.Text.Encoding.ASCII.GetString(br.ReadBytes(1));
                                    if (headerChar.ToLower() == "m")
                                    {
                                        headerChar = System.Text.Encoding.ASCII.GetString(br.ReadBytes(1));
                                        if (headerChar.ToLower() == "t")
                                        {
                                            br.ReadBytes(1);
                                            break;
                                        }
                                    }
                                }
                            }
                            wavFile.fmtID = System.Text.Encoding.ASCII.GetBytes("fmt ");
                            
                            wavFile.fmtSize = br.ReadUInt32(); 
                            wavFile.format = br.ReadUInt16();
                            wavFile.channels = br.ReadUInt16();
                            wavFile.samplingRate = br.ReadUInt32();
                            wavFile.bytePerSec = br.ReadUInt32();
                            wavFile.blockSize = br.ReadUInt16();
                            wavFile.bitPerSampling = br.ReadUInt16();
                            
                            //拡張部分
                            if(wavFile.fmtSize > 16) for(int i = 0; i < (wavFile.fmtSize - 16)/2; i++) br.ReadUInt16();

                            wavFile.dataID = br.ReadBytes(4);
                            wavFile.dataSize = br.ReadUInt32();

                            uint dataArrayLength = wavFile.dataSize / wavFile.blockSize / 4; //4分の1サイズ

                            int[] rightDataArray = new int[dataArrayLength];
                            int[] leftDataArray = new int[dataArrayLength];

                            for (int i = 0; i < rightDataArray.Length; i++)
                            {
                                rightDataArray[i] = br.ReadInt16();
                                leftDataArray[i] = br.ReadInt16();
                            }
                            wavFile.RightData = rightDataArray;
                            wavFile.LeftData = leftDataArray;

                            //Debug
                            Console.WriteLine("wavFile.riffID = " + System.Text.Encoding.ASCII.GetString(wavFile.riffID));
                            Console.WriteLine("wavFile.fileSize = " + wavFile.fileSize);
                            Console.WriteLine("wavFile.waveID = " + System.Text.Encoding.ASCII.GetString(wavFile.waveID));
                            Console.WriteLine("wavFile.fmtID = " + System.Text.Encoding.ASCII.GetString(wavFile.fmtID));
                            Console.WriteLine("wavFile.fmtSize = " + wavFile.fmtSize);
                            Console.WriteLine("wavFile.format = " + wavFile.format);
                            Console.WriteLine("wavFile.channels = " + wavFile.channels);
                            Console.WriteLine("wavFile.samplingRate = " + wavFile.samplingRate);
                            Console.WriteLine("wavFile.bytePerSec = " + wavFile.bytePerSec);
                            Console.WriteLine("wavFile.blockSize = " + wavFile.blockSize);
                            Console.WriteLine("wavFile.bitPerSampling = " + wavFile.bitPerSampling);
                            Console.WriteLine("wavFile.dataID = " + System.Text.Encoding.ASCII.GetString(wavFile.dataID));
                            Console.WriteLine("wavFile.dataSize = " + wavFile.dataSize);
                            Console.WriteLine("Done read wavFile!");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing data: {0}.", e.GetType().Name);
                }
            }
            return wavFile;
        }
    }
}