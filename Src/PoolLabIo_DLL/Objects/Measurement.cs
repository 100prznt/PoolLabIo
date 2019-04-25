using Rca.PoolLabIo.Helpers;
using Rca.PoolLabIo.Objects.Extensions;
using System;
using System.IO;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Measurement result data struct
    /// </summary>
    public class Measurement
    {
        #region Properties
        /// <summary>
        /// Storage ID of the measurement
        /// </summary>
        public int Id
        {
            get
            {
                return m_RawId;
            }
        }

        /// <summary>
        /// Measurement type
        /// <seealso cref="MeasurementType"/>
        /// </summary>
        public MeasurementType Type { get; set; } // 1 byte

        /// <summary>
        /// Measurement status
        /// <seealso cref="MeasurementStatus"/>
        /// </summary>
        public MeasurementStatus Status { get; set; } // 1 byte

        /// <summary>
        /// Timestamp of the measurement
        /// </summary>
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

        /// <summary>
        /// Measvalue
        /// </summary>
        public double Value
        {
            get
            {
                return Convert.ToDouble(m_RawValue);
            }
        }

        public  string DisplayName
        {
            get
            {
                return Type.GetDisplayName();
            }
        }

        public string FormattedValue
        {
            get
            {
                return Value.ToString("f2") + " " + Type.GetUnit() + " " + Type.GetElement();
            }
        }

        #endregion Properties

        #region Members
        ushort m_RawId; // 2 byte
        uint m_RawTimestamp; // 8 byte
        float m_RawValue; //4 byte

        #endregion Members

        #region Services
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

        #endregion Services
    }
}
