using DronesWebApi.Commons.Mappings;

namespace DronesWebApi.Models.Image.Queries.DownloadImageQuery
{
    public class ImageDto: IMapFrom<Core.Domain.Image>
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}
