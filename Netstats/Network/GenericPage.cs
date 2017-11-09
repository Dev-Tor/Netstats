using AngleSharp.Dom.Html;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin (Edosakelvin@gmail.com) All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class GenericPage : IPage
    {
        public GenericPage(IHtmlDocument content)
        {
            Content = content;
        }

        public GenericPage(PageType type, IHtmlDocument content)
        {
            Type = type;
            Content = content;
        }

        public virtual PageType Type { get; set; }

        public virtual IHtmlDocument Content { get; set; }
    }
}