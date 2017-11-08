using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System.Collections.Generic;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin @Velvetta Inc.  All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public static class Extensions
    {
        public static IHtmlCollection<IElement> GetAllElements(this IHtmlDocument document, string selectors)
        {
            return document.QuerySelectorAll(selectors);
        }

        public static IEnumerable<IElement> GetElementsWithContent(this IHtmlCollection<IElement> collection, string content,bool ignoreCase = false, bool matchExact = false)
        {
            return collection.Where(elem =>
            {
                var buffer = elem.TextContent;

                if (ignoreCase)
                {
                    buffer = buffer.ToLowerInvariant();
                    content  = content.ToLowerInvariant();
                }

                return matchExact ? buffer == content : buffer.Contains(content);
            });
        }

        public static IEnumerable<IElement> GetElementsWithAttribute(this IHtmlCollection<IElement> collection, string attributeName, string attributeValue, bool matchExact = false)
        {
            return collection.Where(elem =>
            {
                return elem.Attributes
                           .Any(attribute =>
                           {
                               var foundName = matchExact ? attribute.Name == attributeName : attribute.Name.Contains(attributeName);
                               var foundValue = matchExact ? attribute.Value == attributeValue : attribute.Value.Contains(attributeValue);
                               return foundName && foundValue;
                           });
            });
        }

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
