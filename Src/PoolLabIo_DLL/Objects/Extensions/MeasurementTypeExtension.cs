using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects.Extensions
{
    public static class MeasurementTypeExtension
    {
        public static string GetUnit(this MeasurementType measType)
        {
            Attribute[] attributes = measType.GetAttributes();

            MeasurementTypeAttribute attr = null;

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i].GetType() == typeof(MeasurementTypeAttribute))
                {
                    attr = (MeasurementTypeAttribute)attributes[i];
                    break;
                }
            }

            if (attr == null)
                return "";
            else
                return attr.DisplayUnit;

        }

        public static string GetDisplayName(this MeasurementType measType)
        {
            Attribute[] attributes = measType.GetAttributes();

            MeasurementTypeAttribute attr = null;

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i].GetType() == typeof(MeasurementTypeAttribute))
                {
                    attr = (MeasurementTypeAttribute)attributes[i];
                    break;
                }
            }

            if (attr == null)
                return measType.ToString();
            else
                return attr.DisplayName;
        }

        public static Attribute[] GetAttributes(this MeasurementType measType)
        {
            var fi = measType.GetType().GetField(measType.ToString());
            Attribute[] attributes = (Attribute[])fi.GetCustomAttributes(typeof(Attribute), false);

            return attributes;
        }
    }
}
