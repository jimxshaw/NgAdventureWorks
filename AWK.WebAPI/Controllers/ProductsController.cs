using AWK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWK.WebAPI.Models;

namespace AWK.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Models.Product> Get()
        {
            var context = new AwkEntities();

            List<Models.Product> products = context.Products
                                                    .Where(p => p.ListPrice > 0)
                                                    .Take(50)
                                                    .Select(p => new Models.Product
                                                    {
                                                        ProductId = p.ProductID,
                                                        ProductName = p.Name,
                                                        ProductCode = p.ProductNumber,
                                                        Price = p.ListPrice,
                                                        ReleaseDate = p.SellStartDate

                                                    })
                                                    .ToList();

            return products;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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