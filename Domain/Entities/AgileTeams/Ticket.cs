using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.AgileTeams
{
    public class Ticket
    {
        public string TicketID { get; set; }
        public string TicketName { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public TicketStatuses TicketStatusID { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketTypes TicketTypeID { get; set; }
        public TicketType TicketType { get; set; }
        public string TicketOwnerID { get; set; }
        public ApplicationUser TicketOwner { get; set; }
        public string TicketWorkItemID { get; set; }
        public WorkItem TicketWorkItem { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
