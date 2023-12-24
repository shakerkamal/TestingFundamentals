using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestingFundamentals.Mocking;

public class VideoService
{
    //depency injection as a property
    private readonly IFileReader _fileReader;
    private readonly IVideoRepository _videoRepository;

    //dependecy injection as constructor parameter
    public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
    {
        _fileReader = fileReader ?? new FileReader();
        _videoRepository = videoRepository ?? new VideoRepository();
    }

    //inject a dependency as a method parameter

    //public string ReadVideoTitle(IFileReader fileReader)
    //{
    //    var str = fileReader.Read("video.txt");
    //    var video = JsonConvert.DeserializeObject<Video>(str);

    //    if (video == null)
    //        return "Error parsing the video.";
    //    return video.Title;
    //}


    public string ReadVideoTitle()
    {
        var str = _fileReader.Read("video.txt");
        var video = JsonConvert.DeserializeObject<Video>(str);

        if (video == null)
            return "Error parsing the video.";
        return video.Title;
    }

    public string GetUnprocessedVideosAsCsv()
    {
        var videoIds = new List<int>();
        var videos = _videoRepository.GetUnprocessedVideos();
        foreach (var video in videos)
            videoIds.Add(video.Id);

        return string.Join(",", videoIds);
    }
}

public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsProcessed { get; set; }
}

public class VideoContext : DbContext
{
    public DbSet<Video> Videos { get; set; }
}