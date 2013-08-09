using System;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddOfferDefinitionCommand : Command
    {
        public AddOfferDefinitionCommand(Guid itemId1) : base(itemId1, -1)
        {
        }

        public string Name { get; set; }

        public int DownloadSpeed { get; set; }

        public int UploadSpeed { get; set; }

        public string ProductCategory { get; set; }

        public double BaseSetupPrice { get; set; }
    }
}