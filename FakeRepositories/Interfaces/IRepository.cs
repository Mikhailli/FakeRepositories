using System.Collections.Generic;

namespace FakeRepositories.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();

    void Insert(T anime);
    void Update(T studio);
    void Delete(int idToDelete);

    void Save();
}