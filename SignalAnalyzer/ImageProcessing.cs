using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SignalAnalyzer
{
    class ImageProcessing
    {

        /// <summary>
        /// 色階調反転
        /// </summary>
        public byte[,] ReverseColor(byte[,] data)
        {

            try
            {
                int xSize = data.GetLength(0);
                int ySize = data.GetLength(1);

                byte[,] revdata = new byte[xSize, ySize];

                for (int i = 0; i < ySize; i++)
                {
                    for (int j = 0; j < xSize; j++)
                    {
                        revdata[j, i] = (byte)(255 - data[j, i]);
                    }
                }
                return revdata;
            }
            catch
            {
                return null;
            }
        }

        public int[,] CalcGLCM(byte[,] data, int distance, Direction direction, int startPixel, int endPixel)
        {
            try
            {
                //int xSize = data.GetLength(0);
                int xSize = endPixel - startPixel;
                int ySize = data.GetLength(1);

                int density1 = 0;
                int density2 = 0;

                int[,] dataGLCM = new int[257, 257]; //濃度の範囲

                //initialize
                for (int i = 0; i < dataGLCM.GetLength(0); i++)
                {
                    for (int j = 0; j < dataGLCM.GetLength(1); j++)
                    {
                        dataGLCM[i, j] = 0;
                    }
                }

                if (direction == Direction.Degree0)
                {
                    for (int i = startPixel; i < endPixel - distance; i++)
                    {
                        for (int j = 0; j < ySize; j++)
                        {
                            density1 = data[i, j];
                            density2 = data[i + distance, j];

                            dataGLCM[density1, density2]++;
                        }
                    }

                }
                else if (direction == Direction.Degree45)
                {
                    for (int i = startPixel; i < endPixel - distance; i++)
                    {
                        for (int j = distance; j < ySize; j++)
                        {
                            density1 = data[i, j];
                            density2 = data[i + distance, j - distance];

                            dataGLCM[density1, density2]++;
                        }
                    }
                }
                else if (direction == Direction.Degree90)
                {
                    for (int i = startPixel; i < endPixel; i++)
                    {
                        for (int j = distance; j < ySize; j++)
                        {
                            density1 = data[i, j];
                            density2 = data[i, j - distance];

                            dataGLCM[density1, density2]++;
                        }
                    }
                }
                else if (direction == Direction.Degree135)
                {
                    for (int i = startPixel + distance; i < endPixel; i++)
                    {
                        for (int j = distance; j < ySize; j++)
                        {
                            density1 = data[i, j];
                            density2 = data[i - distance, j - distance];

                            dataGLCM[density1, density2]++;
                        }
                    }
                }

                return dataGLCM;
            }
            catch
            {
                return null;
            }
        }

        public double[,] NormalizeGLCM(int[,] data1, int[,] data2, int[,] data3, int[,] data4)
        {
            var probabilityGLCM = new double[data1.GetLength(0),data1.GetLength(1)];
            double sum = 0;

            for (int i = 0; i < probabilityGLCM.GetLength(0); i++)
            {
                for(int j = 0; j < probabilityGLCM.GetLength(1); j++)
                {
                    probabilityGLCM[i, j] = data1[i, j] + data2[i, j] + data3[i, j] + data4[i, j];
                    sum += probabilityGLCM[i, j];
                }
            }

            sum -= probabilityGLCM[0, 0];
            for (int i = 0; i < probabilityGLCM.GetLength(0); i++)
            {
                for (int j = 0; j < probabilityGLCM.GetLength(1); j++)
                {
                    probabilityGLCM[i, j] = probabilityGLCM[i,j] / sum;
                }
            }
                return probabilityGLCM;
        }

        public double AngularSecondMoment(double[,] data)
        {
            double angularSecondMoment = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    angularSecondMoment += data[i, j] * data[i, j];
                }
            }
            //Console.WriteLine("ASM:"+ angularSecondMoment);
            return angularSecondMoment;
        }

        public double Contrast(double[,] data)
        {
            double contrast = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    contrast += (i - j) * (i - j) * data[i,j];
                }
            }
            //Console.WriteLine("Contrast:" + contrast);
            return contrast;
        }

        public double Mean(double[,] data)
        {
            double mean = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    mean += i * data[i, j];
                }
            }
            //Console.WriteLine("Mean:" + mean);
            return mean;
        }

        public double InverseDifferentMoment(double[,] data)
        {
            double idm = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    idm += data[i, j] / (1 + (i - j) * (i - j));
                }
            }
            //Console.WriteLine("IDM:" + idm);
            return idm;
        }

        public double StandardDeviation(double[,] data)
        {
            double sd = 0;
            double mean = Mean(data);

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    sd += (i - mean) * (i - mean) * data[i, j];
                }
            }

            sd = Math.Sqrt(sd);

            //Console.WriteLine("SD:" + sd);
            return sd;
        }

        public double Entropy(double[,] data)
        {
            double entropy = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if(data[i,j] != 0 ) entropy += data[i,j] * Math.Log10(data[i, j]);
                }
            }

            entropy = -entropy;
            //Console.WriteLine("Entropy:" + entropy);
            return entropy;
        }

        /// <summary>
        /// Bitmapをロードしbyte[,]配列で返す
        /// </summary>
        public byte[,] LoadByteImage(string filename, ProgressBar progressBar)
        {
            try
            {
                Bitmap bitmap = new Bitmap(filename);
                byte[,] data = new byte[bitmap.Width, bitmap.Height];

                progressBar.Minimum = 0;
                progressBar.Maximum = bitmap.Height;
                progressBar.Value = 0;

                // bitmapクラスの画像ピクセル値を配列に挿入
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        // ここではグレイスケールに変換して格納
                        //GetPixel(x, y)
                        data[j, i] =
                            (byte)(
                                (bitmap.GetPixel(j, i).R +
                                bitmap.GetPixel(j, i).B +
                                bitmap.GetPixel(j, i).G) / 3
                                );
                    }
                    progressBar.Value++;
                }
                return data;
            }
            catch
            {
                Console.WriteLine("ファイルが読めません。");
                return null;
            }
        }

        /// <summary>
        /// 画像配列をbmpに書き出す
        /// </summary>
        public void SaveByteImage(byte[,] data, string filename)
        {
            try
            {
                // 縦横サイズを配列から読み取り
                int xsize = data.GetLength(0);
                int ysize = data.GetLength(1);

                Bitmap bitmap = new Bitmap(xsize, ysize);

                // bitmapクラスのSetPixelでbitmapオブジェクトに
                // ピクセル値をセット
                for (int i = 0; i < ysize; i++)
                {
                    for (int j = 0; j < xsize; j++)
                    {
                        bitmap.SetPixel(
                            j,
                            i,
                            Color.FromArgb(
                                data[j, i],
                                data[j, i],
                                data[j, i])
                            );
                    }
                }

                // 画像の保存
                bitmap.Save(filename, ImageFormat.Bmp);
            }
            catch
            {
                Console.WriteLine("ファイルが書き込めません。");
            }
        }
    }
}
