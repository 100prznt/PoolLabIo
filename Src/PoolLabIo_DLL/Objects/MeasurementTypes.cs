using Rca.PoolLabIo.Objects.Extensions;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available measurement result types
    /// See chapter "Measurement Result Types" on page 21 ff in "PoolLab 1.0 Bluetooth API" reference
    /// </summary>
    public enum MeasurementTypes : byte
    {
        [MeasurementType("Gesamtchlor", true, "Cl2", 0, 0.6, 2)]
        TotalChlorine = 1,
        [MeasurementType("Ozon", true, "O3", 0, 4, 2)]
        Ozone = 2,
        [MeasurementType("Chlordioxid", true, "ClO2", 0, 11.4, 1)]
        ChlorineDioxide = 3,
        //4 removed
        [MeasurementType("Aktivsauerstoff (MPS) ", true, "O2", 0, 30, 1)]
        ActiveOxygen = 5,
        [MeasurementType("Brom", true, "Br", 0, 13.5, 1)]
        Bromine = 6,
        [MeasurementType("Wasserstoffperoxid ", true, "H2O2", 0, 2.9, 2)]
        HydrogenPeroxide = 7,
        [MeasurementType("Chlor (frei)", true, "fCl", 0, 6, 2)]
        FreeChlorine = 8,
        [MeasurementType("pH-Wert", false, "pH", 6.5, 8.4, 2)]
        PH = 9,
        [MeasurementType("Alkalinität (Säurekapazität)", true, "CaCO3", 0, 300, 0)]
        TotalAlkalinity = 10,
        [MeasurementType("Cyanursäure", true, "Cya", 0, 160, 0)]
        CyanuricAcid = 11,
        [MeasurementType("Hydrogen Peroxide HR", true, "H2O2", 0, 200, 0)]
        HydrogenPeroxideHR = 12,
        [MeasurementType("Härte (Gesamt)", true, "CaCO3", 0, 500, 1)]
        TotalHardnessHR = 13,
        Isothiazilinone = 14, //only available on ISOLab 1.0 OEM
        NitriteLR = 15, //currently in development for FinWell Pro OEM
        Nitrate = 16, //currently in development for FinWell Pro OEM
        Phosphate = 17, //currently in development for FinWell Pro OEM
        IronLR = 18, //currently in development for FinWell Pro OEM
        DissolvedOxygen = 19, //currently in development for FinWell Pro OEM
        Ammonia = 20, //currently in development for FinWell Pro OEM
        Silica = 21, //currently in development for FinWell Pro OEM
        Copper = 22, //currently in development for FinWell Pro OEM
        [MeasurementType("Härte (Kalzium)", true, "CaCO3", 0, 500, 0)]
        Calcium = 23,
        [MeasurementType("Ozone i.p.o. Chlorine", true, "O3", 0, 4, 2)]
        OzoneIpoChlorine = 24,
        [MeasurementType("Magnesium", true, "Mg", 0, 100, 0)]
        Magnesium = 25,
        [MeasurementType("Kalium", true, "K", 0.8, 12, 1)]
        Potassium = 26,
        PH_HR = 27, //currently in development for FinWell Pro OEM
        PH_LR = 28, //currently in development for FinWell Pro OEM
        PH_HR_Saltwater = 29, //currently in development for FinWell Pro OEM
        PH_HR_Seawater = 30, //currently in development for FinWell Pro OEM
        PH_LR_Saltwater = 31, //currently in development for FinWell Pro OEM
        PH_LR_Seawater = 32, //currently in development for FinWell Pro OEM
        PH_MR_Saltwater = 33, //currently in development for FinWell Pro OEM
        PH_MR_Seawater = 34, //currently in development for FinWell Pro OEM
        [MeasurementType("Härte (Gesamt)", true, "CaCO3", 0, 200, 0)]
        TotalHardness = 35,
        PH_MR = 36, //currently in development for FinWell Pro OEM
        Iodine = 37, //currently in development for FinWell Pro OEM
        [MeasurementType("Harnstoff ", true, "CH4N2O", 0, 2.51, 2)]
        Urea = 38,
        [MeasurementType("PHMB", true, "PHMB", 5, 60, 0)]
        PHMB = 39,
        [MeasurementType("Alkalinität (Meerwasser)", true, "TA", 0, 200, 0)]
        TotalAlkalinity_Seawater = 40,
        [MeasurementType("Gesamtchlor", true, "tCl", 0.03, 8, 2)]
        TotalChlorine_Liquid = 41,
        [MeasurementType("Ozon", true, "O3", 0, 4.07, 2)]
        Ozone_Liquid = 42,
        [MeasurementType("Chlordioxid", true, "ClO2", 0, 11.42, 2)]
        ChlorineDioxide_Liquid = 43,
        [MeasurementType("Aktivsauerstoff (MPS) ", true, "O2", 0, 30, 1)]
        ActiveOxygen_Liquid = 44,
        [MeasurementType("Brom", true, "Br", 0, 13.55, 1)]
        Bromine_Liquid = 45,
        [MeasurementType("Hydrogen Peroxide", true, "H2O2", 0, 2.9, 0)]
        HydrogenPeroxide_Lliquid = 46,
        [MeasurementType("Chlor (frei)", true, "fCl", 0.03, 8, 2)]
        FreeChlorine_Liquid = 47,
        [MeasurementType("pH-Wert", false, "pH", 6.5, 8.4, 2)]
        PH_Liquid = 48,
        [MeasurementType("Ozone i.p.o. Chlorine", true, "O3", 0, 4.07, 2)]
        OzoneIpoChlorine_Liquid = 49,
        /// <summary>
        /// Nicht in BLE Doku spezifiziert!
        /// </summary>
        BoundChlorine = 128,
    }
}
