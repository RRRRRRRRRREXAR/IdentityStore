using Store.BLL.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO GetProduct(int id);
        ProductDTO FindProduct(ProductDTO product);
        ProductCategoryDTO GetCategory(int id);
        IEnumerable GetCategories();
        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="orderDTO"></param>
        void MakeOrder(UserDTO user,OrderDTO orderDTO);
        void Dispose();
    }
}
