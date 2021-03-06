﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using XLog.Core.Services;
using XLog.MongoDb;

namespace XLog.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repository = new MongoDbLogRepository(new OptionsWrapper<MongoDbOptions>(
                new MongoDbOptions
                {
                    CollectionName = "logs",
                    ConnectionString = "mongodb://127.0.0.1:27017",
                    Database = "iis_logs"
                }));

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, 100_000, async index =>
            {
                var person = new Person
                {
                    Name = "name#" + index,
                    Age = 100,
                    Pets = new List<Pet>
                    {
                        new Pet {PetType = PetType.Cat, Name = "Marlissa"}
                    }
                };

                var defaultLogger = new DefaultLogger(null, repository);
                // ReSharper disable once MethodHasAsyncOverload
                using var tracker = defaultLogger.Track("SUPER", person, new { });
                // defaultLogger.Log("shit", person);

                person.Age = 200;
                person.Pets.Add(new Pet {Name = "Shit", PetType = PetType.Dog});
            });

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        private class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public List<Pet> Pets { get; set; }
        }

        private class Pet
        {
            public string Name { get; set; }

            public PetType PetType { get; set; }
        }

        private enum PetType
        {
            Cat,
            Dog
        }
    }
}