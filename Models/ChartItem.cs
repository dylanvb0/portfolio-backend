using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class ChartItem : DSObject {
    public ChartItem(){
    }
    public ChartItem(Entity entity){
      Random random = new Random();
      Id = random.Next(1, int.MaxValue);
      Name = (string)entity["name"];
      Value = (int)entity["x_value"];
      SortOrder = (int)entity["sort_order"];
    }

    public ChartItem(dynamic obj){
      Random random = new Random();
      Id = random.Next(1, int.MaxValue);
      Name = obj.name;
      Value = obj.x_value;
      SortOrder = obj.sort_order;
    }


    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("x_value")]
    public int Value { get; set; }

    [JsonProperty("sort_order")]
    public int SortOrder { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(),
      ["name"] = Name,
      ["x_value"] = Value,
      ["sort_order"] = SortOrder
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
