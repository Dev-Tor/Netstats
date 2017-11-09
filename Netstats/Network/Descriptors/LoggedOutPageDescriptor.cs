using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class LoggedOutPageDescriptor : IPageDescriptor
    {
        static string descriptorMark = "You have been successfully Logged Out!!!";

        public bool IsMatch(IPage page)
        {
            return page.Content.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
