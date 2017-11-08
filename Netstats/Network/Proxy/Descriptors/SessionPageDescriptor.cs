using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class SessionPageDescriptor : IProxyDescriptor
    {
        static string descriptorMark = "Note : If your browser is inactive for more than 23 Hours";

        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
