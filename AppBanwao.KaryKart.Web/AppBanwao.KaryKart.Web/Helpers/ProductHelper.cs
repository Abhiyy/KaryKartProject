using AppBanwao.KarryKart.Model;
using AppBanwao.KaryKart.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Helpers
{
   
    public class ProductHelper
    {

        public ApiHelper _apiHelper = null;

        public ProductHelper()
        {
            _apiHelper = new ApiHelper();
        }

        public IList<ProductDetailsModel> GetAllProducts()
        {
            var productList= _apiHelper.DeserializeToList<ProductDetailsModel>( _apiHelper.SendRequest("Product"));

            if (productList == null)
                productList = new List<ProductDetailsModel>();

            return productList;
        }

        public ProductDetailsModel GetProduct(Guid id)
        {

               var product = JsonConvert.DeserializeObject<ProductDetailsModel>(_apiHelper.SendRequest("Product?id="+id));

               return product;
        }
        
    }
}