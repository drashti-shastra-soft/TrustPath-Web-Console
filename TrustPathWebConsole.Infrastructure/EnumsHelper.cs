using System;

namespace TrustPathWebConsole.Infrastructure
{
    public class EnumsHelper
    {
        public static string GetEnumString(Enums.Operation operation)
        {

            switch (operation)
            {
                case Enums.Operation.Save:
                    return nameof(Enums.Operation.Save);
                case Enums.Operation.Delete:
                    return nameof(Enums.Operation.Delete);
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
        public static string GetEnumString(Enums.MenuItem menuItem)
        {

            switch (menuItem)
            {
                case Enums.MenuItem.Company:
                    return nameof(Enums.MenuItem.Company);
                case Enums.MenuItem.User:
                    return nameof(Enums.MenuItem.User);
                case Enums.MenuItem.Lead:
                    return nameof(Enums.MenuItem.Lead);
                case Enums.MenuItem.Call:
                    return nameof(Enums.MenuItem.Call);
                case Enums.MenuItem.ChangePassword:
                    return nameof(Enums.MenuItem.ChangePassword);
                case Enums.MenuItem.Source:
                    return nameof(Enums.MenuItem.Source);
                case Enums.MenuItem.BusinessType:
                    return nameof(Enums.MenuItem.BusinessType);
                case Enums.MenuItem.State:
                    return nameof(Enums.MenuItem.State);
                default:
                    throw new ArgumentOutOfRangeException(nameof(menuItem), menuItem, null);
            }
        }

        public static string GetEnumString(Enums.Result result)
        {
            switch (result)
            {
                case Enums.Result.Success:
                    return nameof(Enums.Result.Success);
                case Enums.Result.Error:
                    return nameof(Enums.Result.Error);
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public static int GetEnumIndex(Enums.Role role)
        {
            switch (role)
            {
                case Enums.Role.System_Owner:
                    return (int)Enums.Role.System_Owner;
                case Enums.Role.Super_Admin:
                    return (int)Enums.Role.Super_Admin;
                case Enums.Role.Admin:
                    return (int)Enums.Role.Admin;
                case Enums.Role.Employee:
                    return (int)Enums.Role.Employee;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }
    }
}
