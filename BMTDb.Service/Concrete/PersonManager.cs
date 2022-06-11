using BMTDb.Data.Abstract;
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
        private readonly IPersonRepository _personRepository;
        public PersonManager (IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void Create(Person entity)
        {
            _personRepository.Create(entity);
        }

        public void Delete(Person entity)
        {
            _personRepository.Delete(entity);
        }
        public void Update(Person entity)
        {
            _personRepository.Update(entity);
        }

        public List<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public List<Person> GetPersons(int page, int pageSize)
        {
            return _personRepository.GetPersons(page, pageSize);
        }

        public int GetPersonCount()
        {
            return _personRepository.GetPersonCount();
        }
    }
}
