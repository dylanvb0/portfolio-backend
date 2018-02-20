using Google.Cloud.Datastore.V1;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace portfolio_backend.Models
{
  public abstract class DSObject
  {
    [Key]
    [JsonProperty("id")]
    public long Id { get; set; }
    public abstract Entity ToEntity(KeyFactory factory);
  }
}
