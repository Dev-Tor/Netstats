using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
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

    public struct Page : IEquatable<Page>
    {
        static HtmlParser parser = new HtmlParser();

        public Page(PageType type, IHtmlDocument content)
        {
            Type = type;
            Content = content;
        }

        public PageType Type { get; }

        public IHtmlDocument Content { get; }

        public bool Equals(Page other)
        {
            return Type == other.Type && Content == other.Content;
        }

        public override bool Equals(object obj)
        {
            return Equals((Page)obj);
        }

        public override int GetHashCode()
        {
            return new { Type, Content }.GetHashCode();
        }

        public static Page Empty { get { return new Page(PageType.Unknown, null); } }
    }
}