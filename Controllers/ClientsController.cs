using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portfolio_backend.Models;
using Newtonsoft.Json;

namespace portfolio_backend.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return GetDatastore().List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Client Get(long id)
        {
            return GetDatastore().Read(id);
        }

        // GET api/values/5
        [HttpGet("{domain}")]
        public Client Get(string domain)
        {
            var clients = GetDatastore().List();
            foreach(Client client in clients){
              if(domain == client.Domain){
                return client;
              }
            }
            return null;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Client value)
        {
            GetDatastore().Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]Client value)
        {
          value.Id = id;
          GetDatastore().Update(value);
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

        private Datastore<Client> GetDatastore(){
          return new Datastore<Client>("Client", "");
        }

    }
}
