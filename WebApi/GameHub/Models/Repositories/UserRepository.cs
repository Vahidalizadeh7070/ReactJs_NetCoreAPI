using GameHub.Models.Interfaces;
using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // User Repo
    // This class is inherited from UserInterface
    public class UserRepository : UserInterface
    {
        private readonly AppDbContext appDbContext;

        // Constructor
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Create a user
        public User Create(User user)
        {
            appDbContext.User.Add(user);
            user.Id = appDbContext.SaveChanges();
            return user;
        }

        // Get user by email
        public User GetUserByEmail(string email)
        {
            return appDbContext.User.FirstOrDefault(x => x.Email == email);
        }

        // Get user by id
        public User GetUserById(int id)
        {
            return appDbContext.User.FirstOrDefault(x => x.Id == id);
        }
    }
}
