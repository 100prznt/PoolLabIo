namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available OEM variants of PoolLab
    /// See chapter "OEM Variants" on page 18 in "PoolLab 1.0 Bluetooth API" reference
    /// </summary>
    public enum OemVariant : byte
    {
        /// <summary>
        /// Internal Version. No LCD output, no measurement types
        /// </summary>
        Internal = 0x00,
        /// <summary>
        /// Original product. Measurement types 1- 13 and 23 - 24 are available
        /// </summary>
        PoolLab10 = 0x01,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Multitest9in1 = 0x02,
        /// <summary>
        /// Can only do measurement type 14
        /// </summary>
        IsoLab10 = 0x03,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Aquaviva = 0x04,
        /// <summary>
        /// Not yet released. Measurement types 15 - 24 are available
        /// </summary>
        FineWellPro = 0x05,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Innovative = 0x06,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        FalkPool = 0x07,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        WaterLab = 0x08,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        CetEnviro = 0x09,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Trace2O = 0x0A,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        PoolSana = 0x0B,
        /// <summary>
        /// Unknown, OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Dutrion = 0x0C
    }
}
