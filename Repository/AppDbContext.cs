﻿using Domain.Domain_models;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Repository
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShopApplicationUser> ShopApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ProductInShoppingCart> ProductsInShoppingCarts { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Rented> Rented { get; set; }
        public DbSet<ProductInFavourites> ProductsInFavourites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductInOrder> ProductsInOrders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProductInRented> ProductsInRented { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInShoppingCart>()
                .HasKey(psc => new { psc.ProductId, psc.ShoppingCartId });

            modelBuilder.Entity<ProductInRented>()
                .HasKey(psr => new { psr.ProductId, psr.RentedId });

            modelBuilder.Entity<ProductInFavourites>()
                .HasKey(pf => new { pf.ProductId, pf.FavouritesId });

            modelBuilder.Entity<ProductInOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });
        }
    }
}

