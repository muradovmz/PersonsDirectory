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
        public static async Task SeedData(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Relationships.Any())
                {
                    //var relationshipsData = File.ReadAllText("../Persistence/SeedData/relationships.json");

                    //var relationships = JsonSerializer.Deserialize<List<Relationship>>(relationshipsData);

                    var relList = new List<Relationship>()
                    {
                        new Relationship() {Designation="თანამშრომელი"},
                        new Relationship() {Designation="ნაცნობი"},
                        new Relationship() {Designation="ნათესავი"},
                        new Relationship() {Designation="სხვა"}
                    };

                    foreach (var item in relList)
                    {
                        context.Relationships.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Persons.Any())
                {
                    //var personsData = File.ReadAllText("../Persistence/SeedData/persons.json");

                    //var people = JsonSerializer.Deserialize<List<PersonSeedModel>>(personsData);

                    var peopList = new List<Person>()
                    {
                        new Person()
                        {
                            Id= new Guid("632b2279-9fab-498e-99c1-ecc2d0cdba06"),
                            PrivateNumber= "00000000001",
                            FirstnameGE= "ტესტ1",
                            FirstnameEN= "Test1",
                            LastnameGE= "სატესტო1",
                            LastnameEN= "Testing1",
                            Birthdate= DateTime.Parse("1980-01-01"),
                            Address= "სატესტო მისამართი 1",
                            Phone= "000000001",
                            Email= "test1@test.org"
                        },
                        new Person()
                        {
                            Id= new Guid("2f40f5c0-43d7-4c91-9a1b-313402ee79a9"),
                            PrivateNumber= "00000000002",
                            FirstnameGE= "ტესტ2",
                            FirstnameEN= "Test2",
                            LastnameGE= "სატესტო2",
                            LastnameEN= "Testing2",
                            Birthdate= DateTime.Parse("1982-01-01"),
                            Address= "სატესტო მისამართი 2",
                            Phone= "000000002",
                            Email= "test2@test.org"
                        },
                        new Person()
                        {
                            Id= new Guid("2ddc7a48-6ed4-4813-a849-4b1f83b743ab"),
                            PrivateNumber= "00000000003",
                            FirstnameGE= "ტესტ3",
                            FirstnameEN= "Test3",
                            LastnameGE= "სატესტო3",
                            LastnameEN= "Testing3",
                            Birthdate= DateTime.Parse("1983-01-01"),
                            Address= "სატესტო მისამართი 3",
                            Phone= "000000003",
                            Email= "test3@test.org"
                        },
                        new Person()
                        {
                            Id= new Guid("3F8BBF31-442D-450D-5649-08D92BE6D987"),
                            PrivateNumber= "00000000004",
                            FirstnameGE= "ტესტ4",
                            FirstnameEN= "Test4",
                            LastnameGE= "სატესტო4",
                            LastnameEN= "Testing4",
                            Birthdate= DateTime.Parse("1984-01-01"),
                            Address= "სატესტო მისამართი 4",
                            Phone= "000000004",
                            Email= "test4@test.org"
                        },
                        new Person()
                        {
                            Id= new Guid("05394204-3085-4C56-564A-08D92BE6D987"),
                            PrivateNumber= "00000000005",
                            FirstnameGE= "ტესტ5",
                            FirstnameEN= "Test5",
                            LastnameGE= "სატესტო5",
                            LastnameEN= "Testing5",
                            Birthdate= DateTime.Parse("1985-01-01"),
                            Address= "სატესტო მისამართი 5",
                            Phone= "000000005",
                            Email= "test5@test.org"
                        },
                    };

                    foreach (var item in peopList)
                    {
                        var PictureUrl = "images/people/person.png";
                        var pictureFileName = PictureUrl.Substring(16);

                        item.AddPhoto(PictureUrl, pictureFileName);
                        context.Persons.Add(item);
                    }

                    await context.SaveChangesAsync();




                }

                if (!context.RelatedPersons.Any())
                {
                    //var relatedPersonsData = File.ReadAllText("../Persistence/SeedData/relatedPersons.json");

                    //var relatedPeople = JsonSerializer.Deserialize<List<RelatedPersonSeedModel>>(relatedPersonsData);

                    var relPeopList = new List<RelatedPerson>()
                    {
                        new RelatedPerson()
                        {
                            PersonId=new Guid("632b2279-9fab-498e-99c1-ecc2d0cdba06"),
                            RelatedPersonId = new Guid("3F8BBF31-442D-450D-5649-08D92BE6D987"),
                            RelationshipId = 1
                        },
                        new RelatedPerson()
                        {
                            PersonId=new Guid("632b2279-9fab-498e-99c1-ecc2d0cdba06"),
                            RelatedPersonId = new Guid("2F40F5C0-43D7-4C91-9A1B-313402EE79A9"),
                            RelationshipId = 3
                        },
                        new RelatedPerson()
                        {
                            PersonId=new Guid("3F8BBF31-442D-450D-5649-08D92BE6D987"),
                            RelatedPersonId = new Guid("2F40F5C0-43D7-4C91-9A1B-313402EE79A9"),
                            RelationshipId = 4
                        },
                        new RelatedPerson()
                        {
                            PersonId=new Guid("2DDC7A48-6ED4-4813-A849-4B1F83B743AB"),
                            RelatedPersonId = new Guid("3F8BBF31-442D-450D-5649-08D92BE6D987"),
                            RelationshipId = 3
                        },
                        new RelatedPerson()
                        {
                            PersonId=new Guid("3F8BBF31-442D-450D-5649-08D92BE6D987"),
                            RelatedPersonId = new Guid("632b2279-9fab-498e-99c1-ecc2d0cdba06"),
                            RelationshipId = 1
                        },
                    };

                    foreach (var item in relPeopList)
                    {


                        context.RelatedPersons.Add(item);
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
