namespace Netstats.Core.Exceptions
{
    public enum LoginFailReason
    {
        UserAlreadyLoggedInOnSameIp,  //Lmao! this is long af!

        AuthenticationError,

        MaxUserSessionsReached,

        Unknown,
    }
}
