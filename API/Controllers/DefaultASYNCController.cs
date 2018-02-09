using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class DefaultASYNCController : ApiController
    {
        private WebAppEntities db = new WebAppEntities();

        // GET api/DefaultASYNC
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(string search = "", string sortBy = "Id", string sortDirection = "asc", int pageSize = 10, int page = 1)
        {
            var result = from s in db.Product select s;
            var page4Query = page - 1;
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
            //
            List<Product> results = null;
            if (sortDirection.ToLower() == "desc")
            {
                results = result.OrderByDescending(sortExpression).Skip(page4Query * pageSize).Take(pageSize).ToList();
            }
            else
            {
                results = result.OrderBy(sortExpression).Skip(page4Query * pageSize).Take(pageSize).ToList();
            }

            var info = new SearchingSortingPagingInfo()
            {
                Search = search,
                SortBy = sortBy,
                SortDirection = sortDirection,
                PageSize = pageSize,
                PageCount = pageCount,
                Page = page,
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

        // GET api/DefaultASYNC/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product Product = await db.Product.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        // PUT api/DefaultASYNC/5
        public async Task<IHttpActionResult> PutProduct(int id, Product Product)
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
                await db.SaveChangesAsync();
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

        // POST api/DefaultASYNC
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(Product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Product.Id }, Product);
        }

        // DELETE api/DefaultASYNC/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product Product = await db.Product.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            db.Product.Remove(Product);
            await db.SaveChangesAsync();

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