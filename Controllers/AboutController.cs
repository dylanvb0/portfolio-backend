﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [Route("api/{namespce}/[controller]")]
    public class AboutController : Controller
    {
        //GET api/values/5
        // [HttpGet("{id}")]
        // public Project Get(string namespce, long id)
        // {
        //     return GetDatastore().Read(id);
        // }

        // GET api/values
        [HttpGet()]
        public IEnumerable<About> Get(string namespce)
        {
            return GetDatastore(namespce).List();
        }


        // POST api/values
        [HttpPost]
        public long Post(string namespce, [FromBody]About value)
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
        public void Put(string namespce, long id, [FromBody]About value)
        {
          if(!Authorized(namespce)) return;
          value.Id = id;
          GetDatastore(namespce).Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string namespce, long id)
        {
          if(!Authorized(namespce)) return;
          GetDatastore(namespce).Delete(id);
        }

        [HttpOptions]
        public void Options(){
        }

        [HttpOptions("{placeholder}")]
        public void OptionsParam(string placeholder){
        }

        private Datastore<About> GetDatastore(string namespce = "dylanvb"){
          return new Datastore<About>("About", namespce);
        }

        private bool Authorized(string namespce){
          if(!StaticMethods.ClientAuthorized(namespce, Request.Headers["Authorization"])){
            Response.StatusCode = 401;
            return false;
          };
          return true;
        }

    }
}
