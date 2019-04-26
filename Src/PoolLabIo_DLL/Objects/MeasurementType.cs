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
        TotalChlorine = 1,
        [MeasurementType("Ozon", "mg/l", "O3 (ppm)", 0, 4, 2)]
        Ozone = 2,
        [MeasurementType("Chlordioxid", "mg/l", "ClO2 (ppm)", 0, 11.4, 1)]
        ChlorineDioxide = 3,
        //removed
        [MeasurementType("Aktivsauerstoff (MPS) ", "mg/l", "O2 (ppm)", 0, 30, 1)]
        ActiveOxygen = 5,
        [MeasurementType("Brom", "mg/l", "Br (ppm)", 0, 13.5, 1)]
        Bromine = 6,
        [MeasurementType("Wasserstoffperoxid ", "mg/l", "H2O2 (ppm)", 0, 2.9, 2)]
        HydrogenPeroxide = 7,
        [MeasurementType("Chlor (frei)", "mg/l", "fCl (ppm)", 0, 6, 2)]
        FreeChlorine = 8,
        [MeasurementType("pH-Wert", "pH", null, 6.5, 8.4, 2)]
        PH = 9,
        [MeasurementType("Alkalinität (Säurekapazität)", "mg/l", "CaCO3 (ppm)", 0, 300, 0)]
        TotalAlkalinity = 10,
        [MeasurementType("Cyanursäure", "mg/l", "Cya (ppm)", 0, 160, 0)]
        CyanuricAcid = 11,
        HydrogenPeroxideHR = 12,
        [MeasurementType("Härte (Gesamt)", "mg/l", "CaCO3 (ppm)", 0, 500, 1)]
        TotalHardnessHR = 13,
        Isothiazilinone = 14,
        NitriteLR = 15,
        Nitrate = 16,
        Phosphate = 17,
        IronLR = 18,
        DissolvedOxygen = 19,
        Ammonia = 20,
        Silica = 21,
        Copper = 22,
        [MeasurementType("Härte (Kalzium)", "mg/l", "CaCO3 (ppm)", 0, 500, 0)]
        Calcium = 23,
        OzoneIpoChlorine = 24,
        [MeasurementType("Gebnundenes Chlor", "mg/l", "bCl (ppm)", double.NaN, double.NaN, 0)]
        BoundChlorine = 128
    }
}
