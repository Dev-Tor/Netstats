using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class PageDescriptorFactory : IPageDescriptorFactory
    {
        static List<IPageDescriptor> pageDescriptorMap = new List<IPageDescriptor>();

        static PageDescriptorFactory()
        {
            Func<PageType, IPageDescriptor> FetchDescriptor = type =>
            {
                var descriptorType = Assembly.GetExecutingAssembly().GetTypes()
               // Descriptors should implement IPageDescriptor and should also be marked with 
               // a DescriptorFor attribute
               .Where(t => t.IsClass && t.ImplementsInterface<IPageDescriptor>() && t.TypeHasAttribute<DescriptorForAttribute>(a => a.Type == type))
               .FirstOrDefault();

                if (descriptorType == null)
                    throw new Exception($"Unable to find an appropriate descriptor for type: {type}");

                return (IPageDescriptor)Activator.CreateInstance(descriptorType);
            };

            foreach (var pageName in Enum.GetNames(typeof(PageType)).Where(x => x != "Unknown"))
            {
                var pageType = (PageType)Enum.Parse(typeof(PageType), pageName);
                var pageDescriptor = FetchDescriptor(pageType);
                pageDescriptorMap.Add(pageDescriptor);
            }
        }

        public IEnumerable<IPageDescriptor> GetAllDescriptors() => pageDescriptorMap;

        public IPageDescriptor GetDescriptorFor(PageType type) => pageDescriptorMap.FirstOrDefault(x => x.For == type);
    }
}
