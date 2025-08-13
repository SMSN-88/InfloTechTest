
namespace UserControllerTest1;

internal class UserController
{
    private IUserService _object;

    public UserController(IUserService @object) => _object = @object;


}
