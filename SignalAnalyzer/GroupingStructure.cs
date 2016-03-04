using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    class GroupingStructure
    {
        private List<int[]> maxIndexList;
        private List<double[]> gpr5List;
        private int gpr5Length;

        public void setGPR5Length(int value)
        {
            gpr5Length = value;
        }

        public void addMaxIndexList(int[] maxIndex)
        {
            maxIndexList.Add(maxIndex);
        }

        public List<int[]> getMaxIndexList()
        {
            return maxIndexList;
        }

        public void functionGPR5(int rank)
        {
            var gpr5 = new double[gpr5Length];

            if (rank == 0)
            {
                for (int i = 0; i < gpr5.Length; i++)
                {
                    if (i <= gpr5.Length / 2) gpr5[i] = (2.0 * i) / gpr5.Length;
                    else if (i <= gpr5.Length) gpr5[i] = 2 - (2.0 * i) / gpr5.Length;
                }

                gpr5List.Add(gpr5);
            }

            if (rank == 1)
            {
                for (int i = 0; i < gpr5.Length; i++)
                {
                    if (i <= gpr5.Length / 2) gpr5[i] = (2.0 * i) / gpr5.Length;
                    else if (i <= gpr5.Length) gpr5[i] = 2 - (2.0 * i) / gpr5.Length;
                }

                gpr5List.Add(gpr5);
            }
             
        }

        public List<double[]> getGPR5List()
        {
            return gpr5List;
        }


        public void functionGPR5(int begin, int end)
        {
            
        }

    }
}
