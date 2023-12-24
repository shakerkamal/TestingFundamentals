using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public class InstallerHelper
{
    private string _setupDestinationFile;
    private readonly IFileDownloader _fileDownloader;
    public InstallerHelper(IFileDownloader fileDownloader)
    {
        _fileDownloader = fileDownloader;
    }

    public bool DownloadInstaller(string customerName, string installerName)
    {
        try
        {
            _fileDownloader.Download(
                string.Format("http://example.com/{0}/{1}",
                customerName,
                installerName),
                _setupDestinationFile);

            return true;
        }
        catch (WebException)
        {
            return false;
        }

    }
}
