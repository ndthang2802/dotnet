using System;
using System.Linq;
using Application.entities;
using Application.helpers;
using System.Collections.Generic;
using Application.services.user;
namespace Application.services.chat
{
    public class ChatService : IChatService
    {
        private DataContext _dataContext;
        public ChatService(DataContext dataContext){
            this._dataContext = dataContext;
        }

        public ConversationModel joinInRoom(string username, string IdRoom) {

            User attender = _dataContext.Users.FirstOrDefault(u => u.username == username);

            Guid guid = Guid.NewGuid();

            var conversation = _dataContext.Conversations.FirstOrDefault(c => c.Id == IdRoom);

            if (conversation == null) {
                throw new Exception("Room not Exist");
            }

            var join = new Join(attender.Id,IdRoom);

            _dataContext.Joins.Add(join);

            var state = _dataContext.SaveChanges();

            User hostUser = _dataContext.Users.FirstOrDefault(u => u.username == conversation.UserId);
            

            if (state > 0)
            {
                return new ConversationModel(conversation.Id,conversation.Name, new UserModel(hostUser.Id,hostUser.username,hostUser.phoneNumber,hostUser.displayName));
            }
            else
            {
                throw new Exception("st wrong");
            }
        }

        public ConversationModel createRoom(string hostUsername,string roomName) {
            User user = _dataContext.Users.FirstOrDefault(u => u.username == hostUsername);

            Guid guid = Guid.NewGuid();

            var conversation = new Conversation(guid.ToString(),roomName,user.Id.ToString());
            _dataContext.Conversations.Add(conversation);


            var join = new Join(user.Id,conversation.Id);


            _dataContext.Joins.Add(join);

            var state = _dataContext.SaveChanges();

            List<UserModel> Attendants = new List<UserModel>();


            UserModel hostUser = new UserModel(user.Id,user.username,user.phoneNumber,user.displayName);



            if (state > 0)
            {
                return new ConversationModel(conversation.Id,conversation.Name, hostUser);
            }
            else
            {
                throw new Exception("st wrong");
            }
        }
        
        public IDictionary<string , ConversationModel> getConversations(string username) 
        {

            User user = _dataContext.Users.FirstOrDefault(u => u.username == username);
            var conversationList = _dataContext.Joins.ToList().GroupBy(j=>j.ConservationId,j => j.UserId,(key, g) => new {ConservationId = key, attendId = g.ToList() }).Where(j => j.attendId.Contains(user.Id));
            //List<ConversationModel> cvs = new List<ConversationModel>();

            ListConversationModel lcm = new ListConversationModel(); // dictionary idOfConversation : conversation

            foreach(var conversation in conversationList){
                Conversation c = _dataContext.Conversations.FirstOrDefault(c => c.Id == conversation.ConservationId);
                User ccreator = _dataContext.Users.FirstOrDefault(u => u.Id == c.UserId);
                

                var cvsModel = new ConversationModel(c.Id,c.Name,new UserModel(ccreator.Id,ccreator.username,ccreator.phoneNumber,ccreator.displayName));

                lcm.talk.Add(cvsModel.Id, cvsModel);
            }
            return lcm.talk;
        }

        
    } 

}