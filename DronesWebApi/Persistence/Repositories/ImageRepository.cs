using System.Linq;
using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class ImageRepository: Repository<Image>, IImageRepository
    {
        public ImageRepository(DbContext context) : base(context)
        { }

        private DronesContext DronesContext => Context as DronesContext;

        public Image GetImageOfMedication(string medicationCode) =>
            DronesContext.Images.FirstOrDefault(i => i.MedicationCode == medicationCode);
    }
}
