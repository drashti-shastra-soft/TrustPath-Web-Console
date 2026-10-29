    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustPathWebConsole.Infrastructure
{
    public static class Enums
    {
        public enum Operation
        {
            Save = 1,
            Delete = 2,
        }

        public enum MenuItem
        {
            Company = 1,
            User = 2,
            Lead = 3,
            Call = 4,
            ChangePassword = 5,
            Source = 6,
            BusinessType = 7,
            State = 8,
        }

        public enum Result
        {
            Success = 1,
            Error = 2
        }

        public enum Role
        {
            System_Owner = 1,
            Super_Admin = 2,
            Admin = 3,
            Employee = 4
        }
    }
}
