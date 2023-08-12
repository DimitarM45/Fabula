namespace Fabula.Web.Tests.Mocks;

public class CompositionServiceMock
{
    public static ICompositionService Instance()
    {
        Mock<ICompositionService> compositionServiceMock = new Mock<ICompositionService>();

        compositionServiceMock
            .Setup(cs => cs.GetCountAsync())
            .ReturnsAsync(10);
            
        return compositionServiceMock.Object;
    }
}
