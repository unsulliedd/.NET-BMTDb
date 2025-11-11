using BMTDb.Entity;

namespace BMTDb.Data.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> GetPersons(int page, int pageSize);
        int GetPersonCount();
    }
}
