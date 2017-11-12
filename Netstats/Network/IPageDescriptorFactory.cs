using System.Collections.Generic;

namespace Netstats.Network
{
    internal interface IPageDescriptorFactory
    {
        IEnumerable<IPageDescriptor> GetAllDescriptors();

        IPageDescriptor GetDescriptorFor(PageType type);
    }
}