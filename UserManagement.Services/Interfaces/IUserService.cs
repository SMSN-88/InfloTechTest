using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>    
    
    // Sync
    IEnumerable<User> GetAll();
    IEnumerable<User> FilterByActive(bool isActive);
    User? GetById(long id);
    void Add(User user);
    void Update(User user);
    void Delete(long id);

    // Async
    Task<List<User>> GetAllAsync();
    Task<List<User>> FilterByActiveAsync(bool isActive);
    Task<User?> GetByIdAsync(long id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(long id);
}
