using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;
using Gamu.Classmaters.Query.Dtos;
using Gamu.Classmaters.Query.Queries;
using MediatR;

namespace Gamu.Classmaters.Query.Handlers
{
    public class GetAllPersonsQueryHandler: IRequestHandler<GetAllPersonsQuery, IList<Person>>
    {
        private readonly IRepository<Person> personRepository;

        public GetAllPersonsQueryHandler(IRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<IList<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return this.personRepository.AllItems;
        }
    }
}
