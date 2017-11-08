using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class ConfirmActionPageDescriptor : IProxyDescriptor
    {
        public bool IsMatch(IHtmlDocument page)
        {
            return page.GetAllElements("form")
                       .GetElementsWithAttribute("name", "confirmaction", matchExact: true)
                       .Any();
        }
    }
}
