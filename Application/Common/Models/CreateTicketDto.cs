using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using Domain.Enums;

namespace Application.Common.Models
{
    public class CreateTicketDto
    {
        public string TicketName { get; set; }
        public string TicketDescription { get; set; }
        public TicketStatuses TicketStatusID { get; set; }
        public TicketTypes TicketTypeID { get; set; }
        public string TicketOwnerID { get; set; }
        public string TicketWorkItemID { get; set; }
    }
}
