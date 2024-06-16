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
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController( IOrderService orderService)
        {
            this._orderService = orderService;
        }


        [HttpGet("myOrders")]
        public IActionResult GetMyOrders(string email)
        {
            return Ok(_orderService.GetMyOrders(email));
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            return Ok(_orderService.GetOrderDetails(orderId));
        }
    }
}
