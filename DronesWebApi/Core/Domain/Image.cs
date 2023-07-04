namespace DronesWebApi.Core.Domain
{
    public class Image
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }


        public string MedicationCode { get; set; }

        public virtual Medication Medication { get; set; }
    }
}
