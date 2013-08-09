using System;
using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddOfferPolicyCommand : Command
    {
        public AddOfferPolicyCommand(Guid id) : base(id, -1)
        {
        }

        public string OfferDefinition { get; set; }

        public List<string> Locations { get; set; }
    }
}