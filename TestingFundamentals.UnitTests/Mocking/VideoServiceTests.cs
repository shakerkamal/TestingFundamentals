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
    private Mock<IVideoRepository> _repository;
    private Mock<IFileReader> _mockFileReader;
    [SetUp]
    public void Setup()
    {
        //since it will be resued multiple times
        //declared as a setup method removes repeated code in the test methods
        _mockFileReader = new Mock<IFileReader>();
        _repository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_mockFileReader.Object, _repository.Object);
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

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
    {

        _repository.Setup(v => v.GetUnprocessedVideos()).Returns(new List<Video>());

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnsStringOfIds()
    {
        _repository.Setup(v => v.GetUnprocessedVideos()).Returns(new List<Video>
        {
                new Video { Id = 1, IsProcessed = false},
                new Video { Id = 2, IsProcessed = false},
        });

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Does.Contain("1,2"));
    }
}
