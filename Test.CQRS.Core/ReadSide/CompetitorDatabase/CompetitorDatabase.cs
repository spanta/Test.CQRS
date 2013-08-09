using System;
using System.Collections.Generic;

namespace Test.CQRS.ReadSide.CompetitorDatabase
{
    public class CompetitorDatabase:ICompetitorDatabase
    {
        static List<CompetitorDto> competitors=new List<CompetitorDto>();
        public void Clear()
        {
            competitors = new List<CompetitorDto>();
        }

        public List<CompetitorDto.ComponentPackageDto> SearchStandAlonePackages(string competitor, string productCategory, string location)
        {
            var results = new List<CompetitorDto.ComponentPackageDto>();
            competitors.Find(x=>x.Name.Equals(competitor)).StandAlonePackages.ForEach(x=>
                                                                                          {
                                                                                              var productCategoryMatched = true;
                                                                                              if (!x.ProductCategory.Equals(productCategory))
                                                                                              {
                                                                                                  productCategoryMatched = false;
                                                                                              }
                                                                                              if (x.Location.Equals(location) && productCategoryMatched)
                                                                                              {
                                                                                                  results.Add(x);
                                                                                              }
                                                                                          });
            return results;
        }

        public void Add(CompetitorDto competitorDto)
        {
            competitors.Add(competitorDto);
        }

        public void AddOfferDefinition(Guid competitorId, string productCategory, string name, int downloadSpeed, int uploadSpeed, double baseSetupPrice)
        {
            //add standalone and bundled packages
            //apply promotions to all packages
            var newStandAlonePackage = new CompetitorDto.ComponentPackageDto()
                                           {
                                               Name = name,
                                               ProductCategory = productCategory,
                                               DownloadSpeed = downloadSpeed,
                                               UploadSpeed = uploadSpeed,
                                               Location = "Not Applicable",
                                               BaseSetupPrice=baseSetupPrice
                                           };
            competitors.Find(x=>x.Id.Equals(competitorId)).StandAlonePackages.Add(newStandAlonePackage);
        }

        public void AddOfferPolicy(Guid competitorId, string name, List<string> locations)
        {
            var competitor = competitors.Find(x => x.Id.Equals(competitorId));
            var competitorPackage=competitor.StandAlonePackages.Find(x => x.Name.Equals(name));
            var newPackages = new List<CompetitorDto.ComponentPackageDto>();
            locations.ForEach(location=>
                                  {
                                      var newPackage = new CompetitorDto.ComponentPackageDto()
                                                           {
                                                               Name = competitorPackage.Name,
                                                               DownloadSpeed = competitorPackage.DownloadSpeed,
                                                               Location = location,
                                                               ProductCategory = competitorPackage.ProductCategory,
                                                               BaseSetupPrice = competitorPackage.BaseSetupPrice,
                                                               PromotionalSetupPrice = competitorPackage.PromotionalSetupPrice,
                                                               UploadSpeed = competitorPackage.UploadSpeed
                                                           };
                                      //TODO: apply existing promotion policies to the new package
                                      newPackages.Add(newPackage);
                                  });
            newPackages.ForEach(component =>
                                  {
                                      competitor.StandAlonePackages.ForEach(existingPackage =>
                                                              {
                                                                  //if location matches and the product categories are different,
                                                                  if(component.Location.Equals(existingPackage.Location))
                                                                  {
                                                                      //bundle these component packages with all other component packages
                                                                      var bundledPackage = new CompetitorDto.BundledPackageDto(existingPackage.Location);
                                                                      bundledPackage.ComponentPackages.Add(component);
                                                                      bundledPackage.ComponentPackages.Add(existingPackage);
                                                                      competitor.BundledPackages.Add(bundledPackage);
                                                                  }
                                                              });
                                  });
            competitor.StandAlonePackages.AddRange(newPackages);
        }

        public void AddPromotionDefinition(Guid competitorId, string name, double setSetupPrice)
        {
            var competitor = competitors.Find(x => x.Id.Equals(competitorId));
            competitor.PromotionDefinitions.Add(new CompetitorDto.PromotionDefinitionDto()
                                                    {
                                                        Name=name,
                                                        SetSetupxPrice=setSetupPrice
                                                    });
        }

        public void AddPromotionPolicy(Guid competitorId, string name, List<string> locations)
        {
            var competitor = competitors.Find(x => x.Id.Equals(competitorId));
            var promotionDefinition = competitor.PromotionDefinitions.Find(x => x.Name.Equals(name));
            competitor.StandAlonePackages.ForEach(package=>
                                                      {
                                                          //apply promotion policy
                                                          if(locations.Contains(package.Location))
                                                          {
                                                              package.PromotionalSetupPrice = promotionDefinition.SetSetupxPrice;
                                                          }
                                                      });
        }

        public void ChangeOfferDefinition(Guid competitorId, string oldName, string newName)
        {
            var competitor = competitors.Find(x => x.Id.Equals(competitorId));
            competitor.StandAlonePackages.ForEach(x =>
                                                      {
                                                          if(x.Name.Equals(oldName))
                                                          {
                                                              x.Name = newName;
                                                          }
                                                      });
        }

        public List<CompetitorDto.BundledPackageDto> SearchBundledPackages(string competitor, List<string> productCategories, string location)
        {
            var results = new List<CompetitorDto.BundledPackageDto>();
            var compeitors2 = new List<CompetitorDto>(){
                                new CompetitorDto()
                                {
                                    Name = "Comcast",
                                    Id = Guid.NewGuid(),
                                    BundledPackages = new List<CompetitorDto.BundledPackageDto>()
                                                          {
                                                              new CompetitorDto.BundledPackageDto("73072")
                                                                  {
                                                                      ComponentPackages = new List<CompetitorDto.ComponentPackageDto>()
                                                                                              {
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Starter Basic Internet",
                                                                                                          ProductCategory = "Internet"
                                                                                                      },
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Starter Video",
                                                                                                          ProductCategory = "Video"
                                                                                                      }
                                                                                              }
                                                                  },
                                                              new CompetitorDto.BundledPackageDto("73002")
                                                                  {
                                                                      ComponentPackages = new List<CompetitorDto.ComponentPackageDto>()
                                                                                              {
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Starter Basic Internet",
                                                                                                          ProductCategory = "Internet"
                                                                                                      },
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Starter Video",
                                                                                                          ProductCategory = "Video"
                                                                                                      }
                                                                                              }
                                                                  },
                                                              new CompetitorDto.BundledPackageDto("73072")
                                                                  {
                                                                      ComponentPackages = new List<CompetitorDto.ComponentPackageDto>()
                                                                                              {
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Advanced Phone",
                                                                                                          ProductCategory = "Phone"
                                                                                                      },
                                                                                                  new CompetitorDto.ComponentPackageDto()
                                                                                                      {
                                                                                                          Name="Starter Video",
                                                                                                          ProductCategory = "Video"
                                                                                                      }
                                                                                              }
                                                                  }
                                                          }
                                }
                                
            };
            competitors.Find(x => x.Name.Equals(competitor)).BundledPackages.ForEach(x =>
            {
                var productCategoriesMatched = true;
                var locationMatched = x.Location.Equals(location);
                x.ComponentPackages.ForEach(y =>
                                                {
                                                    if (!productCategories.Contains(y.ProductCategory))
                                                    {
                                                        productCategoriesMatched = false;
                                                    }
                                                });
                if(productCategoriesMatched && locationMatched)
                {
                    results.Add(x);
                }
                
            });
            return results;
        }
    }
}