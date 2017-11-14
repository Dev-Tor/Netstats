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
        [Theory(DisplayName = "IsMatch_WhenCalledWithValidContent_ReturnsTrue")]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.AuthenticationFailed)]
        //[InlineData(PageType.BandwidthExceeded, Skip = "BandwidthExceeded mock file not available")]
        //[InlineData(PageType.MaxUserSessionsReached, Skip = "MaxUserSessionsReached mock file not available")]
        public void IsMatch_WhenCalledWithValidContent_ReturnsTrue(PageType type)
        { 
            IPageDescriptorFactory factory = new PageDescriptorFactory();
            var pageContent = DescriptorTestHelper.GetDummyPage(type);
            var descriptor  = factory.GetDescriptorFor(type);

            var expected = descriptor.IsMatch(pageContent);

            Assert.True(expected);
        }

        [Theory(DisplayName = "IsMatch_WhenCalledWithNull_ReturnsFalse")]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.BandwidthExceeded)]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.MaxUserSessionsReached)]
        public void IsMatch_WhenCalledWithNullContent_ReturnsFalse(PageType type)
        {
            IPageDescriptorFactory factory = new PageDescriptorFactory();
            var descriptor = factory.GetDescriptorFor(type);

            var expected = descriptor.IsMatch(null);

            Assert.False(expected);
        }
    }
}
