using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.MeasurementSeries
{
    public class SeriesSettings
    {
        public TimeSpan Lifetime { get; set; }

        public TimeSpan AllowedCyaAge { get; set; }

        public TimeSpan AllowedTaAge { get; set; }

        public static SeriesSettings Default => new()
        {
            Lifetime = TimeSpan.FromHours(1),
            AllowedCyaAge = TimeSpan.FromDays(14),
            AllowedTaAge = TimeSpan.FromDays(14)
        };
    }
}
