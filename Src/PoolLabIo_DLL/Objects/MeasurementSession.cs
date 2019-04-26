using Rca.PoolLabIo.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rca.PoolLabIo.Objects
{
    public class MeasurementSession : Collection<Measurement>
    {

        #region Members

        #endregion Members

        #region Poperties
        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp => this.Select(m => m.Timestamp).MedianDateTime();

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan SessionTimeSpan { get; set; }

        #endregion Poperties

        #region Constructor

        public MeasurementSession()
        {
            //m_InnerList = new List<Measurement>();
        }

        #endregion Constructor

        #region Services
        /// <summary>
        /// Add a new measurement item to the collection
        /// </summary>
        /// <param name="item">Measurement item</param>
        public new void Add(Measurement item)
        {
            TryAdd(item, out MeasurementAddResult result);

            if (result != MeasurementAddResult.Success)
                throw new ArgumentOutOfRangeException("Measurement timestamp is out of allowed timerange.");

        }

        /// <summary>
        /// Add a new measurement item to the collection
        /// </summary>
        /// <param name="item">Measurement item</param>
        public bool TryAdd(Measurement item, out MeasurementAddResult result)
        {
            result = MeasurementAddResult.Success;

            var allowedStartTime = DateTime.MinValue;
            var allowedEndTime = DateTime.MaxValue;

            if (this.Count == 1)
            {
                allowedStartTime = this[0].Timestamp - TimeSpan.FromMilliseconds(SessionTimeSpan.TotalMilliseconds / 2);
                allowedEndTime = allowedStartTime + SessionTimeSpan;
            }
            else
            {
                allowedStartTime = Timestamp - TimeSpan.FromMilliseconds(SessionTimeSpan.TotalMilliseconds / 2);
                allowedEndTime = allowedStartTime + SessionTimeSpan;
            }

            if (item.Timestamp < allowedStartTime)
            {
                result = MeasurementAddResult.StartTimeViolation;
                return false;
            }

            if (item.Timestamp > allowedEndTime)
            {
                result = MeasurementAddResult.EndTimeViolation;
                return false;
            }

            this.Add(item);
            return true;
        }

        /// <summary>
        /// Provides a collection of calculated measurements
        /// </summary>
        /// <returns></returns>
        public Collection<Measurement> GetCalculatedMeasurements()
        {
            var types = this.Select(m => m.Type).Distinct();

            throw new NotImplementedException();
        }

        public Measurement GetAverage(MeasurementType type)
        {
            var meas = new Measurement()
            {
                Id = ushort.MaxValue,
                Value = this.Where(m => m.Type == type).Average(mt => mt.Value),
                Timestamp = this.Timestamp,
                Type = type,
                Status = MeasurementStatus.Calculated
            };

            return meas;
        }

        /// <summary>
        /// Provides a summary of all measurements
        /// </summary>
        /// <returns>Measurement collection, contains only one measurement item per type</returns>
        public Collection<Measurement> GetSummary()
        {
            var types = this.Select(m => m.Type).Distinct();

            var summary = new Collection<Measurement>();

            foreach (var type in types)
                summary.Add(GetAverage(type));

            foreach (var calcMeas in GetCalculatedMeasurements())
                summary.Add(calcMeas);

            return summary;
        }

        #endregion Services
    }
}
