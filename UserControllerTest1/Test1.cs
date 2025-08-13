
using Moq;


namespace UserControllerTest1;

[TestClass]
public class Test1
{
    [TestMethod]
    public async Task List_NoFilter_ReturnsAllUsers()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<User>
            {
                new User { Id = 1, IsActive = true },
                new User { Id = 2, IsActive = false }
            });

        var controller = new UserControllerTest1.UserController(mockUserService.Object);

        //Act
        var result = await controller.List(null) as ViewResult;
        var model = result?.Model as UserListViewModel;

        //Assert
        Assert.IsNotNull(model);
        Assert.AreEqual(1, model.Id);

    }

    [TestMethod]
    public async Task List_ActiveOnly_ReturnsOnlyActiveUsers()
    {
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<User>
            {
                new User { Id = 1, IsActive = true },
                new User { Id = 2, IsActive = false }
            });

        var controller = new UserController(mockService.Object);

        var result = await controller.List(true) as ViewResult;
        var model = result?.Model as UserListViewModel;

        Assert.IsNotNull(model);
        Assert.IsTrue(model.Id.All(u => u.IsActive));
    }

    [TestMethod]
    public async Task List_NonActiveOnly_ReturnsOnlyInactiveUsers()
    {
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<User>
            {
                new User { Id = 1, IsActive = true },
                new User { Id = 2, IsActive = false }
            });

        var controller = new UserController(mockService.Object);

        var result = await controller.List(false) as ViewResult;
        var model = result?.Model as UserListViewModel;

        Assert.IsNotNull(model);
        Assert.IsTrue(model.Id.All(u => !u.IsActive));
    }
}
