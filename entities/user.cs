using System;
namespace Application.entities
{
    public class User
    {
        public string displayName {get;set;}
        public string phoneNumber {get;set;}
        
        public string Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}