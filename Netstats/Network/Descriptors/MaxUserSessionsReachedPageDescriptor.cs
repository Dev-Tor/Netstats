using AngleSharp.Dom.Html;
using System.Linq;

namespace Netstats.Network.Proxy.Descriptors
{
    public class MaxUserSessionsReachedPageDescriptor : IPageDescriptor
    {
        static string descriptorMark = "The no of UserSense session of User:";

        public bool IsMatch(IPage page)
        {
            return page.Content.GetAllElements("p")
                       .GetElementsWithContent(descriptorMark, matchExact: false)
                       .Any();
        }
    }
}
