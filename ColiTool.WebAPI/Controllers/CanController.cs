using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ColiTool.CanBus;
using ColiTool.Common;
using ColiTool.Utils;
using static ColiTool.CanBus.CanBusService;

namespace ColiTool.WebAPI.Controllers
{
    [ApiController]
    [Route("can")]
    public class CanController : ControllerBase
    {
        private readonly CanBusService _canBusService;

        public CanController(IConfiguration configuration)
        {
            _canBusService = new CanBusService(configuration);
        }

        // GET /can/status 
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var status = _canBusService.GetStatus();
            return Ok(status);
        }

        // POST /can/send 
        [HttpPost("send")]
        public async Task<IActionResult> SendCommand([FromBody] Models.SdoRequest request)
        {
            try
            {
                var response = await _canBusService.ProcessSdoRequestAsync(request);
                return Ok(new { message = "Command processed successfully.", response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /can/messages
        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages()
        {
            CanMessage message = await _canBusService.ReceiveAsync(500);
            if (message == null)
                return NotFound(new { message = "No messages received." });

            return Ok(new
            {
                ArbitrationId = message.ArbitrationId,
                Data = message.Data
            });
        }


    }
        [ApiController]
        [Route("eds")]
        public class EdsController : ControllerBase
        {
            private readonly string _edsFilePath;

            public EdsController(IConfiguration configuration)
            {
                _edsFilePath = configuration.GetValue<string>("EdsFilePath");
            }

            [HttpGet]
            public IActionResult GetEdsData()
            {
                try
                {
                    var parser = new EdsParser();
                    var edsData = parser.ParseEdsFile(_edsFilePath);
                    return Ok(edsData);
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }
            }
        }
}
