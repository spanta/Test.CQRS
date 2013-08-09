using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{
    public class OfferDefinitionChangedEventHandler : IEventHandler<OfferDefinitionChangedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public OfferDefinitionChangedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }

        public void Handle(OfferDefinitionChangedEvent handle)
        {
            _competitorDatabase.ChangeOfferDefinition(handle.Id, handle.OldName, handle.NewName);
        }
    }
}