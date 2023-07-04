using System.Collections.Generic;

namespace DronesWebApi.Commons.Configuration
{
    public class UploadFileOptions
    {
        public List<string> AllowedFormats { get; set; } = new List<string>() { ".jpg", ".jpeg", ".png", ".bmp" };
    }
}
