using AmrAudioLibrary.Decoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmrAudioLibrary
{
    public class Amr2Wav
    {
        public void Converter(string amrPath, string wavPath)
        {
            FileInfo inFileInfo = new FileInfo(amrPath);
            if (!inFileInfo.Exists)
            {
                throw new FileNotFoundException(amrPath);
            }

            BinaryReader binaryReader = new BinaryReader(inFileInfo.Open(FileMode.Open));
            if (binaryReader.BaseStream.Length < 10)
            {
                throw new Exception("损坏的音频文件");
            }
            var header = binaryReader.ReadBytes(6).ToList();
            string strHeader = Encoding.UTF8.GetString(header.ToArray());

            // konghao 20181025 10:44:15 add amr 解码器
            IAmrDecoder amr = null;
            // konghao 20181025 10:45:38 add 采样率

            if ("#!AMR\n".Equals(strHeader))
            {

                amr = new AmrnbDecoder();
            }
            else
            {
                //#!AMR-WB
                header.AddRange(binaryReader.ReadBytes(3));
                strHeader = Encoding.UTF8.GetString(header.ToArray());
                if ("#!AMR-WB\n".Equals(strHeader))
                {

                    amr = new AmrwbDecoder();
                }
                else
                {
                    throw new Exception("错误的头文件");
                }
            }
            WaveWriter wav = new WaveWriter(wavPath, amr.rate, 16, 1);
            bool flag = wav.CreateFile();
            if (!flag)
            {
                binaryReader.Close();
                throw new Exception("未能创建WAV文件：" + wavPath);
            }
            amr.Init();
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {

                byte[] buffer = new byte[500];
                // 读入模式字节
                /* Read the mode byte */
                buffer[0] = binaryReader.ReadByte();
                // 按照模式字节显示的数据包的大小来读数据

                int size = amr.sizes[(buffer[0] >> 3) & 0x0f];
                if (size <= 0)
                    break;

                byte[] temBuffer = binaryReader.ReadBytes(size);
                Array.Copy(temBuffer, 0, buffer, 1, size);

                short[] outbuffer = new short[amr.byteSize];

                amr.Decode(buffer, outbuffer);
                byte[] littleendian = new byte[amr.byteSize * 2];
                int j = 0;
                for (int i = 0; i < amr.byteSize; i++)
                {
                    littleendian[j] = (byte)(outbuffer[i] >> 0 & 0xff);
                    littleendian[j + 1] = (byte)(outbuffer[i] >> 8 & 0xff);
                    j = j + 2;
                }

                wav.Write(littleendian, amr.byteSize * 2);
            }

            binaryReader.Close();
            binaryReader.Dispose();

            amr.Exit();
            wav.CloseFile();

        }
    }
}
