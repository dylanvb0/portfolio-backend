using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class Client : DSObject {
    public Client(){
    }
    public Client(long Id, string Name, string Email, string Password, string Namespace){
      this.Name = Name;
      this.Email = Email;
      this.Password = Password;
      this.Namespace = Namespace;
      this.Domain = Domain;
    }
    public Client(Entity entity){
      Id = entity.Key.Path.First().Id;
      Name = (string)entity["Name"];
      Email = (string)entity["Email"];
      Password = (string)entity["Password"];
      Namespace = (string)entity["Namespace"];
      Domain = (string)entity["Domain"];
    }


    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("namespace")]
    public string Namespace { get; set; }

    [JsonProperty("domain")]
    public string Domain { get; set; }

    public override Entity ToEntity() => new Entity()
    {
      Key = GetKey(),
      ["Name"] = Name,
      ["Email"] = Email,
      ["Password"] = Password,
      ["Namespace"] = Namespace,
      ["Domain"] = Domain
    };

    public Key GetKey() {
      return new Key().WithElement("Client", Id);
    }
  }
}
