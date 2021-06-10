using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RelatedPerson
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid RelatedPersonId { get; set; }

        public int RelationshipId { get; set; }
    }
}
