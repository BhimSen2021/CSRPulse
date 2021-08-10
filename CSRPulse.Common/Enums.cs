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
        Auditor = 2,
        BusinessUnit = 3
    }

    public enum ProcessLevel
    {
        Initiator = 1,
        Forwarder = 2,
        FinalApproval = 3
    }

    public enum Critical
    {
        Select = 1,
        No = 2,
        Yes = 3
    }

    public enum MaximumMarks
    {
        Two = 2,
        One = 1,
        Zero = 0,
        NA = -2
    }

    public enum Constitution
    {
        Section_8_Compney = 1,
        Society = 2,
        Trust = 3
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum NGOMemberType
    {
        KeyPerson = 1,
        NoKeyPerson = 2
    }

    public enum YesNo
    {
        Yes = 1,
        No = 0
    }

    public enum CorpusGrantFund
    {
        CorpusFund = 1,
        GrantInflow = 2
    }

    public enum AgencyType
    {
        IndianFundingAgencies = 1,
        ForeignFundingAgencies = 2
    }
}
