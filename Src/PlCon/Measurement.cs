using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlCon
{
    /// <summary>
    /// Measurement result data struct
    /// </summary>
    public class Measurement
    {
        public int Id
        {
            get
            {
                return m_RawId;
            }
        }
        public MeasurementType Type { get; set; } // 1 byte
        public MeasurementStatus Status { get; set; } // 1 byte
        public DateTime Timestamp
        {
            get
            {
                return UnixTimeConverter.UnixTimeToDateTime(m_RawTimestamp);
            }
            set
            {
                m_RawTimestamp = value.ToUnixTime();
            }
        }
        public double Value
        {
            get
            {
                return Convert.ToDouble(m_RawValue);
            }
        }


        ushort m_RawId; // 2 byte
        uint m_RawTimestamp; // 4 byte
        float m_RawValue; //4 byte

        public static Measurement FromBuffer(byte[] buffer, int index = 0)
        {
            using (var reader = new BinaryReader(new MemoryStream(buffer, index, buffer.Length - index)))
            {
                var result = new Measurement
                {
                    m_RawId = reader.ReadUInt16(),
                    Type = (MeasurementType)reader.ReadByte(),
                    Status = (MeasurementStatus)reader.ReadByte(),
                    m_RawTimestamp = reader.ReadUInt32(),
                    m_RawValue = reader.ReadSingle()
                };

                return result;
            }
        }
    }
}
