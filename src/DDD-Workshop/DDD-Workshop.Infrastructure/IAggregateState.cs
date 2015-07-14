using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD_Workshop.Infrastructure
{
    /// <summary>
    /// POCO object that stores the data and context for domain operations.
    /// Only the Aggregate root should make changes to this class
    /// </summary>
    public interface IAggregateState
    {
    }
}
