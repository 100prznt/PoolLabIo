using Rca.PoolLabIo.Helpers;
using Rca.PoolLabIo.Objects.Extensions;
using System;
using System.Diagnostics;
using System.IO;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Measurement result data struct
    /// </summary>
    [DebuggerDisplay("{ParameterName, nq}: {GetFormattedValue(), nq}")]
    public class Measurement
    {
        #region Properties
        /// <summary>
        /// Storage ID of the measurement
        /// </summary>
        public int Id
        {
            get => m_RawId;
            init => m_RawId = Convert.ToUInt16(value);
        }

        /// <summary>
        /// Measurement type
        /// <seealso cref="MeasurementTypes"/>
        /// </summary>
        public MeasurementTypes Type { get; init; } // 1 byte

        /// <summary>
        /// Measurement status
        /// <seealso cref="MeasurementStatus"/>
        /// </summary>
        public MeasurementStatus Status { get; init; } // 1 byte

        /// <summary>
        /// Measurement unit
        /// <seealso cref="DeviceUnits"/>
        /// </summary>
        public DeviceUnits Unit { get; init; }

        /// <summary>
        /// Timestamp of the measurement
        /// </summary>
        public DateTime Timestamp
        {
            get => UnixTimeConverter.UnixTimeToDateTime(m_RawTimestamp);
            init => m_RawTimestamp = UnixTimeConverter.ToUnixTime(value);
        }

        /// <summary>
        /// Measvalue
        /// </summary>
        public double Value
        {
            get
            {
                if (Status == MeasurementStatus.Underrange || Status == MeasurementStatus.Overrange)
                    return double.NaN;
                else
                    return Convert.ToDouble(m_RawValue);
            }
            init => m_RawValue = Convert.ToSingle(value);
        }
        
        /// <summary>
        /// Name of the measured parameter
        /// </summary>
        public  string ParameterName => Type.GetDisplayName();

        /// <summary>
        /// Measurement result with unit
        /// </summary>
        public string FormattedValue => GetFormattedValue();

        /// <summary>
        /// Reserved bytes as string
        /// </summary>
        public string ReservedBytes => BitConverter.ToString(m_ReservedBytes);

        #endregion Properties

        #region Members
        ushort m_RawId;             // 2 bytes
        uint m_RawTimestamp;        // 8 bytes
        float m_RawValue;           // 4 bytes
        byte[] m_ReservedBytes;     // 4 bytes

        #endregion Members

        #region Services
        public static Measurement FromBuffer(byte[] buffer, DeviceUnits unit, int index = 0)
        {
            using (var ms = new MemoryStream(buffer, index, buffer.Length - index))
            {
                using (var reader = new BinaryReader(ms))
                {
                    return new Measurement
                    {
                        m_RawId = reader.ReadUInt16(),
                        Type = (MeasurementTypes)reader.ReadByte(),
                        Status = (MeasurementStatus)reader.ReadByte(),
                        Unit = unit,
                        m_RawTimestamp = reader.ReadUInt32(),
                        m_RawValue = reader.ReadSingle(),
                        m_ReservedBytes = reader.ReadBytes(4)
                    };
                }
            }
        }

        public override string ToString() => GetFormattedValue();

        public string ToString(string format) => GetFormattedValue(format);

        #endregion Services

        #region Internal services

        string GetFormattedValue(string format = "f2")
        {
            if (double.IsNaN(Value))
                return $"{Status} {Type.GetElement()}";
            else if (Type.HasUnit())
                return $"{Value.ToString(format)} {Unit.GetSymbol()} ({Type.GetElement()})";
            else
                return $"{Value.ToString(format)} {Type.GetElement()}";
        }

        #endregion Internal services
    }
}
