using BMTDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTDb.Service.Abstract
{
    public interface IGenreService
    {
        Genre GetById(int id);
        List<Genre> GetAll();
        void Create(Genre entity);
        void Update(Genre entity);
        void Delete(Genre entity);
    }
}
