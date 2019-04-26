using System;
using System.Collections.Generic;
using System.Linq;

namespace Rca.PoolLabIo.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime MedianDateTime(this IEnumerable<DateTime> dateTime)
        {
            var startTime = dateTime.Min();
            var span = dateTime.Max() - startTime;

            return startTime + TimeSpan.FromMilliseconds(span.TotalMilliseconds / 2);
        }
    }
}
