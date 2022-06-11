using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Data.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> GetPersons(int page, int pageSize);
        int GetPersonCount();
    }
}
