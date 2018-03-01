using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;
using System.Collections.Generic;

namespace portfolio_backend.Models {
  public class HorizontalBarChartDashboardBody : DashboardBody {
    public HorizontalBarChartDashboardBody(){
    }
    public HorizontalBarChartDashboardBody(Entity entity){
      ChartItems = ((Entity[])entity["chart_items"]).Select(ety => new ChartItem(ety));
      MinValue = (int)entity["min_value"];
      MaxValue = (int)entity["max_value"];
      XLabels = (string[])entity["x_labels"];
    }
    public HorizontalBarChartDashboardBody(dynamic obj){
      ChartItems = ((IEnumerable<dynamic>)(obj.chart_items ?? Enumerable.Empty<ChartItem>())).Select(item => new ChartItem(item));
      MinValue = obj.min_value;
      MaxValue = obj.max_value;
      XLabels = obj.x_labels.ToObject<string[]>();
    }

    [JsonProperty("chart_items")]
    public IEnumerable<ChartItem> ChartItems { get; set; }

    [JsonProperty("min_value")]
    public int MinValue { get; set; }

    [JsonProperty("max_value")]
    public int MaxValue { get; set; }

    [JsonProperty("x_labels")]
    public IEnumerable<string> XLabels { get; set; }


    public Entity ToEntity() => new Entity()
    {
      ["chart_items"] = (Entity[])(ChartItems ?? Enumerable.Empty<ChartItem>()).Select(item => item.ToEntity(null)).ToArray(),
      ["min_value"] = MinValue,
      ["max_value"] = MaxValue,
      ["x_labels"] = (Value[])(XLabels ?? Enumerable.Empty<string>()).Select(item => ToItemValue(item)).ToArray()
    };

    public Value ToItemValue(string item) => new Value() {
      StringValue = item
    };

  }
}
