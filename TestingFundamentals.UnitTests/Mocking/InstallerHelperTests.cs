using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _mockDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _mockDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_mockDownloader.Object);
        }
        [Test]
        public void DownloadInstaller_ReturnsTrue_ForSucessfulDownload()
        {
            _mockDownloader.Setup(md => md.Download("url", "destination"));
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            //more specific test case
            //mockDownloader.Setup(md => md.Download("http://example.com/customer/installer", null)).Throws<WebException>();

            _mockDownloader.Setup(md => md.Download(It.IsAny<string>(),It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }
    }
}
