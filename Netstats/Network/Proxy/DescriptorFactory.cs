using System;

namespace Netstats.Network.Proxy
{
    public class DescriptorFactory
    {
        public IProxyDescriptor GetDesciptor(PageType type)
        {
            if (type == PageType.Unknown)
                throw new InvalidOperationException("cannot craete parser for unknown type");
            
            var descriptorName = $"{type}PageDescriptor";

            try
            {
                var descriptorType = Type.GetType("Netstats.Network.Proxy.Descriptors" + "." + descriptorName);
                return (IProxyDescriptor)Activator.CreateInstance(descriptorType);
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to locate or load descriptor [{descriptorName}]", ex);
            }
        }
    }
}
