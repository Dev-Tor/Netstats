using Netstats.Core;
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
        public void Create_InvalidCredentials_ThrowsArgumnetException()
        {
            SessionManager manager = new SessionManager();
        }
    }
}
