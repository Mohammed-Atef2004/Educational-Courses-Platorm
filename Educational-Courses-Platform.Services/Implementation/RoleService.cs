using Microsoft.AspNetCore.Identity;
using Educational_Courses_Platform.Entities.Models;
using Educational_Courses_Platform.Services.Interfaces;

namespace Educational_Courses_Platform.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task EnsureRolesSeededAsync()
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!result.Succeeded)
                        throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                if (!await _roleManager.RoleExistsAsync("Student"))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole("Student"));
                    if (!result.Succeeded)
                        throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while seeding roles", ex);
            }
        }



        public async Task AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new ArgumentException("Role does not exist");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

    }
}
