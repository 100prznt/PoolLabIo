using Rca.PoolLabIo.Objects.Extensions;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available measurement result types
    /// See chapter "Measurement Result Types" on page 17 in "PoolLab 1.0 Bluetooth API" reference
    /// </summary>
    public enum MeasurementType : byte
    {
        [MeasurementType("Gesamtchlor", "Cl2 (ppm)", 0, 0.6, 2)]
        TotalChlorine = 0x1,
        [MeasurementType("Ozon", "O3 (ppm)", 0, 4, 2)]
        Ozone,
        ChlorineDioxide,
        //removed
        [MeasurementType("Aktivsauerstoff", "O2 (ppm)", 0, 30, 1)]
        ActiveOxygen = 0x05,
        Bromine,
        HydrogenPeroxide,
        FreeChlorine,
        [MeasurementType("pH-Wert", "pH", 6.5, 8.4, 2)]
        PH,
        TotalAlkalinity,
        HydrogenPeroxideHR,
        TotalHardnessHR,
        Isothiazilinone,
        NitriteLR,
        Nitrate,
        Phosphate,
        IronLR,
        DissolvedOxygen,
        Ammonia,
        Silica,
        [MeasurementType("Kupfer", "Cu (ppm)", 0, 5, 2)]
        Copper,
        Calcium,
        OzoneIpoChlorine
    }
}
