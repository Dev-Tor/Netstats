using System;

namespace Netstats.Management
{
    //===============================================================================
    // Copyright © Edosa Kelvin. All rights reserved.
    // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
    // OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
    // LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
    // FITNESS FOR A PARTICULAR PURPOSE.
    //===============================================================================

    public class UserAccount: IEquatable<UserAccount>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Alias { get; set; }

        public UserAccount(string username, string alias, string password)
        {
            Alias = alias ?? Username;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserAccount);
        }

        public bool Equals(UserAccount other)
        {
            return other == null ? false : Username == other.Username && Password == other.Password && Alias == other.Alias;
        }

        public override int GetHashCode()
        {
            return new { Username, Password, Alias }.GetHashCode();
        }
    }
}
