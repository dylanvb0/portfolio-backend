using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;

namespace portfolio_backend.Models {
  public class DashboardItem : DSObject {
    public DashboardItem(){
    }
    public DashboardItem(Entity entity){
      Id = entity.Key.Path.First().Id;
      HeaderText = (string)entity["header_text"];
      Type = (string)entity["type"];
      SortOrder = (int)entity["sort_order"];
      switch(Type){
        case "simple":
          Body = new SimpleDashboardBody((Entity)entity["body"]);
          break;
        case "list":
          Body = new ListDashboardBody((Entity)entity["body"]);
          break;
        case "horizontal_bar":
          Body = new HorizontalBarChartDashboardBody((Entity)entity["body"]);
          break;
        default:
          Body = null;
          break;
      }
    }
    public DashboardItem(dynamic obj){
      if(obj.id != null)
        Id = obj.id;
      HeaderText = obj.header_text;
      Type = obj.type;
      SortOrder = obj.sort_order;
      switch(Type){
        case "simple":
          Body = new SimpleDashboardBody(obj.body);
          break;
        case "list":
          Body = new ListDashboardBody(obj.body);
          break;
        case "horizontal_bar":
          Body = new HorizontalBarChartDashboardBody(obj.body);
          break;
        default:
          Body = null;
          break;
      }

    }

    [Required]
    [JsonProperty("header_text")]
    public string HeaderText { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("sort_order")]
    public int SortOrder { get; set; }

    [JsonProperty("body")]
    public DashboardBody Body { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(factory),
      ["header_text"] = HeaderText,
      ["type"] = Type,
      ["sort_order"] = SortOrder,
      ["body"] = (Entity)Body.ToEntity()
    };

    public Key GetKey(KeyFactory factory) {
      return factory.CreateKey(Id);
    }
  }

}
