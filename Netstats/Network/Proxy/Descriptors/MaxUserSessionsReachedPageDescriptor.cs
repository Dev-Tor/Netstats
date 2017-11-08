using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class MaxUserSessionsReachedPageDescriptor : IProxyDescriptor
    {
        static string descriptorMark = "The no of UserSense session of User:";

        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
