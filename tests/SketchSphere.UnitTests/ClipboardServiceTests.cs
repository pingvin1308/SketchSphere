using Microsoft.JSInterop;
using Moq;
using SketchSphere.UI.Client;

namespace SketchSphere.UnitTests;

public class ClipboardServiceTests
{
    private readonly Mock<IJSRuntime> _jsRuntimeMock;
    private readonly ClipboardService _clipboardService;

    public ClipboardServiceTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>(MockBehavior.Strict);
        _clipboardService = new ClipboardService(_jsRuntimeMock.Object);
    }
    
    [Fact]
    public async Task GetImage_ShouldReturnNewImage()
    {
        // arrange
        var x = Random.Shared.NextDouble() * Random.Shared.Next(-5_000, 5_000);
        var y = Random.Shared.NextDouble() * Random.Shared.Next(-5_000, 5_000);
        var source = Guid.NewGuid().ToString();
        var expectedSize = new ClipboardService.Size(Random.Shared.Next(0, 3_000), Random.Shared.Next(0, 3_000));
        
        _jsRuntimeMock
            .Setup(mock => mock.InvokeAsync<ClipboardService.Size>("getImageSize", new[] { source }))
            .ReturnsAsync(expectedSize);
        
        // act
        var image = await _clipboardService.GetImage(x, y, source);
        
        // assert
        _jsRuntimeMock.VerifyAll();
        Assert.NotNull(image);
        Assert.NotNull(image.Transform);
        Assert.Equal(x, image.Transform.X1);
        Assert.Equal(y, image.Transform.Y1);
        Assert.Equal(x + expectedSize.Width, image.Transform.X2);
        Assert.Equal(y + expectedSize.Height, image.Transform.Y2);
    }
    
    [Fact]
    public async Task GetImage_ShouldNotReturnNewImage()
    {
        // arrange
        var x = Random.Shared.NextDouble() * Random.Shared.Next(-5_000, 5_000);
        var y = Random.Shared.NextDouble() * Random.Shared.Next(-5_000, 5_000);
        var source = Guid.NewGuid().ToString();
        
        _jsRuntimeMock
            .Setup(mock => mock.InvokeAsync<string>("getImageSize", Array.Empty<object>()))
            .ThrowsAsync(new Exception("Error"));
        
        // act
        await Assert.ThrowsAsync<Exception>(async () => await _clipboardService.GetImage(x, y, source));
        
        // assert
        _jsRuntimeMock.VerifyAll();
        
        _jsRuntimeMock
            .Verify(
                expression: mock => mock.InvokeAsync<ClipboardService.Size>("getImageSize", It.IsAny<object[]>()),
                times: Times.Never);
    }
}