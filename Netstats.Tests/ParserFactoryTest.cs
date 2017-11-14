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
        [Theory(DisplayName = "GetParser_WhenCalledValidPageType_ReturnsAppropriateDescriptor")]
        [InlineData(PageType.Session)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.BandwidthExceeded)]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.MaxUserSessionsReached)]
        public void GetParser_WhenCalledValidPageType_ReturnsAppropriateDescriptor(PageType type)
        {
            var factory = new PageDescriptorFactory();
            var descriptor = factory.GetDescriptorFor(type);
            var expectedType = descriptor.For;

            Assert.Equal(expectedType, type);
        }

        [Fact(DisplayName = "GetParser_WhenCalledWithUnknownPageType_ReturnsNull")]
        public void GetParser_WhenCalledWithUnknownPageType_ReturnsNull()
        {
            var factory = new PageDescriptorFactory();
            var expected = factory.GetDescriptorFor(PageType.Unknown);
            Assert.Null(expected);
        }
    }
}
