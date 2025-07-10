using Microsoft.AspNetCore.Mvc;
using SimListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Dictionary<string, bool>> Get()
        {
            // Ensure Program.MainForm and simConnection are not null before accessing them.
            if (Program.MainForm?.simConnection == null)
            {
                return BadRequest(new Dictionary<string,string>
                        {
                            { "error", "Simulator connection is not initialized." }
                        });
            }

            Dictionary<string, bool> text = new()
                    {
                        { "simulator", Program.MainForm.simConnection.isConnected },
                        { "redis"    , Program.MainForm.redisConnection.isConnected }
                    };

            Program.MainForm?.Invoke(new Action(() => { }));
            return text;
        }
    }
}
