﻿using ArsamBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArsamBackend.Services
{
    public interface IJWTHandler
    {
        public string GenerateToken(AppUser user);
    }
}