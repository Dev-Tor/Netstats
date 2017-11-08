using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class LoggedOutPageDescriptor : IProxyDescriptor
    {
        static string descriptorMark = "You have been successfully Logged Out!!!";

        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
