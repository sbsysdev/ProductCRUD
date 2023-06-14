using ProductCRUD.Data;
using ProductCRUD.Models;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProductCRUD.Controllers
{
    public class ApiCategoryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [ResponseType(typeof(void))]
        public IHttpActionResult Post([FromBody] Category category)
        {
            if (category == null || category.Name.Length == 0) return BadRequest("Please, type a valid category name!");

            Connection connection = new Connection();

            Param categoryNameParam = new Param { Key = "category_name", Value = category.Name };

            bool created = connection.Command("sp_create_category", new List<Param> { categoryNameParam });

            Debug.WriteLine(created);

            if (!created) return StatusCode(HttpStatusCode.Conflict);

            return Created("", new {});
        }

        // PUT api/<controller>/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Category category)
        {
            if (category == null || category.Name.Length == 0) return BadRequest("Please, type a valid category name!");

            Connection connection = new Connection();

            // Find category
            Param categoryIdParam = new Param { Key = "category_id", Value = id };
            
            DataSet categoryDS = connection.Query("sp_read_category_by_id", new List<Param> { categoryIdParam });

            if (categoryDS == null || categoryDS.Tables[0].AsEnumerable().Count() != 1) return NotFound();

            // update category
            Param categoryNameParam = new Param { Key = "category_name", Value = category.Name };

            bool updated = connection.Command("sp_update_category_by_id", new List<Param> { categoryIdParam, categoryNameParam });

            if (!updated) return StatusCode(HttpStatusCode.Conflict);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}