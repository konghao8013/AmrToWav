using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibrary.Decoder
{
    public interface IAmrDecoder
    {
        IntPtr Init();
        void Decode(byte[] buffer, short[] outbuffer);
        void Exit();

        int byteSize { get; }
        int rate { get; }

        int[] sizes { get; }
    }
}
