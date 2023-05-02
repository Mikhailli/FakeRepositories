using System.Collections.Generic;

namespace FakeRepositories.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    void Insert(T entidade, bool autoPersist = true);
    void Update(T entidade, bool autoPersist = true);
    void Delete(T entidade, bool autoPersist = true);

    void Save();
}