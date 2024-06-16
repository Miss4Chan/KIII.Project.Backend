using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        private DbSet<ShopApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<ShopApplicationUser>();
        }
        public void Delete(ShopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }

        public ShopApplicationUser GetById(int id)
        {
            var user = entities.Where(e => e.Id == id)
                 .Include(z => z.UserShoppingCart)
                .Include("UserShoppingCart.ProductsInShoppingCart")
                .Include("UserShoppingCart.ProductsInShoppingCart.Product")
                .Include(z => z.UserRented)
                .Include("UserRented.ProductsInRented")
                .Include("UserRented.ProductsInRented.Product")
                .Include(z => z.UserFavourites)
                .Include("UserFavourites.ProductsInFavourites")
                .Include("UserFavourites.ProductsInFavourites.Product")
                .FirstOrDefault();
   
            if (user == null)
                throw new ArgumentNullException("user");

            return user;
        }


        public ShopApplicationUser GetByUsername(string username)
        {
            var user = entities.Where(e => e.Username == username)
                 .Include(z => z.UserShoppingCart)
                .Include("UserShoppingCart.ProductsInShoppingCart")
                .Include("UserShoppingCart.ProductsInShoppingCart.Product")
                                .Include(z => z.UserRented)
                .Include("UserRented.ProductsInRented")
                .Include("UserRented.ProductsInRented.Product")
                .Include(z => z.UserFavourites)
                .Include("UserFavourites.ProductsInFavourites")
                .Include("UserFavourites.ProductsInFavourites.Product")
                .FirstOrDefault();

            return user;
        }

        public ShopApplicationUser GetByEmail(string email)
        {
            var user = entities.Where(e => e.Email == email)
                 .Include(z => z.UserShoppingCart)
                .Include("UserShoppingCart.ProductsInShoppingCart")
                .Include("UserShoppingCart.ProductsInShoppingCart.Product")
                                                .Include(z => z.UserRented)
                .Include("UserRented.ProductsInRented")
                .Include("UserRented.ProductsInRented.Product")
                .Include(z => z.UserFavourites)
                .Include("UserFavourites.ProductsInFavourites")
                .Include("UserFavourites.ProductsInFavourites.Product")
                .FirstOrDefault();

            return user;
        }

        public IEnumerable<ShopApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(ShopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(ShopApplicationUser entity)
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
