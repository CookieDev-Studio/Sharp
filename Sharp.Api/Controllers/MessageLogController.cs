using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp.FSharp.Service;

namespace Sharp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageLogController : ControllerBase
    {
        [HttpGet("{guildId}")]
        public ActionResult GetMessages(ulong guildId) => Ok(MessageService.getMessages(guildId));
    }
}
