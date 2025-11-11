using BMTDb.Entity;

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
