using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalAnalyzer
{
    public class MetricalStructure
    {
        private int[] rightTime;
        private int[] leftTime;
        private int[] ticks;
        public const int FIRST = 384;
        public const int SECOND = 48;
        public const int THERD = 96;
        public const int FORTH = 144;

        private int beatInterval;
 

        public int[] RightTime
        {
            set { this.rightTime = value; }
            get { return this.rightTime; }
        }

        public int[] LeftTime
        {
            set { this.leftTime = value; }
            get { return this.leftTime; }
        }

        public int[] Ticks
        {
            set { this.ticks = value; }
            get { return this.ticks; }
        }

        public int BeatInterval
        {
            set { this.beatInterval = value; }
            get { return this.beatInterval; }
        }


        //拍節構造を時間軸データに変更する
        public int[] aistToBeat(WavFile wav)
        {
            var beatStructure = new int[wav.RightData.Length];
            int j = 0;

            BeatInterval = this.rightTime[1] - this.rightTime[0];

            for (int i = 0; i < beatStructure.Length; i++)
            {
                //if (i / 441 == this.RightTime[j]) //振幅
                if (i == this.RightTime[j]) //スペクトログラム(i: 4分音符レベル)
                {
                    beatStructure[i] = this.Ticks[j];
                    //Console.WriteLine(this.Ticks[j]);

                    if (j < this.RightTime.Length - 1) j++;
                }
                else { beatStructure[i] = 0; }
            }
            //foreach (var i in beatStructure) Console.WriteLine(i);
                return beatStructure; 
        }

    }
}
