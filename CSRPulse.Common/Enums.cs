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
        External = 2
    }

    public enum EnumLocationType
    {
        Type1 = 1,
        Type2 = 2
    }

    public enum IndResponseType
    {
        Numeric = 1,
        Text = 2,
        YesNo = 3,
        Currency = 4,
    }

    public enum IndicatorType
    {
        Activity = 1,
        Efforts = 2,
        Impact = 3,
        Input = 4,
        Outcome = 5

    }

}
