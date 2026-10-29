using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Domain
{
    public class Lead : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int StateId { get; set; }
        public int BusinessTypeId { get; set; }
        public string Requirement { get; set; }
        public int SourceId { get; set; }
        public string AssignLead { get; set; }
        public string LeadStatus { get; set; }
        public int AuditId { get; set; }
        public string Company { get; set; }
        public string BusinessType { get; set; }
        public string Source { get; set; }
        public DateTime? UpComeFollowUp { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }


}
