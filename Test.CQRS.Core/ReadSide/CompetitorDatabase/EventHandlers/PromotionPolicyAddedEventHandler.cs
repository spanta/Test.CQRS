using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{
    public class PromotionPolicyAddedEventHandler : IEventHandler<PromotionPolicyAddedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public PromotionPolicyAddedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }
        public void Handle(PromotionPolicyAddedEvent handle)
        {
            _competitorDatabase.AddPromotionPolicy(handle.Id, handle.Definition, handle.Locations);
        }
    }
}