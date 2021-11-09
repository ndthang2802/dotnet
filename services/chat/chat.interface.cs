using System.Collections.Generic;
using Application.model.AppData;
namespace Application.services.chat
{
    public interface IChatService
    {
        ConversationModel createTalk(string userNameofCreator, string NameOfTalk);
        ConversationModel createDirectTalk(string userNameofUser1,string userNameofUser2);
       IDictionary<string , ConversationModel> getConversations(string username);
    
    

    }

}