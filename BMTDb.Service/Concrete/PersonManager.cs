#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

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
        private readonly IUnitofWork _unitofWork;

        public PersonManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public string ErrorMessage { get; set; }
        public bool Validation(Person entity)
        {
            var isValid = true;

            return isValid;
        }

        public bool Create(Person entity)
        {
            if (Validation(entity))
            {
                var persons = _unitofWork.Persons.GetAll();
                if ((persons.Any(i => i.Name == entity.Name)) && (persons.Any(i => i.Birthday == entity.Birthday)))
                {
                    ErrorMessage = "Person is Already Exist";
                    return false;
                }
                _unitofWork.Persons.Create(entity);
                _unitofWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(Person entity)
        {
            _unitofWork.Persons.Delete(entity);
            _unitofWork.Save();
        }
        public void Update(Person entity)
        {
            _unitofWork.Persons.Create(entity);
            _unitofWork.Save();
        }

        public List<Person> GetAll()
        {
            return _unitofWork.Persons.GetAll();
        }

        public Person GetById(int id)
        {
            return _unitofWork.Persons.GetById(id);
        }

        public List<Person> GetPersons(int page, int pageSize)
        {
            return _unitofWork.Persons.GetPersons(page, pageSize);
        }

        public int GetPersonCount()
        {
            return _unitofWork.Persons.GetPersonCount();
        }
    }
}
