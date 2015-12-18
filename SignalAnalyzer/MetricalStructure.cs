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
    }
}
