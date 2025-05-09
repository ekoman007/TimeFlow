using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.SharedKernel
{
    // Non-generic version for backward compatibility
    public interface IAggregateRoot
    {
        // Marker interface for aggregate roots
    }

    // Generic version
    public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
    {
        //
    }
}
