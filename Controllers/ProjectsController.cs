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
            if(Authorized(namespce)){
              Console.WriteLine("authorized");
            }else{
              Console.WriteLine("denied");
            }
            return GetDatastore(namespce).List();
        }


        // POST api/values
        [HttpPost]
        public long Post(string namespce, [FromBody]Project value)
        {
          if(!Authorized(namespce)) return -1;
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

        private bool Authorized(string namespce){
          string authHeader = Request.Headers["Authorization"];
          if(authHeader != null && authHeader.StartsWith("Bearer")){
            string token = authHeader.Substring("Bearer ".Length).Trim();
            token = StaticMethods.Base64Decode(token);
            Console.WriteLine(token);
            var clients = ClientsController.GetDatastore().List();
            Client client = null;
            foreach(Client c in clients){
              if(namespce == c.Namespace){
                client = c;
              }
            }
            Console.WriteLine(client);
            if(client == null) return false;
            Console.WriteLine(client.SessionToken);
            Console.WriteLine(StaticMethods.Base64Decode(client.SessionToken));
            if(StaticMethods.Base64Decode(client.SessionToken) == token &&
               client.TokenExpiration.ToUniversalTime() > DateTime.Now.ToUniversalTime()){
                  client.TokenExpiration = DateTime.Now.ToUniversalTime().AddMinutes(30);
                  ClientsController.GetDatastore().Update(client);
                  return true;
            }
            return false;
          }else{
            return false;
          }
        }

    }
}
