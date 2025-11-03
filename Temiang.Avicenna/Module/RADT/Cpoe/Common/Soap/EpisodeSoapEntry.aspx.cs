using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.IO;
using DevExpress.XtraBars;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeSoapEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new EpisodeSOAPE();
            entity.LoadByPrimaryKey(RegistrationNo, SequenceNo);
            var soape = (EpisodeSOAPE)entity;
            txtDateSOAP.SelectedDate = soape.SOAPEDate;
            txtTimeSOAP.SelectedDate = Convert.ToDateTime(soape.SOAPETime);
            txtSubjective.Text = soape.Subjective;
            txtObjective.Text = soape.Objective;
            txtAssesment.Text = soape.Assesment;
            txtPlanning.Text = soape.Planning;
            txtAttendingNotes.Text = soape.AttendingNotes;
            chkIsInformConcern.Checked = soape.IsInformConcern ?? false;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDateSOAP.SelectedDate = timeNow;
            txtTimeSOAP.SelectedDate = timeNow;
            txtSubjective.Text = string.Empty;
            txtObjective.Text = string.Empty;
            txtAssesment.Text = string.Empty;
            txtPlanning.Text = string.Empty;
            txtAttendingNotes.Text = string.Empty;
            chkIsInformConcern.Checked = false;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var soape = new EpisodeSOAPE();
            soape.Query.es.Top = 1;
            soape.Query.Where(
                soape.Query.RegistrationNo == RegistrationNo,
                soape.Query.ServiceUnitID == Request.QueryString["unit"]
                );
            soape.Query.OrderBy(soape.Query.SequenceNo.Descending);

            var es = new EpisodeSOAPE();
            es.RegistrationNo = RegistrationNo;
            es.SequenceNo = soape.Query.Load() ? Convert.ToString(Convert.ToInt32(soape.SequenceNo) + 1) : "1";
            es.IsSummary = false;
            es.IsVoid = false;
            es.ServiceUnitID = Request.QueryString["unit"];
            es.SOAPEDate = txtDateSOAP.SelectedDate;
            es.SOAPETime = txtTimeSOAP.SelectedDate.Value.ToString("HH:mm");
            es.ParamedicID = ParamedicID;
            es.Subjective = txtSubjective.Text;
            es.Objective = txtObjective.Text;
            es.Assesment = txtAssesment.Text;
            es.Planning = txtPlanning.Text;
            es.AttendingNotes = txtAttendingNotes.Text;
            es.IsInformConcern = chkIsInformConcern.Checked;

            es.Save();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var es = new EpisodeSOAPE();
            if (es.LoadByPrimaryKey(RegistrationNo, SequenceNo))
            {
                es.ParamedicID = ParamedicID;
                es.SOAPEDate = txtDateSOAP.SelectedDate;
                es.SOAPETime = txtTimeSOAP.SelectedDate.Value.ToString("HH:mm");
                es.Subjective = txtSubjective.Text;
                es.Objective = txtObjective.Text;
                es.Assesment = txtAssesment.Text;
                es.Planning = txtPlanning.Text;
                es.AttendingNotes = txtAttendingNotes.Text;
                es.IsInformConcern = chkIsInformConcern.Checked;

                es.Save();
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }


        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EpisodeAndHistory;
            // Program Fiture
            IsSingleRecordMode = true; // Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                Session.Remove("Soap:BodyID");

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(Request.QueryString["medno"]))
                {
                    this.Title = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private string SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"];
            }
        }
    }
}
