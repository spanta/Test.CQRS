using System.Collections.Generic;

namespace Test.CQRS.WriteSide.Competitor.Domain
{
    public class OfferPolicy{
        public string Definition { get; set; }

        public List<string> Locations { get; set; }
    }
}