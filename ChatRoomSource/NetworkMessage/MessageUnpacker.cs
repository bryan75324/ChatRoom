using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkMessage
{
    internal class MessageUnpacker : Message
    {
        public MessageUnpacker(byte[] byteData)
        {
            m_byteData = byteData;
            m_readPos = 0;
            Read(out m_id);
            Read(out m_dataLenght);
        }

        public bool Read(out uint i)
        {
            if (BitConverter.IsLittleEndian == false)
            {
                byte[] aBuffer = new byte[sizeof(uint)];
                Buffer.BlockCopy(m_byteData, (int)m_readPos, aBuffer, 0, aBuffer.Length);
                Array.Reverse(aBuffer);
                i = BitConverter.ToUInt32(aBuffer, 0);
            }
            else
            {
                i = BitConverter.ToUInt32(m_byteData, (int)m_readPos);
            }

            m_readPos += sizeof(int);
            return true;
        }

        public bool Read(out int i)
        {
            if (BitConverter.IsLittleEndian == false)
            {
                byte[] aBuffer = new byte[sizeof(int)];
                Buffer.BlockCopy(m_byteData, (int)m_readPos, aBuffer, 0, aBuffer.Length);
                Array.Reverse(aBuffer);
                i = BitConverter.ToInt32(aBuffer, 0);
            }
            else
            {
                i = BitConverter.ToInt32(m_byteData, (int)m_readPos);
            }

            m_readPos += sizeof(int);
            return true;
        }

        public bool Read(out float f)
        {
            if (BitConverter.IsLittleEndian == false)
            {
                byte[] aBuffer = new byte[sizeof(float)];
                Buffer.BlockCopy(m_byteData, (int)m_readPos, aBuffer, 0, aBuffer.Length);
                Array.Reverse(aBuffer);
                f = BitConverter.ToSingle(aBuffer, 0);
            }
            else
            {
                f = BitConverter.ToSingle(m_byteData, (int)m_readPos);
            }

            m_readPos += sizeof(float);
            return true;
        }

        public bool Read(out double d)
        {
            if (BitConverter.IsLittleEndian == false)
            {
                byte[] aBuffer = new byte[sizeof(double)];
                Buffer.BlockCopy(m_byteData, (int)m_readPos, aBuffer, 0, aBuffer.Length);
                Array.Reverse(aBuffer);
                d = BitConverter.ToDouble(aBuffer, 0);
            }
            else
            {
                d = BitConverter.ToDouble(m_byteData, (int)m_readPos);
            }

            m_readPos += sizeof(double);
            return true;
        }

        public bool Read(out string s)
        {
            int sLenght;
            Read(out sLenght);

            s = Encoding.Unicode.GetString(m_byteData, (int)m_readPos, sLenght);
            m_readPos += (uint)sLenght;
            return true;
        }

        private uint m_readPos;
    }
}