using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class SimpleDashboardBody : DashboardBody {
    public SimpleDashboardBody(){
    }
    public SimpleDashboardBody(Entity entity){
      Text = (string)entity["text"];
      TextSize = (string)entity["text_size"];
    }
    public SimpleDashboardBody(dynamic obj){
      Text = obj.text;
      TextSize = obj.text_size;
    }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("text_size")]
    public string TextSize { get; set; }

    public Entity ToEntity() => new Entity()
    {
      ["text"] = Text,
      ["text_size"] = TextSize,
    };

  }
}
