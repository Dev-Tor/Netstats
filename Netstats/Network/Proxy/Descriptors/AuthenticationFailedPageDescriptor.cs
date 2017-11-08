using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class AuthenticationFailedPageDescriptor : IProxyDescriptor
    {
        static string descriptorMark = "Authentication Failed";

        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
