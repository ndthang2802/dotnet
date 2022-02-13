using System.Collections.Generic;
namespace Application.services.chat
{
    public interface IChatService
    {
        ConversationModel joinInRoom(string user, string Idroom);
        ConversationModel createRoom(string userNameofUser1,string userNameofUser2);
       IDictionary<string , ConversationModel> getConversations(string username);
    
    

    }

}