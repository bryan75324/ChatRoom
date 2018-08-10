using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkMessage
{
    public class MessagePakcer : Message
    {
        public MessagePakcer()
            : this(1024)
        {
        }

        public MessagePakcer(uint size)
            : base(size)
        {
            m_writePos = 0;

            // Creat a dummy for message id;
            Write(ID);

            // Craete a dummy for data lenght info;
            Write(0);
        }

        public bool Write(int i)
        {
            byte[] aBuffer = BitConverter.GetBytes(i);
            _Write(aBuffer);
            return true;
        }

        public bool Write(float f)
        {
            byte[] aBuffer = BitConverter.GetBytes(f);
            _Write(aBuffer);
            return true;
        }

        public bool Write(double d)
        {
            byte[] aBuffer = BitConverter.GetBytes(d);
            _Write(aBuffer);
            return true;
        }

        public bool Write(string s)
        {
            byte[] aBuffer = Encoding.Unicode.GetBytes(s);
            if (Write(aBuffer.Length) == false) return false;
            _Write(aBuffer);
            return true;
        }

        public uint Tell()
        {
            return m_writePos;
        }

        public void Seek(uint position)
        {
            m_writePos = position;
        }

        private void _Write(byte[] byteData)
        {
            if (BitConverter.IsLittleEndian == false) Array.Reverse(byteData);
            byteData.CopyTo(m_byteData, byteData.Length);
            m_writePos += (uint)byteData.Length;
            UpdateTotalSize();
        }

        private void UpdateTotalSize()
        {
            m_dataLenght = (uint)m_byteData.Length - sizeof(uint) * 2;
            Seek(sizeof(uint));
            Write(DataLenght);
            Seek((uint)m_byteData.Length);
        }

        private uint m_writePos;
    }
}