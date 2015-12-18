using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalAnalyzer
{
    public class WavFile
    {
        public byte[] riffID;   //byte:符号なし8bit（1 byte）整数
        public uint fileSize;       //unit:符号なし32 bit（4 byte）整数（2GB以上扱えるようになる）
        public byte[] waveID;
        //fmtチャンク
        public byte[] fmtID;
        public uint fmtSize;
        public ushort format;     //ushort:符号なし16bit（2 byte）整数
        public ushort channels;
        public uint samplingRate;
        public uint bytePerSec;
        public ushort blockSize;
        public ushort bitPerSampling;
        //dataチャンク
        public byte[] dataID;
        public uint dataSize;

        //楽曲データ
        private int[] rightData;
        private int[] leftData;

        public int[] RightData
        {
            set { this.rightData = value; }
            get { return this.rightData; }
        }

        public int[] LeftData
        {
            set { this.leftData = value; }
            get { return this.leftData; }
        }
    }
}
