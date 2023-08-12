namespace Fabula.Web.Tests.Mocks;

public class UserServiceMock
{
    public static IUserService Instance()
    {
        Mock<IUserService> userServiceMock = new Mock<IUserService>();

        userServiceMock
            .Setup(us => us.GetCountAsync())
            .ReturnsAsync(10);

        return userServiceMock.Object;
    }
}
