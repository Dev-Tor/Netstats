using AngleSharp.Dom;
using System.Collections.Generic;
using System.Linq;

namespace Netstats.Network
{
    public static class IHtmlCollectionMixins
    {
        public static IEnumerable<IElement> GetElementsWithContent(this IHtmlCollection<IElement> collection, string content, bool ignoreCase = false, bool matchExact = false)
        {
            return collection.Where(elem =>
            {
                var buffer = elem.TextContent;

                if (ignoreCase)
                {
                    buffer = buffer.ToLowerInvariant();
                    content = content.ToLowerInvariant();
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

    }
}
