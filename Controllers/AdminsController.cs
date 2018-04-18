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
        [HttpGet("{email}/{password}")]
        public Admin Get(string email, string password)
        {
            var admins = GetDatastore().List();
            email = StaticMethods.Base64Decode(email);
            password = StaticMethods.Base64Decode(password);
            foreach(Admin admin in admins){
              if(email == admin.Email && BCrypt.CheckPassword(password + "$O*#La", admin.Password)){
                admin.SessionToken = StaticMethods.SecureRandomString();
                admin.TokenExpiration = DateTime.Now.ToUniversalTime().AddMinutes(30);
                GetDatastore().Update(admin);
                admin.Password = null;
                return admin;
              }
            }
            return null;
        }

        // POST api/values
        [HttpPost]
        public long Post([FromBody]Admin value)
        {
            string pwdToHash = value.Password + "$O*#La";
            string hashToStoreInDatabase = BCrypt.HashPassword(pwdToHash, BCrypt.GenerateSalt());
            value.Password = hashToStoreInDatabase;
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

        [HttpOptions("{placeholder}/{placeholder2}")]
        public void OptionsParam(string placeholder, string placeholder2){
        }

        public static Datastore<Admin> GetDatastore(){
          return new Datastore<Admin>("Admin", "");
        }
    }
}
