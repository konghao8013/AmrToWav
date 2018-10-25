using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibrary.Amrwb
{
    public class Decoder
    {
        [DllImport("libopencore-amrwb-0.dll", EntryPoint = "D_IF_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr D_IF_init();

        [DllImport("libopencore-amrwb-0.dll", EntryPoint = "D_IF_decode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void D_IF_decode(IntPtr state, byte[] inBuffer, short[] outBuffer, int bfi);

        [DllImport("libopencore-amrwb-0.dll", EntryPoint = "D_IF_exit", CallingConvention = CallingConvention.Cdecl)]
        public static extern void D_IF_exit(IntPtr state);
    }
}
