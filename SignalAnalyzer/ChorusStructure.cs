using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class ChorusStructure
    {
        private int[] startTime;
        private int[] endTime;
        private string[] label;


        public int[] StartTime
        {
            set { this.startTime = value; }
            get { return this.startTime; }
        }

        public int[] EndTime
        {
            set { this.endTime = value; }
            get { return this.endTime; }
        }

        public string[] Label
        {
            set { this.label = value; }
            get { return this.label; }
        }

        public string[] chorusToBeat(MetricalStructure metric)
        {
            var truthCluster = new string[metric.RightTime.Length];
            string tempLabel = Label[0];
            int j = 0;

            for (int i = 0; i < truthCluster.Length; i++)
            {
                truthCluster[i] = tempLabel;
                if (endTime[j] == metric.RightTime[i]) //スペクトログラム(j: 4分音符レベル)
                {
                    tempLabel = Label[j+1];
                    j++;
                }
            }

            for (int i = 0; i < truthCluster.Length; i++) Console.WriteLine("" + i + ":"+truthCluster[i]);

            return truthCluster;
        }
    }
}