using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public class ParserForAtrribute : Attribute
    {
        PageType For { get; }

        public ParserForAtrribute(PageType pageType)
        {
            For = pageType;
        }
    }
}
