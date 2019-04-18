
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rca.PoolLabIo.Helpers;

namespace Rca.PoolLabIo_Test
{
    [TestClass]
    public class UnixTimeConverter_Test
    {
        [TestMethod]
        public void Convert()
        {
            var currentDateTime = DateTime.Now;
            var unixTimeStamp = currentDateTime.ToUnixTime();
            var backConvertedUnixTimeStamp = UnixTimeConverter.UnixTimeToDateTime(unixTimeStamp);

            Assert.AreEqual(currentDateTime.DayOfYear, backConvertedUnixTimeStamp.DayOfYear, 0, "DayOfYear");
            Assert.AreEqual(currentDateTime.Hour, backConvertedUnixTimeStamp.Hour, 0, "Hour");
            Assert.AreEqual(currentDateTime.Minute, backConvertedUnixTimeStamp.Minute, 0, "Minute");
            Assert.AreEqual(currentDateTime.Second, backConvertedUnixTimeStamp.Second, 0, "Second");
        }
    }
}
