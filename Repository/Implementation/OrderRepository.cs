using Domain.Domain_models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities.Include(z => z.User)
                .Include(z => z.ProductsInOrder)
                .Include("ProductsInOrder.Product").ToList();
        }

        public List<Order> GetMyOrders(string email)
        {
            return entities.Include(z => z.User).Where(u => u.User.Email == email)
                .Include(z => z.ProductsInOrder)
                .Include("ProductsInOrder.Product")
                .Include("ProductsInOrder.Product.ShopApplicationUser")
                .ToList();
        }

        public Order GetOrderDetails(int orderId)
        {
            return entities.Include(z => z.User)
                     .Include(z => z.ProductsInOrder)
                     .Include("ProductsInOrder.Product").SingleOrDefault(z => z.Id == orderId);
        }
    }
}
