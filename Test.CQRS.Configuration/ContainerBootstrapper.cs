using StructureMap;
using Test.CQRS.Infrastructure.Messaging;
using Test.CQRS.Infrastructure.Storage;
using Test.CQRS.Infrastructure.Utils;
using Test.CQRS.ReadSide.CompetitorDatabase;

namespace Test.CQRS.Configuration
{
    static class ContainerBootstrapper
    {
        public static void BootstrapStructureMap()
        {
            
            ObjectFactory.Initialize(x =>
                                         {
                                             x.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));
                                             x.For<IEventStorage>().Singleton().Use<InMemoryEventStorage>();
                                             x.For<IEventBus>().Use<EventBus>();
                                             x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                                             x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                                             x.For<ICommandBus>().Use<CommandBus>();
                                             x.For<IEventBus>().Use<EventBus>();
//                x.For<IReportDatabase>().Use<ReportDatabase>();
//                x.For<IReportDatabase2>().Use<ReportDatabase2>();
//                x.For<IPackageDatabase>().Use<PackageDatabase>();
                                             x.For<ICompetitorDatabase>().Use<CompetitorDatabase>();
                                         });
        }
    }
}