using System.Collections.Generic;

namespace Test.CQRS.WriteSide.Competitor.Domain
{
    public class PromotionPolicy{
        public string Definition { get; set; }

        public List<string> Locations { get; set; }
    }
}