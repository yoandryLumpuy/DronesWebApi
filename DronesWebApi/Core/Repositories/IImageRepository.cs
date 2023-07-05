using DronesWebApi.Core.Domain;

namespace DronesWebApi.Core.Repositories
{
    public interface IImageRepository : IRepository<Image>
    {
        Image GetImageOfMedication(string medicationCode);
    }
}
