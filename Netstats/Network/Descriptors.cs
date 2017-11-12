using System;
using System.Linq;
using AngleSharp.Dom.Html;

namespace Netstats.Network
{
    public class Descriptors
    {
        [DescriptorFor(PageType.AuthenticationFailed)]
        public class AuthenticationFailedDescriptor : IPageDescriptor
        {
            public PageType For => PageType.AuthenticationFailed;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("p")
                           .GetElementsWithContent("Authentication Failed", matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.BandwidthExceeded)]
        public class BandwidthExceededDescriptor : IPageDescriptor
        {
            public PageType For => PageType.BandwidthExceeded;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("p")
                           .GetElementsWithContent("Your Bandwidth quota is over", matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.ConfirmAction)]
        public class ConfirmActionDescriptor : IPageDescriptor
        {
            public PageType For => PageType.ConfirmAction;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("form")
                           .GetElementsWithAttribute("name", "confirmaction", matchExact: true)
                           .Any();
            }
        }

        [DescriptorFor(PageType.LoggedOut)]
        public class LoggedOutDescriptor : IPageDescriptor
        { 
            public PageType For => PageType.LoggedOut;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("p")
                           .GetElementsWithContent("You have been successfully Logged Out!!!", matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.MaxUserSessionsReached)]
        public class MaxUserSessionsReachedPageDescriptor : IPageDescriptor
        {
            public PageType For => PageType.MaxUserSessionsReached;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("p")
                           .GetElementsWithContent("The no of UserSense session of User:", matchExact: false)
                           .Any();
            }
        }

        [DescriptorFor(PageType.Session)]
        public class SessionPageDescriptor : IPageDescriptor
        {
            public PageType For => PageType.Session;

            public bool IsMatch(IHtmlDocument pageContent)
            {
                if (pageContent == null)
                    return false;

                return pageContent.GetAllElements("p")
                           .GetElementsWithContent("Note : If your browser is inactive for more than 23 Hours", matchExact: false)
                           .Any();
            }
        }
    }
}
