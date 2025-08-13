using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Controllers; // make sure this is your actual namespace
using UserManagement.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class UsersControllerTests
{
    [Fact]
    public async Task List_NoFilter_ReturnsAllUsers()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new[]
        {
            new User { Id = 1, IsActive = true },
            new User { Id = 2, IsActive = false }
        });

        var controller = new UsersController(mockService.Object);

        // Act
        var result = await controller.List(null) as ViewResult;
        var model = result?.Model as UserListViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Equal(2, model.Items.Count);
    }

    [Fact]
    public async Task List_ActiveOnly_ReturnsOnlyActiveUsers()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new[]
        {
            new User { Id = 1, IsActive = true },
            new User { Id = 2, IsActive = false }
        });

        var controller = new UsersController(mockService.Object);

        // Act
        var result = await controller.List(true) as ViewResult;
        var model = result?.Model as UserListViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Single(model.Items);
        Assert.All(model.Items, u => Assert.True(u.IsActive));
    }

    [Fact]
    public async Task List_NonActiveOnly_ReturnsOnlyInactiveUsers()
    {
        // Arrange
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new[]
        {
            new User { Id = 1, IsActive = true },
            new User { Id = 2, IsActive = false }
        });

        var controller = new UsersController(mockService.Object);

        // Act
        var result = await controller.List(false) as ViewResult;
        var model = result?.Model as UserListViewModel;

        // Assert
        Assert.NotNull(model);
        Assert.Single(model.Items);
        Assert.All(model.Items, u => Assert.False(u.IsActive));
    }
}
