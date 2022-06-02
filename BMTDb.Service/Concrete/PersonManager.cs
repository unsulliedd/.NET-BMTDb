using BMTDb.Entity;
using BMTDb.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Concrete
{
    public class PersonManager : IPersonService
    {
        private readonly IPersonService _personService;
        public PersonManager (IPersonService personService)
        {
            _personService = personService;
        }
        public void Create(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }
        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAll()
        {
            return _personService.GetAll();
        }

        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
