using System;
namespace Application.entities
{
    public class Conversation
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string UserId {get;set;}

        public Conversation(string id, string name, string userId){
            Id = id;
            Name = name;
            UserId = userId;
        }

    }
}