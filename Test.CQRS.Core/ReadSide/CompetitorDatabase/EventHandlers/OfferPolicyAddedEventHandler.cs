using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{
    public class OfferPolicyAddedEventHandler : IEventHandler<OfferPolicyAddedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public OfferPolicyAddedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }
        public void Handle(OfferPolicyAddedEvent handle)
        {
            _competitorDatabase.AddOfferPolicy(handle.Id, handle.Definition, handle.Locations);
        }
    }
}