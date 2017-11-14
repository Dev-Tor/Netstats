using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Netstats.Network;

namespace Netstats.Core
{
    //===============================================================================
    // Copyright © Edosa Kelvin @Velvetta Inc.  All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class SessionManager : ISessionManager
    {
        readonly object locker = new object();

        public SessionManager(INetworkApi networkApi)
        {
            NetworkApi = networkApi;
        }

        public ISession Current { get; set; }

        public INetworkApi NetworkApi { get; set; }

        public async Task Create(string username, string password)
        {
            Monitor.Enter(locker);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("user credentials");

            //This is not entirely necessary but it's nice to keep things tidy
            if (Current != null)
                await DestroyCurrent();

            try
            {
                var json = await NetworkApi.LoginAsync(username, password);
                var response = JsonConvert.DeserializeObject<LoginResult>(json);

                Current = new Session(response.Token, response.QuotaType, NetworkApi);
            }
            catch (Exception ex)
            {
                // To-do repelace with better loging mechanism
                Debug.WriteLine("unable to login" + ex.Message);
                throw;
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        public async Task DestroyCurrent()
        {
            await NetworkApi.Logout(Current.Token);
            Current.Dispose();
            Current = null;
        }
    }

    public class LoginResult
    {
        public string Token { get; }

        public UserQuotaType QuotaType { get; }

        public LoginResult(string token, UserQuotaType quotaType)
        {
            this.Token = token;
            this.QuotaType = quotaType;
        }
    }
}
