using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class Admin : DSObject {
    public Admin(){
    }
    public Admin(Entity entity){
      Id = entity.Key.Path.First().Id;
      Name = (string)entity["Name"];
      Email = (string)entity["Email"];
      Password = (string)entity["Password"];
    }

    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    public override Entity ToEntity() => new Entity()
    {
      Key = GetKey(),
      ["Name"] = Name,
      ["Email"] = Email,
      ["Password"] = Password
    };

    public Key GetKey() {
      return new Key().WithElement("Admin", Id);
    }
  }

}
