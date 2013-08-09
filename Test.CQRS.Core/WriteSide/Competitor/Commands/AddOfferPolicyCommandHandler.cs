using System;
using Test.CQRS.Infrastructure.Storage;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class AddOfferPolicyCommandHandler : ICommandHandler<AddOfferPolicyCommand>
    {
        private IRepository<Competitor.Domain.Competitor> _repository;

        public AddOfferPolicyCommandHandler(IRepository<Competitor.Domain.Competitor> repository)
        {
            _repository = repository;
        }

        public void Execute(AddOfferPolicyCommand command)
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
            aggregate.AddOfferPolicy(command.OfferDefinition, command.Locations);
            _repository.Save(aggregate, command.Version);
        }
    }
}