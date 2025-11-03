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
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ChartingEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var entity = new PatientDocumentImage();
            entity.LoadByPrimaryKey(PatientID, SequenceNo);
            txtName.Text = entity.Name;
            txtNotes.Text = entity.Notes;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {
            txtName.Text = string.Empty;
            txtNotes.Text = string.Empty;
        }

        private int GetNewSequenceNo()
        {
            var qr = new PatientDocumentImageQuery();
            qr.es.Top = 1;
            qr.Where(qr.PatientID == PatientID);
            qr.Select(qr.SequenceNo);
            qr.OrderBy(qr.SequenceNo.Descending);
            var dtb = qr.LoadDataTable();

            int newSqNo = 1;
            if (dtb.Rows.Count > 0)
                newSqNo = Convert.ToInt32(dtb.Rows[0][0])+1;

            return newSqNo;
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {

            var es = new PatientDocumentImage();
            es.RegistrationNo = RegistrationNo;
            es.SequenceNo = GetNewSequenceNo();
            es.PatientID = PatientID;
            es.RegistrationNo = RegistrationNo;
            es.Name = txtName.Text;
            es.Notes = txtNotes.Text;

            es.Save();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var es = new PatientDocumentImage();
            if (es.LoadByPrimaryKey(PatientID, SequenceNo))
            {
                es.Name = txtName.Text;
                es.Notes = txtNotes.Text;
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

        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (this.Request.QueryString["rt"])
                {
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            ProgramID = AppConstant.Program.ElectronicHealthRecord;

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
                Session.Remove("ImgTemplateID");

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

        private int SequenceNo
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["seqno"]);
            }
        }
    }
}
