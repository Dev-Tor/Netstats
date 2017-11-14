using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Netstats.Management
{
    public class UserAccountManager 
    {
        public UserAccountManager(IBlobCache blobCache)
        {
            UserCache = blobCache ?? BlobCache.InMemory;
        }

        public IBlobCache UserCache { get; }

        public IObservable<int> GetUserCount()
        {
            return GetAll().Select(x => x.Count());
        }

        public IObservable<Unit> Add(UserAccount account)
        {
            return UserCache.InsertObject(account.Username, account);
        }

        public IObservable<Unit> Remove(UserAccount account)
        {
            return UserCache.InvalidateObject<UserAccount>(account.Username);  
        }

        // These are very sensitive 

        public IObservable<Unit> RemoveAll()
        {
            return UserCache.InvalidateAllObjects<UserAccount>();
        }

        public IObservable<IEnumerable<UserAccount>> GetAll()
        {
            return UserCache.GetAllObjects<UserAccount>();
        }
    }
}
