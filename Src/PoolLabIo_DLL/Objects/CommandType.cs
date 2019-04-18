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
        PCMD_API_GET_INFO = 0x1,
        /// <summary>
        /// Set the PoolLab´s date/time
        /// </summary>
        PCMD_API_SET_TIME,
        /// <summary>
        /// Immediately restarts the PoolLab (BLE connection will fail)
        /// </summary>
        PCMD_API_RESET_DEVICE,
        /// <summary>
        /// Send the device into sleep-mode/standby
        /// </summary>
        PCMD_API_SLEEP_DEVICE,
        /// <summary>
        /// Read measurement result from flash
        /// </summary>
        PCMD_API_GET_MEASURES,
        /// <summary>
        /// Delete all saved measurements from the PoolLab
        /// </summary>
        PCMD_API_RESET_MEASURES
    }
}
