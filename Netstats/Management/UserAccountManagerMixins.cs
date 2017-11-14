using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Management
{
    public static class UserAccountManagerMixins
    {
        public static IObservable<UserAccount> GetAccountsWithUsername(this UserAccountManager manager, string username)
        {
            return manager.GetAll()
                          .SelectMany(x => x)
                          .Where(u => u.Username == username);
        }

        public static IObservable<UserAccount> GetAccountWithAlias(this UserAccountManager store, string alias)
        {
            return store.GetAll()
                        .SelectMany(x => x)
                        .Where(u => u.Username == alias);
        }
    }
}
