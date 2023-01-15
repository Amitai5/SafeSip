using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SafeSipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TamperController : ControllerBase
    {
        private readonly ILogger<TamperController> _logger;

        public TamperController(ILogger<TamperController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "SetCoasterTampered")]
        public int SetCoasterTampered(int coasterID)
        {
            SQLDB.Instance.SetCoasterTampered(coasterID);
            return 0;
        }
    }
}
