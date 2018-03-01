using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;
using System.Collections.Generic;

namespace portfolio_backend.Models {
  public class ListDashboardBody : DashboardBody {
    public ListDashboardBody(){
    }
    public ListDashboardBody(Entity entity){
      ListItems = (string[])entity["list_items"];
      TextSize = (string)entity["text_size"];
    }
    public ListDashboardBody(dynamic obj){
      ListItems = obj.list_items.ToObject<string[]>();
      TextSize = obj.text_size;
    }

    [JsonProperty("text")]
    public IEnumerable<string> ListItems { get; set; }

    [JsonProperty("text_size")]
    public string TextSize { get; set; }

    public Entity ToEntity() => new Entity()
    {
      ["list_items"] = (Value[])(ListItems ?? Enumerable.Empty<string>()).Select(item => ToItemValue(item)).ToArray(),
      ["text_size"] = TextSize
    };

    public Value ToItemValue(string item) => new Value() {
      StringValue = item
    };

  }
}
