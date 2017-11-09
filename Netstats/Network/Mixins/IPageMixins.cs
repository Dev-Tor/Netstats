using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network.Mixins
{
    public static class IPageMixins
    {
        public static IHtmlCollection<IElement> GetAllElements(this IPage page, string selectors = null) => page.Content.QuerySelectorAll(selectors);
    }
}
