using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;
using Moq;
using Xunit;

namespace Tests.Data.Autonomus
{
    public class PersonRepository
    {
        private readonly Mock<IRepository<Person>> mockPersonRepository;
        private readonly List<Person> personsDatabase;

        public PersonRepository()
        {
            mockPersonRepository = new Mock<IRepository<Person>>();

            personsDatabase = new List<Person>
            {
                new Person{ Id = 0, Name = "ивнов ивн ивнович", ContactPhone = "89588149095", HasWatsapp = true},
                new Person{ Id = 1, Name = "ивнов ивн ивнович", ContactPhone = "89588149095", HasWatsapp = true},
                new Person{ Id = 2, Name = "ивнов ивн ивнович", ContactPhone = "89588149095", HasWatsapp = false},
                new Person{ Id = 3, Name = "ивнов ивн ивнович", ContactPhone = "89588149095", HasWatsapp = true},
                new Person{ Id = 4, Name = "ивнов ивн ивнович", ContactPhone = "89588149095", HasWatsapp = true},
            };
        }


        [Fact]
        public void ПолуениеВсехЭлементов()
        {
            mockPersonRepository.Setup(a => a.AllItems).Returns(personsDatabase);
            var repository = mockPersonRepository.Object;

            Assert.True(repository.AllItems.Count > 0);
        }

        [Fact]
        public void ВыборкаЭлементовПоЗапросу()
        {
            Expression<Func<Person, bool>> expression = n => n.Id == 1;
            List<Person> personsItem= null;
            mockPersonRepository
                .Setup(a => a.GetItems(It.IsAny<Expression<Func<Person, bool>>>()))
                .Callback((Expression<Func<Person, bool>> n) =>
                    {
                        personsItem = personsDatabase.Where(n.Compile()).ToList();
                    })
                .Returns(()=>
                {
                    return personsItem;
                });
            var repository = mockPersonRepository.Object;

            Assert.True(repository.GetItems(x=>x.Id == 3).Count == 1);
        }

        [Fact]
        public void ИзменениеЭлемента()
        {
            var item = new Person
            {
                Id = 2,
                Name = "Петров Петр Петрович",
                ContactPhone = "89588149095",
                HasWatsapp = false
            };
            mockPersonRepository
                .Setup(a => a.Update(It.IsAny<Person>()))
                .Callback((Person n) => { personsDatabase[2] = n; });
            var repository = mockPersonRepository.Object;
            repository.Update(item);
            Assert.True(personsDatabase[2].Name == "Петров Петр Петрович");
        }
    }
}
