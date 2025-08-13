using System;

internal class UsersController
{
    private IUserService _object;

    public UsersController(IUserService @object) => _object = @object;

    internal async System.Threading.Tasks.Task<ViewResult> List(object value) => throw new NotImplementedException();
}