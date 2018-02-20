using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class Section : DSObject {
    public Section(){
    }
    public Section(Entity entity){
      Random random = new Random();
      Id = random.Next(1, int.MaxValue);
      Title = (string)entity["title"];
      Text = (string)entity["text"];
    }


    [Required]
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(),
      ["title"] = Title,
      ["text"] = Text
    };

    public Key GetKey() {
      if(Id == 0) {
        Random random = new Random();
        Id = random.Next(1, int.MaxValue);
      }
      return new Key().WithElement("Section", Id);
    }
  }

}
