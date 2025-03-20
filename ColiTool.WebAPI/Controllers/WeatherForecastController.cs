using Microsoft.AspNetCore.Mvc;
using ColiTool.CanBus;

namespace ColiTool.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CanController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var status = CanBusService.GetStatus();
            return Ok(status);
        }
        /*
        SIMULACIA PRE MOJE USPOKOJENIE 
        [HttpPost("simulate/enable")]
        public IActionResult EnableSimulation()
        {
            CanBusService.SimulateDeviceConnection = true;
            return Ok(new { Message = "Simulation enabled" });
        }

        [HttpPost("simulate/disable")]
        public IActionResult DisableSimulation()
        {
            CanBusService.SimulateDeviceConnection = false;
            return Ok(new { Message = "Simulation disabled" });
        }
        */
    }
}