using System.Collections.Generic;
using AppMvc.Net.Models;

namespace AppMvc.Net.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]{
                new ProductModel(){Id = 1, name ="Iphone x", price= 1000},
                new ProductModel(){Id = 2, name ="Samsung x", price= 500},
                new ProductModel(){ Id = 3,name ="Sony x", price= 1500},
                new ProductModel(){ Id = 3,name ="Nokia x", price= 1400},
            });
        }
    }
}