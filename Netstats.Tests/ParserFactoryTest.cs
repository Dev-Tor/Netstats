using Netstats.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class ParserFactoryTest
    {
        /// <summary>
        ///  This test works on the basis that every page descriptor associated with a given <see cref="PageType"/> is
        ///  decorated with a <see cref="DescriptorForAttribute"/> which contains the <see cref="PageType"/> the descriptor 
        ///  is associated with
        /// </summary>
        /// <param name="type"> The page type</param>
        [Theory]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.BandwidthExceeded)]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.MaxUserSessionsReached)]
        public void GetParser_WhenCalledValidPageType_ReturnsAppropriateDescriptor(PageType type)
        {
            var descriptor = PageDescriptorFactory.GetDescriptorFor(type);
            var expectedType = descriptor.For;

            Assert.Equal(expectedType, type);
        }

        [Fact]
        public void GetParser_WhenCalledWithUnknownPageType_ReturnsNull()
        {
            Assert.Null(PageDescriptorFactory.GetDescriptorFor(PageType.Unknown));
        }
    }
}
