using System;
using System.Collections.Generic;

namespace Test.CQRS.ReadSide.CompetitorDatabase
{

    public class CompetitorDto
    {


        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ComponentPackageDto> StandAlonePackages=new List<ComponentPackageDto>();
        public List<PromotionDefinitionDto> PromotionDefinitions = new List<PromotionDefinitionDto>();

        public List<BundledPackageDto> BundledPackages=new List<BundledPackageDto>();

        public class BundledPackageDto
        {
            public string Location { get; set; }

            public BundledPackageDto(string location)
            {
                Location = location;
            }

            public string Name { get { return ComponentPackages.Count + " Play"; } }
            public List<ComponentPackageDto> ComponentPackages=new List<ComponentPackageDto>();
        }

        public class ComponentPackageDto
        {
            public string Name { get; set; }

            private double _promotionalSetupPrice=-1;
            public double PromotionalSetupPrice
            {
                get { return _promotionalSetupPrice; }
                set { _promotionalSetupPrice = value; }
            }

            public int DownloadSpeed { get; set; }

            public int UploadSpeed { get; set; }

            public string Location { get; set; }

            public double BaseSetupPrice { get; set; }

            public string ProductCategory { get; set; }

            public ComponentPackageDto()
            {
                DownloadSpeed = -1;
                UploadSpeed = -1;
            }

            public double GetSetupPrice()
            {
                if (PromotionalSetupPrice == -1) return BaseSetupPrice;
                return PromotionalSetupPrice;
            }
        }

        public class PromotionDefinitionDto
        {
            public string Name { get; set; }

            public double SetSetupxPrice { get; set; }
        }
    }
}