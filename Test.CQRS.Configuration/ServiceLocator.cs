using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Test.CQRS.Infrastructure.Messaging;
using Test.CQRS.ReadSide.CompetitorDatabase;

namespace Test.CQRS.Configuration
{
    public sealed class ServiceLocator
    {
        private static  ICommandBus _commandBus;
//        private static IReportDatabase _reportDatabase;
//        private static IReportDatabase2 _reportDatabase2;
//        private static IPackageDatabase _packageDatabase;
        private static  bool _isInitialized;
        private static readonly object _lockThis = new object();
        private static ICompetitorDatabase _competitorDatabase;

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    ContainerBootstrapper.BootstrapStructureMap();
                    _commandBus = ObjectFactory.GetInstance<ICommandBus>();
//                    _reportDatabase = ObjectFactory.GetInstance<IReportDatabase>();
//                    _reportDatabase2 = ObjectFactory.GetInstance<IReportDatabase2>();
//                    _packageDatabase = ObjectFactory.GetInstance<IPackageDatabase>();
                    _competitorDatabase = ObjectFactory.GetInstance<ICompetitorDatabase>();
                    _isInitialized = true;
                }
            }


        }



        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }

//        public static IReportDatabase ReportDatabase
//        {
//            get { return _reportDatabase; }
//        }
//
//        public static IReportDatabase2 ReportDatabase2
//        {
//            get { return _reportDatabase2; }
//        }
//
//        public static IPackageDatabase PackageDatabase
//        {
//            get { return _packageDatabase; }
//        }

        public static ICompetitorDatabase CompetitorDatabase
        {
            get { return _competitorDatabase; }
        }
    }
}
