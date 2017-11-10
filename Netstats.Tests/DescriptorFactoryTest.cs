﻿using Netstats.Network;
using System;
using System.Linq;
using Xunit;

namespace Netstats.Tests
{
    public class DescriptorFactoryTest
    {
        /// <summary>
        ///  This test works on the basis that every page descriptor associated with a given <see cref="PageType"/> is
        ///  decorated with a <see cref="DescriptorForAttribute"/> which contains the <see cref="PageType"/> the descriptor 
        ///  is associated with
        /// </summary>
        /// <param name="type"> The page type</param>
        [Theory]
        [InlineData(PageType.AuthenticationFailed)]
        [InlineData(PageType.BandwidthExceeded)]
        [InlineData(PageType.ConfirmAction)]
        [InlineData(PageType.LoggedOut)]
        [InlineData(PageType.MaxUserSessionsReached)]
        [InlineData(PageType.Session)]
        public void GetDescriptor_WhenCalledValidPageType_ReturnsAppropriateDescriptor(PageType type)
        {
            var descriptor = PageDescriptorFactory.GetDescriptorFor(type);

            var expectedType = descriptor.GetType().GetCustomAttributes(false)
                                                   .Where(x => x is DescriptorForAttribute)
                                                   .Select(x => (x as DescriptorForAttribute).Type)
                                                   .FirstOrDefault();

            Assert.Equal(expectedType, type);
        }

        [Fact]
        public void GetDescriptor_WhenCalledWithUnknownPageType_ThrowsInvavlidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => PageDescriptorFactory.GetDescriptorFor(PageType.Unknown));
        }
    }
}
