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
    public Client(Entity entity){
      Id = entity.Key.Path.First().Id;
      Name = (string)entity["Name"];
      Email = (string)entity["Email"];
      Password = (string)entity["Password"];
      Namespace = (string)entity["Namespace"];
      Domain = (string)entity["Domain"];
      SessionToken = (string)entity["session_token"];
      string expiration = (string)entity["token_expiration"];
      if(expiration != null){
        TokenExpiration = DateTime.Parse((string)entity["token_expiration"]);
      }
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

    [JsonProperty("session_token")]
    public string SessionToken { get; set; }

    [JsonProperty("token_expiration")]
    public DateTime TokenExpiration { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(factory),
      ["Name"] = Name,
      ["Email"] = Email,
      ["Password"] = Password,
      ["Namespace"] = Namespace,
      ["Domain"] = Domain,
      ["session_token"] = SessionToken,
      ["token_expiration"] = TokenExpiration.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")
    };

    public Key GetKey(KeyFactory factory) {
      return factory.CreateKey(Id);
    }
  }
}
