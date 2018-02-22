using System;
﻿using System.Security.Cryptography;
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

        // GET api/values/5
        // [HttpGet("{id}")]
        // public Client Get(long id)
        // {
        //     return GetDatastore().Read(id);
        // }

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

        [HttpGet("{email}/{password}")]
        public Client Get(string email, string password)
        {
            var clients = GetDatastore().List();
            email = Base64Decode(email);
            password = Base64Decode(password);
            foreach(Client client in clients){
              if(email == client.Email && BCrypt.CheckPassword(password + "$O*#La", client.Password)){
                client.SessionToken = SecureRandomString();
                client.TokenExpiration = DateTime.Now.ToUniversalTime().AddMinutes(30);
                GetDatastore().Update(client);
                client.Password = null;
                return client;
              }
            }
            return null;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return GetDatastore().List();
        }

        // POST api/values
        [HttpPost]
        public long Post([FromBody]Client value)
        {
            string pwdToHash = value.Password + "$O*#La"; // Hardcoded salt
            string hashToStoreInDatabase = BCrypt.HashPassword(pwdToHash, BCrypt.GenerateSalt());
            value.Password = hashToStoreInDatabase;
            return GetDatastore().Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public long Put(long id, [FromBody]Client value)
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

        private Datastore<Client> GetDatastore(){
          return new Datastore<Client>("Client", "");
        }

        private string Base64Encode(string plainText){
          var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
          return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string base64EncodedData) {
          var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
          return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private string SecureRandomString(){
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[45];
            provider.GetBytes(byteArray);
            return Base64Encode(BitConverter.ToString(byteArray));
        }


    }
}
