using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Common
{
    public enum Roles
    {
        Admin = 99
    }
    public enum MailSubject
    {
        CustomerRegistration = 2,
        MaintenanceNotification = 4,
        QuickEmail = 5
    }

    public enum UserType
    {
        Internal = 1,
        External=2
    }

    public enum EnumLocationType
    {
        Type1=1,
        Type2=2
    }
}
