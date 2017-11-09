using Netstats.Network;
using NUnit.Framework;
using System;
using System.Linq;

namespace Netstats.Tests
{
    [TestFixture]
    public class DescriptorFactoryTest
    {
        /// <summary>
        ///  This test works on the basis that every page descriptor associated with a given <see cref="PageType"/> is
        ///  decorated with a <see cref="DescriptorForAttribute"/> which contains the <see cref="PageType"/> the descriptor 
        ///  is associated with
        /// </summary>
        /// <param name="type"> The page type</param>
        [Test]
        [TestCase(PageType.AuthenticationFailed)]
        [TestCase(PageType.BandwidthExceeded)]
        [TestCase(PageType.ConfirmAction)]
        [TestCase(PageType.LoggedOut)]
        [TestCase(PageType.MaxUserSessionsReached)]
        [TestCase(PageType.Session)]
        public void GetDescriptor_WhenCalledValidPageType_ReturnsAppropriateDescriptor(PageType type)
        {
            PageDescriptorFactory factory = new PageDescriptorFactory();
            var descriptor = factory.GetDesciptor(type);

            var expectedType = descriptor.GetType().GetCustomAttributes(false)
                                                   .Where(x => x is DescriptorForAttribute)
                                                   .Select(x => (x as DescriptorForAttribute).Type)
                                                   .FirstOrDefault();

            Assert.AreEqual(expectedType, type);
        }

        [Test]
        public void GetDescriptor_WhenCalledWithUnknownPageType_ThrowsInvavlidOperationException()
        {
            PageDescriptorFactory factory = new PageDescriptorFactory();

            Assert.Throws<InvalidOperationException>(() => factory.GetDesciptor(PageType.Unknown));
        }
    }
}
