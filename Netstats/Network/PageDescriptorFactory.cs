using System;
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

    public class PageDescriptorFactory
    {
        public PageDescriptorFactory()
        {
            // Nothing to see here...
        }

        public IPageDescriptor GetDesciptor(PageType type)
        {
            if (type == PageType.Unknown)
                throw new InvalidOperationException("cannot create descriptor for unknown type");

            var descriptorType = Assembly.GetExecutingAssembly().GetTypes()
                // Descriptors should implement IPageDescriptor and should also be marked with 
                // a DescriptorFor attribute which des
                .Where(t => t.IsClass && t.ImplementsInterface<IPageDescriptor>() && t.TypeHasAttribute<DescriptorForAttribute>(a => a.Type == type))
                .FirstOrDefault();
               

            if (descriptorType == null)
                throw new Exception("Unable to find an appropriate descriptor");

            return (IPageDescriptor)Activator.CreateInstance(descriptorType);
        }
    }
}
