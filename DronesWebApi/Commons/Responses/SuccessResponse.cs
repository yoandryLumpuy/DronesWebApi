using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DronesWebApi.Commons.Responses
{
    public record SuccessResponse<T>(T Value, int Status = StatusCodes.Status200OK) where T : class
    {
        public override string ToString()
        {
            return $"{{ Success = {Status}, Value = {Value} }}";
        }
    }
}
