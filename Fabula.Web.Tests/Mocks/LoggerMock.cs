namespace Fabula.Web.Tests.Mocks;

public class LoggerMock<TController>
    where TController : BaseController
{
    public static ILogger<TController> Instance()
    {
        Mock<ILogger<TController>> loggerMock = new Mock<ILogger<TController>>();

        loggerMock
            .Setup(l => l.LogWarning("Warning"));

        return loggerMock.Object;
    }
}
