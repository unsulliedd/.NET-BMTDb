using BMTDb.Data.Abstract;
using BMTDb.Entity;
using BMTDb.Service.Abstract;

namespace BMTDb.Service.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IUnitofWork _unitofWork;

        public GenreManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public void Create(Genre entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Genre entity)
        {
            throw new NotImplementedException();
        }
        public void Update(Genre entity)
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetAll()
        {
            return _unitofWork.Genres.GetAll();
        }

        public Genre GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
