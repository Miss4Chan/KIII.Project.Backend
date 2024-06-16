using Domain.Domain_models;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string email);
        bool deleteProductFromShoppingCart(string email, int productId);
        Order OrderNow(string userId, string deliveryType, string deliveryAddress, string deliveryPhone, string deliveryCity, string deliveryPostalCode);
    }
}
