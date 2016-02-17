using System;
using System.Collections;
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

        public void DrawSpectrogram(PictureBox picture, FrequencyAnalyzer freq, int[] metric, double[][] divStftData, int beatInterval)
        {
            Console.WriteLine("Draw Spectrogram...");
            Graphics g;
            Font myFont;
            Pen pen;
            int xZero, yZero, xMax, yMax, marginRight, marginLeft, marginTop, marginBottom, gramWidth, gramHeight;

            int penSize = 5; //太くすると周波数が少ないときでも隙間なく描画される
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

                var pen2 = new Pen(Color.FromArgb(0, 0, 0), penSize);
                var boundaryPen = new Pen(Color.FromArgb(200, 0, 0), 10);

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

                        /* color */
                        pen.Color = Color.FromArgb(red, green, blue);

                        /* monochrome */
                        //pen.Color = Color.FromArgb(plotData / 5, plotData / 5, plotData / 5);


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
                
                /* ビートを描画したいときに使用 */
                
                var beatDetection = new BeatDetection();
                var beat = beatDetection.main(freq);

                //ピーク検出
                //for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i]*20000));
//                for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i] * 200));
                //立ち上がり成分
               // for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i]/5));
                
                

                /*拍節構造の描画*/
                if(metric != null)
                {
                    beatInterval = beatInterval;

                    for (int i = 0; i < metric.Length; i++)
                    {
                        //g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - metric[i] );
                    }


                    var positiveStftData = new double[divStftData.Length][];
                    int[] minMatrix = Enumerable.Repeat(0, divStftData.Length).ToArray();

                    for (int i = 0; i < minMatrix.Length; i++ )
                    {
                        minMatrix[i] = Math.Abs((int)divStftData[i].Min());
                    }

                        //正の値にマッピング
                    for (int i = 0; i < positiveStftData.Length; i++)
                        {
                            double[] positiveStftFreq = Enumerable.Repeat(0.0, freq.focusFreqLength).ToArray(); //すべて0で初期化

                            for (int j = 0; j < positiveStftFreq.Length; j++)
                            {
                                positiveStftFreq[j] = divStftData[i][j] + minMatrix.Max(); //最小値の絶対値の最大値
                            }
                            positiveStftData[i] = positiveStftFreq;
                        }

                    var peakStftData = freq.peakDetection(positiveStftData);

                   for (int i = 0; i < divStftData.Length; i++)
                   {
                       for (int j = 0; j < freq.focusFreqLength; j++)
                       {
                           //時間軸で分割したスペクトルの描画
                         // g.DrawLine(Pens.Black, xZero + (beatInterval * (i) + 108), yZero - j, xZero + (beatInterval * (i) + 108) + (int)positiveStftData[i][j]/500, yZero - j);

                          //時間軸で分割したスペクトルのピークの描画
                          //g.DrawLine(Pens.Black, xZero + (beatInterval * (i)), yZero - j, xZero + (beatInterval * (i)) + (int)peakStftData[i][j] / 100, yZero - j);
                       }
                   }

                   int changeRatio = 0;

                   var changeRatioData = new int[positiveStftData.Length - 1];
                   changeRatioData = freq.changeRatio(peakStftData);

                   var sumPower = freq.sumSpectoroPower(positiveStftData);

                    for (int i = 0; i < changeRatioData.Length; i++)
                    {
                        //changeRatio = (int)(changeRatioData[i] * 500 /((sumPower[i]+sumPower[i+1])/2));
                        //changeRatio = (int)(changeRatioData[i] * 500 / sumPower[i]);
                        //changeRatio = (int)(changeRatioData[i] / 5000);
                        changeRatio = (int)(changeRatioData[i]/300);

                        //if (changeRatio > 210) g.DrawLine(boundaryPen, xZero + (beatInterval * (i) + 10), yZero, xZero + (beatInterval * (i) + 10), yMax); //グルーピング協会
                        g.DrawLine(pen2, xZero + (beatInterval) * (i) + 10, yMax, xZero + (beatInterval) * (i) + 10, yMax + changeRatio);
                        g.DrawLine(pen2, xZero + (beatInterval * (i) + 10), yMax+100, xZero + (beatInterval * (i) + 10), yMax +100+ (int)(sumPower[i] / 500));
                    }


                    var gpr2a = new double[sumPower.Length];

                    gpr2a[0] = 0;

                    for (int i = 1; i < gpr2a.Length - 1; i++)
                    {
                        /*
                        if (sumPower[i] < sumPower[i - 1] && sumPower[i] < sumPower[i + 1]) gpr2a[i] = 1.0 - (sumPower[i] / sumPower.Max());
                        else gpr2a[i] = 0.0;
                        */ 

                        gpr2a[i] = 1.0 - (sumPower[i] / sumPower.Max());
                    }

                    for (int i = 0; i < gpr2a.Length - 1; i++)
                    {
                       // g.DrawLine(pen2, xZero + beatInterval * i +10, yMax, xZero + beatInterval*i + 10, yMax + (int)(gpr2a[i] * 5000));   
                    }

                    ArrayList beatIntervalArray = new ArrayList();
                    int oldBeat = 0;
                    int newBeatInterval = 0;
                    //ビート間隔を格納
                    for (int i = 0; i < beat.Length; i++)
                    {
                        if (beat[i] > 0.0)
                        {
                            newBeatInterval = i - oldBeat;
                            oldBeat = i;
                            beatIntervalArray.Add(newBeatInterval);
                        }
                    }

                    var gpr2b = new double[beatIntervalArray.Count];

                    gpr2b[0] = 0;

                    for(int i = 1; i < gpr2b.Length-1; i++)
                    {
                        
                        if ((int)beatIntervalArray[i] > (int)beatIntervalArray[i - 1] && (int)beatIntervalArray[i] > (int)beatIntervalArray[i + 1]) gpr2b[i] = 1.0;
                        else gpr2b[i] = 0.0;
                    }

                    int x = 10;
                    for (int i = 0; i < gpr2b.Length-1; i++)
                    {
                        //g.DrawLine(pen2, xZero + x, yMax, xZero + x, yMax + gpr2b[i]*500);
                        x += (int)beatIntervalArray[i+1];
                    }

                    var gpr3 = new double[changeRatioData.Length+1];

                    gpr3[0] = 0;

                    for (int i = 1; i < gpr3.Length - 1; i++)
                    {
                        gpr3[i] = (double)changeRatioData[i - 1] / (double)changeRatioData.Max();
                    }

                    var gpr5 = new double[sumPower.Length];

                    for (int i = 0; i < gpr5.Length; i++)
                    {
                        if (i <= gpr5.Length/2) gpr5[i] = (2.0 * i) / gpr5.Length;
                        else gpr5[i] = 2 -(2.0 * i) / gpr5.Length;
                    }

                    /*
                    for (int i = 0; i < gpr5.Length; i++)
                    {
                        g.DrawLine(pen2, xZero + beatInterval * i, yZero, xZero + beatInterval * i, yZero - (int)(gpr5[i] * 200));
                    }*/

                    double s2a = 1.0;
                    double s2b = 1.0;
                    double s3  = 0.2;
                    var groupingBoundary = new double[sumPower.Length];
                    for(int i = 0; i<groupingBoundary.Length; i++)
                    {
                        groupingBoundary[i] = gpr2a[i] * s2a +/* gpr2b[i] * s2b + */gpr3[i] * s3;  
                    }
                   
                    for(int i = 0; i<groupingBoundary.Length; i++)
                    {
                        groupingBoundary[i] = groupingBoundary[i] * gpr5[i] / groupingBoundary.Max();
                    }

                    for (int i = 0; i < groupingBoundary.Length; i++)
                    {
                        g.DrawLine(pen2, xZero + beatInterval * i , yZero, xZero + beatInterval * i , yZero - (int)(groupingBoundary[i] * 200)); 
                    }


                }

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
