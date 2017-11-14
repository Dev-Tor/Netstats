using AngleSharp.Parser.Html;
using Netstats.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
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

        readonly HttpClient HttpClient;

        readonly HtmlParser parser;

        public NetworkApi(IRequestMaker maker = null)
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://192.168.1.30")
            };
            parser = new HtmlParser(new HtmlParserOptions() { IsStrictMode = false });
            Maker = maker ?? new RequestMaker();
        }

        IRequestMaker Maker { get; set; }

        public async Task<string> LoginAsync(string username, string password)
        {
            var data = new Dictionary<string, string>()
            {
                ["user"] = username,
                ["passwd"] = password,
            };

            try
            {
                return await Maker.Make(requestUrl, data, PageType.Session);
            }
            catch (RequestFailedException ex)
            {
                Debug.WriteLine(ex);

                // Find out exactly why the login failed for a more descriptive exception
                switch (ex.Recieved)
                {
                    // The user has supplied an invalid username or password
                    case PageType.AuthenticationFailed:
                        throw new LoginFailedException(LoginFailReason.AuthenticationError);

                    // The user is currently logged in on the same device
                    case PageType.ConfirmAction:
                        throw new LoginFailedException(LoginFailReason.UserAlreadyLoggedInOnSameIp);

                    // The maximum number of allowed sessions for the user has been reached.
                    // Note: this number is not currently know. Some say it is 2 per student and 5 per staff
                    case PageType.MaxUserSessionsReached:
                        throw new LoginFailedException(LoginFailReason.MaxUserSessionsReached);

                    default:
                        // Oh well...We can't seem to find out why the login failed
                        throw new LoginFailedException(LoginFailReason.Unknown);
                }
            }
            // This is probabbly the one from the network
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
       
        public Task<string> GetUsage(string sessionId)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
