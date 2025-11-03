using System;
using System.Linq;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RSBHP
{
    public partial class SickLetter : Report
    {
        public SickLetter(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeader);

            var query = new SickLetterQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var par = new ParamedicQuery("d");
            var occ = new AppStandardReferenceItemQuery("e");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.LeftJoin(occ).On(pat.SROccupation == occ.ItemID && occ.StandardReferenceID == "Occupation");

            query.Select(par.ParamedicName, pat.PatientName, occ.ItemName.As("Occupation"),
                @"<CASE WHEN c.Sex = 'F' then 'Perempuan' 
                   ELSE 'Laki-laki' END AS 'JK' >",
                @"<CASE WHEN b.AgeInYear > 0 then CAST(b.AgeInYear AS VARCHAR) + ' Thn' 
                   ELSE CASE WHEN b.AgeInMonth > 0 then CAST(b.AgeInMonth AS VARCHAR) + ' Bln'
                   ELSE CAST(b.AgeInDay AS VARCHAR) + ' hr' END END AS 'Umur' >", query.StartDate, query.EndDate,
                @"<DATEDIFF(dd, a.StartDate, a.EndDate) + 1 AS 'Days'>", query.Notes);

            query.Where(query.RegistrationNo == printJobParameters[0].ValueString,
                        query.ParamedicID == printJobParameters[1].ValueString);
            DataTable dtb = query.LoadDataTable();
            this.DataSource = dtb;

            var i = dtb.Rows.Cast<DataRow>().Sum(row => Convert.ToInt16(row["Days"]));
            textBox3.Value = "oleh karena sakit/cuti hamil memerlukan istirahat selama : " + i + "(" + (new Common.Convertion()).NumericToWordsWithoutCurrency(i) + ") hari";

            var healthcare = Healthcare.GetHealthcare();
            
            txtHealthcareName.Value = healthcare.HealthcareName.ToUpper();
            txtAddress1.Value = healthcare.AddressLine1.ToUpper();
            txtAddress2.Value = healthcare.AddressLine2.ToUpper() + " - " + healthcare.ZipCode;
            textBox9.Value = "Telp. "+ healthcare.PhoneNo + "; Fax " + healthcare.FaxNo;
            textBox51.Value = healthcare.City + ", " + string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
           
        }
    }
}