using System;
using System.Collections.Generic;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;

namespace Gamu.Classmaters.Data.Implementation.Mappers
{
    public class PersonMapper : IMapper<IList<object>, Person>
    {
        public Person Map(IList<object> input)
        {
            var person = new Person();
            person.Id = Convert.ToInt32(input[0]);
            person.Name = input[1] != null ? input[1].ToString() : string.Empty;
            if (input.Count > 2)
                person.HasWatsapp = input[2] != null && input[2].ToString() == "Да" ? true : false;
            if (input.Count > 3)
                person.ContactPhone = input[3] != null ? input[3].ToString() : string.Empty;
            return person;
        }
    }
}
