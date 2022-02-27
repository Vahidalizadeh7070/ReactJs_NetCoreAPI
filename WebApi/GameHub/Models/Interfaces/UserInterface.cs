using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // User interface
    public interface UserInterface
    {
        User Create(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
