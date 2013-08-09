namespace Test.CQRS.WriteSide.Competitor.Domain
{
    public class OfferDefinition
    {
        public string Name { get; set; }

        public int DownloadSpeed { get; set; }

        public int UploadSpeed { get; set; }

        public string ProductCategory { get; set; }
    }
}