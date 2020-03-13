using System;
using Gamu.Classmaters.Data.Interfaces;

namespace Gamu.Classmaters.Data.Models
{
    public class Person: IRepositoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  ContactPhone { get; set; }
        public bool HasWatsapp { get; set; }
    }
}
