using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Netstats.Network
{
    public class PageParserFactory
    {
        static List<IPageParser> pageParserMap = new List<IPageParser>();

        static PageParserFactory()
        {
            Func<PageType, IPageParser> FetchParser = type =>
            {
                var parserType = Assembly.GetExecutingAssembly().GetTypes()
                    // Parsers should implement IPageDescriptor and should also be marked with 
                    // a DescriptorFor attribute
                    .Where(t => t.IsClass && t.ImplementsInterface<IPageDescriptor>() && t.TypeHasAttribute<DescriptorForAttribute>(a => a.Type == type))
                    .FirstOrDefault();

                if (parserType == null)
                    throw new Exception($"Unable to find an appropriate parser for type: {type}");

                return (IPageParser)Activator.CreateInstance(parserType);
            };

            foreach (var pageName in Enum.GetNames(typeof(PageType)))
            {
                var pageType = (PageType)Enum.Parse(typeof(PageType), pageName);
                var parser = FetchParser(pageType);
                pageParserMap.Add(parser);
            }
        }

        public static IEnumerable<IPageParser> GetAllParsers() => pageParserMap;

        public static IPageParser GetParserFor(PageType type) => pageParserMap.FirstOrDefault(x => x.For == type);
    }
}
