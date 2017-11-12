using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Netstats.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Tests
{
    public static class DescriptorTestHelper
    {
        static HtmlParser parser = new HtmlParser();

        public static IHtmlDocument GetDummyPage(PageType type)
        {
            var mockFilePath = Path.Combine(Environment.CurrentDirectory, "Mocks", type.ToString() + "Page.html");

            if(File.Exists(mockFilePath))
            {
                return parser.Parse(File.Open(mockFilePath, FileMode.Open)); 
            }

            return null;
        }
    }
}
