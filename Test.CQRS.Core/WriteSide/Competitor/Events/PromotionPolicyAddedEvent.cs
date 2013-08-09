using System;
using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.WriteSide.Competitor.Events
{
    public class PromotionPolicyAddedEvent : Event
    {

        public PromotionPolicyAddedEvent(Guid id)
        {
            AggregateId = id;
        }

        public string Definition { get; set; }

        public List<string> Locations { get; set; }
    }
}