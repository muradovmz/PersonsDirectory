using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RelatedPerson>()
                .HasOne(p => p.RelatPerson)
                .WithMany()
                .HasForeignKey(p => p.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelatedPerson>()
                .HasOne(x => x.Person)
                .WithMany(y => y.RelatedPeople)
                .HasForeignKey(pt => pt.PersonId);

        }
    }
}
