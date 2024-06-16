using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        public readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public readonly IRepository<ProductInOrder> _productInOrderRepository;
        public readonly IRepository<Order> _orderRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<Product> _productRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<ProductInOrder> productInOrderRepository, IRepository<Order> orderRepository, IUserRepository userRepository, IRepository<Product> productRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._productInOrderRepository = productInOrderRepository;
            this._orderRepository = orderRepository;
            this._userRepository = userRepository;
            this._productRepository = productRepository;

        }
        public ShoppingCartDTO getShoppingCartInfo(string email)
        {
            var loggedInUser = _userRepository.GetByEmail(email);

            var userCart = loggedInUser.UserShoppingCart;

            var productsList = userCart.ProductsInShoppingCart.Where(p => p.Product.ProductAvailablity == true).ToList();

            double totalPrice = 0.0;

            foreach (var item in productsList)
            {
                totalPrice += item.Product.ProductPrice;
            }

            var result = new ShoppingCartDTO
            {
                ProductsInShoppingCart = productsList,
                TotalPrice = totalPrice
            };

            return result;
        }

        public bool deleteProductFromShoppingCart(string email, int productId)
        {
            if (!string.IsNullOrEmpty(email) && productId != null)
            {
                var loggedInUser = _userRepository.GetByEmail(email);
                var userShoppingCart = loggedInUser.UserShoppingCart;
                var itemToDelete = userShoppingCart.ProductsInShoppingCart.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();
                userShoppingCart.ProductsInShoppingCart.Remove(itemToDelete);
                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Order OrderNow(string email, string deliveryType, string deliveryAddress, string deliveryPhone, string deliveryCity, string deliveryPostalCode)
        {
            var loggedInUser = _userRepository.GetByEmail(email);
            var userCard = loggedInUser.UserShoppingCart;

            Order order = new Order
            {
                User = loggedInUser,
                UserId = loggedInUser.Id,
                DeliveryType = (DeliveryType)Enum.Parse(typeof(DeliveryType), deliveryType),
                DeliveryAddress = deliveryAddress,
                DeliveryPhone = deliveryPhone,
                DeliveryCity = deliveryCity,
                DeliveryPostalCode = deliveryPostalCode,
                FormattedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                FormattedTime = DateTime.Now.ToString("HH:mm:ss")
            };

            List<ProductInShoppingCart> productsInShoppingCart = userCard.ProductsInShoppingCart;

            float totalPrice = 0;

            foreach (ProductInShoppingCart p in productsInShoppingCart)
            {
                totalPrice += p.Product.ProductPrice;
            }

            order.Subtotal = totalPrice;

            if (order.DeliveryType == DeliveryType.REGULAR)
            {
                totalPrice += 150;
            }
            else
            {
                totalPrice += 250;
            }

            order.Total = totalPrice;

            _orderRepository.Insert(order);

            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();

            var result = productsInShoppingCart.Select(z => new ProductInOrder
            {
                ProductId = z.ProductId,
                Product = z.Product,
                OrderId = order.Id,
                Order = order
            }).ToList();

            productsInOrder.AddRange(result);

            foreach (var item in productsInOrder)
            {
                _productInOrderRepository.Insert(item);
                var product = item.Product;
                product.ProductAvailablity = false;
                _productRepository.Update(product);
            }

            loggedInUser.UserShoppingCart.ProductsInShoppingCart.Clear();

            this._userRepository.Update(loggedInUser);
            return order;

        }
    }
}

