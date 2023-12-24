using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public interface IFileDownloader
{
    void Download(string url, string path);
}

public class FileDownloader : IFileDownloader
{
    public void Download(string url, string path)
    {
        var client = new WebClient();
        client.DownloadFile(url,path);
    }
}
