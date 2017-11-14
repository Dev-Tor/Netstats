using Akavache;
using Netstats.Management;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Netstats.Tests
{
    public class UserAccountManagerTest
    {
        [Fact]
        public void Add_WhenCalled_CallsInsertOnBlobCache()
        {
            var cache = Substitute.For<IBlobCache>();
            UserAccountManager manager = new UserAccountManager(cache);
            var account = new UserAccount("john", "doe", "jin");
            manager.Add(account);
            cache.Received().Insert($"{typeof(UserAccount).FullName}___{account.Username}", Arg.Any<byte[]>());
        }

        [Fact]
        public void Remove_WhenCalled_CallsInvalidateOnBlobCache()
        {
            var cache = Substitute.For<IBlobCache>();
            UserAccountManager manager = new UserAccountManager(cache);
            var account = new UserAccount("john", "doe", "jin");
            manager.Remove(account);
            cache.Received().Invalidate($"{typeof(UserAccount).FullName}___{account.Username}");
        }

        [Fact]
        public void RemoveAll_EmptyCache_DoesNotCallInvalidateOnBlobCache()
        {
            var cache = Substitute.For<IBlobCache>();
            UserAccountManager manager = new UserAccountManager(cache);
            manager.RemoveAll();
            cache.DidNotReceive().InvalidateAll();
        }
    }
}
