using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
   
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository,
            IGenericRepository<ProductType> productTypesRepository,
            IGenericRepository<ProductBrand> productBranddRepository,IMapper mapper)
        {
            _productsRepository = productsRepository;
            _productTypesRepository = productTypesRepository;
            _productBrandsRepository = productBranddRepository;
             _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            var products = await _productsRepository.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
           
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepository.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));
            
            return _mapper.Map<Product,ProductToReturnDto>(product);

        }
   


        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandsRepository.ListAllAsync();

            return Ok(brands);

        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _productTypesRepository.ListAllAsync();

            return Ok(types);

        }

    }
}
