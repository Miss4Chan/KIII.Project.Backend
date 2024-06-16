using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderDetails(int orderId);
        List<Order> GetMyOrders(string email);
    }
}
