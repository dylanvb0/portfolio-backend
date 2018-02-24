using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Google.Cloud.Datastore.V1;
using Google.Protobuf;
using System.Collections.Generic;

namespace portfolio_backend.Models {
  public class Project : DSObject {
    public Project(){
    }
    public Project(Entity entity){
      Id = entity.Key.Path.First().Id;
      Name = (string)entity["name"];
      Description = (string)entity["description"];
      DemoLink = (string)entity["demo_link"];
      HiddenSections = ((Entity[])entity["hidden_sections"]).Select(ety => new Section(ety));
      MarketingUrl = (string)entity["marketing_url"];
      Pictures = (string[])entity["pictures"];
      Season = (string)entity["season"];
      SortOrder = (int)entity["sort_order"];
      Technologies = (string)entity["technologies"];
      Year = (int)entity["year"];

    }

    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("demo_link")]
    public string DemoLink { get; set; }

    [JsonProperty("hidden_sections")]
    public IEnumerable<Section> HiddenSections { get; set; }

    [JsonProperty("marketing_url")]
    public string MarketingUrl { get; set; }

    [JsonProperty("pictures")]
    public IEnumerable<string> Pictures { get; set; }

    [JsonProperty("season")]
    public string Season { get; set; }

    [JsonProperty("sort_order")]
    public int SortOrder { get; set; }

    [JsonProperty("technologies")]
    public string Technologies { get; set; }

    [JsonProperty("year")]
    public int Year { get; set; }

    public override Entity ToEntity(KeyFactory factory) => new Entity()
    {
      Key = GetKey(factory),
      ["name"] = Name,
      ["description"] = Description,
      ["demo_link"] = DemoLink,
      ["hidden_sections"] = (Entity[])(HiddenSections ?? Enumerable.Empty<Section>()).Select(section => section.ToEntity(null)).ToArray(),
      ["marketing_url"] = MarketingUrl,
      ["pictures"] = (Value[])(Pictures ?? Enumerable.Empty<string>()).Select( picture => ToPicValue(picture)).ToArray(),
      ["season"] = Season,
      ["sort_order"] = SortOrder,
      ["technologies"] = Technologies,
      ["year"] = Year
    };

    public Key GetKey(KeyFactory factory) {
      return factory.CreateKey(Id);
    }

    public Value ToPicValue(string picture){
      return new Value(){
        StringValue = picture
      };
    }
  }

}
