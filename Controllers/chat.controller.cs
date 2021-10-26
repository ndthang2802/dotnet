using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Application.helpers;
using Application.entities;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ChatController(DataContext dataContext){
            this._dataContext = dataContext;
        }


        [HttpPost("createCoversation")]
        public bool create(Message message)
        {
            message.CreatedAt = DateTime.Now;
            _dataContext.Messages.Add(message);
            var success = _dataContext.SaveChanges() > 0;
            return success;
        }
    }
}