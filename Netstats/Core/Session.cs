using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;
using Netstats.Network;

namespace Netstats.Core
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class Session : ReactiveObject, ISession
    {
        private IConnectableObservable<SessionFeed> refreshObservabe;

        private IDisposable refreshDisposable;

        public Session(string id, UserQuotaType quotaType, INetworkApi networkApi)
        {
            Token = id;
            QuotaType = quotaType;
            NetworkApi = networkApi;

            //refreshObservabe = Observable.Timer(DateTime.Now, TimeSpan.FromSeconds(2))
            //                             .Select(_ => GetLatestFeedAsync())
            //                             .Do(feed => Update(feed))
            //                             .Publish();

            //refreshDisposable = refreshObservabe.Connect();

            //refreshObservabe.Subscribe();
        }

        SessionManager Manager { get; set; }

        public INetworkApi NetworkApi { get; }

        public string Token { get; }

        public UserQuotaType QuotaType { get; }

        private double total;
        public double Total
        {
            get { return total; }
            set
            {
                this.RaiseAndSetIfChanged(ref total, value);
            }
        }

        private double used;
        public double Used
        {
            get { return used; }
            set
            {
                this.RaiseAndSetIfChanged(ref used, value);
                this.RaisePropertyChanged("Left");
            }
        }

        private double download;
        public double Download
        {
            get { return download; }
            set
            {
                this.RaiseAndSetIfChanged(ref download, value);
            }
        }

        public double upload;
        public double Upload
        {
            get { return upload; }
            set
            {
                this.RaiseAndSetIfChanged(ref upload, value);
            }
        }

        public double Left { get { return Total - Used; } }

        public IObservable<SessionFeed> RefreshObservable { get { return refreshObservabe.AsObservable(); } }

        private SessionFeed GetLatestFeedAsync()
        {
            //var json = NetworkApi.GetCurrentUsage(Token).Result;
            //return JsonConvert.DeserializeObject<SessionFeed>(json);

            throw new NotImplementedException();
        }
        
        private void Update(SessionFeed feed)
        {
            Total = feed.Total;
            Used = feed.Used;
            Download = feed.Used;
            Upload = feed.Upload;
        }

        public void Dispose()
        {
            refreshDisposable.Dispose();
            Manager.DestroyCurrent().Wait();
        }
    }
}
