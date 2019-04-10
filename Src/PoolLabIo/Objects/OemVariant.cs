namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available OEM variants of PoolLab
    /// </summary>
    public enum OemVariant : byte
    {
        /// <summary>
        /// Internal Version. No LCD output, no measurement types
        /// </summary>
        Internal,
        /// <summary>
        /// Original product. Measurement types 1- 13 and 23 - 24 are available
        /// </summary>
        PoolLab10,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Multitest9in1,
        /// <summary>
        /// Can only do measurement type 14
        /// </summary>
        IsoLab10,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Aquaviva,
        /// <summary>
        /// Not yet released. Measurement types 15 - 24 are available
        /// </summary>
        FineWellPro
    }
}
