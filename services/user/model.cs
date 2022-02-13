
namespace Application.services.user
{
    public class UserModel 
    {
        public string Id { get; set; }
        public string username {get;set;}
        public string phoneNumber {get;set;}
        public string displayName {get;set;}

        public UserModel(string id, string username, string phoneNumber,string displayName){
            this.Id = id;
            this.username= username;
            this.phoneNumber = phoneNumber;
            this.displayName = displayName;
        }
    }
    
}