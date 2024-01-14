namespace TestingFundamentals.Mocking;

public class HouseKeeperService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStatementGenerator _statementGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IXtraMessageBox _messageBox;

    public HouseKeeperService(
        IUnitOfWork unitOfWork, 
        IStatementGenerator statementGenerator, 
        IEmailSender emailSender,
        IXtraMessageBox messageBox)
    {
        _unitOfWork = unitOfWork;
        _statementGenerator = statementGenerator;
        _emailSender = emailSender;
        _messageBox = messageBox;
    }

    public void SendStatementEmails(DateTime statementDate)
    {
        var houseKeepers = _unitOfWork.Query<HouseKeeper>();
        foreach(var houseKeeper in houseKeepers)
        {
            if (houseKeeper.Email is null)
                continue;

            var statementFilename = _statementGenerator.SaveStatement(houseKeeper.Oid, houseKeeper.FullName, statementDate);

            if(string.IsNullOrWhiteSpace(statementFilename))
                continue;

            var emailAddress = houseKeeper.Email;
            var emailBody = houseKeeper.StatementEmailBody;

            try
            {
                _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                    string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, houseKeeper.FullName)); 
            }
            catch(Exception ex)
            {
                _messageBox.Show(ex.Message, string.Format("Email failure: {0}",emailAddress),MessageBoxButtons.OK);
            }
        }
    }
}
public enum MessageBoxButtons
{
    OK
}

public interface IXtraMessageBox
{
    void Show(string s, string houseKeeperStatements, MessageBoxButtons ok);
}

public class XtraMessageBox : IXtraMessageBox
{
    public void Show(string s, string houseKeeperStatements, MessageBoxButtons ok)
    {
    }
}

public class MainForm
{
    public bool HouseKeeperStatementsSending { get; set; }
}

public class HouseKeeper
{
    public int Oid { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string StatementEmailBody { get; set; } = string.Empty;
}
