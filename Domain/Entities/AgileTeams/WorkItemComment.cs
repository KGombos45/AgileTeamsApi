using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AgileTeams
{
    public class WorkItemComment
    {
        public string CommentID { get; set; }
        public string Comment { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string CommentWorkItemID { get; set; }
        public WorkItem WorkItem { get; set; }
    }
}
