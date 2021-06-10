using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class RelatedPersonSeedModel
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public string RelatedPersonId { get; set; }

        public int RelationshipId { get; set; }
    }
}
