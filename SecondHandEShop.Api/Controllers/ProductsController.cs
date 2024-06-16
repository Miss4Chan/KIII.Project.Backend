using Domain.Domain_models;
using Domain.DTO;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Linq;

namespace SecondHandEShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet("productConditions")]
        public IActionResult GetConditionTypes()
        {
            var conditionTypes = Enum.GetValues(typeof(Condition)).Cast<Condition>().Select(x => x.ToString()).ToList();
            return Ok(conditionTypes);
        }

        [HttpGet("productSex")]
        public IActionResult GetSexTypes()
        {
            var sexTypes = Enum.GetValues(typeof(Sex)).Cast<Sex>().Select(x => x.ToString()).ToList();
            return Ok(sexTypes);
        }

        [HttpGet("productTypes")]
        public IActionResult GetProductTypes()
        {
            var productTypes = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().Select(x => x.ToString()).ToList();
            return Ok(productTypes);
        }

        [HttpGet("productSizes")]
        public IActionResult GetProductSizes()
        {
            var productSizes = Enum.GetValues(typeof(Size)).Cast<Size>().Select(x => x.ToString()).ToList();
            return Ok(productSizes);
        }

        [HttpGet("productSubcategory")]
        public IActionResult GetProductSubcateogry()
        {
            var productSubcategories = Enum.GetValues(typeof(ClothingSubcategory)).Cast<ClothingSubcategory>().Select(x => x.ToString()).ToList();
            return Ok(productSubcategories);
        }

        [HttpGet]
        public IActionResult GetProducts(string rent="",string type = "", string sex = "", string subcategory="", string searchTerm = "", string colorFilter = "", string sizeFilter = "", string conditionFilter = "", string sortByPrice = "", string sortByUserRating = "", string shoeNumberRange="")
        {
            
            return Ok(_productService.GetProducts(rent,type, sex, subcategory, searchTerm, colorFilter, sizeFilter, conditionFilter, sortByPrice, sortByUserRating, shoeNumberRange));
        }

        [HttpGet("myProducts")]
        public IActionResult GetMyProducts()
        {
            return Ok(_productService.GetMyProducts());
        }
        [HttpGet("{id}",Name ="GetProduct")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productService.GetProduct(id));
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var newProduct = _productService.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { newProduct.Id }, newProduct);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(ProductDTO productDTO)
        {
            _productService.DeleteProduct(productDTO);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditProduct(ProductDTO productDTO)
        {
            return Ok(_productService.EditProduct(productDTO));
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(AddProductToDTO item)
        {
            return Ok(_productService.AddToShoppingCart(item.Product, item.Email));
        }

        [HttpPost("AddToFavourites")]
        public IActionResult AddToFavourites(AddProductToDTO item)
        {
            return Ok(_productService.AddToFavourites(item.Product, item.Email));
        }

        [HttpPost("AddToRented")]
        public IActionResult AddToRented(AddProductToDTO item, DateTime EndDate)
        {
            return Ok(_productService.AddToRented(item.Product, item.Email,EndDate));
        }
    }
}
