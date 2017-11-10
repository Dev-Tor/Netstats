using Netstats.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class NetworkApiTest
    {
        [Fact]
        public void MakeRequest_WhenCalledWithNullData_ThrowsArgumentNullExceptionAsync()
        {
            NetworkApi api = new NetworkApi();
            Assert.Throws<AggregateException>(() => api.MakeRequest(null, CancellationToken.None).Wait());
        }
    }
}
