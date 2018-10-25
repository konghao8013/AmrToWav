using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibrary.Decoder
{
    public class AmrnbDecoder : IAmrDecoder
    {
        IntPtr _amr;
        public int byteSize => 160;

        public int rate => 8000;

        public int[] sizes => new int[] { 12, 13, 15, 17, 19, 20, 26, 31, 5, 6, 5, 5, 0, 0, 0, 0 };

        public void Decode(byte[] buffer, short[] outbuffer)
        {
            Amrnb.Decoder.Decoder_Interface_Decode(_amr, buffer, outbuffer, 0);
        }

        public void Exit()
        {
            Amrnb.Decoder.Decoder_Interface_exit(_amr);
        }

        public IntPtr Init()
        {
            return _amr = Amrnb.Decoder.Decoder_Interface_init();
        }
    }
}
