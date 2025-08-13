
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;  
namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List1(bool? isActive)
    {

        var users = _userService.GetAll();
        if (isActive.HasValue)
        {
            users = users.Where(u => u.IsActive == isActive.Value);
        }

        var model = new UserListItemViewModel
        {

            Items = users.Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            }).ToList()
        };

        return View(model);
    }

    //------------CREATE------------------
    [HttpGet("create")]
    public IActionResult Create() => View();
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        if (!ModelState.IsValid)        
            return View(user);

            _userService.Add(user);
            return RedirectToAction(nameof(List1));
        
    }

        //------------VIEW------------------
        [HttpGet("view/{id}")]
        public IActionResult actionResult(long id)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);

        }

        //------------EDIT------------------
        [HttpGet("edit/{id}")]
        public IActionResult Edit(long id)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userService.Update(user);
            return RedirectToAction(nameof(List1));
        }

        //------------DELETE------------------
        [HttpGet("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            _userService.Delete(id);
            return RedirectToAction(nameof(List1));
        }

    public async Task<IActionResult> List(bool? isActive)
    {
        var allUsers = await _userService.GetAllAsync();

        if (isActive.HasValue)
        {
            allUsers = allUsers
                .Where(u => u.IsActive == isActive.Value)
                .ToList();
        }

        var model = new UserListItemViewModel
        {
            Items = allUsers.Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            }).ToList()
        };

        return View(model);
    }

    
}
