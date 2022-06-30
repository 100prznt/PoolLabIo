using Rca.PoolLabIo.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects
{
    public enum DeviceUnits : byte
    {
        [DeviceUnit("ppm")]
        PPM = 0x00,
        [DeviceUnit("mg/l")]
        MGL = 0x01
    }
}
