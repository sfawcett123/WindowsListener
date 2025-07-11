using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Broadcast.controllers
{
    public class StatusDetails
    {
        public bool simulator {  get; set; }
        public bool redis { get; set; }
        public string? version { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<StatusDetails> Get()
        {
            // Ensure Program.MainForm and simConnection are not null before accessing them.
            if (Program.MainForm?.simConnection == null)
            {
                return BadRequest(new Dictionary<string, string>
                        {
                            { "error", "Simulator connection is not initialized." }
                        });
            }

            StatusDetails status = new()
            {
                version = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "Unknown",
                simulator = Program.MainForm.simConnection.isConnected,
                redis = Program.MainForm.redisConnection.isConnected
            };

            Program.MainForm?.Invoke(new Action(() => { }));
            return status;
        }
    }
}
