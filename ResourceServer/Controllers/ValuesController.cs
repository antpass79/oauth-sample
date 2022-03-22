using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ResourceServer.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            Console.WriteLine($"id {id}");

            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            Console.WriteLine($"value {value}");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            Console.WriteLine($"id {id}, value {value}");
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Console.WriteLine($"id {id}");
        }
    }
}
