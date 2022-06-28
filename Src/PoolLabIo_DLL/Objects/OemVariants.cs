namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available OEM variants of PoolLab
    /// See chapter "OEM Variants" on page 23 in "PoolLab 1.0 Bluetooth API" reference
    /// </summary>
    public enum OemVariants : ushort
    {
        /// <summary>
        /// Internal Version. No LCD output, no measurement types
        /// </summary>
        Internal_0 = 0,
        /// <summary>
        /// Original product. Measurement types 1- 13 and 23 - 24 are available
        /// </summary>
        PoolLab10 = 1,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Multitest9in1 = 2,
        /// <summary>
        /// Can only do measurement type 14
        /// </summary>
        IsoLab10 = 3,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Aquaviva = 4,
        /// <summary>
        /// Not yet released. Measurement types 15 - 24 are available
        /// </summary>
        FineWellPro = 5,
        /// <summary>
        /// Internal Version. OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Innovative = 6,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        FolkPool = 7,
        /// <summary>
        /// Internal Version. OEM ID provided by Water-i.d. GmbH
        /// </summary>
        WaterLab = 8,
        /// <summary>
        /// Internal Version. OEM ID provided by Water-i.d. GmbH
        /// </summary>
        CetEnviro = 9,
        /// <summary>
        /// Internal Version. OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Trace2O = 10,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        PoolSana = 11,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Dutrion = 12,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Spc = 13,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Steinbach = 14,
        /// <summary>
        /// Internal Version. OEM ID provided by Water-i.d. GmbH
        /// </summary>
        Internal_15 = 15,
        /// <summary>
        /// Same measurement types as OEM ID 1 (PoolLab)
        /// </summary>
        Evolution = 16
    }
}
