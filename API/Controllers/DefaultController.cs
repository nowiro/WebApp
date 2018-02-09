using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class DefaultController : ApiController
    {
        private WebAppEntities db = new WebAppEntities();

        // GET api/Default
        public IHttpActionResult GetProduct(string search = "", string sortBy = "Id", string sortDirection = "asc", int pageSize = 10, int page = 0)
        {
            var result = from s in db.Product select s;

            // IOrderedQueryable<Product> sortExpression;
            Func<Product, Object> sortExpression = null;

            switch (sortBy)
            {
                case "Name":
                    sortExpression = (s => s.Name);
                    break;
                case "Description":
                    sortExpression = (s => s.Description);
                    break;
                default:
                    sortExpression = (s => s.Id);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(s => s.Name.Contains(search) || s.Description.Contains(search));
            }

            var resultCount = result.Count();
            var pageCount = Convert.ToInt32(Math.Ceiling((double)(Convert.ToDouble(resultCount) / Convert.ToDouble(pageSize))));

            List<Product> results = null;

            if (sortDirection.ToLower() == "desc")
            {
                results = result.OrderByDescending(sortExpression).Skip(page * pageSize).Take(pageSize).ToList();
            }
            else
            {
                results = result.OrderBy(sortExpression).Skip(page * pageSize).Take(pageSize).ToList();
            }

            var info = new SearchingSortingPagingInfo()
            {
                Search = search,
                SortBy = sortBy,
                SortDirection = sortDirection,
                PageSize = pageSize,
                PageCount = resultCount > 0 ? pageCount : 0,
                Page = resultCount > 0 ? page + 1 : 0,
                ResultCount = resultCount
            };

            ResultsInfo resultsInfo = new ResultsInfo
            {
                Data = results,
                Info = info
            };


            if (resultsInfo == null)
            {
                return NotFound();
            }

            return Ok(resultsInfo);
        }

        // GET api/Default/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product Product = db.Product.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        // PUT api/Default/5
        public IHttpActionResult PutProduct(int id, Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Product.Id)
            {
                return BadRequest();
            }

            db.Entry(Product).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Default
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(Product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Product.Id }, Product);
        }

        // DELETE api/Default/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product Product = db.Product.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            db.Product.Remove(Product);
            db.SaveChanges();

            return Ok(Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.Id == id) > 0;
        }
    }
}