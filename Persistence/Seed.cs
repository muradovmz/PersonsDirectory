using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.Relationships.Any())
                {
                    var relationshipsData = File.ReadAllText("../Persistence/SeedData/relationships.json");

                    var relationships = JsonSerializer.Deserialize<List<Relationship>>(relationshipsData);

                    foreach (var item in relationships)
                    {
                        context.Relationships.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Persons.Any())
                {
                    var personsData = File.ReadAllText("../Persistence/SeedData/persons.json");

                    var people = JsonSerializer.Deserialize<List<PersonSeedModel>>(personsData);

                    foreach (var item in people)
                    {
                        var pictureFileName = item.PictureUrl.Substring(16);

                        Guid guid;
                        Guid.TryParse(item.Id, out guid);

                        var person = new Person
                        {
                            Id = guid,
                            PrivateNumber = item.PrivateNumber,
                            FirstnameGE = item.FirstnameGE,
                            FirstnameEN = item.FirstnameEN,
                            LastnameGE = item.LastnameGE,
                            LastnameEN = item.LastnameEN,
                            Birthdate = item.Birthdate,
                            Address = item.Address,
                            Phone = item.Phone,
                            Email = item.Email
                        };
                        person.AddPhoto(item.PictureUrl, pictureFileName);
                        context.Persons.Add(person);
                    }
                    
                    await context.SaveChangesAsync();

                  


                }

                if (!context.RelatedPersons.Any())
                {
                    var relatedPersonsData = File.ReadAllText("../Persistence/SeedData/relatedPersons.json");

                    var relatedPeople = JsonSerializer.Deserialize<List<RelatedPersonSeedModel>>(relatedPersonsData);

                    foreach (var item in relatedPeople)
                    {
                        Guid personGuid;
                        Guid relatedPersonGuid;
                        Guid.TryParse(item.PersonId, out personGuid);
                        Guid.TryParse(item.RelatedPersonId, out relatedPersonGuid);

                        var relatedPerson = new RelatedPerson
                        {
                            PersonId = personGuid,
                            RelatedPersonId = relatedPersonGuid,
                            RelationshipId = item.RelationshipId
                        };

                        context.RelatedPersons.Add(relatedPerson);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Seed>();
                logger.LogError(ex.Message);
            }



        }
    }
}
