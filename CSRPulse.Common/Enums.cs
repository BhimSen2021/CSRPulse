﻿using System;
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
        Village = 1,
        Town = 2
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
    public enum Plans
    {
        Basic = 1,
        Standard = 2,
        Professional = 3
    }

    public enum PartnerType
    {
        NGO = 1,
        Auditor = 2
    }

    public enum ProcessLevel
    {
        Initiator=1,
        Forwarder=2,
        FinalApproval=3
    }
}
