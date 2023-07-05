using System.Collections.Generic;

namespace DronesWebApi.Commons.Configuration
{
    public class UploadFileOptions
    {
        public List<string> AllowedFormats { get; set; } = new List<string>() { ".jpg", ".jpeg", ".png", ".bmp" };

        public int MaxFileSizeInBytes { get; set; } = 10 * 1024 * 1024;
    }
}
