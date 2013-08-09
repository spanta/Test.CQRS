using System;
using Test.CQRS.Infrastructure.Storage;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddOfferDefinitionCommandHandler : ICommandHandler<AddOfferDefinitionCommand>
    {
        private IRepository<Competitor.Domain.Competitor> _repository;

        public AddOfferDefinitionCommandHandler(IRepository<Competitor.Domain.Competitor> repository)
        {
            _repository = repository;
        }

        public void Execute(AddOfferDefinitionCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }

            var aggregate = _repository.GetById(command.Id);
            aggregate.AddOfferDefinition(command.Name, command.ProductCategory, command.DownloadSpeed, command.UploadSpeed, command.BaseSetupPrice);
            _repository.Save(aggregate, command.Version);
        }
    }
}