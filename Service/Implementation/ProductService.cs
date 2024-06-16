using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Domain.Identity;
using Microsoft.
    Core.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
using System.Linq;


namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
        public readonly IUserRepository _userRepository;
        public readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        public readonly IRepository<ProductInFavourites> _productInFavouritesRepository;
        private readonly ShopApplicationUser _user;
        private readonly IRepository<ProductInRented> _productInRentedRepository;

        public ProductService (IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IProductRepository productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IRepository<ProductInFavourites> productInFavouritesRepository, IRepository<ProductInRented> productInRentedRepository)
        {
            this._userRepository = userRepository;
            this._productRepository = productRepository;
            this._productInShoppingCartRepository = productInShoppingCartRepository;
            this._productInFavouritesRepository = productInFavouritesRepository;
            this._user = _userRepository.GetByEmail(httpContextAccessor.HttpContext.User.Identity.Name);
            this._productInRentedRepository = productInRentedRepository;
            //httpContextAccessor.HttpContext.User.Identity.Name --> The name we have inside the JWT Token
        }

        public ProductDTO CreateProduct(Product product)
        {
            _productRepository.Insert(product);
            product.ShopApplicationUser = _user;
            product.ProductAvailablity = true;
            
            _productRepository.Update(product);

            return (ProductDTO) product;
        }

        public void DeleteProduct(ProductDTO productDTO)
        {
            var product = _productRepository.GetById(productDTO.Id);
            _productRepository.Delete(product);
        }

        public ProductDTO EditProduct(ProductDTO productDTO)
        {
            var product = _productRepository.GetById(productDTO.Id);
            product.ProductDescription = productDTO.ProductDescription;
            product.ProductName = productDTO.ProductName;
            product.ProductType = (ProductType)Enum.Parse(typeof(ProductType), productDTO.ProductType);
            product.ProductSizeNumber = productDTO.ProductSizeNumber;
            product.ProductPrice = productDTO.ProductPrice;
            product.ProductColor = productDTO.ProductColor;
            product.ProductMeasurements = productDTO.ProductMeasurements;
            product.ProductSize = Enum.TryParse(productDTO.ProductSize, out Size size) ? size : (Size?)null;
            product.ProductSubcategory = Enum.TryParse(productDTO.ProductSubcategory, out ClothingSubcategory clothing) ? clothing : (ClothingSubcategory?)null;
            product.ProductImage = productDTO.ProductImage;
            product.ProductMaterial = productDTO.ProductMaterial;
            product.ProductBrand = productDTO.ProductBrand;
            product.Condition = (Condition)Enum.Parse(typeof(Condition), productDTO.ProductCondition);
            product.ProductSex = (Sex)Enum.Parse(typeof(Sex), productDTO.ProductSex);
            product.ProductAvailablity = productDTO.ProductAvailablity;

            //so gospod napred
            product.ProductDaysRent = productDTO.ProductDaysRent;
            product.ProductRent = productDTO.ProductRent;

            _productRepository.Update(product);

            return (ProductDTO)product;
        }

        public List<ProductDTO> GetProducts(string rent,string type, string sex, string subcategory, string searchTerm, string colorFilter, string sizeFilter, string conditionFilter, string sortByPrice, string sortByUserRating, string shoeNumberRange)
        {
            var products = _productRepository.GetAllAvaliableProducts();

            //Filter by renting
            if(!string.IsNullOrEmpty(rent))
            {
                products = products.Where(p => p.ProductRent == Boolean.Parse(rent)).ToList();
            }
            //Filter by type
            if (!string.IsNullOrEmpty(type) && Enum.TryParse<ProductType>(type, out var productType))
            {
                products = products.Where(p => p.ProductType== productType).ToList();
            }

            // Filter by productSex
            if (!string.IsNullOrEmpty(sex) && Enum.TryParse<Sex>(sex, out var productSex))
            {
                products = products.Where(p => p.ProductSex == productSex).ToList();
            }

            //Filter by subcategory
            if (!string.IsNullOrEmpty(subcategory))
            {
                var subcategories = subcategory.Split('/');
                List<ClothingSubcategory> clothingSubcategories = new List<ClothingSubcategory>();

                foreach (var cs in subcategories)
                {
                    if (Enum.TryParse<ClothingSubcategory>(cs, out var subcat))
                    {
                        clothingSubcategories.Add(subcat);
                    }
                }

                products = products.Where(p => p.ProductSubcategory.HasValue && clothingSubcategories.Contains(p.ProductSubcategory.Value)).ToList();
            }

            // Filter by productName
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Filter by color
            if (!string.IsNullOrEmpty(colorFilter))
            {
                products = products.Where(p => p.ProductColor == colorFilter).ToList();
            }

            // Filter by size
            if (!string.IsNullOrEmpty(sizeFilter) && Enum.TryParse<Size>(sizeFilter, out var size))
            {
                products = products.Where(p => p.ProductSize == size).ToList();
            }


            // Filter by condition
            if (!string.IsNullOrEmpty(conditionFilter) && Enum.TryParse<Condition>(conditionFilter, out var condition))
            {
                products = products.Where(p => p.Condition == condition).ToList();
            }

            //Filter by shoe number

            if (!string.IsNullOrEmpty(type) && type.Equals("shoes") && !string.IsNullOrEmpty(shoeNumberRange))
            {

               int[] shoeNumberRangeArray = shoeNumberRange.Split(',').Select(int.Parse).ToArray();
                products = products.Where(p => p.ProductSizeNumber >= shoeNumberRangeArray[0] && p.ProductSizeNumber <= shoeNumberRangeArray[1]).ToList();
            }

            // Sort by price (ascending order)
            if (sortByPrice?.ToLower() == "desc")
            {
                products = products.OrderByDescending(p => p.ProductPrice).ToList();
            }
            else if (sortByPrice?.ToLower() == "asc")
            {
                products = products.OrderBy(p => p.ProductPrice).ToList();
            }

            //sort by user rating
            if (sortByUserRating?.ToLower() == "desc")
            {
                products = products.OrderByDescending(p => p.ShopApplicationUser.UserRating).ToList();
            }
            else if (sortByUserRating?.ToLower() == "asc")
            {
                products = products.OrderBy(p => p.ShopApplicationUser.UserRating).ToList();
            }

            List<ProductDTO> productsDTO = products.Select(p => (ProductDTO)p).ToList();

            return productsDTO;
        }

        public List<ProductDTO> GetMyProducts()
        {
            return _productRepository.GetMyProducts(_user.Email);
        }

        public ProductDTO GetProduct(int id)
        {
            return (ProductDTO)_productRepository.GetById(id);
               
        }

        public bool AddToShoppingCart(Product product, string email)
        {
            var user = _userRepository.GetByEmail(email);

            var userShoppingCart = user.UserShoppingCart;

            if (userShoppingCart != null && product != null)
            {
                var isAlreadyAdded = userShoppingCart.ProductsInShoppingCart.FirstOrDefault(p => p.ProductId == product.Id);

                if (isAlreadyAdded == null)
                {
                    var p = _productRepository.GetById(product.Id);
                    var productInShoppingCart = new ProductInShoppingCart
                    {
                        ShoppingCart = userShoppingCart,
                        Product = p,
                        ShoppingCartId = userShoppingCart.Id,
                        ProductId = p.Id
                    };

                    _productInShoppingCartRepository.Insert(productInShoppingCart);
                }
                return true;
            }

            return false;
        }

        public bool AddToFavourites(Product product, string email)
        {
            var user = _userRepository.GetByEmail(email);

            var userFavourites = user.UserFavourites;

            if (userFavourites != null && product != null)
            {
                var isAlreadyAdded = userFavourites.ProductsInFavourites.FirstOrDefault(p => p.ProductId == product.Id);

                if (isAlreadyAdded == null)
                {
                    var p = _productRepository.GetById(product.Id);
                    var productInFavourites = new ProductInFavourites
                    {
                        Favourites = userFavourites,
                        Product = p,
                        FavouritesId = userFavourites.Id,
                        ProductId = p.Id
                    };

                    _productInFavouritesRepository.Insert(productInFavourites);
                }
                return true;
            }

            return false;
        }

        public bool AddToRented(Product product, string email, DateTime EndDate)
        {
            var user = _userRepository.GetByEmail(email);

            var userRented = user.UserRented;

            if (userRented != null && product != null)
            {
                var isAlreadyAdded = userRented.ProductsInRented.FirstOrDefault(p => p.ProductId == product.Id);

                if (isAlreadyAdded == null)
                {
                    var p = _productRepository.GetById(product.Id);
                    var productInRented = new ProductInRented
                    {
                        Rented = userRented,
                        Product = p,
                        RentedId = userRented.Id,
                        ProductId = p.Id,
                        StartDate = DateTime.Now,
                        EndDate = EndDate

                    };

                    _productInRentedRepository.Insert(productInRented);
                }
                return true;
            }

            return false;
        }
    }
}
