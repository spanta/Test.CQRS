using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{
    public class OfferDefinitionAddedEventHandler : IEventHandler<OfferDefinitionAddedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public OfferDefinitionAddedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }

        public void Handle(OfferDefinitionAddedEvent handle)
        {
            _competitorDatabase.AddOfferDefinition(handle.Id,
                                                   handle.ProductCategory,
                                                   handle.Name,
                                                   handle.DownloadSpeed,
                                                   handle.UploadSpeed,
                                                   handle.SetupPrice);
        }
    }
}