using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _videoService;
    private Mock<IFileReader> _mockFileReader;
    [SetUp]
    public void Setup()
    {
        //since it will be resued multiple times
        //declared as a setup method removes repeated code in the test methods
        _mockFileReader = new Mock<IFileReader>();
        _videoService = new VideoService(_mockFileReader.Object);
    }
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        //not an actual object
        //by default it has no behavior
        //_mockFileReader = new Mock<IFileReader>();
        _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");
        //_videoService = new VideoService(_mockFileReader.Object);

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}
