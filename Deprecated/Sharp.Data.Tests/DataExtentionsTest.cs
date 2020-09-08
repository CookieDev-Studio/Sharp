using System;
using Xunit;
using Sharp.Data.Depricated;
using System.IO;

namespace Sharp.Data.Tests.Depecrated
{
    public class DataExtentionsTest
    {
        [Fact]
        public void GetConnection()
        {
            var conection = DataExtentions.GetConnection();
            Exception exception = null;

            try { conection.Open(); }
            catch (Exception e) { exception = e; }
            finally { conection.Close(); }

            Assert.Null(exception);
        }
    }
}
