using System;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class CreateCompetitorCommand : Command
    {
        public CreateCompetitorCommand(Guid itemId1, string name) : base(itemId1, -1)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}