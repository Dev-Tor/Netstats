using System;
using System.Linq;
using System.Reflection;

namespace Netstats.Network
{
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
                // Make sure it's a class that implements IPageDescriptor firstly
                .Where(t => t.IsClass && t.GetInterface("IPageDescriptor") != null)
                // Make sure it's decorated with a DescriptorFor attribute that corresponds to the requested page type
                .Where(t =>
                {
                    return t.GetCustomAttributes(false)
                            .Where(x => x is DescriptorForAttribute)
                            .Select(x => (DescriptorForAttribute)x)
                            .Any(x => x.Type == type);
                }).FirstOrDefault();

            if (descriptorType == null)
                throw new Exception("Unable to find an appropriate descriptor");

            return (IPageDescriptor)Activator.CreateInstance(descriptorType);
        }
    }
}
