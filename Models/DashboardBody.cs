using Google.Cloud.Datastore.V1;

namespace portfolio_backend.Models
{
  public interface DashboardBody
  {
    Entity ToEntity();
  }
}
