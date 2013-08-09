using System;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Competitor.Events
{
    public class OfferDefinitionChangedEvent : Event
    {

        public OfferDefinitionChangedEvent(Guid id)
        {
            AggregateId = id;
        }

        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}