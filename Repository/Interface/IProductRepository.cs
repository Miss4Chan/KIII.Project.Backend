using Domain.Domain_models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Insert(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        Product GetById(int id);
        IEnumerable<Product> GetAllAvaliableProducts();
        List<ProductDTO> GetProductsByEmail(string email);
        List<ProductDTO> GetMyProducts(string email);
    }
}
