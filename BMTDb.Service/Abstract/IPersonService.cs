using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IPersonService
    {
        Person GetById(int id);
        List<Person> GetAll();
        void Create(Person entity);
        void Update(Person entity);
        void Delete(Person entity);
    }
}
