namespace ChefsBook.Auth.Security
{
    public static class KnownClaims
    {
        public const string UserId = "sub"; // Well-known JWT claim
        public const string Role = "role";
    }
}