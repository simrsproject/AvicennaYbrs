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
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MeasuredGoalEntry : BasePageDialogHistEntry
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

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close
            //ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            Splitter.Orientation = Orientation.Horizontal;
            Splitter.Items[0].Height=Unit.Pixel(250);

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Measured Goal of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuEdit.Enabled = !string.IsNullOrEmpty(txtSeqNo.Text);
        }

        #region override method

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = new RegistrationMeasuredGoal();
            var isLoaded = false;
            if (string.IsNullOrEmpty(txtSeqNo.Text))
            {
                var qr = new RegistrationMeasuredGoalQuery();
                qr.es.Top = 1;
                qr.Where(qr.RegistrationNo == RegistrationNo);
                qr.OrderBy(qr.SeqNo.Descending);
                isLoaded = ent.Load(qr);

            }
            else
            {
                isLoaded = ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt());
            }

            if (isLoaded)
                txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);

            txtProblem.Text = ent.Problem;
            txtPlanning.Text = ent.Planning;
            txtGoal.Text = ent.Goal;
            txtQty.Value = Convert.ToDouble(ent.Qty);
            txtIterationQty.Value = Convert.ToDouble(ent.IterationQty);

            ComboBox.PopulateWithOneStandardReference(cboSRTimeType, AppEnum.StandardReference.TimeType.ToString(), ent.SRTimeType);


        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdMeasuredGoal.Columns[0].Visible = newVal == AppEnum.DataMode.Read;
            grdMeasuredGoal.Columns[grdMeasuredGoal.Columns.Count-2].Visible = newVal == AppEnum.DataMode.Read;
        }
        protected override void OnMenuNewClick()
        {
            txtSeqNo.Text = string.Empty;
            txtProblem.Text = string.Empty;
            txtPlanning.Text = string.Empty;
            txtGoal.Text = string.Empty;
            txtQty.Value = 0;
            txtIterationQty.Value = 0;

            var timeType = cboSRTimeType.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRTimeType, AppEnum.StandardReference.TimeType);
            ComboBox.SelectedValue(cboSRTimeType, timeType);
            

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
            if (!args.IsCancel)
            {
                grdMeasuredGoal.Rebind();
            }
        }

        private bool Save(ValidateArgs args, bool isNewRecord=false)
        {
            var ent = new RegistrationMeasuredGoal();
            if (isNewRecord || !ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.SeqNo = NewSeqNo();
                txtSeqNo.Text = ent.SeqNo.ToString();

                ent.SRUserType = AppSession.UserLogin.SRUserType;
            }

            ent.Problem = txtProblem.Text;
            ent.Planning = txtPlanning.Text;
            ent.Goal = txtGoal.Text;
            ent.Qty = Convert.ToInt16(txtQty.Value);
            ent.IterationQty = Convert.ToInt16(txtIterationQty.Value);
            ent.SRTimeType = cboSRTimeType.SelectedValue;
            ent.IsVoid = false;
            ent.Save();

            return true;
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, false);
            if (!args.IsCancel)
            {
                grdMeasuredGoal.Rebind();
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            MedicalRecordEditableValidate(args, RegistrationNo);
        }

        protected override void OnMenuEditClick()
        {
            var timeType = cboSRTimeType.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRTimeType, AppEnum.StandardReference.TimeType);
            ComboBox.SelectedValue(cboSRTimeType,timeType);
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var seqNo = txtSeqNo.Text.ToInt();

            var nmd = new RegistrationMeasuredGoal();
            if (nmd.LoadByPrimaryKey(RegistrationNo, seqNo))
            {
                nmd.IsVoid = true;
                nmd.Save();
            }

            grdMeasuredGoal.Rebind();
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
            MedicalRecordAddableValidate(args, RegistrationNo);
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

        private int NewSeqNo()
        {
            var qr = new RegistrationMeasuredGoalQuery("a");
            var fb = new RegistrationMeasuredGoal();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SeqNo.ToInt() + 1;
            }
            return 1;
        }

        public override void OnServerValidate(ValidateArgs args)
        {
            if (txtIterationQty.Value == 0 || txtQty.Value == 0 || string.IsNullOrEmpty(cboSRTimeType.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = @"Time Criteria selected properly";
            }
        }

        #region List
        protected void grdMeasuredGoal_ItemCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (e.CommandName == "View")
            {
                txtSeqNo.Text = string.Format("{0:00000}",item.OwnerTableView.DataKeyValues[item.ItemIndex]["SeqNo"]);
                OnPopulateEntryControl(new ValidateArgs());
                RefreshMenuStatus();
            }
        }
        protected void grdMeasuredGoal_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }

        protected void grdMeasuredGoal_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMeasuredGoal.DataSource = MeasuredGoalDataTable();
        }

        protected void grdMeasuredGoal_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var seqNo =item.OwnerTableView.DataKeyValues[item.ItemIndex]["SeqNo"].ToInt();

            var nmd = new RegistrationMeasuredGoal();
            if (nmd.LoadByPrimaryKey(RegistrationNo, seqNo))
            {
                nmd.IsVoid = true;
                nmd.Save();
            }

            grdMeasuredGoal.Rebind();
        }

        private DataTable MeasuredGoalDataTable()
        {
            var query = new RegistrationMeasuredGoalQuery("a");
            var time = new AppStandardReferenceItemQuery("time");
            query.LeftJoin(time).On(query.SRTimeType == time.ItemID && time.StandardReferenceID == "TimeType");

            var user = new AppUserQuery("u");
            query.LeftJoin(user).On(query.CreatedByUserID == user.UserID);
            query.Select
            (
                query, time.ItemName.As("SRTimeTypeName"), user.UserName.As("CreatedByUserName")
            );

            query.Where(query.RegistrationNo == RegistrationNo);

            query.OrderBy(query.SeqNo.Descending);
            var dtb = query.LoadDataTable();

            return dtb;
        }


        #endregion
    }
}
