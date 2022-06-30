using Rca.PoolLabIo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.MeasurementSeries
{
    public class SeriesBuilder
    {
        TimeSpan m_SeriesLifetime;
        List<Measurement> m_InnerMeasurementList;

        public int SeriesCount { get; set; }

        public SeriesBuilder(TimeSpan livetime)
        {
            m_SeriesLifetime = livetime;
            m_InnerMeasurementList = new List<Measurement>();
        }

        public void Add(Measurement measurement)
        {
            m_InnerMeasurementList.Add(measurement);
        }

        public void Add(IEnumerable<Measurement> measurements)
        {
            m_InnerMeasurementList.AddRange(measurements);
        }

        public List<Measurement> GetLatestSeries()
        {
            throw new NotImplementedException();
        }
    }
}
