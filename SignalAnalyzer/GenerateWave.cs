using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class GenerateWave
    {

        /*
         * beat
         * 概要：波を生成する処理
         * @param data
         * @param flag
         * @return なし
         */

        public double[] beat(WavFile wav, MetricalStructure beat)
        {
            var beatStructure = new double[wav.RightData.Length];
            Boolean isBeat = false;
            int j = 0;
            int count = 0;
            int f0 = 0;

            for (int i = 0; i < beatStructure.Length; i++)
            {
                if (i / 441 == beat.RightTime[j]) 
                {
                    isBeat = true;
                    count = 0;
                    if (beat.Ticks[j] == MetricalStructure.FIRST) f0 = 880;
                    else f0 = 440;
                    if (j < beat.RightTime.Length-1) j++;
                }

                if (isBeat)
                {
                    beatStructure[i] = 4000 * Math.Sin(2.0*Math.PI*count*f0/wav.samplingRate);
                    count++;
                    if (count >= 3000) isBeat = false;
                }
                else beatStructure[i] = 0;
            }

                return beatStructure; 
        }
         
    }
}
