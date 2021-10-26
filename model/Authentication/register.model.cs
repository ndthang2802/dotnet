using System;
namespace Application.model.Authentication
{  
    public class RegisterModel
    {
        public string displayName {get;set;}
        public string phoneNumber {get;set;}
        public string username { get; set; }
        public string password { get; set; }
    }
}