using KarryKart.API.Helpers;
using KarryKart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KarryKart.API.Controllers
{
    //[EnableCors(origins: "http://localhost:15557", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        // GET api/<controller>
        ProductHelper _productHelper = null;
        public List<ProductModel> Get(bool ActiveOnly = true)
        {

            try
            {
                _productHelper = new ProductHelper();
                return _productHelper.GetAllProducts(ActiveOnly);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _productHelper = null;
            }
            return null;
        }

        // GET api/<controller>/5
        public ProductModel Get(Guid id)
        {
            try
            {
                _productHelper = new ProductHelper();
                return _productHelper.GetProductDetail(id);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _productHelper = null;
            }
            return null;
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}