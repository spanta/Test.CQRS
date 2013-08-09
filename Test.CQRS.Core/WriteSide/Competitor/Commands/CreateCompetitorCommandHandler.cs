using System;
using Test.CQRS.Infrastructure.Storage;
using Test.CQRS.WriteSide.Common.Commands;

namespace Test.CQRS.WriteSide.Competitor.Commands
{
    public class CreateCompetitorCommandHandler : ICommandHandler<CreateCompetitorCommand>
    {
        private IRepository<Competitor.Domain.Competitor> _repository;

        public CreateCompetitorCommandHandler(IRepository<Competitor.Domain.Competitor> repository)
        {
            _repository = repository;
        }

        public void Execute(CreateCompetitorCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_repository == null)
            {
                throw new InvalidOperationException("Repository is not initialized.");
            }
            var aggregate = new Competitor.Domain.Competitor(command.Id, command.Name);
            aggregate.Version = -1;
            _repository.Save(aggregate, aggregate.Version);
        }
    }
}