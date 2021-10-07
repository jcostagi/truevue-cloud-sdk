using Refit;
using System;
using System.Threading.Tasks;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IUserApi
    {
        [Post("/api/v1/roles")]
        Task<string> GetRoles([Query] string apikey, [Authorize("Bearer")] string token = null);

        [Post("/api/v1/users?enabled=true")]
        Task<string> GetUserByRole([Query] string apikey, [Query] Guid businessUnitId, [Query] Guid tenantId, [Query] Guid roleId, [Authorize("Bearer")] string token = null);
    }
}
