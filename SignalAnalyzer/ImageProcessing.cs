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

        public int[,] CalcGLCM(byte[,] data, int distance, Direction direction)
        {
            try
            {
                int xSize = data.GetLength(0);
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
                    for (int i = 0; i < xSize - distance; i++)
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
                    for (int i = 0; i < xSize - distance; i++)
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
                    for (int i = 0; i < xSize; i++)
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
                    for (int i = distance; i < xSize; i++)
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
