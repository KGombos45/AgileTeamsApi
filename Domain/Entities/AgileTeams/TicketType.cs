using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AgileTeams
{
    public class TicketType
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }
}
