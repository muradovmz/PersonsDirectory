using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RelatedPerson:BaseEntity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid RelatedPersonId { get; set; }
        public Person RelatPerson { get; set; }

        public int RelationshipId { get; set; }
        public Relationship Relationship { get; set; }
    }
}
