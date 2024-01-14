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

    [SetUp]
    public void SetUp()
    {
        _houseKeeper = new HouseKeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
        _unitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork.Setup(uow => uow.Query<HouseKeeper>()).Returns(new List<HouseKeeper>
        {
           _houseKeeper
        }.AsQueryable());
        _statementGenerator = new Mock<IStatementGenerator>();
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
}
 