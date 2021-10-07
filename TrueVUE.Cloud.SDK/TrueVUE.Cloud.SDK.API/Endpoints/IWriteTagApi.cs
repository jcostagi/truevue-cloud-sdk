using Refit;
using System.Threading.Tasks;
using TrueVUE.Cloud.SDK.API.Requests;

namespace TrueVUE.Cloud.SDK.API.Endpoints
{
    public interface IWriteTagApi
    {
        [Post("/api/v1/writetags/applytagwrites")]
        Task<string> ApplyTagWrites([Query] string apikey, [Body] ApplyTagWritesRequest request, [Authorize("Bearer")] string token = null);

        [Post("/api/v1/commissioning/reportTagWrite")]
        Task<string> ReportTagWrite([Query] string apikey, [Body] ReportTagWriteRequest request, [Authorize("Bearer")] string token = null);

        [Post("/api/v1/writetags/applytagmoves")]
        Task<string> ApplyTagMoves([Query] string apikey, [Body] ApplyTagMovesRequest request, [Authorize("Bearer")] string token = null);

    }
}
