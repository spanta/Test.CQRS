using System;
using System.Collections.Generic;

namespace Test.CQRS.ReadSide.CompetitorDatabase
{
    public interface ICompetitorDatabase
    {
        void Clear();
        List<CompetitorDto.ComponentPackageDto> SearchStandAlonePackages(string competitor, string productCategory, string location);
        void Add(CompetitorDto competitorDto);
        void AddOfferDefinition(Guid competitorId, string productCategory, string name, int downloadSpeed, int uploadSpeed, double setupPrice);
        void AddOfferPolicy(Guid id, string definition, List<string> locations);
        void AddPromotionDefinition(Guid competitorId, string name, double setSetupPrice);
        void AddPromotionPolicy(Guid id, string definition, List<string> locations);
        void ChangeOfferDefinition(Guid competitorId, string oldName, string newName);
        List<CompetitorDto.BundledPackageDto> SearchBundledPackages(string competitor, List<string> productCategories, string location);
    }
}
