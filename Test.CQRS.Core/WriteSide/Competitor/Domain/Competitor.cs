using System;
using System.Collections.Generic;
using Test.CQRS.Infrastructure.Storage.Memento;
using Test.CQRS.WriteSide.Common.Domain;
using Test.CQRS.WriteSide.Common.Events;
using Test.CQRS.WriteSide.Competitor.Events;

namespace Test.CQRS.WriteSide.Competitor.Domain
{
    public class Competitor : AggregateRoot,
        IHandle<CompetitorCreatedEvent>,
        IHandle<OfferDefinitionAddedEvent>,
        IHandle<OfferPolicyAddedEvent>,
        IHandle<PromotionDefinitionAddedEvent>,
        IHandle<PromotionPolicyAddedEvent>,
        IHandle<OfferDefinitionChangedEvent>,
            IOriginator
    {
        private List<OfferDefinition> offerDefinitions = new List<OfferDefinition>();
        private List<OfferPolicy> offerPolicies = new List<OfferPolicy>();
        private List<PromotionDefinition> promotionDefinitions = new List<PromotionDefinition>();
        private List<PromotionPolicy> promotionPolicies = new List<PromotionPolicy>();
        public Competitor(){}
        public Competitor(Guid id, string name)
        {
            ApplyChange(new CompetitorCreatedEvent(id, name));
        }
        public BaseMemento GetMemento()
        {
            //TODO: memento
            return new CompetitorMemento() {};
        }

        public void SetMemento(BaseMemento memento)
        {
            //TODO: memento
            
        }

        public void AddOfferDefinition(string name, string productCategory, int downloadSpeed, int uploadSpeed, double setupPrice)
        {
            ApplyChange(new OfferDefinitionAddedEvent(Id,name,productCategory,downloadSpeed,uploadSpeed, setupPrice));
        }

        public void Handle(CompetitorCreatedEvent e)
        {
            Id = e.AggregateId;
            Name = e.Name;
            Version = e.Version;
        }
        public void Handle(OfferDefinitionAddedEvent e)
        {
            offerDefinitions.Add(new OfferDefinition()
                                     {
                                         Name = e.Name,
                                         DownloadSpeed = e.DownloadSpeed,
                                         UploadSpeed = e.UploadSpeed,
                                         ProductCategory = e.ProductCategory
                                     });
        }

        protected string Name { get; set; }

        public void AddOfferPolicy(string offerDefinition, List<string> locations)
        {
            ApplyChange(new OfferPolicyAddedEvent(Id){Definition=offerDefinition,Locations=locations});
        }

        public void AddPromotionDefinition(string name, double setSetupPrice)
        {
            ApplyChange(new PromotionDefinitionAddedEvent(Id){Name=name,SetSetupPrice=setSetupPrice});
        }

        public void AddPromotionPolicy(string promotionDefinition, List<string> locations)
        {
            ApplyChange(new PromotionPolicyAddedEvent(Id){Definition=promotionDefinition,Locations=locations});
        }

        public void ChangeOfferDefinition(string newName, string oldName)
        {
            ApplyChange(new OfferDefinitionChangedEvent(Id){OldName=oldName,NewName=newName});
        }

        public void Handle(OfferPolicyAddedEvent e)
        {
            offerPolicies.Add(new OfferPolicy() { Definition = e.Definition, Locations = e.Locations });
        }

        public void Handle(PromotionDefinitionAddedEvent e)
        {
            promotionDefinitions.Add(new PromotionDefinition() { Name = e.Name, SetSetupPrice = e.SetSetupPrice });
        }

        public void Handle(PromotionPolicyAddedEvent e)
        {
            promotionPolicies.Add(new PromotionPolicy() { Definition = e.Definition, Locations = e.Locations });
        }

        public void Handle(OfferDefinitionChangedEvent e)
        {
            offerDefinitions.ForEach(x=>
                                         {
                                             if(x.Name.Equals(e.OldName))
                                             {
                                                 x.Name = e.NewName;
                                             }
                                         });
            offerPolicies.ForEach(x =>
                                      {
                                          if (x.Definition.Equals(e.OldName))
                                          {
                                              x.Definition = e.NewName;
                                          }
                                      });
        }
    }
}