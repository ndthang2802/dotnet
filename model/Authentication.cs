namespace Application.model
{  
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class RegisterModel
    {
        public string displayName {get;set;}
        public string phoneNumber {get;set;}
        public string username { get; set; }
        public string password { get; set; }
    }

    public class TokenModel {
        public string accessToken {get;set;}
        public string refreshToken {get;set;}
        public TokenModel(string AccessToken,string RefreshToken) {
            this.accessToken = AccessToken;
            this.refreshToken = RefreshToken;
        }
    }

    public class AccessToken {
        public string accessToken {get;set;}

    }
}