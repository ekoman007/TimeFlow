using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class WorkSchedule : AggregateRoot<int>
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public DayOfWeek Day { get; set; }

        public int WorkShiftId { get; set; }
        public WorkShift WorkShift { get; set; }
    } 

}
