using Netstats.Network.Proxy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netstats.Network;
using Netstats.Network.Proxy.Descriptors;

namespace Netstats.Tests
{
    [TestFixture]
    public class DescriptorFactoryTest
    {
        [Test]
        [TestCase(PageType.AuthenticationFailed, typeof(AuthenticationFailedPageDescriptor))]
        [TestCase(PageType.BandwidthExceeded, typeof(BandwidthExceededPageDescriptor))]
        [TestCase(PageType.ConfirmAction, typeof(ConfirmActionPageDescriptor))]
        [TestCase(PageType.LoggedOut, typeof(LoggedOutPageDescriptor))]
        [TestCase(PageType.MaxUserSessionsReached, typeof(MaxUserSessionsReachedPageDescriptor))]
        [TestCase(PageType.Session, typeof(SessionPageDescriptor))]
        public void GetDescriptor_WhenCalledValidPageType_ReturnsAppropriateDescriptor(PageType type, Type descriptorType)
        {
            PageDescriptorFactory factory = new PageDescriptorFactory();
            var descriptor = factory.GetDesciptor(type);
            var expected = descriptor.GetType(); 

            Assert.AreEqual(descriptorType, expected);
        }

        [Test]
        public void GetDescriptor_WhenCalledWithUnknwonPageType_ThrowsInvavlidOperationException()
        {
            PageDescriptorFactory factory = new PageDescriptorFactory();

            Assert.Throws<InvalidOperationException>(() => factory.GetDesciptor(PageType.Unknown));
        }
    }
}
