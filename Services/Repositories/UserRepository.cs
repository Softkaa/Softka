using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Models;

namespace Softka.Services
{
   
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _context; 
        public UserRepository(BaseContext context)
        {
            _context = context;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var Search = await _context.Users.Where(u => u.Id == id)
                            .Select(obj => new User
                            {
                                Names = obj.Names,
                                LastNames = obj.LastNames,
                                TypeDocument = obj.TypeDocument,
                                Document = obj.Document,
                                Email = obj.Email,
                                Age = obj.Age                                
                            }).SingleOrDefaultAsync();
                        
            return Search;
        
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}