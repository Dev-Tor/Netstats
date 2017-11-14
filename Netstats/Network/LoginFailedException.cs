using System;

namespace Netstats.Core.Exceptions
{
    public class LoginFailedException : Exception
    {
        readonly static string error_msg = "Login failed!";

        public LoginFailedException() : base(error_msg)
        {
            // Nothing to see here...
        }

        public LoginFailedException(LoginFailReason reason) : base(error_msg + ": " + reason)
        {
            FailReason = reason;
        }

        public LoginFailedException(LoginFailReason reason, Exception exception) : base(error_msg + ": " + reason, exception)
        {
            FailReason = reason;
        }

        public LoginFailReason FailReason { get; }

        public override string ToString()
        {
            return error_msg + $": {FailReason}";
        }
    }
}
