using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [Route("api/[controller]")]
    public class AdminsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return GetDatastore().List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Admin Get(long id)
        {
            return GetDatastore().Read(id);
        }

        // POST api/values
        [HttpPost]
        public long Post([FromBody]Admin value)
        {
            return GetDatastore().Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public long Put(long id, [FromBody]Admin value)
        {
          value.Id = id;
          return GetDatastore().Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
          GetDatastore().Delete(id);
        }

        [HttpOptions]
        public void Options(){
        }

        [HttpOptions("{id}")]
        public void OptionsParam(int id){
        }

        private Datastore<Admin> GetDatastore(){
          return new Datastore<Admin>("Admin", "");
        }

    }
}
