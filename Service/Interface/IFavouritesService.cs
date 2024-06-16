using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IFavouritesService
    {
        List<ProductInFavourites> getFavouritesInfo(string userId);
        bool deleteProductFromFavourites(string email, int productId);
    }
}
