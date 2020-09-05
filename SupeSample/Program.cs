using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audit.Core;

namespace SupeSample
{
    class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public List<Pet> Pets { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }

        public PetType PetType { get; set; }
    }

    enum PetType
    {
        Cat,
        Dog
    }

    class Program
    {
        static void Main(string[] args)
        {
            Configuration.Setup()
                .UseMongoDB(config => config
                    .ConnectionString("mongodb://localhost:27017")
                    .Database("Audit")
                    .Collection("Event"));

            var parallelLoopResult = Parallel.For(0, 500_000, new ParallelOptions()
            {
                MaxDegreeOfParallelism = 20
            }, i =>
            {
                var person = new Person
                {
                    Name = "name#" + i,
                    Age = 100,
                    Pets = new List<Pet>
                    {
                        new Pet {PetType = PetType.Cat, Name = "Marlissa"}
                    }
                };

                using var scope = AuditScope.Create(new AuditScopeOptions()
                {
                    EventType = "Order:Update",
                    TargetGetter = () => person,
                    ExtraFields = new {MyProperty = "value"}
                });

                person.Age = 200;
                person.Pets.Add(new Pet {Name = "Shit", PetType = PetType.Dog});
            });


            Console.ReadLine();
        }
    }
}