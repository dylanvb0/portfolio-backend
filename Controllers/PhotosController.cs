using System;
﻿using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
// using System.Web.Http;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using portfolio_backend.Models;

namespace portfolio_backend.Controllers
{
    [Route("api/{namespce}/[controller]")]
    public class PhotosController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public PhotosController(IHostingEnvironment environment) {
            _hostingEnvironment = environment;
        }

        // GET api/values
        [HttpGet()]
        public IEnumerable<string> Get(string namespce)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, namespce);
            var images = Path.Combine(uploads, "images");
            DirectoryInfo directory = new DirectoryInfo(images);
            return directory.GetFiles().ToList().Select(file => file.Name);
        }


        // POST api/values
        [HttpPost]
        public async Task<long> Post(string namespce)
        {
          if(!Authorized(namespce)) return -1;
          var file = Request.Form.Files[0];
          var uploads = Path.Combine(_hostingEnvironment.WebRootPath, namespce);
          var images = Path.Combine(uploads, "images");
          var filePath = Path.Combine(images, file.FileName);
          using (var fileStream = new FileStream(filePath, FileMode.Create)) {
              await file.CopyToAsync(fileStream);
          }
          return 0;
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public void Delete(string namespce, string name)
        {
          if(!Authorized(namespce)) return;
          var uploads = Path.Combine(_hostingEnvironment.WebRootPath, namespce);
          var images = Path.Combine(uploads, "images");
          DirectoryInfo directory = new DirectoryInfo(images);
          var result = directory.GetFiles(name);
          if(result.Length == 1){
            result[0].Delete();
          }
        }

        [HttpOptions]
        public void Options(){
        }

        [HttpOptions("{placeholder}")]
        public void OptionsParam(string placeholder){
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
