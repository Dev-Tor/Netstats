using AngleSharp.Dom.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public class Parsers
    {
        [ParserFor(PageType.Session)]
        public class SessionPageParser : IPageParser
        {
            public PageType For => PageType.Session;

            public string Parse(IHtmlDocument pageContent)
            {
                // The value of x should be the heading of field that is required eg. Upload, Download etc.
                Func<string, string> ParseValue = x =>
                {
                    return pageContent.GetAllSiblings("td.para1", x.EndsWith(":") ? x : x + ":")
                                      .Select(elem => elem.TextContent)
                                      .FirstOrDefault();
                };

                // Converts a data value to megabyte
                Func<string, double> ToMegabyte = input =>
                {
                    input = input.Trim().ToLowerInvariant();

                    if (input.EndsWith("mb"))
                    {
                        var value = input.Substring(0, input.IndexOf("mb"));
                        return double.Parse(value);
                    }

                    else if (input.EndsWith("gb"))
                    {
                        var value = input.Substring(0, input.IndexOf("gb"));
                        return double.Parse(value) * 1024;
                    }

                    else
                        return double.Parse(input) * 1024;
                };

                // Parse the session id from the page
                Func<string> ParseSesssionToken = () =>
                {
                    return pageContent.GetAllElements("meta")
                                      .GetElementsWithAttribute("content", "gi", matchExact: false)
                                      .FirstOrDefault()?
                                      .Attributes["content"].Value
                                      .Split('=')
                                      .LastOrDefault();
                };

                return JsonConvert.SerializeObject(
                    new
                    {
                    // The special Token associated with the session
                    Token = ParseSesssionToken(),
                    // The total amount of bandwidth allocated to the user in megabytes
                    Total = ToMegabyte(ParseValue("Group Allowed Bandwidth")),
                    // The amount of data used in megabytes
                    Used = ToMegabyte(ParseValue("Total Bandwidth")),
                    // The amount of data downloaded in megabytes
                    Download = ToMegabyte(ParseValue("Download")),
                    // The amount of data uploaded in megabytes
                    Upload = ToMegabyte(ParseValue("Upload")),
                    // The quota allocated to the user
                    QuotaType = ParseValue("Bandwidth Quota Schedule")
                    });
            }
        }
    }
}
