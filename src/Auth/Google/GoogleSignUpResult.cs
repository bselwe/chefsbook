namespace ChefsBook.Auth.Google
{
    public class GoogleSignUpResult
    {
        public bool IsSuccess { get; set; }
        public AuthUser User { get; set; }
        public string ErrorString { get; set; }
    }
}