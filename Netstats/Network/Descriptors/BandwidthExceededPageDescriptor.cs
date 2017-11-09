using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class BandwidthExceededPageDescriptor : IPageDescriptor
    {
        static string descriptorMark = "Your Bandwidth quota is over";

        public bool IsMatch(IPage page)
        {
            return page.Content.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
