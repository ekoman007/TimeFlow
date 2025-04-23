using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class BusinessModule : AggregateRoot<int>
    {
        public int BusinessProfileId { get; private set; }
        public string ModuleKey { get; private set; } // shembull: "Appointments", "Payroll"
        public DateTime PurchaseDate { get; private set; }

        public static BusinessModule Create(int businessProfileId, string moduleKey)
        {
            return new BusinessModule
            {
                BusinessProfileId = businessProfileId,
                ModuleKey = moduleKey,
                PurchaseDate = DateTime.UtcNow
            };
        }
    }
}
