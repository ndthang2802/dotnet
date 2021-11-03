using System;
using Application.model.Authentication;
using Application.model;
using System.Collections.Generic;
using System.Security.Claims;
using Application.model.AppData;
using System.Collections;
using System.Linq;

namespace Application.services.chat
{
    public interface IChatService
    {
        ConversationModel createTalk(string userNameofCreator, string NameOfTalk);
        ConversationModel createDirectTalk(string userNameofUser1,string userNameofUser2);

       //List<ConversationModel> getConversations(string username);
       List<ConversationModel> getConversations(string username);


    }

}