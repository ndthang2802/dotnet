using System.Collections.Generic;
namespace Application.model.AppData
{
    public class ConversationModel 
    {
        public string Id { get; set; }
        public string name {get;set;}
        public UserModel creator {get;set;}

        public List<UserModel> UsersAttend {get;set;} 

        public ConversationModel(string id,string name, UserModel creator, List<UserModel> attendants){
            this.Id = id;
            this.name = name;
            this.creator = creator;
            this.UsersAttend = attendants;
        }
    }

    public class CreateConversation
    {
        public string name { get; set; }
    }

    public class DeleteConversation
    {
        public string Id { get; set; }
    }

    public class CreateDirectTalk
    {
        public string Username1 { get; set; }
        public string Username2 { get; set; }

    }

    public class AddUserToConversation 
    {
        public string Id {get;set;}
    }
}