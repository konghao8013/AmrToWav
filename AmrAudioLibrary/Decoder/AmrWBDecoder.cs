using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibrary.Decoder
{
    public class AmrwbDecoder : IAmrDecoder
    {
        IntPtr _amr;

        public int byteSize => 320;
        public int rate => 16000;

        public int[] sizes => new int[] { 17, 23, 32, 36, 40, 46, 50, 58, 60, 5, -1, -1, -1, -1, -1, 0 };

        public void Decode(byte[] buffer, short[] outbuffer)
        {
            Amrwb.Decoder.D_IF_decode(_amr, buffer, outbuffer, 0);
        }

        public void Exit()
        {
            Amrwb.Decoder.D_IF_exit(_amr);
        }

        public IntPtr Init()
        {
            return _amr = Amrwb.Decoder.D_IF_init();
        }
    }
}
