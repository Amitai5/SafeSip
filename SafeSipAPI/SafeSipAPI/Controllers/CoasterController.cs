using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SafeSipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoasterController : ControllerBase
    {
        private readonly ILogger<CoasterController> _logger;

        public CoasterController(ILogger<CoasterController> logger)
        {
            _logger = logger;
        }


        [HttpPost(Name = "SetCoasterTampered")]
        public void SetCoasterTampered(int coasterID)
        {
            SQLDB.Instance.SetCoasterTampered(coasterID);
        }

        [HttpGet(Name = "GetIsActive")]
        public bool GetIsActive(int coasterID)
        {
            return SQLDB.Instance.GetIsActive(coasterID);
        }
    }
}
