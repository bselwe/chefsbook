namespace ChefsBook.Auth.Google
{
    public static class GoogleConsts
    {
        public const string GrantType = "google";
        public const string GrantField = "assertion";

        public static class Errors
        {
            public const string NoAssertion = "google_no_assertion";
            public const string InvalidAssertion = "google_invalid_assertion";
            public const string CouldNotRegisterUser = "could_not_register_user";
        }
    }
}