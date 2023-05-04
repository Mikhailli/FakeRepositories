using System.Collections.Generic;

namespace FakeRepositories.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);

    void Save();
}