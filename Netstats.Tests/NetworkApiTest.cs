using Netstats.Network;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Netstats.Tests
{
    [TestFixture]
    public class NetworkApiTest
    {
        [Test]
        public void MakeRequest_WhenCalledWithNullData_ThrowsArgumentNullExceptionAsync()
        {
            NetworkApi api = new NetworkApi();
            Assert.Throws<AggregateException>(() => api.MakeRequest(null, CancellationToken.None).Wait());
        }
    }
}
