using System;
using Gamu.Classmaters.Data.Implementation;
using Gamu.Classmaters.Data.Implementation.Mappers;
using Gamu.Classmaters.Query.Handlers;
using Gamu.Classmaters.Query.Queries;
using MediatR;
using Xunit;

namespace Gamu.Classmaters.Test.Cqrs.Queries
{
    public class Person: BaseTestClass
    {
        private readonly IMediator mediator;

        public Person()
        {
            mediator = BuildMediator();
        }

        [Fact]
        public void ПолучениеВсехPerson()
        {
            //var personMapper = new PersonMapper();
            //var personRepository = new PersonRepository(personMapper);
            var getAllQuery = new GetAllPersonsQuery();
            var result = mediator.Send(getAllQuery).Result;
        }
    }
}
