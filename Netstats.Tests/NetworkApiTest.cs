using Netstats.Network;
using NSubstitute;
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
        [Fact(DisplayName = "Login_WhenCalled_CallsMake")]
        public async Task Login_WhenCalled_ReturnsStringAsync()
        {
            string username = "john";
            string password = "doe";
            IRequestMaker mockRequestMaker = Substitute.For<IRequestMaker>();
            mockRequestMaker.Make("/cgi-bin/user_session.ggi", Arg.Any<Dictionary<string, string>>(), PageType.Session)
                            .Returns(Task.Run(() => "I'm fake"));
            NetworkApi api = new NetworkApi(mockRequestMaker);
            await api.LoginAsync(username, password);
            await mockRequestMaker.Received().Make("/cgi-bin/user_session.ggi", Arg.Any<Dictionary<string, string>>(), PageType.Session);
        }
    }
}
