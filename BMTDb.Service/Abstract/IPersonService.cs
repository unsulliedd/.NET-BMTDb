using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IPersonService : IValidator<Person>
    {
        Person GetById(int id);
        List<Person> GetAll();
        bool Create(Person entity);
        void Update(Person entity);
        void Delete(Person entity);
        List<Person> GetPersons(int page, int pageSize);
        int GetPersonCount();
    }
}
