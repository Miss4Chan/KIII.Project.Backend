using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(int orderId);
        public List<Order> GetMyOrders(string email);
    }
}
