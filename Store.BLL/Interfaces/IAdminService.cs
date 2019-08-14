using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IAdminService
    {
        void CreateProduct(ProductDTO product);
        void UpdateProduct(ProductDTO product);
        void DeleteProduct(int id);
        void CreateCategory(ProductCategoryDTO category);
        void UpdateCategory(ProductCategoryDTO category);
        void DeleteCategory(ProductCategoryDTO category);
        List<ProductCategoryDTO> GetCategories();
        ProductCategoryDTO GetCategory(int id);
        List<ProductDTO> GetProducts();
        ProductDTO GetProduct(int id);
        void Dispose();
    }
}
