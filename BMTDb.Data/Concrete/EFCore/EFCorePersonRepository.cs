#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using BMTDb.Data.Abstract;
using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Concrete.EFCore
{
    public class EFCorePersonRepository : EFCoreGenericRepository<Person>, IPersonRepository
    {
        public EFCorePersonRepository(BMTDbContext context) : base(context)
        {

        }
        private BMTDbContext BMTDbContext
        {
            get { return context as BMTDbContext; }
        }

        public int GetPersonCount()
        {
                var persons = BMTDbContext.Persons.AsQueryable();
                return persons.Count();
        }

        public List<Person> GetPersons(int page, int pageSize)
        {
            var persons = BMTDbContext.Persons.AsQueryable();
            return persons.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
