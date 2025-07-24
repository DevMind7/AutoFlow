using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Modules.Models
{
    public record LeaveRequest(string UserId, DateTime StartDate, DateTime EndDate, string Reason);

}
