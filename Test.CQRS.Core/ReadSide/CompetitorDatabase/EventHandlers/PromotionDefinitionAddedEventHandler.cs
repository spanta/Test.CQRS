using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{
    public class PromotionDefinitionAddedEventHandler : IEventHandler<PromotionDefinitionAddedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public PromotionDefinitionAddedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }

        public void Handle(PromotionDefinitionAddedEvent handle)
        {
            _competitorDatabase.AddPromotionDefinition(handle.Id, handle.Name, handle.SetSetupPrice);
        }
    }
}