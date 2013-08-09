using System;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddPromotionDefinitionCommand : Command{
        public AddPromotionDefinitionCommand(Guid id) : base(id, -1)
        {
        }

        public string Name { get; set; }

        public double SetSetupPrice { get; set; }
    }
}