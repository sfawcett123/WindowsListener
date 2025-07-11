using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Broadcast.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public ActionResult<String> Get()
        {
            Program.MainForm?.Invoke(new Action(() => { }));
            Form1.StartSimulator(Program.MainForm.yaml?.SimulatorCommand ?? "" );
            return "Simulator started";
        }
    }
}
