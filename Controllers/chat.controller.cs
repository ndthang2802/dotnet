using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Application.helpers;
using Application.entities;
using Application.services.chat;
using Application.model;
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


        [HttpPost("getCoversation")]
        [AuthorizeAttribute]

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


        [HttpPost("createRoom")]
        [AuthorizeAttribute]
        public IActionResult createRoom([FromBody] CreateRoom room)
        {
            var creatorUsername = HttpContext.Items["User"].ToString();
            try {
                var Conversation = _chatService.createRoom(creatorUsername,room.name);
                return Ok(Conversation);
            }
            catch {
                return BadRequest("Error");
            }
        }

        [HttpPost("joinRoom")]
        [AuthorizeAttribute]
        public IActionResult joinRoom([FromBody] JoinRoom room)
        {
            var creatorUsername = HttpContext.Items["User"].ToString();
            try {
                var Conversation = _chatService.joinInRoom(creatorUsername,room.IdRoom);
                return Ok(Conversation);
            }
            catch {
                return BadRequest("Error");
            }
        }
    }
}