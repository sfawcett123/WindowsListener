using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class StartupController
    {
        [HttpGet]
        public ActionResult<String> Get()
        {
            return "Not implemented";
        }
    }
}
