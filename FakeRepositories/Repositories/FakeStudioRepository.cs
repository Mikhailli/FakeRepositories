using System.Collections.Generic;
using FakeRepositories.Models;

namespace FakeRepositories.Repositories;

public class FakeStudioRepository : FakeGenericRepository<Studio>
{
    public FakeStudioRepository(ICollection<Studio> collection) : base(collection)
    {
        
    }
}