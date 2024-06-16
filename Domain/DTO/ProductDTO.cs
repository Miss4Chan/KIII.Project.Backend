using Domain.Domain_models;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductSubcategory { get; set; }
        public string ProductSize { get; set; }
        public string ProductMeasurements { get; set; }
        public string ProductColor { get; set; }
        public int? ProductSizeNumber { get; set; }
        public float ProductPrice { get; set; }
        public string ProductCondition { get; set; }
        public string ProductMaterial { get; set; } 
        public string ProductBrand { get; set; }

        public bool ProductAvailablity { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProductSex { get; set; }
        public double UserRating { get; set; }
        public string ProductImage { get; set; }

        public bool ProductRent { get; set; }
        public int ProductDaysRent { get; set; }    

        public static explicit operator ProductDTO(Product p) => new ProductDTO
        {
            Id = p.Id,
            ProductDescription = p.ProductDescription,
            ProductName = p.ProductName,
            ProductAvailablity = p.ProductAvailablity,
            ProductPrice = p.ProductPrice,
            ProductType = p.ProductType.ToString(),
            ProductSize = p.ProductSize.ToString(),
            ProductSubcategory = p.ProductSubcategory.ToString(),
            ProductColor = p.ProductColor,
            ProductMeasurements = p.ProductMeasurements,
            ProductSizeNumber = p.ProductSizeNumber,
            Username = p.ShopApplicationUser.Username,
            Email = p.ShopApplicationUser.Email,
            ProductBrand = p.ProductBrand,
            ProductCondition = p.Condition.ToString(),
            ProductMaterial = p.ProductMaterial,
            ProductSex = p.ProductSex.ToString(),
            UserRating = p.ShopApplicationUser.UserRating,
            ProductRent = p.ProductRent,
            ProductDaysRent = p.ProductDaysRent,
            ProductImage = p.ProductImage,
    };
    }
}
