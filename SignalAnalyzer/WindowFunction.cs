using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class WindowFunction
    {
        public double[] Gaussian(int[] data)
        {
            var gaussianData = new double[data.Length];
            double dispersion = CalcDispersion(data);
            for (int i = 0; i < data.Length; i++) gaussianData[i] = data[i] * Math.Exp(-(i*i) / dispersion);

            return gaussianData;
        }

        public double[] Hanning(int[] data)
        {
            var hanningData = new double[data.Length];

            for (int i = 0; i < data.Length; i++) hanningData[i] = data[i] * (0.5 - 0.5 * Math.Cos(2*Math.PI * i / (data.Length-1)));

            return hanningData;
        }

        public double CalcMean(int[] data)
        {
            int sum = 0;
            double mean;
            foreach (var item in data) sum += item;
            mean = sum / data.Length;
            return mean;
        }

        public double CalcDispersion(int[] data)
        {
            double sum = 0;
            double dispersion = 0;
            double mean = CalcMean(data);
            foreach (var item in data) sum += (item - mean)*(item - mean);
            dispersion = sum / data.Length;
            return dispersion;
        }
    }
}
