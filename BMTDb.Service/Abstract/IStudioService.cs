using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IStudioService
    {
        Studio GetById(int id);
        List<Studio> GetAll();
        void Create(Studio entity);
        void Update(Studio entity);
        void Delete(Studio entity);
    }
}
