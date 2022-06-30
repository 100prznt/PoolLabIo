using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects.Extensions
{
    public static class DeviceUnitExtensions
    {

        public static string GetSymbol(this DeviceUnits measType)
        {
            var attributes = measType.GetAttributes();

            DeviceUnitAttribute attr = null;

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i].GetType() == typeof(DeviceUnitAttribute))
                {
                    attr = (DeviceUnitAttribute)attributes[i];
                    break;
                }
            }

            if (attr == null)
                return measType.ToString();
            else
                return attr.Symbol;
        }

        public static Attribute[] GetAttributes(this DeviceUnits measType)
        {
            var fi = measType.GetType().GetField(measType.ToString());
            Attribute[] attributes = (Attribute[])fi.GetCustomAttributes(typeof(Attribute), false);

            return attributes;
        }
    }
}
