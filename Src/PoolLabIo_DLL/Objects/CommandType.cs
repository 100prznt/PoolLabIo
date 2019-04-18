namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available commands and their IDs
    /// </summary>
    public enum CommandType : byte
    {
        /// <summary>
        /// Returns information about the device
        /// </summary>
        PCMD_API_GET_INFO = 0x01,
        /// <summary>
        /// Set the PoolLab´s date/time
        /// </summary>
        PCMD_API_SET_TIME = 0x02,
        /// <summary>
        /// Immediately restarts the PoolLab (BLE connection will fail)
        /// </summary>
        PCMD_API_RESET_DEVICE = 0x03,
        /// <summary>
        /// Send the device into sleep-mode/standby
        /// </summary>
        PCMD_API_SLEEP_DEVICE = 0x04,
        /// <summary>
        /// Read measurement result from flash
        /// </summary>
        PCMD_API_GET_MEASURES = 0x05,
        /// <summary>
        /// Delete all saved measurements from the PoolLab
        /// </summary>
        PCMD_API_RESET_MEASURES = 0x06
    }
}
