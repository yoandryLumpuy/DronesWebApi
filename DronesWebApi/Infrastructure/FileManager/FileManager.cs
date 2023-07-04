using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace DronesWebApi.Infrastructure.FileManager
{
    public class FileManager: IFileManager
    {
        public Task<(string FileName, string ContentType, byte[] Data)> ConvertToBytesArray(IFormFile file)
        {
            var stream = new MemoryStream();

            file.CopyToAsync(stream);

            new FileExtensionContentTypeProvider().TryGetContentType(Path.GetFileName(file.FileName), out var contentType);

            contentType ??= "application/octet-stream";

            return Task.FromResult((FileName: file.FileName, ContentType: contentType, Data: stream.ToArray()));
        }
    }
}
