using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class ConfirmActionPageDescriptor : IPageDescriptor
    {
        public bool IsMatch(IPage page)
        {
            return page.Content.GetAllElements("form")
                       .GetElementsWithAttribute("name", "confirmaction", matchExact: true)
                       .Any();
        }
    }
}
