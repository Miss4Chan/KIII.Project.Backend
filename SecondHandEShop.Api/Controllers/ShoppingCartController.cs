using Domain.Domain_models;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ShoppingCartController : ControllerBase
    {
        private IShoppingCartService _shoppingCartService;

        private IOrderService _orderService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            this._shoppingCartService = shoppingCartService;
            this._orderService = orderService;
        }

        [HttpGet]
            public IActionResult GetShoppingCart(string email)
            {
                return Ok(_shoppingCartService.getShoppingCartInfo(email));
            }

        [HttpDelete]
            public IActionResult DeleteFromShoppingCart(string email, int product)
            {

            return Ok(_shoppingCartService.deleteProductFromShoppingCart(email, product));
            }

        [HttpGet("Order")]
        public IActionResult OrderNow(string email, string deliveryType, string deliveryAddress, string deliveryPhone, string deliveryCity, string deliveryPostalCode)
        {
            return Ok(_shoppingCartService.OrderNow(email, deliveryType, deliveryAddress, deliveryPhone, deliveryCity, deliveryPostalCode));
        }
    }
}

