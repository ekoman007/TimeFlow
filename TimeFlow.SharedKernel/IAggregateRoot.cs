using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.SharedKernel
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        //
    }
}
