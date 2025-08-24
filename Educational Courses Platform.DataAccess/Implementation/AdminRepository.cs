using Educational_Courses_Platform.DataAccess.Data;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.DataAccess.Implementation
{
    public class AdminRepository : GenericRepository<ApplicationUser>, IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser user)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userInDb != null)
            {
                userInDb.UserName = user.UserName;
                userInDb.Email = user.Email;

            }
        }
    }
}
