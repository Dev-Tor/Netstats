using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public class RequestMaker : IRequestMaker
    {
        readonly HtmlParser htmlParser;

        readonly HttpClient client;

        readonly IPageParserFactory parserFactory;

        readonly IPageDescriptorFactory descriptorFactory;

        public RequestMaker(IPageDescriptorFactory descriptorFactory = null, IPageParserFactory parserFactory = null)
        {
            client = new HttpClient();
            htmlParser = new HtmlParser();
            parserFactory = parserFactory ?? new PageParserFactory();
            descriptorFactory = descriptorFactory ?? new PageDescriptorFactory();
        }

        public async Task<string> Make(string requestUrl, Dictionary<string,string> data, PageType expected)
        {
            try
            {
                var responseMsg = await client.PostAsync(requestUrl, new FormUrlEncodedContent(data));
                var response = await responseMsg.Content.ReadAsStringAsync();
                var htmlContent = htmlParser.Parse(response);

                var descriptor = descriptorFactory.GetAllDescriptors().FirstOrDefault(x => x.IsMatch(htmlContent));
                if (descriptor == null)
                    throw new RequestFailedException("Unable to find matching descriptor for page");

                var pageType = descriptor.For;
                var page = new Page(pageType, htmlContent);

                if (page.Type != expected)
                    throw new RequestFailedException($"Request failed. Expected: {expected} instead recieved: {page.Type}") { Recieved = page.Type };

                var parser = parserFactory.GetParserFor(page.Type);
                return parser.Parse(page.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
