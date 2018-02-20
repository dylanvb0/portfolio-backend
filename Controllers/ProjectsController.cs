using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [Route("api/{namespce}/[controller]")]
    public class ProjectsController : Controller
    {
        //GET api/values/5
        // [HttpGet("{id}")]
        // public Project Get(string namespce, long id)
        // {
        //     return GetDatastore().Read(id);
        // }

        // GET api/values
        [HttpGet()]
        public IEnumerable<Project> Get(string namespce)
        {
            return GetDatastore(namespce).List();
        }


        // POST api/values
        [HttpPost]
        public long Post(string namespce, [FromBody]Project value)
        {
          if(value.Id <= 0){
            return GetDatastore(namespce).Create(value);
          }else{
            return GetDatastore(namespce).Update(value);
          }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string namespce, long id, [FromBody]Project value)
        {
          value.Id = id;
          GetDatastore(namespce).Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string namespce, long id)
        {
          GetDatastore(namespce).Delete(id);
        }

        [HttpOptions]
        public void Options(){
        }

        [HttpOptions("{id}")]
        public void OptionsParam(int id){
        }

        private Datastore<Project> GetDatastore(string namespce = "dylanvb"){
          return new Datastore<Project>("Project", namespce);
        }

    }
}
