using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netstats.Network.Mixins;

namespace Netstats.Network
{
    public class Descriptors
    {
        [DescriptorFor(PageType.AuthenticationFailed)]
        public class AuthenticationFailedDescriptor : IPageDescriptor
        {
            static string descriptorMark = "Authentication Failed";

            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("p")
                           .GetElementsWithContent(descriptorMark, matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.BandwidthExceeded)]
        public class BandwidthExceededDescriptor : IPageDescriptor
        {
            static string descriptorMark = "Your Bandwidth quota is over";

            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("p")
                           .GetElementsWithContent(descriptorMark, matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.ConfirmAction)]
        public class ConfirmActionDescriptor : IPageDescriptor
        {
            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("form")
                           .GetElementsWithAttribute("name", "confirmaction", matchExact: true)
                           .Any();
            }
        }

        [DescriptorFor(PageType.LoggedOut)]
        public class LoggedOutDescriptor : IPageDescriptor
        {
            static string descriptorMark = "You have been successfully Logged Out!!!";

            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("p")
                           .GetElementsWithContent(descriptorMark, matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.MaxUserSessionsReached)]
        public class MaxUserSessionsReachedPageDescriptor : IPageDescriptor
        {
            static string descriptorMark = "The no of UserSense session of User:";

            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("p")
                           .GetElementsWithContent(descriptorMark, matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.Session)]
        public class SessionPageDescriptor : IPageDescriptor
        {
            static string descriptorMark = "Note : If your browser is inactive for more than 23 Hours";

            public bool IsMatch(IPage page)
            {
                return page.GetAllElements("p")
                           .GetElementsWithContent(descriptorMark, matchExact: false)
                           .Any();
            }
        }
    }
}
