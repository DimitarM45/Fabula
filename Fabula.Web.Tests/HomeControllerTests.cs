namespace Fabula.Web.Tests;

using Microsoft.AspNetCore.Mvc;

[TestFixture]

public class HomeControllerTests
{
    private HomeController homeController;

    private IUserService userService;

    private ICompositionService compositionService;

    private ILogger<HomeController> logger;

    [OneTimeSetUp]

    public void SetUp()
    {
        userService = UserServiceMock.Instance();
        compositionService = CompositionServiceMock.Instance();
        logger = LoggerMock<HomeController>.Instance();

        homeController = new HomeController(userService, compositionService, logger);
    }

    [Test]

    public async Task Test_IndexShouldReturnCorrectView()
    {
        var result = await homeController.Index();

        Assert.IsNotNull(result);

        var viewResult = result as ViewResult;

        Assert.IsNotNull(viewResult);
    }
}
