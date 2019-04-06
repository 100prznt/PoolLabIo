using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlCon
{
    /// <summary>
    /// Available measurement result types
    /// </summary>
    public enum MeasurementType : byte
    {
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
