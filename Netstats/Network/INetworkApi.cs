using System.Threading;
using System.Threading.Tasks;

namespace Netstats.Network
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public interface INetworkApi
    {
        Task<string> LoginAsync(string username, string password);

        //Task<string> Login(string username, string password, CancellationToken token, bool overrideCurrent = true);

        Task<string> GetUsage(string sessionId);

        //Task<string> GetCurrentUsage(string sessionId, CancellationToken token);

        Task Logout(string sessionId);

        //Task Logout(string sessionId, CancellationToken token);
    }
}
