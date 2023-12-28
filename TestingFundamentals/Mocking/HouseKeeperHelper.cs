using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public class HouseKeeperHelper
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStatementGenerator _statementGenerator;
    private readonly IEmailSender _emailSender;

    public HouseKeeperHelper(IUnitOfWork unitOfWork, IStatementGenerator statementGenerator, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _statementGenerator = statementGenerator;
        _emailSender = emailSender;
    }

    public bool SendStatementEmails(DateTime statementDate)
    {
        var houseKeepers = _unitOfWork.Query<HouseKeeper>();

        foreach(var  houseKeeper in houseKeepers)
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
                return true;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, string.Format("Email failure: {0}",emailAddress),MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
public enum MessageBoxButtons
{
    OK
}

public class XtraMessageBox
{
    public static void Show(string s, string houseKeeperStatements, MessageBoxButtons ok)
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
    public string FullName { get; set; }
    public string Email { get; set; }
    public string StatementEmailBody { get; set; }
}
