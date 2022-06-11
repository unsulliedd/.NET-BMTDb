#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable IDE0063 // Use simple 'using' statement

using BMTDb.Data.Abstract;
using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCorePersonRepository : EFCoreGenericRepository<Person, BMTDbContext>, IPersonRepository
    {
        public int GetPersonCount()
        {
            using (var context = new BMTDbContext())
            {
                var persons = context.Persons.AsQueryable();

                return persons.Count();
            }
        }

        public List<Person> GetPersons(int page, int pageSize)
        {
            using var context = new BMTDbContext();
            {
                var persons = context.Persons.AsQueryable();

                return persons.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}
