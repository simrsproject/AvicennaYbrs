using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class CompletenessAnalysisDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var btnOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            btnOk.Visible = false;
            var btnCancel = (Button)Helper.FindControlRecursive(Master, "btnCancel");
            btnCancel.Text = "Close";

            ProgramID = AppConstant.Program.MedicalRecordFileCompletenessAnalysis;
            if (Request.QueryString["view"] == "sur")
            {
                tabStrip.Tabs[1].Visible = true;
                pgSurgicalAnesthesi.Selected = true;
                pgPHR.Visible = false;
                Title = "Surgical & Anesthetist History";
            }
            else
            {
                tabStrip.Tabs[0].Visible = true;
                pgPHR.Selected = true;
                pgSurgicalAnesthesi.Visible = false;
                Title = "Patient Health Record History";
            }
        }

        protected void grdPHR_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new PatientHealthRecordQuery("a");
            var form = new QuestionFormQuery("b");
            var unit = new ServiceUnitQuery("z");
            query.InnerJoin(form).On(query.QuestionFormID == form.QuestionFormID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.Where(query.RegistrationNo == Request.QueryString["regno"], query.QuestionFormID == Request.QueryString["qfid"]);
            query.Select(
                query.TransactionNo,
                query.RegistrationNo,
                query.QuestionFormID,
                form.QuestionFormName,
                query.ReferenceNo,
                @"<CAST(CONVERT(VARCHAR(10), a.RecordDate, 112) + ' ' + a.RecordTime AS DATETIME) AS RecordDateTime>",
                query.CreateByUserID,
                query.ServiceUnitID,
                unit.ServiceUnitName
            );

            grdPHR.DataSource = query.LoadDataTable();
        }

        protected void grdEpisodeProcedure_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var query = new ServiceUnitBookingQuery("a");
            var su = new ServiceUnitQuery("s");
            var rm = new ServiceRoomQuery("rm");

            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(rm).On(query.RoomID == rm.RoomID);

            query.Select(
                query.RegistrationNo,
                query.BookingNo,
                query.RealizationDateTimeFrom.As("ProcedureDate"),
                "<LEFT(CONVERT(VARCHAR, a.RealizationDateTimeFrom, 8), 5) as ProcedureTime>",
                query.Diagnose, 
                query.PostDiagnosis,
                su.ServiceUnitName, 
                rm.RoomName
                );

            query.Where(query.RegistrationNo == Request.QueryString["regno"]);

            query.OrderBy(query.BookingNo.Descending);

            grdEpisodeProcedure.DataSource = query.LoadDataTable();
        }
    }
}