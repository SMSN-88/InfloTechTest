using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userService = new();

    private UsersController CreateController() => new(_userService.Object);

    private User[] SetupUsers(
        string forename = "Johnny",
        string surname = "User",
        string email = "juser@example.com",
        bool isActive = true,
        string dob = "1990-05-15"
    )
    {
        var users = new[]
        {
            new User
            {
                Id = 1,
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive,
                DateOfBirth = DateTime.Parse(dob)
            }
        };

        _userService
            .Setup(s => s.GetAll())
            .Returns(users);

        _userService
            .Setup(s => s.FilterByActive(isActive))
            .Returns(users);

        // Add this to mock the async call
        _userService
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(users.ToList());

        return users;
    }

    // --- Filters Section ---

    [Fact]
    public async Task List_WhenServiceReturnsUsers_ModelMustContainUsersAsync()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = await controller.List(true);
        var viewResult = result as ViewResult;
        viewResult.Should().NotBeNull();

        // Assert: Verifies that the action of the method under test behaves as expected.
        viewResult.Model
            .Should().BeOfType<UserListItemViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task List_FilterInactiveUsers_ModelShouldContainOnlyInactiveUsers()
    {
        var controller = CreateController();
        var users = SetupUsers(isActive: false);

        var result = await controller.List(false);
        var viewResult = result as ViewResult;
        viewResult.Should().NotBeNull();

        viewResult.Model.Should().BeOfType<UserListItemViewModel>()
                        .Which.Items.Should().BeEquivalentTo(users);
    }

    // --- User Model Properties ---
    [Fact]
    public void User_ShouldStoreAndReturnCorrectDOB()
    {
        var dob = new DateTime(1990, 5, 15);
        var users = SetupUsers(dob: "1990-05-15");

        users.First().DateOfBirth.Should().Be(dob);
    }
    [Fact]
    public void User_ShouldStoreBasicPropertiesCorrectly()
    {
        var users = SetupUsers();

        var user = users.First();
        user.Forename.Should().Be("Johnny");
        user.Surname.Should().Be("User");
        user.Email.Should().Be("juser@example.com");
        user.IsActive.Should().BeTrue();
    }

    // --- Actions Section ---
    [Fact]
    public void Add_ShouldCallServiceAddAndRedirect()
    {
        var controller = CreateController();
        var user = new User { Id = 1, Forename = "New", Surname = "User" };

        var result = controller.Create(user);

        _userService.Verify(s => s.Add(user), Times.Once);
        result.Should().BeOfType<RedirectToActionResult>()
            .Which.ActionName.Should().Be("List1");
    }

    [Fact]
    public void View_ShouldReturnCorrectUser()
    {
        var controller = CreateController();
        var user = SetupUsers().First();

        _userService.Setup(s => s.GetById(1)).Returns(user);

        var result = controller.View(1) as ViewResult;
        result.Should().NotBeNull();
        result.Model.Should().BeOfType<UserManagement.Models.User>();
        result.Model.Should().Be(user);
    }

    [Fact]
    public void Edit_ShouldUpdateUserAndRedirect()
    {
        var controller = CreateController();
        var user = SetupUsers().First();

        var result = controller.Edit(user.Id, user);

        _userService.Verify(s => s.Update(user), Times.Once);
        result.Should().BeOfType<RedirectToActionResult>()
            .Which.ActionName.Should().Be("List1");
    }

    [Fact]
    public void Delete_ShouldRemoveUserAndRedirect()
    {
        var controller = CreateController();
        var userId = 1;

        var result = controller.DeleteConfirmed(userId);

        _userService.Verify(s => s.Delete(userId), Times.Once);
        result.Should().BeOfType<RedirectToActionResult>()
            .Which.ActionName.Should().Be("List1");
    }

    //private User[] SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true, string dob)
    //{
    //    var users = new[]
    //    {
    //        new User
    //        {
    //            Forename = forename,
    //            Surname = surname,
    //            Email = email,
    //            IsActive = isActive,
    //            DateOfBirth = DateTime.Parse(dob)
    //        }
    //    };

    //    _userService
    //        .Setup(s => s.GetAll())
    //        .Returns(users);

    //    return users;
    //}

}
