using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

    }
}
