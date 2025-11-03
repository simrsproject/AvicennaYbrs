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
    public partial class PioEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];

        private Pio _current;
        private Pio PioCurrent
        {
            get
            {
                if (_current == null)
                {
                    var ent = new Pio();
                    if (!IsPostBack)
                        ent.LoadByPrimaryKey(Request.QueryString["pn"].ToInt());
                    else
                        ent.LoadByPrimaryKey(txtPioNo.Text.ToInt());

                    _current = ent;
                }

                return _current;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DrugInfService;

            // Program Fiture
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            ComboBox.StandardReferenceItem(cboSROccupation, "Occupation", true, "PIO");
            ComboBox.StandardReferenceItem(cboSRDurationType, "DurationType", true);
            ComboBox.StandardReferenceItem(cboSRAnswerMethod, "AnswerMethod", true);
            ComboBox.StandardReferenceItem(cboSRQuestionMethod, "AnswerMethod", true); // Isinya sama dengan cboSRAnswerMethod
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = PioCurrent;
            txtPioNo.Text = string.Format("{0:00000000000}", ent.PioNo);
            ComboBox.SelectedValue(cboSRQuestionMethod, ent.SRQuestionMethod);
            txtPioDateTime.SelectedDate = ent.PioDateTime;
            txtQuestionerName.Text = ent.QuestionerName;
            ComboBox.SelectedValue(cboSROccupation, ent.SROccupation);
            ComboBox.PopulateWithOneServiceUnit(cboServiceUnitID, ent.ServiceUnitID);
            txtQuestion.Text = ent.Question;
            txtInformation.Text = ent.Information;
            txtOtherCategory.Text = ent.OtherCategory;
            txtOtherSource.Text = ent.OtherSources;
            optRecipentType.SelectedValue = (ent.IsInternRecipient ?? false) ? "1" : "0";
            ComboBox.SelectedValue(cboSRAnswerMethod, ent.SRAnswerMethod);
            ComboBox.SelectedValue(cboSRDurationType, ent.SRDurationType);
            txtAnswerDateTime.SelectedDate = ent.AnswerDateTime;

            grdPioCategoryLine.Rebind();
            grdPioSourceLine.Rebind();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var isEdited = newVal != AppEnum.DataMode.Read;
            grdPioCategoryLine.Columns[0].Display = isEdited; // Selected
            grdPioCategoryLine.Columns[1].Display = !isEdited; // IsSelected

            grdPioSourceLine.Columns[0].Display = isEdited; // Selected
            grdPioSourceLine.Columns[1].Display = !isEdited; // IsSelected

        }
        protected override void OnMenuNewClick()
        {
            txtPioNo.Text = string.Format("{0:00000}", NewPioNo());
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtPioDateTime.SelectedDate = timeNow;
            grdPioCategoryLine.Rebind();
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = new Pio();
                if (isNewRecord || !ent.LoadByPrimaryKey(txtPioNo.Text.ToInt()))
                {
                    ent.PioNo = NewPioNo();
                }
                ent.PioDateTime = txtPioDateTime.SelectedDate;
                ent.SRQuestionMethod = cboSRQuestionMethod.SelectedValue;
                ent.QuestionerName = txtQuestionerName.Text;
                ent.SROccupation = cboSROccupation.SelectedValue;
                ent.Question = txtQuestion.Text;
                ent.Information = txtInformation.Text;
                ent.Information = txtInformation.Text;
                ent.OtherCategory = txtOtherCategory.Text;
                ent.OtherSources = txtOtherSource.Text;
                ent.IsInternRecipient = (optRecipentType.SelectedValue == "1");

                if (string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    ent.str.ServiceUnitID = string.Empty;
                else
                    ent.ServiceUnitID = cboServiceUnitID.SelectedValue;

                if (string.IsNullOrEmpty(cboSRAnswerMethod.SelectedValue))
                    ent.str.SRAnswerMethod = string.Empty;
                else
                    ent.SRAnswerMethod = cboSRAnswerMethod.SelectedValue;

                if (string.IsNullOrEmpty(cboSRDurationType.SelectedValue))
                    ent.str.SRDurationType = string.Empty;
                else
                    ent.SRDurationType = cboSRDurationType.SelectedValue;

                if (txtPioDateTime.IsEmpty)
                    ent.str.AnswerDateTime = string.Empty;
                else
                    ent.AnswerDateTime = txtPioDateTime.SelectedDate;
                ent.Save();


                // PioCategoryLine
                var catColl = new PioCategoryLineCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    catColl.Query.Where(catColl.Query.PioNo == txtPioNo.Text.ToInt());
                    catColl.LoadAll();
                    catColl.MarkAllAsDeleted();
                    catColl.Save();
                }

                catColl = new PioCategoryLineCollection();
                foreach (GridDataItem item in grdPioCategoryLine.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));

                    if (chkIsSelected.Checked)
                    {
                        var entLine = catColl.AddNew();
                        entLine.PioNo = txtPioNo.Text.ToInt();
                        entLine.SRPioCategory = item.GetDataKeyValue("ItemID").ToString();
                    }
                }

                // PioCategoryLine
                var sourceColl = new PioSourceLineCollection();
                if (DataModeCurrent == AppEnum.DataMode.Edit)
                {
                    sourceColl.Query.Where(sourceColl.Query.PioNo == txtPioNo.Text.ToInt());
                    sourceColl.LoadAll();
                    sourceColl.MarkAllAsDeleted();
                    sourceColl.Save();
                }

                sourceColl = new PioSourceLineCollection();
                foreach (GridDataItem item in grdPioSourceLine.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));

                    if (chkIsSelected.Checked)
                    {
                        var entLine = sourceColl.AddNew();
                        entLine.PioNo = txtPioNo.Text.ToInt();
                        entLine.SRPioSource = item.GetDataKeyValue("ItemID").ToString();
                    }
                }

                catColl.Save();
                sourceColl.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            grdPioCategoryLine.Rebind();
            return true;
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
            grdPioCategoryLine.Rebind();
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
            var ent = PioCurrent;
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
            return (PioCurrent.IsDeleted ?? false) == false && PioCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
        }

        public override bool OnGetStatusMenuDelete()
        {
            // Bisa klik tombol Delete
            return (PioCurrent.IsDeleted ?? false) == false && PioCurrent.CreatedByUserID == AppSession.UserLogin.UserID;
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

        private int NewPioNo()
        {
            var qr = new PioQuery("a");
            var fb = new Pio();
            qr.es.Top = 1;
            qr.OrderBy(qr.PioNo.Descending);

            if (fb.Load(qr))
            {
                return fb.PioNo.ToInt() + 1;
            }
            return 1;
        }

        #region PioCategoryLine
        protected void grdPioCategoryLine_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = PioCategoryLineDataTable(txtPioNo.Text.ToInt());
        }

        private DataTable PioCategoryLineDataTable(int pioNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new PioCategoryLineQuery("a");

            que.LeftJoin(qrLine)
                .On(que.ItemID == qrLine.SRPioCategory & qrLine.PioNo == pioNo);

            que.Where(que.StandardReferenceID == "PioCategory");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, que.ReferenceID, "<CONVERT(BIT,CASE WHEN a.SRPioCategory IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdPioCategoryLine_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;
            }
        }
        #endregion

        #region PioSourceLine
        protected void grdPioSourceLine_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            grd.DataSource = PioSourceLineDataTable(txtPioNo.Text.ToInt());
        }

        private DataTable PioSourceLineDataTable(int pioNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new PioSourceLineQuery("a");

            que.LeftJoin(qrLine)
                .On(que.ItemID == qrLine.SRPioSource & qrLine.PioNo == pioNo);

            que.Where(que.StandardReferenceID == "PioSource");
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, que.ReferenceID, "<CONVERT(BIT,CASE WHEN a.SRPioSource IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdPioSourceLine_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;
            }
        }
        #endregion

    }
}
