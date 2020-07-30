using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp.Service;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageLogController : ControllerBase
    {
        readonly MessageService _messageService;

        public MessageLogController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{guildId}")]
        public ActionResult GetMessages(ulong guildId)
        {
            return Ok(_messageService.GetMessages(guildId));
        }
    }
}
