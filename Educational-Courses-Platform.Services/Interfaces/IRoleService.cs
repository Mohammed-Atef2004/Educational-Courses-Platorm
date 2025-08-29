using System.Threading.Tasks;

namespace Educational_Courses_Platform.Services.Interfaces
{
    public interface IRoleService
    {
        Task EnsureRolesSeededAsync();
        Task AssignRoleToUserAsync(string userId, string roleName);
    }
}
