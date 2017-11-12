using AngleSharp.Dom.Html;
using Netstats.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class DescriptorTests
    { 
        [Theory]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.BandwidthExceeded, Skip = "BandwidthExceeded mock file not available")]
        [InlineData(PageType.MaxUserSessionsReached, Skip = "MaxUserSessionsReached mock file not available")]
        public void IsMatch_WhenCalledWithValidContent_ReturnsTrue(PageType type)
        {
            var pageContent = DescriptorTestHelper.GetDummyPage(type);
            var descriptor  = PageDescriptorFactory.GetDescriptorFor(type);

            Assert.True(descriptor.IsMatch(pageContent));
        }

        [Theory]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.BandwidthExceeded)]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.MaxUserSessionsReached)]
        public void IsMatch_WhenCalledWithNullContent_ReturnsFalse(PageType type)
        {
            var descriptor = PageDescriptorFactory.GetDescriptorFor(type);

            Assert.False(descriptor.IsMatch(null));
        }
    }
}
