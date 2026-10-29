using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuditId { get; set; }
    }
}
