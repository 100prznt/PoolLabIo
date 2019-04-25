using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MeasurementTypeAttribute : Attribute
    {
        #region Properties
        public string DisplayName { get; set; }

        public string Unit { get; set; }

        public string Element { get; set; }

        public double MinResultValue { get; set; }

        public double MaxResultValue { get; set; }

        public int ResultDecimals { get; set; }
        #endregion Properties

        #region Constructor

        public MeasurementTypeAttribute(string displayName, string unit, string element, double minValue, double maxValue, int decimals)
        {
            DisplayName = displayName;
            Unit = unit;
            Element = element;
            MinResultValue = minValue;
            MaxResultValue = maxValue;
            ResultDecimals = decimals;
        }

        #endregion Constructor
    }
}
