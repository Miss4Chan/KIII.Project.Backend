using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandEShop.Api.Controllers
{
 
        [Route("[controller]")]
        [ApiController]
        public class FavouritesController : ControllerBase
        {
            private IFavouritesService _favouritesService;
            public FavouritesController(IFavouritesService favouritesService)
            {
                this._favouritesService = favouritesService;
            }

            [HttpGet]
            public IActionResult GetFavourites(string email)
            {
                return Ok(_favouritesService.getFavouritesInfo(email));
            }

            [HttpDelete]
            public IActionResult DeleteFromFavourites(string email, int product)
            {

                return Ok(_favouritesService.deleteProductFromFavourites(email, product));
            }
        }
}
