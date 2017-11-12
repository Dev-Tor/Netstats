using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class NetworkApi : INetworkApi
    {
        static readonly string requestUrl = "/cgi-bin/user_session.ggi";

        readonly HttpClient HttpClient;

        readonly HtmlParser parser

        public NetworkApi()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://192.168.1.30");

            parser = new HtmlParser(new HtmlParserOptions() { IsStrictMode = false });
        }

        IPageParserFactory ParserFactory { get; set; }

        IPageDescriptorFactory DescriptorFactory { get; set; }

        public Task<string> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Page> MakeRequest(Dictionary<string, string> data, CancellationToken cancelTtoken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            try
            {
                var responseMsg = await HttpClient.PostAsync(requestUrl, new FormUrlEncodedContent(data), cancelTtoken);
                var response = await responseMsg.Content.ReadAsStringAsync();

                var content = parser.Parse(response);

                // Get descriptors for all page types except Unknown
                foreach (var descriptor in  DescriptorFactory.GetAllDescriptors())
                {
                    if (descriptor.IsMatch(content))
                    {
                        var pageType = descriptor.For;
                        return new Page(pageType, content);
                    }
                }

                // This line may change in the future as i'm not sure whether to throw an exception or just 
                // return a Page with PageType of Unknown
                return new Page(PageType.Unknown, null);
                return Page.Create(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to complete login", ex);
            }
        }
    }
}
