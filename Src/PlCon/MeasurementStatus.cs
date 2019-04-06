using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlCon
{
    /// <summary>
    /// Available measurement status
    /// </summary>
    public enum MeasurementStatus : byte
    {
        OK,
        Underrange,
        Overrange
    }
}
