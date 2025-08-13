using System;
using UserManagement.Models;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    //public List<UserListItemViewModel> Items { get; set; } = new();
    public IEnumerable<User> Items { get; set; } = new List<User>();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public List<UserListItemViewModel>? Items { get; internal set; }
}
