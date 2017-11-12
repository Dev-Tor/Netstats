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

    public static class PageDescriptorFactory
    {
        static List<IPageDescriptor> pageDescriptorMap = new List<IPageDescriptor>();

        static PageDescriptorFactory()
        {
            foreach (var pageName in Enum.GetNames(typeof(PageType)).Where(x => x != "Unknown"))
            {
                var pageType = (PageType)Enum.Parse(typeof(PageType), pageName);
                var pageDescriptor = GetDescriptor(pageType);
                pageDescriptorMap.Add(pageDescriptor);
            }
        }

        private static IPageDescriptor GetDescriptor(PageType type)
        {
            if (type == PageType.Unknown)
                throw new InvalidOperationException("cannot create descriptor for unknown type");

            var descriptorType = Assembly.GetExecutingAssembly().GetTypes()
                // Descriptors should implement IPageDescriptor and should also be marked with 
                // a DescriptorFor attribute
                .Where(t =>t.IsClass && t.ImplementsInterface<IPageDescriptor>() && t.TypeHasAttribute<DescriptorForAttribute>(a => a.Type == type))
                .FirstOrDefault();

            if (descriptorType == null)
                throw new Exception($"Unable to find an appropriate descriptor for type: {type}");

            return (IPageDescriptor)Activator.CreateInstance(descriptorType);
        }

        public static IEnumerable<IPageDescriptor> GetAllDescriptors() => pageDescriptorMap;

        public static IPageDescriptor GetDescriptorFor(PageType type) => pageDescriptorMap.First(x => x.For == type);
    }
}
