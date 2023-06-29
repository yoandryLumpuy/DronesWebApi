using System.Collections.Generic;

namespace DronesWebApi.Commons.Responses
{
    public record ErrorResponse(int Status, string Detail, IDictionary<string, string[]> Errors)
    {
        public override string ToString()
        {
            return $"{{ Status = {Status}, Detail = {Detail}, Errors = {Errors} }}";
        }
    }
}
