using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales
{
    public partial class PrescriptionReview : BasePageDialogEntry
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public string PrescriptionNo
        {
            get
            {
                return Request.QueryString["prescno"];
            }
        }
        public string Ptype
        {
            get
            {
                return Request.QueryString["ptype"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["prgid"]))
                ProgramID = Request.QueryString["prgid"];
            else
                ProgramID = AppConstant.Program.PrescriptionSales;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var patientID = PatientID;
                if (string.IsNullOrEmpty(patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    patientID = reg.PatientID;
                }

                switch (Ptype)
                {
                    case "p":
                        lblReview.Text = "Prescription Review";
                        break;
                    case "d":
                        lblReview.Text = "Drug Review";
                        break;
                    case "n":
                        lblReview.Text = "Follow-up";
                        break;
                }

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(patientID))
                {
                    this.Title = lblReview.Text + " of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;

            switch (Ptype)
            {
                case "p":
                    //Prescription Review
                    grdPrescriptionReview.Columns[1].Display = isEdited;
                    grdPrescriptionReview.Columns[2].Display = !isEdited;

                    //Drug Review
                    grdPrescriptionReview.Columns[3].Display = false;
                    grdPrescriptionReview.Columns[4].Display = true;

                    //Follow Up
                    grdPrescriptionReview.Columns[5].Display = false;
                    grdPrescriptionReview.Columns[6].Display = true;

                    break;
                case "d":
                    //Prescription Review
                    grdPrescriptionReview.Columns[1].Display = false;
                    grdPrescriptionReview.Columns[2].Display = true;

                    //Drug Review
                    grdPrescriptionReview.Columns[3].Display = isEdited;
                    grdPrescriptionReview.Columns[4].Display = !isEdited;

                    //Follow Up
                    grdPrescriptionReview.Columns[5].Display = false;
                    grdPrescriptionReview.Columns[6].Display = true;


                    break;
                case "n":
                    //Prescription Review
                    grdPrescriptionReview.Columns[1].Display = false;
                    grdPrescriptionReview.Columns[2].Display = true;

                    //Drug Review
                    grdPrescriptionReview.Columns[3].Display = false;
                    grdPrescriptionReview.Columns[4].Display = true;

                    //Follow Up
                    grdPrescriptionReview.Columns[5].Display = isEdited;
                    grdPrescriptionReview.Columns[6].Display = !isEdited;

                    break;
            }
        }
        protected override void OnMenuNewClick()
        {
            grdPrescriptionReview.Rebind();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            // Update Prescription
            var presc = new TransPrescription();
            presc.LoadByPrimaryKey(PrescriptionNo);
            presc.ReviewByUserID = AppSession.UserLogin.UserID;
            presc.Save();

            SavePrescriptionReview();

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.value = document.getElementById('{0}').value;", hdfReturnValue.ClientID);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
            grdPrescriptionReview.Rebind();
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
            grdPrescriptionReview.Rebind();
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var line = new TransPrescriptionReviewCollection();
            line.Query.Where(line.Query.PrescriptionNo == PrescriptionNo);
            line.LoadAll();
            line.MarkAllAsDeleted();
            line.Save();

            // Update Prescription
            var presc = new TransPrescription();
            presc.LoadByPrimaryKey(PrescriptionNo);
            presc.str.ReviewByUserID = string.Empty;
            presc.Save();

            grdPrescriptionReview.Rebind();
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion


        #region Entry
        private int NewSeqNo()
        {
            var qr = new PatientEducationQuery("a");
            var fb = new PatientEducation();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (fb.Load(qr))
            {
                return MethodExts.ToInt(fb.SeqNo) + 1;
            }
            return 1;
        }
        private void SavePrescriptionReview()
        {
            using (var trans = new esTransactionScope())
            {
                var medColl = new TransPrescriptionReviewCollection();
                medColl.Query.Where(medColl.Query.PrescriptionNo == PrescriptionNo);
                medColl.LoadAll();

                if (medColl.Count == 0)
                {
                    foreach (GridDataItem item in grdPrescriptionReview.MasterTableView.Items)
                    {
                        var med = medColl.AddNew();
                        med.PrescriptionNo = PrescriptionNo;
                        med.SRPrescriptionReview = item.GetDataKeyValue("ItemID").ToString();

                        switch (Ptype)
                        {
                            case "p":
                                var chkPresc = ((CheckBox)item.FindControl("chkIsPrescriptionReview"));
                                med.IsPrescriptionReview = chkPresc.Checked;
                                med.PrescriptionReviewDateTime = DateTime.Now;
                                med.PrescriptionReviewByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "d":
                                var chkDrug = ((CheckBox)item.FindControl("chkIsDrugReview"));
                                med.IsDrugReview = chkDrug.Checked;
                                med.DrugReviewDateTime = DateTime.Now;
                                med.DrugReviewByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "n":
                                var txtNote = ((RadTextBox)item.FindControl("txtNote"));
                                med.Note = txtNote.Text;
                                med.NoteDateTime = DateTime.Now;
                                med.NoteByUserID = AppSession.UserLogin.UserID;
                                break;
                        }
                    }
                }
                else
                {
                    foreach (GridDataItem item in grdPrescriptionReview.MasterTableView.Items)
                    {
                        var med = (medColl.Where(b => b.SRPrescriptionReview == item.GetDataKeyValue("ItemID").ToString())).Take(1).Single();
                        switch (Ptype)
                        {
                            case "p":
                                var chkPresc = ((CheckBox)item.FindControl("chkIsPrescriptionReview"));
                                med.IsPrescriptionReview = chkPresc.Checked;
                                med.PrescriptionReviewDateTime = DateTime.Now;
                                med.PrescriptionReviewByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "d":
                                var chkDrug = ((CheckBox)item.FindControl("chkIsDrugReview"));
                                med.IsDrugReview = chkDrug.Checked;
                                med.DrugReviewDateTime = DateTime.Now;
                                med.DrugReviewByUserID = AppSession.UserLogin.UserID;
                                break;
                            case "n":
                                var txtNote = ((RadTextBox)item.FindControl("txtNote"));
                                med.Note = txtNote.Text;
                                med.NoteDateTime = DateTime.Now;
                                med.NoteByUserID = AppSession.UserLogin.UserID;
                                break;
                        }
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            // Return value to parent page
            hdfReturnValue.Value = string.Format("{0} @{1}", AppSession.UserLogin.UserName, DateTime.Now.ToString(AppConstant.DisplayFormat.DateTimeSecond));
        }

        private DataTable PrescriptionReviewDataTable(string prescriptionNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrReview = new TransPrescriptionReviewQuery("a");

            que.LeftJoin(qrReview)
            .On(que.ItemID == qrReview.SRPrescriptionReview && qrReview.PrescriptionNo == prescriptionNo);

            que.Where(que.StandardReferenceID == "PrescriptionReview");

            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrReview.IsDrugReview, qrReview.IsPrescriptionReview, qrReview.Note);
            return que.LoadDataTable();
        }


        #endregion
        protected void grdPrescriptionReview_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Read)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var itemID = dataItem.GetDataKeyValue("ItemID");

                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNote"));
                var notes = dataItem["Note"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;

                var chkIsPresc = ((CheckBox)dataItem.FindControl("chkIsPrescriptionReview"));
                chkIsPresc.Checked = ((CheckBox)(dataItem["IsPrescriptionReview"].Controls[0])).Checked;
                if (itemID.Equals("PRV08")) // 	Kadaluarsa Obat
                {
                    chkIsPresc.Visible = false;
                }

                var chkIsDrug = ((CheckBox)dataItem.FindControl("chkIsDrugReview"));
                chkIsDrug.Checked = ((CheckBox)(dataItem["IsDrugReview"].Controls[0])).Checked;

                if (itemID.Equals("PRV01")) // Kejelasan penulisan
                {
                    chkIsDrug.Visible = false;
                }
            }
        }
        protected void grdPrescriptionReview_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPrescriptionReview.DataSource = PrescriptionReviewDataTable(PrescriptionNo);

        }
    }
}
