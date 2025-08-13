using 
using Moq;
namespace DOBTest;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public async Task List_MapsDateOfBirth_Correctly()
    {
        //Arrange
        var dob = new DateTime(1990, 5, 15);
        var mockService = new Mock<IUserService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<User>
            {
                new User { Id = 1, Forename = "John", DateOfBirth = dob, IsActive = true }
        });
        var controller = new ();

        //Act
        var result = controller.List(null) as ViewResult;
        var model = result?.Model as User;

        //Assert
        Assert.Equals(dob, model.DateOfBirth.ToString());
    }
}
