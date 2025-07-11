using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Broadcast.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            if (Program.MainForm == null || Program.MainForm.yaml == null)
            {
                return BadRequest("MainForm or YAML configuration is not initialized.");
            }

            Program.MainForm.Invoke(new Action(() => { }));
            Form1.StartSimulator(Program.MainForm.yaml.SimulatorCommand);
            return "Simulator started";
        }
    }
}
