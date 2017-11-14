using System.Collections.Generic;

namespace Netstats.Network
{
    public interface IPageDescriptorFactory
    {
        IEnumerable<IPageDescriptor> GetAllDescriptors();

        IPageDescriptor GetDescriptorFor(PageType type);
    }
}