using Netstats.Core;
using Netstats.Core.Exceptions;
using Netstats.Network;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class SessionManagerTest
    {
        [Fact]
        public async Task Create_InvalidUsernameOrPassword_ThrowsArgumentExceptionAsync()
        {
            // no need to mock network client as it's not needed
            SessionManager manager = new SessionManager(null);
            await Assert.ThrowsAsync<ArgumentException>(() => manager.Create(null, null));
        }

        [Fact]
        public async Task Create_ValidUsernameAndPassoword_CreatesSession()
        {
            var prebuiltJson = JsonConvert.SerializeObject(new LoginResult("qwerty", UserQuotaType.Monthly));
            var mockClinet = Substitute.For<INetworkApi>();
            mockClinet.LoginAsync("john", "smith").Returns(prebuiltJson);
            SessionManager sessionManager = new SessionManager(mockClinet);
            await sessionManager.Create("john", "smith");

            Assert.NotNull(sessionManager.Current);
        }

        [Fact]
        public async Task Create_ApiThrowsLoginFailedException_ThrowsLoginFailedException()
        {
            var mockClinet = Substitute.For<INetworkApi>();
            mockClinet.When(x => x.LoginAsync(Arg.Any<string>(), Arg.Any<string>()))
                      .Do(x => throw new LoginFailedException(LoginFailReason.AuthenticationError));

            SessionManager sessionManager = new SessionManager(mockClinet);
            await Assert.ThrowsAsync<LoginFailedException>(() => sessionManager.Create("john", "smith"));
        }

        [Fact]
        public async Task DestroyCurrent_WhenCalled_CallsLogoutInApi()
        {
            var mockClinet = Substitute.For<INetworkApi>();
            var mockSession = Substitute.For<ISession>();
            mockSession.Token.Returns("e4dd99ae701");

            SessionManager sessionManager = new SessionManager(mockClinet)
            {
                Current = mockSession
            };

            await sessionManager.DestroyCurrent();

            await mockClinet.Received().Logout("e4dd99ae701");
        }

        [Fact]
        public async Task DestroyCurrent_WhenCalled_CallsDisposeOnSession()
        {
            var mockClinet = Substitute.For<INetworkApi>();
            var mockSession = Substitute.For<ISession>();
            mockSession.Token.Returns("e4dd99ae701");

            SessionManager sessionManager = new SessionManager(mockClinet)
            {
                Current = mockSession
            };

            await sessionManager.DestroyCurrent();

            mockSession.Received().Dispose();
        }
    }
}
