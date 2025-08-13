
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;

using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    // Sync versions
    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public IEnumerable<User> FilterByActive(bool isActive) =>
        _dataAccess.GetAll<User>().Where(u => u.IsActive == isActive);

    public User? GetById(long id) =>
        _dataAccess.GetAll<User>().FirstOrDefault(u => u.Id == id);

    public void Add(User user) => _dataAccess.Add(user);

    public void Update(User user) => _dataAccess.Update(user);

    public void Delete(long id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _dataAccess.Delete(user);
        }
    }

    // Async versions
    public Task<List<User>> GetAllAsync() =>
        Task.FromResult(GetAll().ToList());

    public Task<List<User>> FilterByActiveAsync(bool isActive) =>
        Task.FromResult(FilterByActive(isActive).ToList());

    public Task<User?> GetByIdAsync(long id) =>
        Task.FromResult(GetById(id));

    public Task AddAsync(User user)
    {
        Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        Update(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(long id)
    {
        Delete(id);
        return Task.CompletedTask;
    }
}
