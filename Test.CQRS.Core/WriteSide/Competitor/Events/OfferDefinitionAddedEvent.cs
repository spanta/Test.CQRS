using System;
using System.Linq;
using System.Text;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Competitor.Events
{
    public class OfferDefinitionAddedEvent : Event
    {

        public OfferDefinitionAddedEvent(Guid id, string name, string productCategory, int downloadSpeed, int uploadSpeed, double setupPrice)
        {
            Name = name;
            ProductCategory = productCategory;
            DownloadSpeed = downloadSpeed;
            UploadSpeed = uploadSpeed;
            SetupPrice = setupPrice;
            AggregateId = id;
        }

        public string Name { get; set; }
        public string ProductCategory { get; set; }
        public int DownloadSpeed { get; set; }
        public int UploadSpeed { get; set; }
        public double SetupPrice { get; set; }
    }
}
