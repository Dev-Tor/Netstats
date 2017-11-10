using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public static class IHtmlDoucumentMixins
    {
        public static IEnumerable<IElement> GetAllSiblings(this IHtmlDocument document, string selector, string value)
        {
            if (document == null)
                return Enumerable.Empty<IElement>();
            return document.QuerySelectorAll(selector).Where(x => x.TextContent.Trim().ToLowerInvariant().Contains(value.Trim().ToLowerInvariant()))
                                                      .SelectMany(x => ((IElement)x.Parent).Children)
                                                      .Where(x => x.TextContent != value);
        }
    }
}
