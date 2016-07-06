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

        private int plotData, red, green, blue;

        private int xZero, yZero, xMax, yMax, marginRight, marginLeft, marginTop, marginBottom, gramWidth, gramHeight;
        private Font myFont;
        private Pen spectrogramPen;
        private int penSize;

        private float xStep, yStep, dataMax, dataMin;
        private String xLabel, yLabel;

        private double[] groupingBoundary, groupingBoundary2, groupingBoundary3, gpr2a, gpr2b, gpr3, gpr5;
        private int beatInterval;
        private int maxIndex;

        private List<int> maxIndexList;

        public List<int> getMaxIndexList()
        {
            return maxIndexList;
        }

        public int getMaxIndex()
        {
            return maxIndex;
        }

        public void draw(Chart chartcontrol, int[] data)
        {
            //軸ラベルの設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Title = "t";
            chartcontrol.ChartAreas["ChartArea1"].AxisY.Title = "A";

            //X軸最小値、最大値、目盛間隔の設定
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartcontrol.ChartAreas["ChartArea1"].AxisX.Maximum = data.Length;
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
            for (int i = 0; i < data.Length; i += 441) series.Points.AddXY(i, data[i]);

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

        /**
         * initSpectrogram
         * 概要：スペクトログラムの初期化
         * @param なし
         * @return なし
         */

        public void initSpectrogram(PictureBox picture)
        {
            Console.WriteLine("Draw Spectrogram...");

            penSize = 5;
            xStep = 1;
            yStep = 1;

            dataMax = 0;
            dataMin = 0;

            xLabel = "";
            yLabel = "";

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


        }
        public void drawAxis(Graphics g, PictureBox picture)
        {
            g.DrawString("0", myFont, Pens.Black.Brush, 0, picture.Width - 2); //原点
            g.DrawLine(Pens.Black, xZero, yZero, xMax, yZero); // x軸
            g.DrawLine(Pens.Black, xZero, yZero, xZero, yMax); // y軸

            //x軸のラベル
            g.DrawString(xLabel, myFont, Pens.Black.Brush, picture.Width / 2, yZero + (marginBottom / 2));

            //y軸のラベル
            g.DrawString(yLabel, myFont, Pens.Black.Brush, 5, gramHeight / 2);
        }

        public void drawBeatComponent(FrequencyAnalyzer freq, Graphics g, double[] beat)
        {
            //ピーク検出
            //for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i]*20000));
            //                for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i] * 200));
            //立ち上がり成分
            for (int i = 0; i < beat.Length; i++) g.DrawLine(Pens.Black, (xZero + i), yZero, (xZero + i), yZero - (int)(beat[i] * 250));
        }

        public void DrawSpectrogram(PictureBox picture, FrequencyAnalyzer freq, int[] metric, double[][] divStftData, int beatInterval, ProgressBar progressBar)
        {
            Graphics g;

            initSpectrogram(picture);


            progressBar.Minimum = 0;
            progressBar.Maximum = freq.stftData.Length;
            progressBar.Value = 0;
            try
            {
                picture.Refresh();
                picture.Image = new Bitmap(picture.Width, picture.Height);
                g = Graphics.FromImage(picture.Image);
                myFont = new Font("Arial", 9);
                spectrogramPen = new Pen(Color.FromArgb(0, 0, 0), penSize);

                var boundaryPen = new Pen(Color.FromArgb(200, 0, 0), 10);

                //グラフの描画

                float bottomUp, dataIntervalNomalization;
                float penSizeHalf = penSize / 2;

                for (int time = 1; time < freq.stftData.Length - 1; time++)
                {
                    for (int i = 0; i < freq.stftData[0].Length; i++)
                    {
                        if (dataMax < freq.stftData[time][i]) dataMax = (float)freq.stftData[time][i];
                        if (dataMin > freq.stftData[time][i]) dataMin = (float)freq.stftData[time][i];
                    }

                    dataIntervalNomalization = 1 / (dataMax - dataMin);
                    bottomUp = System.Math.Abs(dataMin);

                    for (int i = 0; i < freq.stftData[0].Length / 4; i++)
                    {
                        plotData = (int)((freq.stftData[time][i] + bottomUp) * 255 * 11 * dataIntervalNomalization);
                        ToHsv(plotData);

                        /* color */
                         spectrogramPen.Color = Color.FromArgb(red, green, blue);


                        /* monochrome */
                        //spectrogramPen.Color = Color.FromArgb(255 - plotData / 10, 255 - plotData / 10, 255 - plotData / 10);

                        g.DrawLine(spectrogramPen,
                             (float)(xZero + xStep * (time - 1)),
                             (float)(yZero - i * yStep - penSizeHalf),
                             (float)(xZero + xStep * (time)),
                             (float)(yZero - i * yStep - penSizeHalf));
                    }
                    progressBar.Value = time;
                }

                drawAxis(g, picture);

                picture.Image.Save("C:/Users/sawada/Desktop/test.bmp");

                var beatDetection = new BeatDetection();
                var beat = beatDetection.main(freq);

                //drawBeatComponent(freq, g, beat);

                if (metric != null) drawMetricStructure(g, metric, divStftData, freq, beatInterval, beat);
                this.beatInterval = beatInterval;

                //Graphicsリソース解放
                g.Dispose();
                Console.WriteLine("Done draw!");
            }
            catch (Exception e)
            {

            }
        }


        public void ExportSpectrogram(PictureBox picture, FrequencyAnalyzer freq, ProgressBar progressBar, String name)
        {
            Graphics g;

            initSpectrogram(picture);

            progressBar.Minimum = 0;
            progressBar.Maximum = freq.stftData.Length;
            progressBar.Value = 0;
            try
            {
                picture.Refresh();
                picture.Image = new Bitmap(picture.Width, picture.Height);
                g = Graphics.FromImage(picture.Image);
                myFont = new Font("Arial", 9);
                spectrogramPen = new Pen(Color.FromArgb(0, 0, 0), penSize);

                var boundaryPen = new Pen(Color.FromArgb(200, 0, 0), 10);

                //グラフの描画

                float bottomUp, dataIntervalNomalization;
                float penSizeHalf = penSize / 2;

                for (int time = 1; time < freq.stftData.Length - 1; time++)
                {
                    for (int i = 0; i < freq.stftData[0].Length; i++)
                    {
                        if (dataMax < freq.stftData[time][i]) dataMax = (float)freq.stftData[time][i];
                        if (dataMin > freq.stftData[time][i]) dataMin = (float)freq.stftData[time][i];
                    }

                    dataIntervalNomalization = 1 / (dataMax - dataMin);
                    bottomUp = System.Math.Abs(dataMin);

                    for (int i = 0; i < freq.stftData[0].Length / 4; i++)
                    {
                        plotData = (int)((freq.stftData[time][i] + bottomUp) * 255 * 10 * dataIntervalNomalization);
                        ToHsv(plotData);

                        /* color */
                        // spectrogramPen.Color = Color.FromArgb(red, green, blue);


                        /* monochrome */
                        spectrogramPen.Color = Color.FromArgb(255 - plotData / 10, 255 - plotData / 10, 255 - plotData / 10);

                        g.DrawLine(spectrogramPen,
                             (float)(xStep * (time - 1)),
                             (float)(picture.Height - i * yStep - penSizeHalf),
                             (float)(xStep * (time)),
                             (float)(picture.Height - i * yStep - penSizeHalf));
                    }
                    progressBar.Value = time;
                }

                picture.Image.Save("C:/Users/sawada/Desktop/spectrogram/"+name+".bmp");

                //Graphicsリソース解放
                g.Dispose();
                Console.WriteLine("Done draw!");
            }
            catch (Exception e)
            {

            }
        }

        public void drawMetricStructure(Graphics g, int[] metric, double[][] divStftData, FrequencyAnalyzer freq, int beatInterval, double[] beat)
        {
            var metricPen = new Pen(Color.FromArgb(0, 0, 0), penSize);

            for (int i = 0; i < metric.Length; i++)
            {
                g.DrawLine(Pens.Blue, (xZero + i), yZero, (xZero + i), yZero - metric[i]);
            }

            var positiveStftData = new double[divStftData.Length][];
            int[] minMatrix = Enumerable.Repeat(0, divStftData.Length).ToArray();

            for (int i = 0; i < minMatrix.Length; i++)
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
                changeRatio = (int)(changeRatioData[i] / 300);

                //if (changeRatio > 210) g.DrawLine(boundaryPen, xZero + (beatInterval * (i) + 10), yZero, xZero + (beatInterval * (i) + 10), yMax); //グルーピング協会
                //               g.DrawLine(metricPen, xZero + (beatInterval) * (i) + 10, yMax, xZero + (beatInterval) * (i) + 10, yMax + changeRatio);
                //               g.DrawLine(metricPen, xZero + (beatInterval * (i) + 10), yMax + 100, xZero + (beatInterval * (i) + 10), yMax + 100 + (int)(sumPower[i] / 500));
            }


            gpr2a = new double[sumPower.Length];

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
                // g.DrawLine(metricPen, xZero + beatInterval * i +10, yMax, xZero + beatInterval*i + 10, yMax + (int)(gpr2a[i] * 5000));   
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

            gpr2b = new double[beatIntervalArray.Count];

            gpr2b[0] = 0;

            for (int i = 1; i < gpr2b.Length - 1; i++)
            {

                if ((int)beatIntervalArray[i] > (int)beatIntervalArray[i - 1] && (int)beatIntervalArray[i] > (int)beatIntervalArray[i + 1]) gpr2b[i] = 1.0;
                else gpr2b[i] = 0.0;
            }

            int x = 10;
            for (int i = 0; i < gpr2b.Length - 1; i++)
            {
                //g.DrawLine(metricPen, xZero + x, yMax, xZero + x, yMax + gpr2b[i]*500);
                x += (int)beatIntervalArray[i + 1];
            }

            gpr3 = new double[changeRatioData.Length + 1];

            gpr3[0] = 0;

            for (int i = 1; i < gpr3.Length - 1; i++)
            {
                gpr3[i] = (double)changeRatioData[i - 1] / (double)changeRatioData.Max();
            }

            gpr5 = new double[sumPower.Length];

            for (int i = 0; i < gpr5.Length; i++)
            {
                if (i <= gpr5.Length / 2) gpr5[i] = (2.0 * i) / gpr5.Length;
                else gpr5[i] = 2 - (2.0 * i) / gpr5.Length;
            }

            /*
            for (int i = 0; i < gpr5.Length; i++)
            {
                g.DrawLine(metricPen, xZero + beatInterval * i, yZero, xZero + beatInterval * i, yZero - (int)(gpr5[i] * 200));
            }*/

            double s2a = 1.0;
            double s2b = 1.0;
            double s3 = 0.2;

            groupingBoundary = new double[sumPower.Length];

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                groupingBoundary[i] = gpr2a[i] * s2a + gpr2b[i] * s2b + gpr3[i] * s3;
            }

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                groupingBoundary[i] = groupingBoundary[i] * gpr5[i] / groupingBoundary.Max();
                //groupingBoundary[i] = groupingBoundary[i] * Math.Sin(Math.PI * i / groupingBoundary.Length) / groupingBoundary.Max();
            }

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                g.DrawLine(metricPen, xZero + beatInterval * i, yZero, xZero + beatInterval * i, yZero - (int)(groupingBoundary[i] * 200));
            }
        }


        int[] maxIndexArray = new int[3];
        double[][] gpr5Array = new double[3][];
        double[] groupingArray3;
        public void drawGroupingStructure(PictureBox picture, int valueGPR2a, int valueGPR2b, int valueGPR3, int valueGPR5)
        {
            groupingBoundary2 = new double[groupingBoundary.Length];
            groupingBoundary3 = new double[groupingBoundary.Length];

            Graphics g;
            Pen pen = new Pen(Color.FromArgb(0, 0, 0), 5);
            Pen penMax = new Pen(Color.Red, 5);
            picture.Image = new Bitmap("C:/Users/sawada/Desktop/test.bmp");
            picture.Size = new Size(picture.Image.Width, picture.Image.Height);
            g = Graphics.FromImage(picture.Image);


            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                groupingBoundary[i] = gpr2a[i] * (double)(valueGPR2a / 10.0) + gpr2b[i] * (double)(valueGPR2b / 10.0) + gpr3[i] * (double)(valueGPR3 / 10.0);
                // groupingBoundary2[i] = groupingBoundary[i];
                // groupingBoundary3[i] = groupingBoundary[i];
            }

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                groupingBoundary[i] = groupingBoundary[i] * gpr5[i] * (double)(valueGPR5 / 10.0) / groupingBoundary.Max();
            }

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                g.DrawLine(pen, xZero + beatInterval * i, yZero, xZero + beatInterval * i, yZero - (int)(groupingBoundary[i] * 500));
            }



            maxIndex = System.Array.IndexOf(groupingBoundary, groupingBoundary.Max());
            maxIndexArray[0] = maxIndex;


            Array.Copy(groupingBoundary, 0, groupingBoundary2, 0, maxIndex - 1);
            Array.Copy(groupingBoundary, maxIndex, groupingBoundary3, 0, groupingBoundary.Length - maxIndex);

            functionGPR5(0, maxIndex, 0);
            functionGPR5(maxIndex, groupingBoundary.Length, 1);

            for (int i = 0; i < groupingBoundary.Length; i++)
            {
                //groupingBoundary[i] = groupingBoundary[i] * gpr5[i] * (double)(valueGPR5 / 10.0) / groupingBoundary.Max();
                groupingBoundary2[i] = groupingBoundary2[i] * gpr5Array[0][i] * (double)(valueGPR5 / 10.0) / groupingBoundary2.Max();
                groupingBoundary3[i] = groupingBoundary3[i] * gpr5Array[1][i] * (double)(valueGPR5 / 10.0) / groupingBoundary3.Max();
            }


            maxIndex = System.Array.IndexOf(groupingBoundary2, groupingBoundary2.Max());
            maxIndexArray[1] = maxIndex;

            maxIndex = System.Array.IndexOf(groupingBoundary3, groupingBoundary3.Max());
            maxIndexArray[2] = maxIndex + maxIndexArray[0];

            //Console.WriteLine("" + maxIndexArray);

            //maxIndexArray[2] = System.Array.IndexOf(groupingArray3, groupingArray3.Max());

            g.DrawLine(penMax, xZero + beatInterval * maxIndexArray[0], yZero, xZero + beatInterval * maxIndexArray[0], yZero - 500);
            g.DrawLine(penMax, xZero + beatInterval * maxIndexArray[1], yZero, xZero + beatInterval * maxIndexArray[1], yZero - 500);
            g.DrawLine(penMax, xZero + beatInterval * maxIndexArray[2], yZero, xZero + beatInterval * maxIndexArray[2], yZero - 500);
            //g.DrawLine(penMax, xZero + beatInterval * maxIndexArray[2], yZero, xZero + beatInterval * maxIndexArray[2], yZero - 500);

        }

        public void functionGPR5(int begin, int max, int h)
        {
            var tempGPR5 = new double[gpr5.Length];

            for (int i = 0; i < gpr5.Length; i++)
            {
                if (i <= begin + (max / 2)) tempGPR5[i] = (2.0 * i) / max;
                else tempGPR5[i] = 2 - (2.0 * i) / max;
            }
            gpr5Array[h] = tempGPR5;

        }

        /**
         * ToHsv
         * 概要：Hsvに変換する
         * @param red 
         * @param green  
         * @param blue
         * @return なし
         */

        /*
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
        */

        
        public void ToHsv(int plotHsvData)
        {
            switch (plotHsvData / 255)
            {


                case 0:
                    red = 0;
                    green = 0;
                    blue = 0 + (plotHsvData % 255) / 10;
                    break;

                case 1:
                    red = 0;
                    green = 0;
                    blue = 255 / 10 + (plotHsvData % 255) / 10;
                    break;

                case 2:
                    red = 0;
                    green = 0;
                    blue = 255*2 / 10 + (plotHsvData % 255) / 10;
                    break;

                case 3:
                    red = 0;
                    green = 0;
                    blue = 255*3 / 10 + (plotHsvData % 255) / 10;
                    break;

                case 4:
                    red = 0;
                    green = 0;
                    blue = 255 * 4 / 10 + (plotHsvData % 255) / 10;
                    break;

                case 5:
                    red = 0;
                    green = 0;
                    blue = 255/2 + (plotHsvData % 255) / 2;
                    break;

                case 6:
                    red = 0;
                    green = 0 + (plotHsvData % 255)/2;
                    blue = 255 - (plotHsvData % 255);
                    break;
        
                /*
            case 4:
                red = 0+ (plotHsvData % 255);
                green = 255 / 2 + (plotHsvData % 255) / 2;
                blue = 255;
                break;
                */
/*
                case 7:
                    red = 0;
                    green = 255;
                    blue = 255 - (plotHsvData % 255);
                    break;
                    */

        
                case 7:
                    red = 0 + (plotHsvData % 255);
                    green = 255 / 2 + (plotHsvData % 255)/2;
                    blue = 0;
                    break;

                case 8:
                    red = 255;
                    green = 255 - (plotHsvData % 255)/2;
                    blue = 0;
                    break;

                case 9:
                    red = 255;
                    green = 255/2 - (plotHsvData % 255)/2;
                    blue = 0;
                    break;

                case 10:
                    red = 255 - (plotHsvData % 255) / 2;
                    green = 0;
                    blue = 0;
                    break;

            }
        }
        
        

        public void DrawGLCM(PictureBox picture, int[,]data)
        {
            Graphics g;

            initSpectrogram(picture);
            try
            {
                picture.Refresh();
                picture.Image = new Bitmap(picture.Width, picture.Height);
                g = Graphics.FromImage(picture.Image);
                myFont = new Font("Arial", 9);
                spectrogramPen = new Pen(Color.FromArgb(0, 0, 0), penSize);

                var boundaryPen = new Pen(Color.FromArgb(200, 0, 0), 10);

                //グラフの描画

                float bottomUp, dataIntervalNomalization;
                float penSizeHalf = penSize / 2;

                for (int i = 1; i < data.GetLength(0); i++)
                {
                    for (int j = 1; j < data.GetLength(1); j++)
                    {
                        if (dataMax < data[i,j]) dataMax = (float)data[i,j];
                        if (dataMin > data[i,j]) dataMin = (float)data[i,j];
                    }
                }

                for(int j = 1; j < data.GetLength(0); j++)
                {
                    dataIntervalNomalization = 1 / (dataMax - dataMin);

                    for (int i = 1; i < data.GetLength(1); i++)
                    {
                        plotData = (int)((data[j,i]) * 255 * 10 * dataIntervalNomalization);
                        ToHsv(plotData);

                        /* color */
                        spectrogramPen.Color = Color.FromArgb(red, green, blue);


                        /* monochrome */
                        //spectrogramPen.Color = Color.FromArgb(255 - plotData / 10, 255 - plotData / 10, 255 - plotData / 10);

                        g.DrawLine(spectrogramPen,
                             (float)(xZero + 2 * (j - 1)),
                             (float)(yZero - i * 2 - penSizeHalf),
                             (float)(xZero + 2 * (j)),
                             (float)(yZero - i * 2 - penSizeHalf));
                    }
                }

                drawAxis(g, picture);

                picture.Image.Save("C:/Users/sawada/Desktop/glcmDegree.bmp");    

                //Graphicsリソース解放
                g.Dispose();
                Console.WriteLine("Done draw!");
            }
            catch (Exception e)
            {

            }
        }

    }
}
