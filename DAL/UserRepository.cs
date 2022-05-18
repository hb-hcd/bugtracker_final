using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace SD_125_BugTracker.DAL;

public class UserRepository : IUserRepository<ApplicationUser> {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
        _context = context;
        _userManager = userManager;
    }

    public Task<ICollection<ApplicationUser>> GetListAsync(Func<ApplicationUser, bool>? whereFunction) {
        if (whereFunction is null) {
            throw new ArgumentNullException();
        }
        
        ICollection<ApplicationUser> users = new List<ApplicationUser>();
        if (_userManager.Users != null) {
            users = _userManager.Users.AsEnumerable().Where(whereFunction).ToList();
        }

        return Task.FromResult(users);
    }

    public Task<ApplicationUser> GetAsync(Func<ApplicationUser, bool>? firstFunction) {
        if (firstFunction is null) {
            throw new ArgumentNullException();
        }
        
        ApplicationUser? users = new();
        if (_userManager.Users != null) {
            users = _userManager.Users.First(firstFunction);
        }

        return Task.FromResult(users);
    }

    public Task CreateAsync(ApplicationUser? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _userManager.CreateAsync(entity);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ApplicationUser? entity) {
        if (entity is null) {
            throw new ArgumentNullException();
        }
        
        _userManager.UpdateAsync(entity);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int? id) {
        if (id is null) {
            throw new ArgumentNullException();
        }

        _userManager.DeleteAsync(_userManager.Users.First(i => Equals(i, id)));

        return Task.CompletedTask;
    }

    public Task AssignRole(string? role, ApplicationUser? user) {
        if (role is null && user is null) {
            
        }
        
        Task task = _userManager.AddToRoleAsync(user, role);

        return task;
    }
    
    public Task UnAssignRole(string? role, ApplicationUser? user) {
        if (role is null && user is null) {
            throw new ArgumentNullException();
        }
        Task? task = _userManager.RemoveFromRoleAsync(user, role);

        return task;
    }

    public Task SaveAsync() {
        return _context.SaveChangesAsync();
    }
}
