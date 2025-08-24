using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Models.Repositories
{
    public interface IAdminRepository:IGenericRepository<ApplicationUser>
    {
        public void Update(ApplicationUser user);
    }
}
