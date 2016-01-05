using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;

namespace SignalAnalyzer
{
    public class Graph
    {

        private int plotData;
        private int red;
        private int green;
        private int blue;

        public void draw(Chart chartcontrol, int[] data)
        {
            //軸ラベルの設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Title = "A";

            //X軸最小値、最大値、目盛間隔の設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Maximum =data.Length;
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Interval = 441000;

            //Y軸最小値、最大値、目盛間隔の設定
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Minimum = -30000;
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Maximum = 30000;

            //スクロールバーの表示
            AxisScaleView sv = chartcontrol.ChartAreas["ChartArea1"].AxisX.ScaleView;
            sv.SmallScrollSize = 44100;
            sv.Position = 1;
            sv.Size = 44100 * 60;

            AxisScrollBar sb = chartcontrol.ChartAreas["ChartArea1"].AxisX.ScrollBar;
            sb.ButtonStyle = ScrollBarButtonStyles.All;
            sb.IsPositionedInside = true;

            //Seriesの作成
            Series series = new Series();

            //グラフのタイプを指定(折れ線グラフ)
            series.ChartType = SeriesChartType.Line;

            //グラフのデータを追加
            for (int i = 0; i < data.Length; i += 441) series.Points.AddXY(i,data[i]);

            //作ったSeriesをchartコントロールに追加する
            chartcontrol.Series.Add(series);
        }

        public void drawBeat(Chart chartcontrol, double[] data)
        {
            //軸ラベルの設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Title = "A";

            //X軸最小値、最大値、目盛間隔の設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Maximum = data.Length;

            //Y軸最小値、最大値、目盛間隔の設定
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Minimum = -5000;
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Maximum = 5000;

            //Seriesの作成
            Series series = new Series();

            //グラフのタイプを指定(折れ線グラフ)
            series.ChartType = SeriesChartType.Column;

            //グラフのデータを追加
            for (int i = 0; i < data.Length; i++) series.Points.AddXY(i, data[i]);

            //作ったSeriesをchartコントロールに追加する
            chartcontrol.Series.Add(series);
        }

        public void DrawSpectrogram(PictureBox picture, FrequencyAnalyzer freq)
        {
            Console.WriteLine("Draw Spectrogram...");
            Graphics g;
            Font myFont;
            Pen pen;
            int xZero, yZero, xMax, yMax, marginRight, marginLeft, marginTop, marginBottom, gramWidth, gramHeight;

            int penSize = 15; //太くすると周波数が少ないときでも隙間なく描画される
            float xStep, yStep;
            float dataMax = 0;
            float dataMin = 0;

            String xLabel = "";
            String yLabel = "";

            try
            {
                picture.Refresh();
                picture.Image = new Bitmap(picture.Width, picture.Height);
                g = Graphics.FromImage(picture.Image);
                myFont = new Font("Arial", 9);
                pen = new Pen(Color.FromArgb(0, 0, 0), penSize);

                xLabel = "[ s ]";
                yLabel = "[ kHz ]";
                marginRight = 10;
                marginLeft = 65;
                marginTop = 10;
                marginBottom = 40;
                gramWidth = picture.Width - (marginRight + marginLeft);
                gramHeight = picture.Height - (marginTop + marginBottom);

                xZero = marginLeft;
                yZero = picture.Height - marginBottom;
                xMax = picture.Width - marginRight;
                yMax = marginTop;

                //xStep = (float)gramWidth / (float)(freq.stftData.Length - 2);
                xStep = 1;
                yStep = 1;
                //yStep = System.Math.Abs((float)gramHeight / (freq.stftData[0].Length/4));

                //グラフの描画

                for (int time = 1; time < freq.stftData.Length - 1; time++)
                {
                    for (int i = 0; i < freq.stftData[0].Length; i++)
                    {
                        if (dataMax < freq.stftData[time][i]) dataMax = (float)freq.stftData[time][i];
                        if (dataMin > freq.stftData[time][i]) dataMin = (float)freq.stftData[time][i];
                    }

                    for (int i = 0; i < freq.stftData[0].Length / 4; i++)
                    {
                        float bottomUp = System.Math.Abs(dataMin);

                        plotData = (int)((freq.stftData[time][i] + bottomUp) * 255 * 5 / (dataMax - dataMin));
                        ToHsv(plotData);

                        //Pen p = new Pen(Color.FromArgb(red, green, blue), penSize);
                        pen.Color = Color.FromArgb(red, green, blue);

                        g.DrawLine(pen,
                             (float)(xZero + xStep * (time - 1)),
                             (float)(yZero - i * yStep - penSize / 2),
                             (float)(xZero + xStep * (time)),
                             (float)(yZero - i * yStep - penSize / 2));
                    }
                    if (time % 100 == 0) Console.Write(string.Format("{0, 3:d0}% \r", 100 * time / freq.stftData.Length));
                }

                g.DrawString("0", myFont, Pens.Black.Brush, 0, picture.Width - 2); //原点
                g.DrawLine(Pens.Black, xZero, yZero, xMax, yZero); // x軸
                g.DrawLine(Pens.Black, xZero, yZero, xZero, yMax); // y軸

                //x軸のラベル
                g.DrawString(xLabel, myFont, Pens.Black.Brush, picture.Width / 2, yZero + (marginBottom / 2));

                //y軸のラベル
                g.DrawString(yLabel, myFont, Pens.Black.Brush, 5, gramHeight / 2);

                var beatDetection = new BeatDetection();
                var beat = beatDetection.main(freq);

                /* ピーク検出 */
                for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i]*20000));

                /* 立ち上がり成分 */
                //for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i] /10));
                

                //Graphicsリソース解放
                g.Dispose();
                Console.WriteLine("Done draw!");
            }
            catch (Exception e)
            {

            }
        }

        /**
         * ToHsv
         * 概要：Hsvに変換する
         * @param red 
         * @param green  
         * @param blue
         * @return なし
         */

        public void ToHsv(int plotHsvData)
        {
            switch (plotHsvData / 255)
            {
                case 0:
                    red = 0;
                    green = 0;
                    blue = 0 + (plotHsvData % 255);
                    break;
               
                case 1:
                    red = 0;
                    green = 0 + (plotHsvData % 255);
                    blue = 255;
                    break;
        
                case 2:
                    red = 0;
                    green = 255;
                    blue = 255 - (plotHsvData % 255);
                    break;
                   
                case 3:
                    red = 0 + (plotHsvData % 255);
                    green = 255;
                    blue = 0;
                    break;

                case 4:
                    red = 255;
                    green = 255 - (plotHsvData % 255);
                    blue = 0;
                    break;
            }
        }

    }
}
