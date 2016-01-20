using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class FrequencyAnalyzer
    {
        private double[] reData, imData;
        public int shiftLength = 441;
        public int windowLength = 2048;
        public double[][] stftData;
        public static double[] twiddleFactor;

        public void STFT(int[] data)
        {
            stftData = new double[data.Length / shiftLength][];
            var splitData = new int[data.Length/shiftLength][];

            Console.WriteLine("Split Data...");
            for (int i = 0; i < splitData.Length; i++) splitData[i] = SplitWindowLength(data,i);
            Console.WriteLine("Done split!");

            Console.WriteLine("Data times Gaussian is...");
            var window = new WindowFunction();
            var gaussianData = new double[splitData.Length][];
            for (int i = 0; i < gaussianData.Length; i++) gaussianData[i] = window.Hanning(splitData[i]);
            Console.WriteLine("Done!");

            Console.WriteLine("STFT Data...");
            for (int i = 0; i < stftData.Length; i++)
            {
                stftData[i] = FFT(gaussianData[i]);
                if(i%100 == 0) Console.Write(string.Format("{0, 3:d0}% \r", 100 * i / stftData.Length));
            }
            Console.WriteLine("Done FFT!");
        }

        private int[] SplitWindowLength(int[] data, int i)
        {
            var windowLengthData = new int[windowLength];
            
            //Skip Takeは遅い
            //for (int i = 0; i < splitData.Length; i++) splitData[i] = data.Skip(shiftLength * i).Take(windowLength).ToArray(); 

            //Array.Copyのほうが早い
            
            int start = i * shiftLength;
            int endLength = data.Length - start;

            if (i * shiftLength + windowLength > data.Length) Array.Copy(data, start, windowLengthData, 0, endLength);
            else Array.Copy(data, start, windowLengthData, 0, windowLength);

            return windowLengthData;
        }

        //beatInterval分スペクトルを時間軸方向に足し合わせる
        public double[][] sumSpectoroInterval(MetricalStructure metric)
        {
            
            var newStftData = new double[this.stftData.Length / metric.BeatInterval - 1][];
            //var sumSpector = new double[windowLength];

            for (int i = 0; i < newStftData.Length; i++)
            {
                double[] sumSpector = Enumerable.Repeat(0.0, windowLength).ToArray(); //すべて0で初期化

                for(int j = 0; j < windowLength; j++)
                {
                    for (int k = metric.RightTime[i]; k < metric.RightTime[i + 1]; k++)
                    {
                        if(k < stftData.Length) sumSpector[j] += stftData[k][j];
                    }
                }
                newStftData[i] = sumSpector;
            }
            return newStftData;
        }

        //スペクトルを周波数方向に足し合わせる
        public double[] sumSpectoroPower(double[][] oldStftdata)
        {
            var newStftData = new double[oldStftdata.Length];

            for (int i = 0; i < newStftData.Length; i++)
            {
                for (int j = 0; j < windowLength; j++)
                {
                    newStftData[i] += Math.Abs(oldStftdata[i][j]);
                }
            }
            return newStftData;
        }

        //隣接するbinの変化度を求める
        public int[] changeRatio(double[][] intervalStftData)
        {
            var changeRatioData = new int[intervalStftData.Length-1];
            
            for (int i = 0; i<changeRatioData.Length; i++)
            {
                for (int j = 0; j < windowLength; j++)
                {
                    if (intervalStftData[i+1] != null) changeRatioData[i] += Math.Abs((int)intervalStftData[i + 1][j] - (int)intervalStftData[i][j]);
                }
            }

            return changeRatioData;

        }

        private double[] FFT(double[] data)
        {
            //時間軸グラフに描画したデータからの実数と虚数部格納用
            this.reData = new double[windowLength];
            this.imData = new double[windowLength];

            //周波数軸(パワー)グラフのデータ格納用(FFT後のデータ)
            var powerData = new double[windowLength];
            //周波数軸(dB)グラフのデータ格納用(FFT後のデータ)
            var dBData = new double[windowLength];
            //周波数軸(位相)グラフのデータ格納用(FFT後のデータ)
            var phaseData = new double[windowLength];

            CalcButterfly(data);

            for (int i = 0; i < windowLength; i++)
            {
                //パワースペクトル算出
                //数が大きくなるので、windowLengthで割る
                powerData[i] = (this.reData[i] * this.reData[i] + this.imData[i] * this.imData[i]) / windowLength;

                //位相スペクトル算出
                phaseData[i] = System.Math.Atan2(this.imData[i], this.reData[i]);

                //dB変換
                if (powerData[i] > 0) dBData[i] = 10 * System.Math.Log10(powerData[i]);
                else dBData[i] = 0.00000;
            }

            return dBData;
        }


        private void CalcButterfly(double[] data)
        {
            int i, j, k, h, m, kp;

            //回転因子用の値を一時格納する変数
            double wtmpr;
            double wtmpi;

            //ビッド反転の値を一時格納する変数
            double tmpr;
            double tmpi;

            //バタフライ演算用の値を一時格納する変数
            double vtmpr;
            double vtmpi;

            //windowLengthが2の何乗であるかを求める
            int power = (int)System.Math.Log(windowLength, 2);

            //データの入れ替え（時間軸データを実数部にコピー）
            for (i = 0; i < windowLength; i++) this.reData[i] = data[i] / windowLength;

            if (twiddleFactor == null)
            {
                Console.WriteLine("回転因子");
                //回転因子格納用
                twiddleFactor = new double[windowLength];

                //回転因子の計算
                for (j = 0; j < windowLength / 2; j++)
                {
                    twiddleFactor[j] = System.Math.Cos(2 * j * System.Math.PI / windowLength);
                    //FFTとIFFTでは符号が逆になる
                    twiddleFactor[j + (windowLength / 2)] = -1 * System.Math.Sin(2 * j * System.Math.PI / windowLength);
                }
            }

            //ビット反転
            j = 0;
            for (i = 0; i <= windowLength - 2; i++)
            {
                if (i < j)
                {
                    //実数部のビット反転
                    tmpr = this.reData[j];
                    this.reData[j] = this.reData[i];
                    this.reData[i] = tmpr;

                    //虚数部のビット反転
                    tmpi = this.imData[j];
                    this.imData[j] = this.imData[i];
                    this.imData[i] = tmpi;
                }
                k = windowLength / 2;
                while (k <= j)
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }

            //バタフライ演算
            for (i = 1; i <= power; i++)
            {
                m = (int)System.Math.Pow(2, i);
                h = m / 2;

                for (j = 0; j < h; j++)
                {
                    wtmpr = twiddleFactor[j * (windowLength / m)];
                    wtmpi = twiddleFactor[j * (windowLength / m) + windowLength / 2];

                    for (k = j; k < windowLength; k += m)
                    {
                        kp = k + h;
                        vtmpr = this.reData[kp] * wtmpr - this.imData[kp] * wtmpi;
                        vtmpi = this.reData[kp] * wtmpi + this.imData[kp] * wtmpr;

                        //実数部のビット反転
                        tmpr = this.reData[k] + vtmpr;
                        this.reData[kp] = this.reData[k] - vtmpr;
                        this.reData[k] = tmpr;

                        //虚数部のビット反転
                        tmpi = this.imData[k] + vtmpi;
                        this.imData[kp] = this.imData[k] - vtmpi;
                        this.imData[k] = tmpi;
                    }
                }
            }
         }
    }
}
