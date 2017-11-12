using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public class ParserFor : Attribute
    {
        PageType For { get; }

        public ParserFor(PageType pageType)
        {
            For = pageType;
        }
    }
}
