using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public interface IStatementGenerator
{
    string SaveStatement(int houseKeeperOid, string houseKeeperName, DateTime statementDate);
}

public class StatementGenerator : IStatementGenerator
{
    public string SaveStatement(int houseKeeperOid, string houseKeeperName, DateTime statementDate)
    {
        var report = new HouseKeeperStatementReport(houseKeeperOid, statementDate);

        if (!report.HasData)
            return string.Empty;

        report.CreateDocument();

        var fileName = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, houseKeeperName));

        report.ExportToPdf(fileName);

        return fileName;
    }

}
