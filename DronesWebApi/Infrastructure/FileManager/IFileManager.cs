using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DronesWebApi.Infrastructure.FileManager
{
    public interface IFileManager
    {
        Task<(string FileName, string ContentType, byte[] Data)> ConvertToBytesArray(IFormFile file);
    }
}
