using System;
using System.Collections.Generic;
using Test.CQRS.Configuration;
using Test.CQRS.ReadSide.CompetitorDatabase;
using Test.CQRS.WriteSide.Competitor.Commands;
using NUnit.Framework;
using Telogical.Framework.Specifications;

namespace Specifications
{

    [Subject("Packages")]
    class Searching_Packages : Specification
    {
        [SetUp]
        public void Init()
        {
            new ServiceLocator();
            ServiceLocator.CompetitorDatabase.Clear();
        }

        private List<CompetitorDto.ComponentPackageDto> _comcastInternetPackagesInNorman;
        private List<CompetitorDto.ComponentPackageDto> _comcastInternetPackagesInOkc;
        private List<CompetitorDto.BundledPackageDto> _comcastInternetVideoPackagesInNorman;
        private List<CompetitorDto.BundledPackageDto> _comcastPhoneVideoPackagesInNorman;

        public override void Given()
        {
            i_have_comcast_offerings();
        }
        private void i_have_comcast_offerings()
        {
            /*
             * Competitor is the Aggregate Root in my domain model. The justification is that in our domain everything revolves around a competitor.
             * In this POC, there are 6 commands in the domain. These commands are used to generate domain events that are used to auto configure standalone and bundled packages: 
             *      CreateCompetitor - when we start monitoring a Competitor, 
             *      AddOfferDefinition - when a competitor starts its product offering, This command describes the product only
             *      AddOfferPolicy - when a competitor offers to a specific market location. At this time, this POC only involves location w.r.t. policy
             *      AddPromotionDefinition - when a competitor starts its campaign with a specific promotion. This command describes only the effect of the promotion.
             *      AddPromotionPolicy - when a competitor applies promotion across markets and product categories.
             *      ChangeOfferDefinition - when a competitor updates the offering, for example change of name.This POC only supports change of name.
            */
            var competitorGuid = Guid.NewGuid();
            var createCommand = new CreateCompetitorCommand(competitorGuid, "Comcast");
            ServiceLocator.CommandBus.Send(createCommand);

            var offerDefinitionCommand = new AddOfferDefinitionCommand(competitorGuid)
            {
                Name = "Basic Internet",
                ProductCategory = "Internet",
                DownloadSpeed = 5,
                UploadSpeed = 1,
                BaseSetupPrice=50
            };
            ServiceLocator.CommandBus.Send(offerDefinitionCommand);

            var offerPolicyCommand = new AddOfferPolicyCommand(competitorGuid)
            {
                OfferDefinition = "Basic Internet",
                Locations = new List<string> { "10001", "10002", "73072", "73069", "73002" }
            };
            ServiceLocator.CommandBus.Send(offerPolicyCommand);

            var promotionDefinitionCommand = new AddPromotionDefinitionCommand(competitorGuid)
            {
                Name = "$2 Setup",
                SetSetupPrice = 2
            };
            ServiceLocator.CommandBus.Send(promotionDefinitionCommand);

            var promotionPolicyCommand = new AddPromotionPolicyCommand(competitorGuid)
            {
                PromotionDefinition = "$2 Setup",
                Name = "Free Video and Phone Setup in Norman",
                ProductCategories = new List<string> { "Video", "Phone" },
                Locations = new List<string>() { "73072", "73069" }
            };
            ServiceLocator.CommandBus.Send(promotionPolicyCommand);

            var offerDefinitionChangeCommand = new ChangeOfferDefinitionCommand(competitorGuid)
            {
                OldName = "Basic Internet",
                NewName = "Starter Basic Internet"
            };
            ServiceLocator.CommandBus.Send(offerDefinitionChangeCommand);

            //for bundled packages

            var offerDefinitionCommand2 = new AddOfferDefinitionCommand(competitorGuid)
            {
                Name = "Starter Video",
                ProductCategory = "Video",
                BaseSetupPrice = 100
            };
            ServiceLocator.CommandBus.Send(offerDefinitionCommand2);

            var offerPolicyCommand2 = new AddOfferPolicyCommand(competitorGuid)
            {
                OfferDefinition = "Starter Video",
                Locations = new List<string> { "10001", "10002", "73072", "73069", "73002" }
            };
            ServiceLocator.CommandBus.Send(offerPolicyCommand2);

            var offerDefinitionCommand3 = new AddOfferDefinitionCommand(competitorGuid)
            {
                Name = "Starter Phone",
                ProductCategory = "Phone",
                BaseSetupPrice = 25
            };
            ServiceLocator.CommandBus.Send(offerDefinitionCommand3);

            var offerPolicyCommand3 = new AddOfferPolicyCommand(competitorGuid)
            {
                OfferDefinition = "Starter Phone",
                Locations = new List<string> { "10001", "10002", "73072", "73069", "73002" }
            };
            ServiceLocator.CommandBus.Send(offerPolicyCommand3);
        }

        public override void When()
        {
            _comcastInternetPackagesInNorman = ServiceLocator.CompetitorDatabase.SearchStandAlonePackages("Comcast", "Internet", "73072");
            _comcastInternetPackagesInOkc = ServiceLocator.CompetitorDatabase.SearchStandAlonePackages("Comcast", "Internet", "73002");
            _comcastInternetVideoPackagesInNorman = ServiceLocator.CompetitorDatabase.SearchBundledPackages("Comcast", new List<string> { "Internet", "Video" }, "73072");
            _comcastPhoneVideoPackagesInNorman = ServiceLocator.CompetitorDatabase.SearchBundledPackages("Comcast", new List<string> { "Phone", "Video" }, "73072");
        }

        [Test]
        public void It_should_search_standalone_packages()
        {
            _comcastInternetPackagesInNorman.Count.ShouldEqual(1);
            _comcastInternetPackagesInOkc.Count.ShouldEqual(1);
            var normanPackage = _comcastInternetPackagesInNorman[0];
            normanPackage.Name.ShouldEqual("Starter Basic Internet");
            normanPackage.GetSetupPrice().ShouldEqual(2);
            normanPackage.DownloadSpeed.ShouldEqual(5);
            normanPackage.UploadSpeed.ShouldEqual(1);
            var okcPackage = _comcastInternetPackagesInOkc[0];
            okcPackage.Name.ShouldEqual("Starter Basic Internet");
            okcPackage.GetSetupPrice().ShouldEqual(50);
            okcPackage.DownloadSpeed.ShouldEqual(5);
            okcPackage.UploadSpeed.ShouldEqual(1);
        }

        [Test]
        public void It_should_search_double_play_bundled_packages()
        {
            _comcastInternetVideoPackagesInNorman.Count.ShouldEqual(1);
            var internetVideoNormanPackage = _comcastInternetVideoPackagesInNorman[0];
            internetVideoNormanPackage.Name.ShouldEqual("2 Play");
            internetVideoNormanPackage.Location.ShouldEqual("73072");
            internetVideoNormanPackage.ComponentPackages.Count.ShouldEqual(2);
            var internetComponent = internetVideoNormanPackage.ComponentPackages[1];
            internetComponent.ProductCategory.ShouldEqual("Internet");
            internetComponent.Name.ShouldEqual("Starter Basic Internet");
            var videoComponent = internetVideoNormanPackage.ComponentPackages[0];
            videoComponent.ProductCategory.ShouldEqual("Video");
            videoComponent.Name.ShouldEqual("Starter Video");

            _comcastPhoneVideoPackagesInNorman.Count.ShouldEqual(1);
            var phoneVideoNormanPackage = _comcastPhoneVideoPackagesInNorman[0];
            phoneVideoNormanPackage.ComponentPackages.Count.ShouldEqual(2);
            var phoneComponent = phoneVideoNormanPackage.ComponentPackages[0];
            phoneComponent.ProductCategory.ShouldEqual("Phone");
            phoneComponent.Name.ShouldEqual("Starter Phone");
            var internetComponent2 = phoneVideoNormanPackage.ComponentPackages[1];
            internetComponent2.ProductCategory.ShouldEqual("Video");
            internetComponent2.Name.ShouldEqual("Starter Video");
        }
    }
}