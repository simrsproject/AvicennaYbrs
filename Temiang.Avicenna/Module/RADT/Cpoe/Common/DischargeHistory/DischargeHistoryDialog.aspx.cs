using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class DischargeHistoryDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EpisodeAndHistory;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(Request.QueryString["pid"]);

                Page.Title = "Discharge History for: " + pat.PatientName + " (MRN. " + pat.MedicalNo + ")" ;

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = Registrations();
        }

        private DataTable Registrations()
        {
            var query = new RegistrationQuery("a");
            var suQ = new ServiceUnitQuery("b");
            var parQ = new ParamedicQuery("c");
            var dmQ = new AppStandardReferenceItemQuery("d");
            var dcQ = new AppStandardReferenceItemQuery("e");

            query.InnerJoin(suQ).On(query.ServiceUnitID == suQ.ServiceUnitID);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.LeftJoin(dmQ).On(query.SRDischargeMethod == dmQ.ItemID && dmQ.StandardReferenceID == "DischargeMethod");
            query.LeftJoin(dcQ).On(query.SRDischargeCondition == dcQ.ItemID &&
                                   dcQ.StandardReferenceID == "DischargeCondition");

            query.Select
            (
                query.PatientID,
                query.RegistrationNo,
                @"<CAST(CONVERT(VARCHAR(10), a.RegistrationDate, 112) + ' ' + a.RegistrationTime AS DATETIME) AS RegistrationDateTime>",
                parQ.ParamedicName,
                suQ.ServiceUnitName,
                query.BedID,
                @"<CAST(CONVERT(VARCHAR(10), a.DischargeDate, 112) + ' ' + a.DischargeTime AS DATETIME) AS DischargeDate>",
                dmQ.ItemName.As("DischargeMethod"),
                dcQ.ItemName.As("DischargeCondition"),
                query.DischargeNotes,
                query.DischargeMedicalNotes

            );

            query.Where(query.PatientID == Request.QueryString["pid"], query.IsVoid == false);
            query.OrderBy(query.RegistrationDate.Descending, query.RegistrationTime.Descending);

            var dtb = query.LoadDataTable();
            dtb.Columns.Add(new DataColumn("Diagnosis", typeof(string)));
            dtb.Columns.Add(new DataColumn("ICD10", typeof(string)));
            dtb.Columns.Add(new DataColumn("Therapy", typeof(string)));

            foreach (DataRow row in dtb.Rows)
            {
                var regNo = row["RegistrationNo"].ToString();

                

                // Diagnose entri oleh dokter
                var rimQr = new RegistrationInfoMedicQuery();
                rimQr.Where(rimQr.RegistrationNo == regNo);
                rimQr.es.Top = 1;
                rimQr.Select(rimQr.Info3, rimQr.Info4);
                var rim = new RegistrationInfoMedic();
                if (rim.Load(rimQr))
                {
                    row["Diagnosis"] = rim.Info3;
                    row["Therapy"] = rim.Info4;
                }
                else
                {
                    // Cari didata lama
                    var soapQr = new EpisodeSOAPEQuery();
                    soapQr.Where(soapQr.RegistrationNo == regNo);
                    soapQr.es.Top = 1;
                    soapQr.Select(soapQr.Assesment, soapQr.Planning);
                    var soap = new EpisodeSOAPE();
                    if (soap.Load(soapQr))
                    {
                        row["Diagnosis"] = soap.Assesment;
                        row["Therapy"] = soap.Planning;
                    }
                }

                row["ICD10"] = EpisodeDiagnose.DiagnoseSummaryHtml(regNo);
            }
            return dtb;
        }

    }
}
