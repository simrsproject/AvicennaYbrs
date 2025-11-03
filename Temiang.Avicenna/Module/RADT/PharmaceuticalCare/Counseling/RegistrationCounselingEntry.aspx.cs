using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class RegistrationCounselingEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        private RegistrationCounseling _current;
        private RegistrationCounseling RegistrationCounselingCurrent
        {
            get
            {
                if (_current == null)
                {
                    var ent = new RegistrationCounseling();
                    if (!IsPostBack)
                        ent.LoadByPrimaryKey(RegistrationNo, Request.QueryString["cn"].ToInt());
                    else
                        ent.LoadByPrimaryKey(RegistrationNo, txtCounselingNo.Text.ToInt());

                    _current = ent;
                }

                return _current;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close

            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Counseling Note of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = RegistrationCounselingCurrent;
            txtCounselingNo.Text = string.Format("{0:00000}", ent.CounselingNo);
            txtCounselingDateTime.SelectedDate = ent.CounselingDateTime;
            txtCounselingNotes.Text = ent.CounselingNotes;
            grdRegistrationCounselingLine.Rebind();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;
            grdRegistrationCounselingLine.Columns[0].Display = isEdited; // Selected
            grdRegistrationCounselingLine.Columns[1].Display = !isEdited; // IsSelected
            grdRegistrationCounselingLine.Columns[3].Display = !isEdited; // Notes
            grdRegistrationCounselingLine.Columns[4].Display = isEdited; // Notes Edit
        }
        protected override void OnMenuNewClick()
        {
            txtCounselingNo.Text = string.Format("{0:00000}", NewCounselingNo());
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtCounselingDateTime.SelectedDate = timeNow;
            grdRegistrationCounselingLine.Rebind();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new RegistrationCounseling();
                if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, txtCounselingNo.Text.ToInt()))
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.CounselingNo = NewCounselingNo();

                    // Save Unit History krn di reg di tgl berikutnya bisa berubah
                    SetServiceUnitHistory(ent, isNewRecord);
                }

                ent.CounselingDateTime = txtCounselingDateTime.SelectedDate;
                ent.CounselingNotes = txtCounselingNotes.Text;
                ent.Save();


                // RegistrationCounselingLine
                var counsColl = new RegistrationCounselingLineCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    counsColl.Query.Where(counsColl.Query.RegistrationNo == RegistrationNo, counsColl.Query.CounselingNo == txtCounselingNo.Text.ToInt());
                    counsColl.LoadAll();
                    counsColl.MarkAllAsDeleted();
                    counsColl.Save();
                }

                counsColl = new RegistrationCounselingLineCollection();
                var selecteds = new List<SelectedLine>();
                foreach (GridDataItem item in grdRegistrationCounselingLine.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));

                    if (chkIsSelected.Checked)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var entLine = counsColl.AddNew();
                        entLine.RegistrationNo = RegistrationNo;
                        entLine.CounselingNo = txtCounselingNo.Text.ToInt();
                        entLine.SRDrugCounseling = item.GetDataKeyValue("ItemID").ToString();
                        entLine.Notes = txtNotes.Text;

                        selecteds.Add(new SelectedLine(item.GetDataKeyValue("ItemID").ToString(), item.GetDataKeyValue("ReferenceID").ToString()));
                    }
                }

                // Lengkapi dgn parentid nya
                var prevSearch = string.Empty;
                foreach (var line in selecteds)
                {
                    // Check parent exist
                    if (!string.IsNullOrEmpty(line.ParentID) && prevSearch != line.ParentID)
                    {
                        prevSearch = line.ParentID;
                        var isExist = false;
                        foreach (var counLine in counsColl)
                        {
                            if (counLine.SRDrugCounseling == line.ParentID)
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            var entLine = counsColl.AddNew();
                            entLine.RegistrationNo = RegistrationNo;
                            entLine.CounselingNo = txtCounselingNo.Text.ToInt();
                            entLine.SRDrugCounseling = line.ParentID;
                        }
                    }
                }

                counsColl.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            grdRegistrationCounselingLine.Rebind();
            return true;
        }

        private void SetServiceUnitHistory(RegistrationCounseling ent, bool isNewRecord)
        {
            // Set history Service Unit
            var isNotSet = true;
            if (!isNewRecord)
            {
                var pt = PatientTransfer.LoadLastTransfer(RegistrationNo,txtCounselingDateTime.SelectedDate.Value);
                if (pt != null)
                {
                    ent.ServiceUnitID = pt.ToServiceUnitID;
                    ent.RoomID = pt.ToRoomID;
                    ent.BedID = pt.ToBedID;

                    isNotSet = false;
                }
            }

            if (isNotSet)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                ent.ServiceUnitID = reg.ServiceUnitID;
                ent.RoomID = reg.RoomID;
                ent.BedID = reg.BedID;
            }

        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
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
            grdRegistrationCounselingLine.Rebind();
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
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
            var ent = RegistrationCounselingCurrent;
            ent.IsDeleted = true;
            ent.Save();
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
            // Bisa klik tombol Edit
            return (RegistrationCounselingCurrent.IsDeleted ?? false) == false && RegistrationCounselingCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Bisa klik tombol Delete
            return (RegistrationCounselingCurrent.IsDeleted ?? false) == false && RegistrationCounselingCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
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

        private int NewCounselingNo()
        {
            var qr = new RegistrationCounselingQuery("a");
            var fb = new RegistrationCounseling();
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.es.Top = 1;
            qr.OrderBy(qr.CounselingNo.Descending);

            if (fb.Load(qr))
            {
                return fb.CounselingNo.ToInt() + 1;
            }
            return 1;
        }


        private class SelectedLine
        {
            public string ItemID { get; set; }
            public string ParentID { get; set; }
            public SelectedLine(string itemID, string parentID)
            {
                ItemID = itemID;
                ParentID = parentID;
            }
        }
        protected void grdRegistrationCounselingLine_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = RegistrationCounselingDataTable(RegistrationNo, txtCounselingNo.Text.ToInt());
        }

        private DataTable RegistrationCounselingDataTable(string registrationNo, int counselingNo, bool isJustSelected = false)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new RegistrationCounselingLineQuery("a");

            if (isJustSelected)
                que.InnerJoin(qrLine)
                    .On(que.ItemID == qrLine.SRDrugCounseling && qrLine.RegistrationNo == registrationNo && qrLine.CounselingNo == counselingNo);
            else
                que.LeftJoin(qrLine)
                .On(que.ItemID == qrLine.SRDrugCounseling && qrLine.RegistrationNo == registrationNo && qrLine.CounselingNo == counselingNo);

            que.Where(que.StandardReferenceID == "DrugCounseling");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrLine.Notes, que.ReferenceID, "<CONVERT(BIT,CASE WHEN a.SRDrugCounseling IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdRegistrationCounselingLine_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Read)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;

                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["Notes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;
            }
        }
    }
}
