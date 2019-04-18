namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available measurement status
    /// </summary>
    public enum MeasurementStatus : byte
    {
        /// <summary>
        /// Result is OK
        /// </summary>
        OK,
        /// <summary>
        /// Result is Underrange
        /// </summary>
        Underrange,
        /// <summary>
        /// Result is Overrange
        /// </summary>
        Overrange
    }
}
