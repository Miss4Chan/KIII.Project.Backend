using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart> ProductsInShoppingCart { get; set; }
        public double TotalPrice { get; set; }
    }
}
