using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin (Edosakelvin@gmail.com) All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class Page 
    {
        public Page(PageType type, IHtmlDocument content)
        {
            Type = type;
            Content = content;
        }

        public PageType Type { get; set; }

        public IHtmlDocument Content { get; set; }

        public static Page Create(IHtmlDocument content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            // Get descriptors for all page types except Unknown
            foreach (var descriptor in PageDescriptorFactory.GetAllDescriptors())
            {
                if (descriptor.IsMatch(content))
                {
                    var pageType = descriptor.For;
                    return new Page(pageType, content);
                }
            }

            // This line may change in the future as i'm not sure whether to throw an exception or just 
            // return a Page with PageType of Unknown
            return new Page(PageType.Unknown, content);
        }
    }
}