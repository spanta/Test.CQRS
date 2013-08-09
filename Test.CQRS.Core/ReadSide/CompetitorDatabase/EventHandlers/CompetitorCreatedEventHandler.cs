using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.ReadSide.CompetitorDatabase.EventHandlers
{

    public class CompetitorCreatedEventHandler : IEventHandler<CompetitorCreatedEvent>
    {
        private readonly ICompetitorDatabase _competitorDatabase;

        public CompetitorCreatedEventHandler(ICompetitorDatabase competitorDatabase)
        {
            _competitorDatabase = competitorDatabase;
        }

        public void Handle(CompetitorCreatedEvent handle)
        {
            _competitorDatabase.Add(new CompetitorDto() { Id = handle.Id, Name = handle.Name});
        }
    }
}
