using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class BeatDetection
    {

        public double[] main(FrequencyAnalyzer freqAnalyzer)
        {
            //立ち上がり成分抽出
            double[] sumD = ExtractRisingComponent(freqAnalyzer.stftData, 0,1000, 44100);

            //ピーク検出
            double[] peekTime = PeakDetection(sumD);
            
            //自己相関
            double[] R = CalcAutocorrelation(peekTime, peekTime);

            //ピーク検出
            peekTime = PeakDetection(R);
            //foreach(var item in peekTime) Console.WriteLine(item);

            //ビート間隔
            int beatInterval = CalcBeatInterval(peekTime);
            Console.WriteLine(beatInterval);

            //ズレの検出
            int startTime = CalcStartTime(peekTime);
            Console.WriteLine(startTime);

            //ビート系列の作成
            double[] beat = makeBeat(beatInterval, peekTime.Length, startTime);
            Console.WriteLine(peekTime.Length);

            return beat;
        }

        public int CalcStartTime(double[] data)
        {
            int startTime = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0)
                {
                    startTime = i;
                    break;
                }
            }
                return startTime;
        }

        /**
         * AutocorrelationFunction
         * 概要：自己相関を求める
         * @param data 自己相関を求める対象データ
         * @param dataLen 対象データの大きさ
         * @return R[] 自己相関配列
         */
        public double[] CalcAutocorrelation(double[] data1, double[] data2)
        {
            double[] R = new double[data1.Length];

            for (int j = 0; j < data1.Length; j++)
            {
                for (int i = 0; i < data1.Length - j; i++) R[j] += data1[i] * data2[i + j];
                R[j] = R[j] / (data1.Length - j);
            }
            return R;
        }

        /**
         * RisingComponentAnalysis
         * 概要：立ち上がり成分を抽出する
         * @param p 立ち上がり成分を抽出する対象であるFFT後の周波数成分
         * @param numberOfWindow 窓の数
         * @param freqBand
         * @return p[t,f] パワーが増加し続けている周波数成分
         */
        public double[] ExtractRisingComponent(double[][] p, int freqFrom, int freqTo, int samplingFreq)
        {

            int freqBand = freqTo - freqFrom;

            //周波数帯域の始点(Hz -> 周波数番号)
            int freqFromNumber = freqFrom / (samplingFreq / p[0].Length);

            //周波数分解能(Hz -> 周波数番号)
            int freqResolution = freqBand / (samplingFreq / p[0].Length);

            //立ち上がりの度合い
            var df = new double[freqResolution];

            //各時刻における立ち上がり成分の合計(指定された周波数帯域)
            double[] sumD;
            sumD = new double[p.Length];

            for (int t = 2; t < p.Length-1; t++)
            {
                for (int f = 1; f < freqResolution; f++)
                {
                    double ppTemp1 = Math.Max(p[t - 1][f + freqFromNumber], p[t - 1][f - 1 + freqFromNumber]);
                    double pp = Math.Max(ppTemp1, p[t - 1][f + 1 + freqFromNumber]);

                    double npTemp1 = Math.Min(p[t + 1][f - 1 + freqFromNumber], p[t + 1][f + 1 + freqFromNumber]);
                    double np = Math.Min(p[t + 1][f + freqFromNumber], npTemp1);

                    if (Math.Min(p[t][f + freqFromNumber], p[t + 1][f + freqFromNumber]) > pp) df[f] = Math.Max(p[t + 1][f + freqFromNumber], p[t][f + freqFromNumber]) - pp;
                    else df[f] = 0;

                    sumD[t] += df[f];
                }
            }
            return sumD;
        }

        public double[] PeakDetection(double[] data)
        {
            var peekTime = SavitzkyGolayFilter(data, 25);
            
            double maxPeek = 0.0;

            for (int i = 0; i < peekTime.Length-1; i++)
            {
                if (peekTime[i] > 0 && peekTime[i + 1] < 0)
                {
                    if (maxPeek < peekTime[i]) maxPeek = peekTime[i];
                    peekTime[i] = peekTime[i] / maxPeek;
                }
                else peekTime[i] = 0.0;
            }
            return peekTime;
        }

        /**
       * SavitzkyGolayFilter
       * 概要：2次多項式適合による平滑化微分
       * @param data            平滑化する対象データ
       * @param smoothingNumber 平常化の時間幅
       * @param dataLen         対象データの長さ
       * @return newData        平滑化後のデータ
       */
        public double[] SavitzkyGolayFilter(double[] data, int smoothingNumber)
        {
            double[] newData;
            newData = new double[data.Length];

            int Nomalization = 0;
            int[] W5 = { -3, 12, 17, 12, -3 };
            int[] W7 = { -2, 3, 6, 7, 6, 3, -2 };
            int[] W9 = { -21, 14, 39, 54, 59, 54, 39, 14, -21 };
            int[] W11 = { -36, 9, 44, 69, 84, 89, 84, 69, 44, 9, -36 };
            int[] W13 = { -11, 0, 9, 16, 21, 24, 25, 24, 21, 16, 9, 0, -11 };
            int[] W15 = { -78, -13, 42, 87, 122, 147, 162, 167, 162, 147, 122, 87, 42, -13, -78 };
            int[] W17 = { -105, -30, 35, 90, 135, 170, 195, 210, 215, 210, 195, 170, 135, 90, 35, -30, -105};
            int[] W25 = { -253, -138, -33, 62, 147, 222, 287, 342, 387, 422, 447, 462, 467, 462, 447, 422, 387, 342, 287, 222, 147, 62, -33, -138, -253 };

            switch (smoothingNumber)
            {
                case 5:
                    Nomalization = 35;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W5, Nomalization);
                    break;

                case 7:
                    //Nomalization = 21;
                    Nomalization = 105;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W7, Nomalization);
                    break;

                case 9:
                    Nomalization = 231;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W9, Nomalization);
                    break;

                case 11:
                    Nomalization = 429;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W11, Nomalization);
                    break;

                case 13:
                    //Nomalization = 143;
                    Nomalization = 715;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W13, Nomalization);
                    break;

                case 15:
                    Nomalization = 1105;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W15, Nomalization);
                    break;

                case 17:
                    Nomalization = 1615;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W17, Nomalization);
                    break;

                case 25:
                    Nomalization = 5175;
                    newData = SavitzkyGolayCalc(data, smoothingNumber, W25, Nomalization);
                    break;
            }
            return newData;
        }

        /**
         * SavitzkyGolayCalc
         * 概要：S-GFilterの計算部分
         * @param data            平滑化する対象データ
         * @param n               平常化の時間幅
         * @param dataLen         対象データの長さ
         * @param W               重みとなるS-G係数
         * @param N               正規化定数
         * @return newData        平滑化後のデータ
         */

        public double[] SavitzkyGolayCalc(double[] data, int n, int[] W, int N)
        {
            double[] newData;
            newData = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = -(n - 1) / 2; j < (n - 1) / 20; j++)
                {
                    if (i + j < 0) newData[i] = data[i];
                    else newData[i] = newData[i] + W[(n - 1) / 2 + j] * data[i + j];
                }
                newData[i] = newData[i] / N;
            }
            return newData;
        }

        /**
         * BeetTimeDetection
         * 概要： ビート時刻の検出
         * @param data            ビート時刻を求める対象データ
         * @return beetTime       ビート時刻系列
         */

        public int CalcBeatInterval(double[] data)
        {
            var beetTime = new double[data.Length];
            int oldBeet = 0;
            int beatInterval = 0;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
        
            ArrayList beatIntervalArray = new ArrayList();

            //ビート間隔を格納
            for (int i = 0; i < data.Length; i++) 
            {
                if (data[i] > 0.0)
                {
                    beatInterval = i - oldBeet;
                        oldBeet = i;
                        beatIntervalArray.Add(beatInterval);
                }
            }
             
            //最頻値を求める
            int mode = 0;
            int count = 0;
            int tempCount;

            for (int i = 0; i < beatIntervalArray.Count; i++)
            {
                tempCount = 1;
                for (int j = i + 1; j < beatIntervalArray.Count; j++)
                {
                    if ((int)beatIntervalArray[i] == (int)beatIntervalArray[j]) tempCount++;
                }

                if (tempCount > count)
                {
                    count = tempCount;
                    mode = (int)beatIntervalArray[i];
                }
            }
            beatInterval = mode;

            //妥当な時間内になるようにマージ
            while (beatInterval < 32) beatInterval += mode;
            
          return beatInterval;
        }


        public double[] makeBeat(int beatInterval, int beatLength, int startTime)
        {
            var beatTime = new double[beatLength]; 
            for (int i = 0; i < beatTime.Length; i++) beatTime[i] = 0.0;
            for (int i = startTime; i < beatTime.Length; i += beatInterval) beatTime[i] = 1.0;

            return beatTime;
        }
    }
}
