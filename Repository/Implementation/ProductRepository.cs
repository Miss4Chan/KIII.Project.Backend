using Domain.Domain_models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        private DbSet<Product> entities;
        string errorMessage = string.Empty;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<Product>();
        }

        public void Delete(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return entities.Include(p => p.ShopApplicationUser).ToList();
        }

        public IEnumerable<Product> GetAllAvaliableProducts()
        {
            return entities.Where(p => p.ProductAvailablity == true).Include(p => p.ShopApplicationUser).ToList();
        }

        public Product GetById(int id)
        {
            return entities.Where(e => e.Id == id).Where(p => p.ProductAvailablity == true).Include(p=>p.ShopApplicationUser).FirstOrDefault();
        }

        public List<ProductDTO> GetProductsByEmail(string email)
        {
            return entities
                .Where(p => p.ShopApplicationUser.Email == email)
                .Where(p => p.ProductAvailablity == true)
                .Select(p => (ProductDTO)p)
                .ToList();
        }

        public List<ProductDTO> GetMyProducts(string email)
        {
            return entities
                .Where(p => p.ShopApplicationUser.Email == email)
                 .Select(p => (ProductDTO)p)
                 .ToList();
        }

        public void Insert(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            context.SaveChanges();
        }

        public void Update(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
