using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MeasurementTypeAttribute : Attribute
    {
        #region Properties
        public string DisplayName { get; set; }

        public string DisplayUnit { get; set; }

        public double MinResultValue { get; set; }

        public double MaxResultValue { get; set; }

        public int ResultDecimals { get; set; }
        #endregion Properties

        #region Constructor

        public MeasurementTypeAttribute(string displayName, string displayUnit, double minValue, double maxValue, int decimals)
        {
            DisplayName = displayName;
            DisplayUnit = displayUnit;
            MinResultValue = minValue;
            MaxResultValue = maxValue;
            ResultDecimals = decimals;
        }

        #endregion Constructor
    }
}
