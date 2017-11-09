using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class AuthenticationFailedPageDescriptor : IPageDescriptor
    {
        static string descriptorMark = "Authentication Failed";

        public bool IsMatch(IPage page)
        {
            return page.Content.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
