using System;
using System.Linq;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RSUTAMA
{
    public partial class SickLetter : Report
    {
        public SickLetter(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var query = new SickLetterQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var par = new ParamedicQuery("d");
            var occ = new AppStandardReferenceItemQuery("e");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.LeftJoin(occ).On(pat.SROccupation == occ.ItemID && occ.StandardReferenceID == "Occupation");

            query.Select
            (
                par.ParamedicName,
                pat.PatientName,
                occ.ItemName.As("Occupation"),
                query.StartDate,
                query.EndDate,
                pat.Ssn,
                @"<DATEDIFF(dd, a.StartDate, a.EndDate) + 1 AS 'Days'>",
                query.Notes,
                "<RTRIM(c.StreetName + ' ' + c.District + ' ' + c.City + ' ' + c.ZipCode) AS Addr>",
                reg.AgeInYear
            );

            query.Where(query.RegistrationNo == printJobParameters[0].ValueString,
                query.ParamedicID == printJobParameters[1].ValueString,
                query.SRLetterType == printJobParameters[2].ValueString);
            DataTable dtb = query.LoadDataTable();
            this.DataSource = dtb;

            var i = dtb.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Days"]));
            textBox3.Value = "( dengan huruf : " + (new Common.Convertion()).NumericToWordsWithoutCurrency(i) +
                             " hari )";

            var healthcare = Healthcare.GetHealthcare();
            
            textBox51.Value = healthcare.City + ", " + string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
        }
    }
}