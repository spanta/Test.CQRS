using System;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Competitor.Events
{
    public class PromotionDefinitionAddedEvent : Event
    {

        public PromotionDefinitionAddedEvent(Guid id)
        {
            AggregateId = id;
        }

        public string Name { get; set; }

        public double SetSetupPrice { get; set; }
    }
}