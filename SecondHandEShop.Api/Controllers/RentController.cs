using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace SecondHandEShop.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentedService _rentedService;

        public RentController(IRentedService rentedService)
        {
            this._rentedService = rentedService;
        }

        [HttpGet]
        public IActionResult GetRented(string email)
        {
            return Ok(_rentedService.getRentedInfo(email));
        }
    }
}
