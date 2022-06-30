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
        /// <summary>
        /// Name of the masurement parameter
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Measurement value has an unit
        /// </summary>
        public bool HasUnit { get; set; }

        /// <summary>
        /// Element wich are measured
        /// </summary>
        public string Element { get; set; }

        public double MinResultValue { get; set; }

        public double MaxResultValue { get; set; }

        public int ResultDecimals { get; set; }
        #endregion Properties

        #region Constructor
        /// <summary>
        /// Provide additional infos about the measurement type
        /// </summary>
        /// <param name="parameterName">Name of the masurement parameter</param>
        /// <param name="hasUnit">Measurement value has an unit</param>
        /// <param name="element">Element wich are measured</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="decimals"></param>
        public MeasurementTypeAttribute(string parameterName, bool hasUnit, string element, double minValue, double maxValue, int decimals)
        {
            ParameterName = parameterName;
            HasUnit = hasUnit;
            Element = element;
            MinResultValue = minValue;
            MaxResultValue = maxValue;
            ResultDecimals = decimals;
        }

        #endregion Constructor
    }
}
