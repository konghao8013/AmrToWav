using AmrAudioLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = DateTime.Now.ToString("MMddHHmmss");
            var a = new Amr2Wav();
            //AMR-NB001.amr
            //B15-3.amr
            var outPath = @"C:\Users\konghao\Desktop\音频转换测试\" + time + "AMR-NB001.wav";
            a.Converter(@"C:\Users\konghao\Desktop\B15-3.amr", outPath);
            Console.WriteLine("转换完成：" + outPath);
            Console.ReadLine();
        }
    }
}
