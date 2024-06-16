using Domain.Domain_models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Order GetOrderDetails(int orderId)
        {
            return _orderRepository.GetOrderDetails(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }
        public List<Order> GetMyOrders(string email)
        {
            return this._orderRepository.GetMyOrders(email);
        }
    }
}
