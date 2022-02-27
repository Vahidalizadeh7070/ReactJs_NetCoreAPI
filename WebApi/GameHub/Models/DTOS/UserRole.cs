using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.DTOS
{
    // This is a static class that store user and admin role
    // We can use enum instead of this class
    public static class UserRole
    {
        public const string User = "User";
        public const string Admin= "Admin";
    }
}
