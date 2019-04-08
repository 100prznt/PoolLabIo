using Rca.PoolLabIo.Objects.Extensions;

namespace Rca.PoolLabIo.Objects
{
    /// <summary>
    /// Available measurement result types
    /// </summary>
    public enum MeasurementType : byte
    {
        [MeasurementType("Gesamtchlor", "Cl2 (ppm)", 0, 0.6, 2)]
        TotalChlorine = 0x1,
        Ozone,
        ChlorineDioxide,
        //removed
        ActiveOxygen = 0x05,
        Bromine,
        HydrogenPeroxide,
        FreeChlorine,
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
        Copper,
        Calcium,
        OzoneIpoChlorine
    }
}
