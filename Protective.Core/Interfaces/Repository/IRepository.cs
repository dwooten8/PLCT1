using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protective.Core.Interfaces.Repository
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        T Create(T item);
        bool Update(T item);
        bool Delete(T item);
    }
}
