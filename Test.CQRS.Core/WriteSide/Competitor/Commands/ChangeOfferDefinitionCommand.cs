using System;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class ChangeOfferDefinitionCommand:Command
    {
        public ChangeOfferDefinitionCommand(Guid id) : base(id, -1)
        {
        }

        public string OldName { get; set; }

        public string NewName { get; set; }
    }
}