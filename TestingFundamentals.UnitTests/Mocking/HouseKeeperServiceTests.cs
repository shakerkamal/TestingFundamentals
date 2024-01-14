using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IStatementGenerator> _statementGenerator;
    private Mock<IEmailSender> _emailSender;
    private Mock<IXtraMessageBox> _messageBox;
    private HouseKeeperService _service;
    private DateTime _statementDate = new DateTime(2022, 1, 1);
    private HouseKeeper _houseKeeper;
    private string _statementFileName;

    [SetUp]
    public void SetUp()
    {
        _houseKeeper = new HouseKeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(uow => uow.Query<HouseKeeper>()).Returns(new List<HouseKeeper>
        {
           _houseKeeper
        }.AsQueryable());

        _statementFileName = "fileName";
        _statementGenerator = new Mock<IStatementGenerator>();
        _statementGenerator
            .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
            .Returns(() => _statementFileName); //lazy evaluation with empty lambda expression

        _emailSender = new Mock<IEmailSender>();
        _messageBox = new Mock<IXtraMessageBox>();
        _service = new HouseKeeperService(
            _unitOfWork.Object,
            _statementGenerator.Object,
            _emailSender.Object,
            _messageBox.Object);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(sg => 
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
    }

    #region without parameterized test for email input
    [Test]
    public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
    {
        _houseKeeper.Email = null;

        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
    }

    [Test]
    public void SendStatementEmails_HouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatements()
    {
        _houseKeeper.Email = " ";

        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
    }

    [Test]
    public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateStatements()
    {
        _houseKeeper.Email = "";

        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
    }
    #endregion 

    #region Parameterized Test with email value input as test cases
    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_WhenCalled_ShouldNotGenerateStatements(string email)
    {
        _houseKeeper.Email = email;

        _service.SendStatementEmails(_statementDate);

        _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
    }
    #endregion


    [Test]
    public void SendStatementEmails_WhenCalled_ShouldEmailStatements()
    {
        // =====> Has already been setup in the SetUp method <=====

        //_statementGenerator
        //    .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
        //    .Returns(_statementFileName);

        _service.SendStatementEmails(_statementDate);

        VerifyEmailIsSent();
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_StatementFileNameIsNullOrWhiteSpaceOrEmptyString_ShouldNotEmailStatements(string fileName)
    {
        _statementFileName = fileName;

        _service.SendStatementEmails(_statementDate);

        VerifyEmailNotSent();
    }

    [Test]
    public void SendStatementEmails_EmailSendingFails_ShouldDisplayAMessageBox()
    {
        _emailSender.Setup(es => es.EmailFile(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())
        ).Throws<Exception>();

        _service.SendStatementEmails(_statementDate);

        VerifyMessageBoxIsShown();
    }

    #region private helper method
    private void VerifyEmailIsSent()
    {
        _emailSender.Verify(es => es.EmailFile(
            _houseKeeper.Email,
            _houseKeeper.StatementEmailBody,
            _statementFileName,
            It.IsAny<string>()));
    }

    private void VerifyEmailNotSent()
    {
        _emailSender.Verify(es => es.EmailFile(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()), Times.Never);
    }

    private void VerifyMessageBoxIsShown()
    {
        _messageBox.Verify(mb => mb.Show(
           It.IsAny<string>(),
           It.IsAny<string>(),
           MessageBoxButtons.OK));
    }
    #endregion
}
