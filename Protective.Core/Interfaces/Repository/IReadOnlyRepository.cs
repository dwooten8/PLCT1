using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protective.Core.Interfaces.Repository
{
    public interface IReadOnlyRepository<T>
    {
        T GetEntity(T item);
        List<T> GetList();
    }
}
