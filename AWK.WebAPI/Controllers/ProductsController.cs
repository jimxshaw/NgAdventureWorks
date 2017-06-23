using AWK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWK.WebAPI.Models;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using System.Web.Mvc;

namespace AWK.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:53805", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET api/<controller>
        [EnableQuery()] // Enables OData querying.
        public IQueryable<Models.Product> Get()
        {
            var context = new AwkEntities();

            List<Models.Product> products = context.Products
                                                    .Where(p => p.ListPrice > 0)
                                                    .Take(20)
                                                    .Select(p => new Models.Product
                                                    {
                                                        ProductId = p.ProductID,
                                                        ProductName = p.Name,
                                                        ProductCode = p.ProductNumber,
                                                        Price = p.ListPrice,
                                                        ReleaseDate = p.SellStartDate

                                                    })
                                                    .ToList();

            // To utilize OData querying, the return type must be an IQueryable.
            return products.AsQueryable();
        }

        // Action not need due to using OData above.
        // GET api/<controller>/queryString
        //public IEnumerable<Models.Product> Get(string search)
        //{
        //    var context = new AwkEntities();

        //    List<Models.Product> products = context.Products
        //                                            .Where(p => p.ListPrice > 0)
        //                                            .Take(20)
        //                                            .Select(p => new Models.Product
        //                                            {
        //                                                ProductId = p.ProductID,
        //                                                ProductName = p.Name,
        //                                                ProductCode = p.ProductNumber,
        //                                                Price = p.ListPrice,
        //                                                ReleaseDate = p.SellStartDate

        //                                            })
        //                                            .ToList();

        //    return products.Where(p => p.ProductCode.Contains(search));
        //}

        // GET api/<controller>/5
        public Models.Product Get(int id)
        {
            var context = new AwkEntities();

            var product = new Models.Product();

            if (id > 0)
            {
                var productEntity = context.Products.FirstOrDefault(p => p.ProductID == id);

                if (productEntity != null)
                {
                    product = new Models.Product
                    {
                        ProductId = productEntity.ProductID,
                        ProductName = productEntity.Name,
                        ProductCode = productEntity.ProductNumber,
                        Price = productEntity.ListPrice,
                        ReleaseDate = productEntity.SellStartDate
                    };
                }
                else
                {
                    product = new Models.Product
                    {
                        ProductId = 1000,
                        ProductName = "Default Name",
                        ProductCode = "XYZ123",
                        Price = 39.99M,
                        ReleaseDate = DateTime.Now
                    };
                }


            }
            else
            {
                product = new Models.Product
                {
                    ProductId = 1000,
                    ProductName = "Default Name",
                    ProductCode = "XYZ123",
                    Price = 39.99M,
                    ReleaseDate = DateTime.Now
                };
            }

            return product;
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Models.Product product)
        {
            var context = new AwkEntities();

            if (product != null)
            {
                var productEntity = new Data.Product
                {
                    Name = product.ProductName,
                    ProductNumber = product.ProductCode,
                    ListPrice = product.Price,
                    SellStartDate = product.ReleaseDate
                };

                context.Products.Add(productEntity);
                context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] Models.Product product)
        {
            var context = new AwkEntities();

            if (id > 0)
            {
                if (product != null)
                {
                    var productEntity = context.Products.FirstOrDefault(p => p.ProductID == id);

                    productEntity.Name = product.ProductName;
                    productEntity.ProductNumber = product.ProductCode;
                    productEntity.ListPrice = product.Price;
                    productEntity.SellStartDate = product.ReleaseDate;

                    context.SaveChanges();

                    return Ok();
                }
            }

            return NotFound();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}