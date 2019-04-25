using Rca.PoolLabIo.Objects.Extensions;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available measurement result types
    /// See chapter "Measurement Result Types" on page 17 in "PoolLab 1.0 Bluetooth API" reference
    /// </summary>
    public enum MeasurementType : byte
    {
        [MeasurementType("Gesamtchlor", "mg/l", "Cl2 (ppm)", 0, 0.6, 2)]
        TotalChlorine = 0x1,
        [MeasurementType("Ozon", "mg/l", "O3 (ppm)", 0, 4, 2)]
        Ozone,
        [MeasurementType("Chlordioxid", "mg/l", "ClO2 (ppm)", 0, 11.4, 1)]
        ChlorineDioxide,
        //removed
        [MeasurementType("Aktivsauerstoff (MPS) ", "mg/l", "O2 (ppm)", 0, 30, 1)]
        ActiveOxygen = 0x05,
        [MeasurementType("Brom", "mg/l", "Br (ppm)", 0, 13.5, 1)]
        Bromine,
        [MeasurementType("Wasserstoffperoxid ", "mg/l", "H2O2 (ppm)", 0, 2.9, 2)]
        HydrogenPeroxide,
        [MeasurementType("Chlor (frei)", "mg/l", "fCl (ppm)", 0, 6, 2)]
        FreeChlorine,
        [MeasurementType("pH-Wert", "pH", null, 6.5, 8.4, 2)]
        PH,
        [MeasurementType("Alkalinität (Säurekapazität)", "mg/l", "CaCO3 (ppm)", 0, 300, 0)]
        TotalAlkalinity,
        [MeasurementType("Cyanursäure", "mg/l", "Cya (ppm)", 0, 160, 0)]
        CyanuricAcid,
        HydrogenPeroxideHR,
        [MeasurementType("Härte (Gesamt)", "mg/l", "CaCO3 (ppm)", 0, 500, 1)]
        TotalHardnessHR,
        Isothiazilinone,
        NitriteLR,
        Nitrate,
        Phosphate,
        IronLR,
        DissolvedOxygen,
        Ammonia,
        Silica,
        Copper,
        [MeasurementType("Härte (Kalzium)", "mg/l", "CaCO3 (ppm)", 0, 500, 0)]
        Calcium,
        OzoneIpoChlorine
    }
}
