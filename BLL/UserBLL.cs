using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL;

public class UserBusinessLogic {
    private readonly IUserRepository<ApplicationUser> _userRepository;

    public UserBusinessLogic(IUserRepository<ApplicationUser> userRepository) {
        _userRepository = userRepository;
    }

    public Task? AssignRole(string? role, ApplicationUser? user) {
        return _userRepository.AssignRole(role, user);
    }

    public async Task<ICollection<ApplicationUser>> GetUsers() {
        return await _userRepository.GetListAsync(_ => true);
    }

    public async Task<ApplicationUser?> GetUserById(string? userId) {
        return await _userRepository.GetAsync(u => u.Id == userId);
    }
    
    public async Task<ApplicationUser?> GetUserByName(string? name) {
        return await _userRepository.GetAsync(u => u.UserName == name);
    }
    
    public Task SaveAsync() {
        return _userRepository.SaveAsync();
    }
}