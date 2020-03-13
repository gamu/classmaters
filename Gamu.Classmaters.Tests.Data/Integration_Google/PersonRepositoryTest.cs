using System;
using System.IO;
using System.Threading;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;
using Gamu.Classmaters.Data.Implementation;
using Moq;
using Xunit;
using Gamu.Classmaters.Data.Implementation.Mappers;

namespace Gamu.Classmaters.Tests.Data.Integration_Google
{
    public class PersonRepositoryTest
    {
        private readonly Mock<IRepository<Person>> mockPersonRepository;
        private readonly IRepository<Person> personRepository;

        public PersonRepositoryTest()
        {
            personRepository = new PersonRepository(new PersonMapper());
        }

        [Fact]
        public void ПолуениеВсехЭлементов()
        {
            var persons = personRepository.AllItems;

            Assert.True(persons.Count > 0);
        }
    }
}
