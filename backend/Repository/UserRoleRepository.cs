using CourseMate.Models;
using CourseMate.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseMate.Repository
{
    public interface IUserRoleRepository
    {
        Task<UserRole?> GetRoleByEmailDomain(string email);
        Task<IEnumerable<UserRole>> GetAllActiveRoles();
    }

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserRole?> GetRoleByEmailDomain(string email)
        {
            return await _context.UserRoles
                .Where(r => r.IsActive && email.EndsWith(r.EmailDomain))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllActiveRoles()
        {
            return await _context.UserRoles
                .Where(r => r.IsActive)
                .ToListAsync();
        }
    }
}