using System;
using System.Linq;
using Application.model.Authentication;
using Application.entities;
using Application.helpers;
using System.Collections.Generic;
using Application.model.AppData;
namespace Application.services.chat
{
    public class ChatService : IChatService
    {
        private DataContext _dataContext;
        public ChatService(DataContext dataContext){
            this._dataContext = dataContext;
        }

        public ConversationModel createTalk(string userNameofCreator, string NameOfTalk) {

            User creator = _dataContext.Users.FirstOrDefault(u => u.username == userNameofCreator);

            Guid guid = Guid.NewGuid();

            var conversation = new Conversation(guid.ToString(),NameOfTalk,creator.Id.ToString());

            var join = new Join();
            join.UserId = creator.Id;
            join.ConservationId = conversation.Id;

            _dataContext.Conversations.Add(conversation);
            _dataContext.Joins.Add(join);

            var state = _dataContext.SaveChanges();

            List<UserModel> Attendants = new List<UserModel>();

            if (state > 0)
            {
                return new ConversationModel(conversation.Id,conversation.Name, new UserModel(creator.Id,creator.username,creator.phoneNumber,creator.displayName), Attendants);
            }
            else
            {
                throw new Exception("st wrong");
            }
        }

        public ConversationModel createDirectTalk(string userNameofUser1,string userNameofUser2) {
            User user1 = _dataContext.Users.FirstOrDefault(u => u.username == userNameofUser1);
            User user2 = _dataContext.Users.FirstOrDefault(u => u.username == userNameofUser2);

            Guid guid = Guid.NewGuid();

            var conversation = new Conversation(guid.ToString()," ",user1.Id.ToString());
            _dataContext.Conversations.Add(conversation);


            var join1 = new Join();
            join1.UserId = user1.Id;
            join1.ConservationId = conversation.Id;

            var join2 = new Join();
            join2.UserId = user2.Id;
            join2.ConservationId = conversation.Id;


            _dataContext.Joins.Add(join1);
            _dataContext.Joins.Add(join2);

            var state = _dataContext.SaveChanges();

            List<UserModel> Attendants = new List<UserModel>();


            UserModel p1 = new UserModel(user1.Id,user1.username,user1.phoneNumber,user1.displayName);
            UserModel p2 = new UserModel(user2.Id,user2.username,user2.phoneNumber,user2.displayName);

            Attendants.Add(p1);
            Attendants.Add(p2);


            if (state > 0)
            {
                return new ConversationModel(conversation.Id,conversation.Name, p1, Attendants);
            }
            else
            {
                throw new Exception("st wrong");
            }
        }
        
        public List<ConversationModel> getConversations(string username) 
        {

            User user = _dataContext.Users.FirstOrDefault(u => u.username == username);
            var conversationList = _dataContext.Joins.ToList().GroupBy(j=>j.ConservationId,j => j.UserId,(key, g) => new {ConservationId = key, attendId = g.ToList() }).Where(j => j.attendId.Contains(user.Id));
            List<ConversationModel> cvs = new List<ConversationModel>();
            foreach(var conversation in conversationList){
                Conversation c = _dataContext.Conversations.FirstOrDefault(c => c.Id == conversation.ConservationId);
                User ccreator = _dataContext.Users.FirstOrDefault(u => u.Id == c.UserId);
                List<UserModel> attendants = new List<UserModel>();
                foreach(var attendant in conversation.attendId){
                    User a = _dataContext.Users.FirstOrDefault(u => u.Id == attendant);
                    UserModel a_ = new UserModel(a.Id,a.username,a.phoneNumber,a.displayName);
                    attendants.Add(a_);
                }
                cvs.Add(new ConversationModel(c.Id,c.Name,new UserModel(ccreator.Id,ccreator.username,ccreator.phoneNumber,ccreator.displayName),attendants));
            }
            return cvs;
        }

        
    } 

}