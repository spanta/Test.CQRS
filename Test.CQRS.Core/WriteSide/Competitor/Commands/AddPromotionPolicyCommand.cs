using System;
using System.Collections.Generic;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddPromotionPolicyCommand:Command
    {
        public AddPromotionPolicyCommand(Guid id) : base(id, -1)
        {
        }

        public string PromotionDefinition { get; set; }

        public string Name { get; set; }

        public List<string> ProductCategories { get; set; }

        public List<string> Locations { get; set; }
    }
}