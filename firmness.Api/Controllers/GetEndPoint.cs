using Microsoft.AspNetCore.Mvc;

namespace firmness.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    
    public class GetEndPointController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("The API is running");
        }
    }
}    