using System;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class PrintManager
    {
        public static string CreatePrintJob(string reportID, PrintJobParameterCollection jobPars)
        {
            return CreatePrintJob(reportID, jobPars, AppSession.UserLogin.UserID);
        }

        public static string CreatePrintJob(string reportID, PrintJobParameterCollection jobPars, string UserID)
        {
            return CreatePrintJob(reportID, jobPars, UserID, Helper.GetUserHostName());
        }
        public static string CreatePrintJob(string reportID, PrintJobParameterCollection jobPars, string UserID, string hostName)
        {
            string printerName;
            //using (var scope = new esTransactionScope())
            {
                var printID = GetPrintID(reportID, hostName);
                if (string.IsNullOrEmpty(printID))
                    return string.Empty;

                var appProgram = new AppProgram();
                appProgram.LoadByPrimaryKey(reportID);
                var printJob = new PrintJob
                {
                    ProgramID = reportID,
                    UserID = UserID,
                    PrintDateTime = DateTime.Now,
                    PrinterID = printID,
                    ApplicationID = appProgram.ApplicationID ?? ApplicationSettings.DefaultApplication.Name,
                    UserHostName = hostName
                };

                var isHasExist = true;
                while (isHasExist)
                {
                    // Cegah NewPrintNo() ternyata sudah terpakai
                    var newPrintNo = NewPrintNo();
                    var checkPrintJob = new PrintJob();
                    if (!checkPrintJob.LoadByPrimaryKey(newPrintNo))
                    {
                        printJob.PrintNo = newPrintNo;
                        printJob.Save(); // Langsung save supaya up to date hasil NewPrintNo() oleh user lain

                        isHasExist = false;
                    }
                }

                foreach (var item in jobPars)
                {
                    // Dicheck dahulu karena ada kasus parameter Name nya double dan belum diketahui darimana shg mudahnya dicheck ;a;u save satu persatu (Handono 2024 okt 04)
                    var checkJobPar = new PrintJobParameter();
                    if (!checkJobPar.LoadByPrimaryKey(printJob.PrintNo ?? 0, item.Name))
                    {
                        var newJobPar = new PrintJobParameter()
                        {
                            PrintNo = printJob.PrintNo,
                            Name = item.Name,
                            ValueDateTime = item.ValueDateTime,
                            ValueNumeric = item.ValueNumeric,
                            ValueString = item.ValueString
                        };
                        newJobPar.Save();
                    }
                }


                var printer = new Printer();
                printer.LoadByPrimaryKey(printID);
                printerName = printer.PrinterName;

                //scope.Complete();
            }

            return printerName;
        }

        public static void CreatePrintJob(string reportID, string zplCommandPrint)
        {
            var hostName = Helper.GetUserHostName();
            var printID = GetPrintID(reportID, hostName);

            if (!string.IsNullOrEmpty(printID))
            {
                var appProgram = new AppProgram();
                appProgram.LoadByPrimaryKey(reportID);
                var printJob = new PrintJob
                {
                    ProgramID = reportID,
                    UserID = AppSession.UserLogin.UserID,
                    PrintDateTime = DateTime.Now,
                    PrinterID = printID,
                    ApplicationID = appProgram.ApplicationID ?? ApplicationSettings.DefaultApplication.Name,
                    UserHostName = hostName //HttpContext.Current.Request.UserHostName
                };
                using (var scope = new esTransactionScope())
                {
                    printJob.PrintNo = NewPrintNo();
                    printJob.ZplCommand = zplCommandPrint;
                    printJob.Save();
                    scope.Complete();
                }
            }
        }

        public static string CreatePrintJobNoTransaction(string reportID, PrintJobParameterCollection jobPars)
        {
            var hostName = Helper.GetUserHostName();

            string printerName = "";

            string printID = GetPrintID(reportID, hostName);
            if (string.IsNullOrEmpty(printID))
                return string.Empty;

            var appProgram = new AppProgram();
            appProgram.LoadByPrimaryKey(reportID);
            var printJob = new PrintJob
            {
                ProgramID = reportID,
                UserID = AppSession.UserLogin.UserID,
                PrintDateTime = DateTime.Now,
                PrinterID = printID,
                PrintNo = NewPrintNo(),
                ApplicationID = appProgram.ApplicationID ?? ApplicationSettings.DefaultApplication.Name,
                UserHostName = hostName //HttpContext.Current.Request.UserHostName
            };
            foreach (var item in jobPars)
            {
                item.PrintNo = printJob.PrintNo;
            }

            printJob.Save();
            jobPars.Save();

            Printer printer = new Printer();
            printer.LoadByPrimaryKey(printID);
            printerName = printer.PrinterName;

            return printerName;
        }

        private static string GetPrintID(string reportID, string userHostName = "")
        {
            var printID = string.Empty;

            if (string.IsNullOrEmpty(userHostName)) userHostName = Helper.GetUserHostName();

            if (string.IsNullOrEmpty(userHostName)) userHostName = HttpContext.Current.Request.UserHostName;

            var printerOther = new UserHostPrinterOther();
            if (printerOther.LoadByPrimaryKey(userHostName, reportID))
                printID = printerOther.PrinterID;
            else
            {
                //Default Printer
                var userHostPrinter = new UserHostPrinter();
                if (userHostPrinter.LoadByPrimaryKey(userHostName))
                    printID = userHostPrinter.PrinterID;
            }
            return printID;
        }

        private static Int32 NewPrintNo()
        {
            Int32 printNo;
            var lastNum = new AppAutoNumberLast();
            if (!lastNum.LoadByPrimaryKey("PrintNo", Convert.ToDateTime("1900-01-01"), "", 1900, 01, 01))
            {
                lastNum = new AppAutoNumberLast();
                lastNum.SRAutoNumber = "PrintNo";
                lastNum.EffectiveDate = Convert.ToDateTime("1900-01-01");
                lastNum.YearNo = 1900;
                lastNum.MonthNo = 1;
                lastNum.DayNo = 1;
                lastNum.DepartmentInitial = "";

                // Get last PrintNo
                var jobQuery = new PrintJobQuery();
                jobQuery.Select(jobQuery.PrintNo.Max());
                var dtb = jobQuery.LoadDataTable();
                var obj = dtb.Rows[0][0];
                if (obj is DBNull)
                {
                    var jobLogQuery = new PrintJobLogQuery();
                    jobLogQuery.Select(jobLogQuery.PrintNo.Max());
                    var dtbLog = jobLogQuery.LoadDataTable();
                    obj = dtbLog.Rows[0][0];
                    printNo = obj is DBNull ? 1 : Convert.ToInt32(obj) + 1;
                }
                else
                    printNo = Convert.ToInt32(obj) + 1;

                lastNum.LastNumber = printNo;
                lastNum.LastCompleteNumber = printNo.ToString();
                lastNum.Save();
            }
            else
            {
                printNo = (lastNum.LastNumber ?? 0) + 1;
                lastNum.LastNumber = printNo;
                lastNum.LastCompleteNumber = printNo.ToString();
                lastNum.Save();
            }

            return printNo;
        }
    }
}
