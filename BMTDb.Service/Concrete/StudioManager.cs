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
    public class StudioManager : IStudioService
    {
        private readonly IStudioRepository _studioRepository;
        public StudioManager(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        public void Create(Studio entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Studio entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Studio entity)
        {
            throw new NotImplementedException();
        }

        public List<Studio> GetAll()
        {
            return _studioRepository.GetAll();
        }

        public Studio GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
