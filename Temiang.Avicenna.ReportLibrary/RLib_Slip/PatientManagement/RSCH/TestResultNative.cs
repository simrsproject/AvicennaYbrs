namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RSCH
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for TestResultNative.
    /// </summary>
    public partial class TestResultNative : Telerik.Reporting.Report
    {
        public TestResultNative(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            TestResultQuery query = new TestResultQuery("a");
            TransChargesQuery charges = new TransChargesQuery("b");
            RegistrationQuery reg = new RegistrationQuery("c");
            PatientQuery patient = new PatientQuery("d");
            ParamedicQuery dok = new ParamedicQuery("e");
            ItemQuery item = new ItemQuery("f");
            ServiceUnitQuery unit = new ServiceUnitQuery("g");
            TransChargesItemQuery tci = new TransChargesItemQuery("h");

            query.Select
                (
                    query.TransactionNo,
                    charges.RegistrationNo,
                    query.ItemID,
                    "<f.ItemName + ':' AS 'itemname'>",
                    query.ParamedicID,
                    dok.ParamedicName,
                    query.TestResultDateTime,
                    query.TestResult.As("Result"),
                    patient.PatientName,
                    "<ISNULL(d.MedicalNo,'-') AS 'MedicalNo'>",
                    "<CONVERT(VARCHAR(MAX), d.DateOfBirth, 101) + ' - ' + dbo.[fnCalculateAge](d.DateOfBirth) AS 'DateOfBirth'>",
                    "<CASE WHEN d.sex = 'F' THEN 'Female' ELSE 'MALE' END AS 'Sex'>",
                    patient.Sex,
                    "<CASE WHEN d.StreetName = ''  AND d.City = '' THEN '-' " +
                            "WHEN d.StreetName <> '' AND d.City = '' THEN d.StreetName " +
                            "WHEN d.StreetName = '' AND d.City <> '' THEN d.City " +
                            "ELSE d.StreetName + ' - ' + d.City " +
                    "END AS 'Address'>",
                    "<CASE WHEN d.PhoneNo = ''  AND d.MobilePhoneNo = '' THEN '-' " +
                            "WHEN d.PhoneNo <> '' AND d.MobilePhoneNo = '' THEN d.PhoneNo " +
                            "WHEN d.PhoneNo = '' AND d.MobilePhoneNo <> '' THEN d.MobilePhoneNo " +
                            "ELSE d.PhoneNo + ' / ' + d.MobilePhoneNo " +
                    "END AS 'HP'>",
                    unit.ServiceUnitName
                );

            query.InnerJoin(charges).On(query.TransactionNo == charges.TransactionNo);
            query.InnerJoin(reg).On(reg.RegistrationNo == charges.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.LeftJoin(dok).On(query.ParamedicID == dok.ParamedicID);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(unit).On(charges.ToServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(tci).On(charges.TransactionNo == tci.TransactionNo && query.ItemID == tci.ItemID);

            query.Where
                (
                    query.TransactionNo == printJobParameters[0].ValueString,
                    tci.SequenceNo == printJobParameters[1].ValueString
                );

            DataTable dtb = query.LoadDataTable();
            DataSource = dtb;


        }
    }
}