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
        public OemVariants ActiveId { get; private set; }

        /// <summary>
        /// Firmware version code
        /// </summary>
        public int FwVersion => m_RawFwVersion;

        /// <summary>
        /// Number of saved measurement results on the device
        /// </summary>
        public int ResultCount => m_RawResultCount;

        /// <summary>
        /// Current date/time
        /// </summary>
        public DateTime DeviceTime => UnixTimeConverter.UnixTimeToDateTime(m_RawDeviceTime);

        /// <summary>
        /// MAC-Address of the PoolLAB
        /// </summary>
        public byte[] Mac { get; private set; } //00:A0:50:NN:NN:NN

        /// <summary>
        /// Battery charge level in percent (0..100)
        /// </summary>
        public int BatteryLevel => m_RawBatteryLevel;
        

        #endregion Properties

        #region Members
        ushort m_RawFwVersion;      // 2 byte
        ushort m_RawResultCount;    // 2 byte
        ulong m_RawDeviceTime;      // 8 byte
        ushort m_RawBatteryLevel;   // 2 byte

        #endregion Members

        #region Services
        /// <summary>
        /// Decode a binary buffer to a PoolLabInformation object
        /// </summary>
        /// <param name="buffer">Binary buffer</param>
        /// <param name="index">Data start position in buffer</param>
        /// <returns>Decoded object</returns>
        public static PoolLabInformation FromBuffer(byte[] buffer, int index = 1)
        {
            using (var reader = new BinaryReader(new MemoryStream(buffer, index, buffer.Length - index)))
            {
                return  new PoolLabInformation
                {
                    ActiveId = (OemVariants)reader.ReadUInt16(),
                    m_RawFwVersion = reader.ReadUInt16(),
                    m_RawResultCount = reader.ReadUInt16(),
                    m_RawDeviceTime = reader.ReadUInt64(),
                    Mac = reader.ReadBytes(6),
                    m_RawBatteryLevel = reader.ReadUInt16()
                };
            }
        }

        #endregion Services
    }
}
