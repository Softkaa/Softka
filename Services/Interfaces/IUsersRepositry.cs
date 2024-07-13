//we generated the interface of Users
using Softka.Models;
using Microsoft.AspNetCore.Mvc;
namespace Softka.Services
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAll (); // We listed All users
        public Task<User> GetById (int id); // We listed By Id of User
        //public void Create (User user); // Method for create User Basically the register.
        public void Delete (User user); // Delete User
        public void Update (User user); //Update User

        
    }
}
