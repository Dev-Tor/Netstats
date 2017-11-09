using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public interface IPage
    {
        IHtmlDocument Content { get; set; }

        PageType Type { get; set; }
    }
}
