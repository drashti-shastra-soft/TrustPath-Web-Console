using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Domain
{
    public class LeadCall : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int LeadId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? UpComeFollowUp { get; set; }
        public string CommunicationMode { get; set; }
        public string CallStatus { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }

    }

}
