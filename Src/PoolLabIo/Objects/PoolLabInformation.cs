using Rca.PoolLabIo.Helpers;
using System;
using System.IO;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Information about the device
    /// </summary>
    public class PoolLabInformation
    {
        #region Properties
        /// <summary>
        /// OEM ID of the device
        /// </summary>
        public OemVariant ActiveId { get; set; }

        /// <summary>
        /// Firmware version code
        /// </summary>
        public int FwVersion
        {
            get
            {
                return m_RawFwVersion;
            }
        }

        /// <summary>
        /// Number of saved measurement results on the device
        /// </summary>
        public int ResultCode
        {
            get
            {
                return m_RawResultCode;
            }
        }

        /// <summary>
        /// Current date/time
        /// </summary>
        public DateTime DeviceTime
        {
            get
            {
                return UnixTimeConverter.UnixTimeToDateTime(m_RawDeviceTime);
            }
        }

        /// <summary>
        /// Battery charge level in percent (0..100)
        /// </summary>
        public int BatteryLevel
        {
            get
            {
                return m_RawBatteryLevel;
            }
        }
        #endregion Properties

        #region Members

        ushort m_RawFwVersion; // 2 byte
        ushort m_RawResultCode; // 2 byte
        uint m_RawDeviceTime; // 8 byte
        byte[] m_RawMac; // 6 byte
        ushort m_RawBatteryLevel; //2 byte
        #endregion Members

        #region Services
        public static PoolLabInformation FromBuffer(byte[] buffer, int index = 0)
        {
            using (var reader = new BinaryReader(new MemoryStream(buffer, index, buffer.Length - index)))
            {
                var result = new PoolLabInformation
                {
                    ActiveId = (OemVariant)reader.ReadByte(),
                    m_RawFwVersion = reader.ReadUInt16(),
                    m_RawResultCode = reader.ReadUInt16(),
                    m_RawDeviceTime = reader.ReadUInt32(),
                    m_RawMac = reader.ReadBytes(6),
                    m_RawBatteryLevel = reader.ReadUInt16()
                };

                return result;
            }
        }
        #endregion Services
    }
}
