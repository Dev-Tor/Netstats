using System.Collections.Generic;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public interface IRequestMaker
    {
        Task<string> Make(string requestUrl, Dictionary<string, string> data, PageType expected);
    }
}