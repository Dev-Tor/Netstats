using Netstats.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class PageTests
    {
        [Fact]
        public void Create_WhenCalledWithInvalidContent_ReturnsEmptyPage()
        {
            var expected = Page.Create(null);
            Assert.Equal(expected, Page.Empty);
        }

        [Fact]
        public void Create_WhenCalledWithEmpty_ReturnsEmptyPage()
        {
            var expected = Page.Create(string.Empty);
            Assert.Equal(expected, Page.Empty);
        }
    }
}
