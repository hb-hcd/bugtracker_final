namespace SD_125_BugTracker.DAL; 

public interface IUserRepository<T> where T : class {
    Task<ICollection<T>> GetListAsync(Func<T, bool>? whereFunction);
    Task<T> GetAsync(Func<T, bool>? firstFunction);
    Task CreateAsync(T? entity);
    Task UpdateAsync(T? entity);
    Task DeleteAsync(int? id);
    Task AssignRole(string? role, T? user);
    Task UnAssignRole(string? role, T? user);
    Task SaveAsync();
}
