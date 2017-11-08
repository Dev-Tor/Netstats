using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

    public class NetworkApi : INetworkApi
    {
        static readonly string requestUrl = "/cgi-bin/user_session.ggi";

        static HttpClient HttpClient;

        public NetworkApi()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://192.168.1.30");
        }

        public void MakeRequest(object p1, object p2, object cancelationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<string> MakeRequest(IEnumerable<KeyValuePair<string, string>> data, CancellationToken cancelTtoken)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            try
            {
                var responseMsg = await HttpClient.PostAsync(requestUrl, new FormUrlEncodedContent(data), cancelTtoken);
                return await responseMsg.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to complete login", ex);
            }
        }
    }
}
