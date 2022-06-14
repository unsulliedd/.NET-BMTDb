#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
#pragma warning disable IDE0063 // Use simple 'using' statement

using BMTDb.Data.Abstract;
using BMTDb.Data.Concrete.EFCore;
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

        public string ErrorMessage { get; set; }
        public bool Validation(Person entity)
        {
            var isValid = true;

            return isValid;
        }

        public PersonManager (IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public bool Create(Person entity)
        {
            if (Validation(entity))
            {
                using (var context = new BMTDbContext())
                {
                    var persons = context.Persons;
                    if ((persons.Any(i => i.Name == entity.Name)) && (persons.Any(i => i.Birthday == entity.Birthday)))
                    {
                        ErrorMessage = "Person is Already Exist";
                        return false;
                    }
                    _personRepository.Create(entity);
                    return true;
                }
            }
            return false;
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
