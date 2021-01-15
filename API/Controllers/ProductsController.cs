using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
     
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productsRepository.GetProductsAsync();

            return Ok(products);
           
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            
            return product;
        }


        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productsRepository.GetProductBrandsAsync();

            return Ok(brands);

        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _productsRepository.GetProductTypesAsync();

            return Ok(types);

        }

    }
}
