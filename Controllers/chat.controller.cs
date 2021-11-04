using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Application.helpers;
using Application.entities;
using Application.services.chat;
using Application.model.AppData;
namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService){
            this._chatService = chatService;
        }


        [HttpPost("createCoversation")]
        [Authorize]
        public IActionResult createConversation([FromBody] CreateConversation conversation)
        {
            var creatorUsername = HttpContext.Items["User"].ToString();
            try {
                var ConversationModel = _chatService.createTalk(creatorUsername,conversation.name);
                return Ok(ConversationModel);
            }
            catch {
                return BadRequest("Error");
            }


        }
        [HttpPost("getCoversation")]
        [Authorize]

        public IActionResult getConversation(){
            var username = HttpContext.Items["User"].ToString();

            try {
                var ConversationList = _chatService.getConversations(username);
                return Ok(ConversationList);
            }
            catch {
                return BadRequest("Error");
            }
        }


        [HttpPost("createDirectTalk")]
        [Authorize]
        public IActionResult createDirectTalk([FromBody] CreateDirectTalk user2)
        {
            var creatorUsername = HttpContext.Items["User"].ToString();
            try {
                var Conversation = _chatService.createDirectTalk(creatorUsername,user2.Username);
                return Ok(Conversation);
            }
            catch {
                return BadRequest("Error");
            }
        }
    }
}