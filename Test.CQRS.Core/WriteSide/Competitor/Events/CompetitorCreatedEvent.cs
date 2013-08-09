using System;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Competitor.Events
{
    public class CompetitorCreatedEvent : Event
    {

        public CompetitorCreatedEvent(Guid id, string name)
        {
            Name = name;
            AggregateId = id;
        }

        public string Name { get; set; }
    }
}