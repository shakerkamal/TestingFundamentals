using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TestingFundamentals.Mocking;

public class VideoService
{
    private readonly IFileReader fileReader;

    public VideoService(IFileReader fileReader)
    {
        this.fileReader = fileReader;
    }

    public string ReadVideoTitle()
    {
        var str = this.fileReader.Read("video.txt");
        var video = JsonConvert.DeserializeObject<Video>(str);

        if (video == null)
            return "Error parsing the video.";
        return video.Title;
    }

    public string GetUnprocessedVideosAsCsv()
    {
        var videoIds = new List<int>();

        using (var context = new VideoContext())
        {
            var videos = (from video in context.Videos
                          where !video.IsProcessed
                          select video).ToList();

            foreach (var video in videos)
                videoIds.Add(video.Id);

            return string.Join(",", videoIds);
        }
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