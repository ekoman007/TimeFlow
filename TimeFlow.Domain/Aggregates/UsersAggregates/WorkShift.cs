using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class WorkShift : AggregateRoot<int>
    {
        public string Name { get; set; } // "Turni i paradites", "Turni i mbremjes"
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsDayOff { get; set; }

        public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    }

}
