using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gamu.Classmaters.Config;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;
using Google.Apis.Sheets.v4;

namespace Gamu.Classmaters.Data.Implementation
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly SheetsService sheetsService;
        private readonly ClassmatersConfig configuration;
        private readonly IMapper<IList<object>, Person> personMapper;

        public PersonRepository(IMapper<IList<object>, Person> personMapper)
        {
            sheetsService = GoogleSheetsConnector.GetService();
            configuration = CofigurationContext.Current();
            this.personMapper = personMapper;
        }

        private List<Person> GetPeople()
        {
            var result = new List<Person>();
            var request =
                    sheetsService.Spreadsheets.Values.Get(configuration.GoogleSpreadsheetName, "A2:D54");
            var response = request.Execute();
            var values = response.Values;
            
            foreach (var row in values)
            {
                var person = personMapper.Map(row);
                result.Add(person);
            }

            return result;
        }

        

        public IList<Person> AllItems => GetPeople();

        public void Add(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public IList<Person> GetItems(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
