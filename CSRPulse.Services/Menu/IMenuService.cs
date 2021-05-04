﻿using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IMenuService
    {
        List<Menu> GetMenuByUserAsync(int UserID);
    }
}
