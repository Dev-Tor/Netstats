using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class BandwidthExceededPageDescriptor : IProxyDescriptor
    {
        static string descriptorMark = "Your Bandwidth quota is over";

        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
