using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Models;
using Softka.Utils.PasswordHashing;

namespace Softka.Services
{
   
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _context; 
        private readonly Bcrypt _bcrypt;
        public UserRepository(BaseContext context, Bcrypt bcrypt)
        {
            _context = context;
            _bcrypt = bcrypt;
        }

        public void Add(User user, string password)
        {

            //We hash password
            string HashedPassword = _bcrypt.HashPassword(password);

            if(_bcrypt.VerifyPassword(password, HashedPassword))
            {
                //we indicate what the hashed password will be
                user.Password = HashedPassword;


                _context.Users.Add(user);
                _context.SaveChanges();
                Utils.Exceptions.ErrorExceptions.CreateOk(); 
            }
            else
            {
                Utils.Exceptions.ErrorExceptions.CreateBadRequest();
            }

            
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