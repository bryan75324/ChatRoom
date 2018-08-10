using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkMessage
{
    public class Message
    {
        /// <summary>
        /// Message ID.
        /// </summary>
        public uint ID { get { return m_id; } }

        /// <summary>
        /// The message container total size of the message byte buffer.
        /// </summary>
        public uint totalSize { get { return m_totalSize; } }

        /// <summary>
        /// Prensent data lenght in this message.
        /// </summary>
        public uint DataLenght { get { return m_dataLenght; } }

        public Message()
            : this(1024)
        {
        }

        public Message(uint size)
        {
            m_byteData = new byte[size];
            m_totalSize = (uint)m_byteData.Length;
        }

        public Message(uint size, uint id)
            : this(size)
        {
            m_id = id;
        }

        /// <summary>
        /// Assign a new message ID.
        /// </summary>
        /// <param name="id">New message ID</param>
        public void SetMessageID(uint id)
        {
            m_id = id;
        }

        protected uint m_id;
        protected uint m_dataLenght;
        protected uint m_totalSize;
        protected byte[] m_byteData = null;
    }
}