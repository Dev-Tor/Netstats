using System;

namespace Netstats.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class DescriptorForAttribute : Attribute
    {
        public PageType Type { get; }

        public DescriptorForAttribute(PageType type)
        {
            this.Type = type;
        }
    }
}