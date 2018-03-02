using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;
using System.Collections.Generic;

namespace portfolio_backend.Models {
  public class About : DSObject {
    public About(){
    }
    public About(Entity entity){
      Id = entity.Key.Path.First().Id;
      Overview = (string)entity["overview"];
      Text = (string)entity["text"];
      Pictures = (string[])entity["pictures"];
      Email = (string)entity["email"];
      LinkedIn = (string)entity["linkedin"];
      GitHub = (string)entity["github"];
      StackOverflow = (string)entity["stackoverflow"];
    }

    [Required]
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("overview")]
    public string Overview { get; set; }

    [JsonProperty("pictures")]
    public IEnumerable<string> Pictures { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("linkedin")]
    public string LinkedIn { get; set; }

    [JsonProperty("github")]
    public string GitHub { get; set; }

    [JsonProperty("stackoverflow")]
    public string StackOverflow { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(factory),
      ["text"] = ToTextValue(Text),
      ["overview"] = Overview,
      ["pictures"] = (Value[])(Pictures ?? Enumerable.Empty<string>()).Select( picture => ToPicValue(picture)).ToArray(),
      ["email"] = Email,
      ["linkedin"] = LinkedIn,
      ["github"] = GitHub,
      ["stackoverflow"] = StackOverflow
    };

    public Key GetKey(KeyFactory factory) {
      return factory.CreateKey(Id);
    }

    public Value ToPicValue(string picture){
      return new Value(){
        StringValue = picture
      };
    }

    public Value ToTextValue(string text){
      return new Value(){
        StringValue = text,
        ExcludeFromIndexes = true
      };
    }
  }

}
