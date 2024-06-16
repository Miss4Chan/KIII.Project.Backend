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
    public class FavouritesService : IFavouritesService
    {

        public readonly IRepository<Favourites> _favouritesRepository;
        public readonly IUserRepository _userRepository;

        public FavouritesService(
        IRepository<Favourites> favouritesRepository,
        IUserRepository userRepository,
        IRepository<ProductInFavourites> productInFavouritesRepository)
        {
            this._favouritesRepository = favouritesRepository;
            this._userRepository = userRepository;
        }
        public bool deleteProductFromFavourites(string email, int productId)
        {
            if (!string.IsNullOrEmpty(email) && productId != null)
            {
                var loggInUser = _userRepository.GetByEmail(email);
                var userFavourites = loggInUser.UserFavourites;
                var itemToDelete = userFavourites.ProductsInFavourites.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();
                userFavourites.ProductsInFavourites.Remove(itemToDelete);
                _favouritesRepository.Update(userFavourites);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProductInFavourites> getFavouritesInfo(string email)
        {
            var loggInUser = _userRepository.GetByEmail(email);

            var userFavourites = loggInUser.UserFavourites;
            var productsList = userFavourites.ProductsInFavourites.Where(p => p.Product.ProductAvailablity == true).ToList();

            return productsList;
        }
    }
}
