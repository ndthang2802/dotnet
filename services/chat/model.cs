using System.Collections.Generic;
using Application.services.user;

namespace Application.services.chat
{
     public class CreateRoom
    {
        public string name {get;set;}

        public CreateRoom(string name){
            this.name = name;
        }
    }
    public class ConversationModel 
    {
        public string Id { get; set; }
        public string name {get;set;}
        public UserModel creator {get;set;}


        public ConversationModel(string id,string name, UserModel creator){
            this.Id = id;
            this.name = name;
            this.creator = creator;
        }
    }

    public class ListConversationModel {
        public IDictionary<string , ConversationModel> talk {get;set;}
        public ListConversationModel(){
           talk = new Dictionary<string , ConversationModel>();
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

    public class JoinRoom
    {
        public string IdRoom { get; set; }

    }

    public class AddUserToConversation 
    {
        public string Id {get;set;}
    }
}