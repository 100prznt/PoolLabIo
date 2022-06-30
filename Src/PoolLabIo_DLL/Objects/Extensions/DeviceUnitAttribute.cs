using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DeviceUnitAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Symbol of the unit
        /// </summary>
        public string Symbol { get; set; }
        #endregion Properties

        #region Constructor
        /// <summary>
        /// Provide additional infos about the measurement unit
        /// </summary>
        /// <param name="symbol">Symbol of the unit</param>
        public DeviceUnitAttribute(string symbol)
        {
            Symbol = symbol;
        }

        #endregion Constructor
    }
}
