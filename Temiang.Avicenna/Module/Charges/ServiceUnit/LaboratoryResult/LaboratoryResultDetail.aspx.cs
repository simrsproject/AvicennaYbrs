using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Interop.SYSMEX;
using System.Globalization;
using System.Drawing;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class LaboratoryResultDetail : BasePageDetail
    {
        private string _healthcareInitial;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List 
            UrlPageSearch = "#";
            UrlPageList = "LaboratoryResultList.aspx";

            ProgramID = AppConstant.Program.LaboratoryResult;

            _healthcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;

            if (!IsPostBack)
            {
                //ToolBarMenuReloadLab.Visible = true;

                RadTabStrip1.Tabs[1].Text = string.Format("Online Result {0}", AppSession.Parameter.LisInterop);

                var sup = new ServiceUnitParamedicQuery("a");
                var p = new ParamedicQuery("b");
                sup.Select(p.ParamedicID, p.ParamedicName);
                sup.InnerJoin(p).On(sup.ParamedicID == p.ParamedicID);
                sup.Where(sup.ServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, p.IsActive == true);
                var dtb = sup.LoadDataTable();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in dtb.Rows)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
                }

                if (AppSession.Parameter.HealthcareInitial == "RSSMCB") grdTransChargesItem.MasterTableView.SortExpressions.AddSortExpression(new GridSortExpression() { FieldName = "SuperDisplaySequence", SortOrder = GridSortOrder.Ascending });
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB")
                {
                    RadTabStrip1.Tabs[0].Selected = !(AppSession.Parameter.IsUsingHisInterop);
                    RadTabStrip1.Tabs[1].Selected = !RadTabStrip1.Tabs[0].Selected;

                    MultiPage1.PageViews[0].Selected = !(AppSession.Parameter.IsUsingHisInterop);
                    MultiPage1.PageViews[1].Selected = !RadTabStrip1.Tabs[0].Selected;
                }
            }
        }

        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Rebuild")
            {
                var item = e.Item as Telerik.Web.UI.GridDataItem;
                if (item == null) return;

                var seqNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                var labs = new ItemLaboratoryProfileCollection();
                labs.Query.Where(labs.Query.ParentItemID == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ItemID"]));
                labs.Query.OrderBy(labs.Query.DisplaySequence.Ascending);
                if (labs.Query.Load())
                {
                    foreach (var lab in labs)
                    {
                        var entityLab = new TransChargesItem();
                        entityLab.Query.Where(
                            entityLab.TransactionNo == txtTransactionNo.Text,
                            entityLab.Query.ParentNo == seqNo,
                            entityLab.Query.ItemID == lab.DetailItemID
                            );
                        if (entityLab.Query.Load()) continue;

                        entityLab = new TransChargesItem();
                        entityLab.TransactionNo = txtTransactionNo.Text;

                        var tci = new TransChargesItem();
                        tci.Query.es.Top = 1;
                        tci.Query.Where(tci.Query.TransactionNo == txtTransactionNo.Text, tci.ParentNo == seqNo);
                        tci.Query.OrderBy(tci.Query.SequenceNo.Descending);
                        if (tci.Query.Load())
                        {
                            var sequenceNo = tci.SequenceNo.Substring(tci.SequenceNo.Length - 3, 3);
                            entityLab.SequenceNo = seqNo + string.Format("{0:000}", int.Parse(sequenceNo) + 1);
                        }
                        else entityLab.SequenceNo = seqNo + string.Format("{0:000}", 1);

                        tci = new TransChargesItem();
                        tci.LoadByPrimaryKey(txtTransactionNo.Text, seqNo);

                        entityLab.ParentNo = seqNo;
                        entityLab.ReferenceNo = string.Empty;
                        entityLab.ReferenceSequenceNo = string.Empty;
                        entityLab.ItemID = lab.DetailItemID;
                        entityLab.ItemName = string.Empty;
                        entityLab.ChargeClassID = tci.ChargeClassID;
                        entityLab.ParamedicID = tci.ParamedicID;
                        entityLab.ParamedicName = string.Empty;
                        entityLab.IsAdminCalculation = false;
                        entityLab.IsVariable = false;
                        entityLab.IsCito = false;
                        entityLab.ChargeQuantity = 0;
                        entityLab.StockQuantity = 0;
                        entityLab.SRItemUnit = tci.SRItemUnit;
                        entityLab.CostPrice = 0;
                        entityLab.Price = 0;
                        entityLab.CitoAmount = 0;
                        entityLab.IsCitoInPercent = false;
                        entityLab.BasicCitoAmount = 0;
                        entityLab.RoundingAmount = 0;
                        entityLab.SRDiscountReason = string.Empty;
                        entityLab.IsAssetUtilization = false;
                        entityLab.AssetID = string.Empty;
                        entityLab.IsBillProceed = false;
                        entityLab.IsOrderRealization = false;
                        entityLab.IsPackage = false;
                        entityLab.IsVoid = false;
                        entityLab.Notes = string.Empty;
                        entityLab.IsItemTypeService = false;
                        entityLab.SRCenterID = string.Empty;
                        entityLab.IsApprove = false;
                        entityLab.IsItemRoom = false;
                        entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                        entityLab.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                        entityLab.Save();
                    }
                }

                grdTransChargesItem.Rebind();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //if (txtExecutionDate.SelectedDate.Value.Date > txtTransactionDate.SelectedDate.Value.Date)
            //{
            //    args.MessageText = string.Format("Execution Date should not be greater than Transaction Date.");
            //    args.IsCancel = true;
            //    return;
            //}

            var entity = new TransCharges();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "TransCharges";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new TransCharges();

            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsValidated ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            using (var trans = new esTransactionScope())
            {
                entity.IsValidated = true;
                entity.ValidatedByUserID = AppSession.UserLogin.UserID;
                entity.ValidatedDateTime = DateTime.Parse(txtExecutionDate.SelectedDate.Value.ToShortDateString() + " " + txtExecutionTime.TextWithLiterals);

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new TransCharges();

                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                entity.IsValidated = false;
                entity.Save();

                trans.Complete();
            }
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            ToolBarMenuReloadLab.Visible = this.IsUserEditAble && !(bool)ViewState["IsValidated"];
            return !(bool)ViewState["IsValidated"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemTransChargesItem(oldVal, newVal);

            if (this.IsUserEditAble)
            {
                ToolBarMenuReloadLab.Visible = (newVal == AppEnum.DataMode.Read) && !(bool)ViewState["IsValidated"];
            }
        }

        private void RefreshCommandItemTransChargesItem(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdTransChargesItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdTransChargesItem.Columns[5].Visible = isVisible;
            grdTransChargesItem.Columns[6].Visible = !isVisible;
            grdTransChargesItem.Columns[8].Visible = isVisible;
            grdTransChargesItem.Columns[9].Visible = !isVisible;
            grdTransChargesItem.Rebind();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransCharges();
            if (parameters.Length > 0)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    entity.LoadByPrimaryKey(Request.QueryString["id"]);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transCharges = (TransCharges)entity;
            txtTransactionNo.Text = transCharges.TransactionNo;
            ViewState["IsValidated"] = transCharges.IsValidated ?? false;
            ViewState["IsVoid"] = transCharges.IsVoid ?? false;

            txtRegistrationNo.Text = transCharges.RegistrationNo;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtAgeInYear.Text = Helper.GetAgeInYear(pat.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInMonth.Text = Helper.GetAgeInMonth(pat.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtAgeInDay.Text = Helper.GetAgeInDay(pat.DateOfBirth ?? (new DateTime()).NowAtSqlServer().Date).ToString();
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                PopulatePatientImage(pat.PatientID);

                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var par = new Paramedic();
                    par.LoadByPrimaryKey(reg.ParamedicID);
                    txtParamedicID.Text = par.ParamedicID;
                    txtParamedicName.Text = par.ParamedicName;
                }
                else
                {
                    txtParamedicID.Text = string.Empty;
                    txtParamedicName.Text = string.Empty;
                }

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID)) txtGuarantorName.Text = guar.GuarantorName;
                else txtGuarantorName.Text = string.Empty;
            }

            if (transCharges.TransactionDate.HasValue)
            {
                txtTransactionDate.SelectedDate = transCharges.TransactionDate.Value.Date;
                txtTransactionTime.Text = transCharges.TransactionDate.Value.ToString("HH:mm");
            }

            if (transCharges.ValidatedDateTime.HasValue)
            {
                txtExecutionDate.SelectedDate = transCharges.ValidatedDateTime.Value.Date;
                txtExecutionTime.Text = transCharges.ValidatedDateTime.Value.ToString("HH:mm");
            }
            else
            {
                txtExecutionDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtExecutionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            }

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(transCharges.FromServiceUnitID);

            txtServiceUnitID.Text = unit.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            txtNotes.Text = transCharges.Notes;

            var room = new ServiceRoom();
            if (room.LoadByPrimaryKey(transCharges.str.RoomID)) txtRoomName.Text = room.RoomName;
            else txtRoomName.Text = string.Empty;

            var c = new Class();
            if (c.LoadByPrimaryKey(transCharges.str.ClassID)) txtClassName.Text = c.ClassName;
            else txtClassName.Text = string.Empty;

            if (!string.IsNullOrEmpty(transCharges.BedID)) txtBedID.Text = transCharges.BedID;
            else txtBedID.Text = string.Empty;

            //if (!string.IsNullOrEmpty(transCharges.LaboratoryParamedicID)) 
            cboParamedicID.SelectedValue = transCharges.LaboratoryParamedicID;
            //else
            //{
            //    var item = cboParamedicID.Items.Where(i => i.Value == reg.ParamedicID).SingleOrDefault();
            //    if (item != null) cboParamedicID.SelectedValue = item.Value;
            //}
        }

        private void SetEntityValue(TransCharges entity)
        {
            entity.Notes = txtNotes.Text;
            entity.LaboratoryParamedicID = cboParamedicID.SelectedValue;

            entity.ValidatedByUserID = AppSession.UserLogin.UserID;
            entity.ValidatedDateTime = DateTime.Parse(txtExecutionDate.SelectedDate.Value.ToShortDateString() + " " + txtExecutionTime.TextWithLiterals);
        }

        private void SaveEntity(TransCharges entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
                {
                    if (!(dataItem["ItemName"].FindControl("txtResult") as RadTextBox).Visible) continue;

                    var tci = new TransChargesItem();
                    tci.LoadByPrimaryKey(txtTransactionNo.Text, dataItem["SequenceNo"].Text);
                    tci.ResultValue = (dataItem["ItemName"].FindControl("txtResult") as RadTextBox).Text;
                    tci.IsDuplo = (dataItem["ItemName"].FindControl("chkIsDuplo") as CheckBox).Checked;
                    tci.IsDescriptionResult = (dataItem["ItemName"].FindControl("chkIsDescription") as CheckBox).Checked;
                    tci.Notes = (dataItem["ItemName"].FindControl("txtNotes") as RadTextBox).Text;
                    tci.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransChargesQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text,
                        que.IsOrder == true
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text,
                        que.IsOrder == true
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new TransCharges();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
        }

        private DataTable TransChargesItems
        {
            get
            {
                var result = new TransChargesItemCollection();
                //if (AppSession.Parameter.HealthcareInitial == "RSSMCB") return result.LaboratoryResultByTransactionNoRSSMCB(txtTransactionNo.Text);
                //else 
                return result.LaboratoryResultByTransactionNo(txtTransactionNo.Text);
            }
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind") grdTransChargesItem.Rebind();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //if (AppSession.Parameter.IsUsingHisInterop.ToLower() == "no") printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnReloadLabClick()
        {
            //if (AppSession.Parameter.IsUsingHisInterop.ToLower() == "no")
            {
                var tci = new TransChargesItemCollection();
                tci.Query.Where(tci.Query.TransactionNo == txtTransactionNo.Text);
                tci.Query.Load();

                //level 1
                foreach (var t in tci.Where(t => string.IsNullOrEmpty(t.ParentNo)))
                {
                    var ilp = new ItemLaboratoryProfileCollection();
                    ilp.Query.Where(ilp.Query.ParentItemID == t.ItemID);
                    ilp.Query.OrderBy(ilp.Query.DisplaySequence.Ascending);
                    if (!ilp.Query.Load()) continue;

                    var level1 = from i in ilp
                                 where !(tci.Where(c => c.ParentNo == t.SequenceNo).Select(c => c.ItemID)).Contains(i.DetailItemID)
                                 orderby i.DisplaySequence ascending
                                 select i;

                    foreach (var i in level1)
                    {
                        var entityLab = tci.AddNew();
                        entityLab.TransactionNo = t.TransactionNo;

                        var sequenceNo = (tci.Where(c => c.ParentNo == t.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                        entityLab.SequenceNo = t.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                        entityLab.ParentNo = t.SequenceNo;
                        entityLab.ReferenceNo = string.Empty;
                        entityLab.ReferenceSequenceNo = string.Empty;
                        entityLab.ItemID = i.DetailItemID;
                        entityLab.ItemName = string.Empty;
                        entityLab.ChargeClassID = string.Empty;
                        entityLab.ParamedicID = txtParamedicID.Text;
                        entityLab.ParamedicName = txtParamedicName.Text;
                        entityLab.IsAdminCalculation = false;
                        entityLab.IsVariable = false;
                        entityLab.IsCito = false;
                        entityLab.ChargeQuantity = 0;
                        entityLab.StockQuantity = 0;
                        entityLab.SRItemUnit = t.SRItemUnit;
                        entityLab.CostPrice = 0;
                        entityLab.Price = 0;
                        entityLab.CitoAmount = 0;
                        entityLab.IsCitoInPercent = false;
                        entityLab.BasicCitoAmount = 0;
                        entityLab.RoundingAmount = 0;
                        entityLab.SRDiscountReason = string.Empty;
                        entityLab.IsAssetUtilization = false;
                        entityLab.AssetID = string.Empty;
                        entityLab.IsBillProceed = false;
                        entityLab.IsOrderRealization = false;
                        entityLab.IsPaymentConfirmed = false;
                        entityLab.IsPackage = false;
                        entityLab.IsVoid = false;
                        entityLab.Notes = string.Empty;
                        entityLab.IsItemTypeService = false;
                        entityLab.SRCenterID = string.Empty;
                        entityLab.IsApprove = false;
                        entityLab.IsItemRoom = false;
                        entityLab.SRCitoPercentage = string.Empty;
                        entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                        entityLab.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }

                //level 2
                foreach (var t in tci.Where(t => !string.IsNullOrEmpty(t.ParentNo)))
                {
                    var ilp = new ItemLaboratoryProfileCollection();
                    ilp.Query.Where(ilp.Query.ParentItemID == t.ItemID);
                    ilp.Query.OrderBy(ilp.Query.DisplaySequence.Ascending);
                    if (!ilp.Query.Load()) continue;

                    var level2 = from i in ilp
                                 where !(tci.Where(c => c.ParentNo == t.SequenceNo).Select(c => c.ItemID)).Contains(i.DetailItemID)
                                 orderby i.DisplaySequence ascending
                                 select i;

                    foreach (var i in level2)
                    {
                        var entityLab = tci.AddNew();
                        entityLab.TransactionNo = t.TransactionNo;

                        var sequenceNo = (tci.Where(c => c.ParentNo == t.SequenceNo).OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo.Substring(c.SequenceNo.Length - 3, 3))).Take(1).SingleOrDefault();
                        entityLab.SequenceNo = t.SequenceNo + string.Format("{0:000}", string.IsNullOrEmpty(sequenceNo) ? 1 : int.Parse(sequenceNo) + 1);

                        entityLab.ParentNo = t.SequenceNo;
                        entityLab.ReferenceNo = string.Empty;
                        entityLab.ReferenceSequenceNo = string.Empty;
                        entityLab.ItemID = i.DetailItemID;
                        entityLab.ItemName = string.Empty;
                        entityLab.ChargeClassID = string.Empty;
                        entityLab.ParamedicID = txtParamedicID.Text;
                        entityLab.ParamedicName = txtParamedicName.Text;
                        entityLab.IsAdminCalculation = false;
                        entityLab.IsVariable = false;
                        entityLab.IsCito = false;
                        entityLab.ChargeQuantity = 0;
                        entityLab.StockQuantity = 0;
                        entityLab.SRItemUnit = t.SRItemUnit;
                        entityLab.CostPrice = 0;
                        entityLab.Price = 0;
                        entityLab.CitoAmount = 0;
                        entityLab.IsCitoInPercent = false;
                        entityLab.BasicCitoAmount = 0;
                        entityLab.RoundingAmount = 0;
                        entityLab.SRDiscountReason = string.Empty;
                        entityLab.IsAssetUtilization = false;
                        entityLab.AssetID = string.Empty;
                        entityLab.IsBillProceed = false;
                        entityLab.IsOrderRealization = false;
                        entityLab.IsPaymentConfirmed = false;
                        entityLab.IsPackage = false;
                        entityLab.IsVoid = false;
                        entityLab.Notes = string.Empty;
                        entityLab.IsItemTypeService = false;
                        entityLab.SRCenterID = string.Empty;
                        entityLab.IsApprove = false;
                        entityLab.IsItemRoom = false;
                        entityLab.SRCitoPercentage = string.Empty;
                        entityLab.CreatedByUserID = AppSession.UserLogin.UserID;
                        entityLab.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }

                tci.Save();

                grdTransChargesItem.Rebind();
            }

            if (AppSession.Parameter.IsUsingHisInterop)
            {
                switch (AppSession.Parameter.HisInteropConfigName)
                {
                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                        grdOnline.Rebind();
                        break;
                }
            }
        }

        protected void grdOnline_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                var dtb = new DataTable();
                dtb.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
                dtb.Columns.Add(new DataColumn("NoUrut", typeof(int)));
                dtb.Columns.Add(new DataColumn("KodePemeriksaan", typeof(string)));
                dtb.Columns.Add(new DataColumn("NamaPemeriksaan", typeof(string)));
                dtb.Columns.Add(new DataColumn("Hasil", typeof(string)));
                dtb.Columns.Add(new DataColumn("StandardValue", typeof(string)));
                dtb.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
                dtb.Columns.Add(new DataColumn("KodeTest", typeof(string)));
                dtb.Columns.Add(new DataColumn("Teks", typeof(string)));
                dtb.Columns.Add(new DataColumn("Unit", typeof(string)));
                dtb.Columns.Add(new DataColumn("Normal", typeof(string)));
                dtb.Columns.Add(new DataColumn("Flag", typeof(string)));
                dtb.Columns.Add(new DataColumn("ValidateDateTime", typeof(DateTime)));
                dtb.Columns.Add(new DataColumn("ValidateBy", typeof(string)));
                dtb.Columns.Add(new DataColumn("OrderTestId", typeof(string)));

                switch (AppSession.Parameter.HisInteropConfigName)
                {
                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                        var vw = new VwHasilPasienVanslabCollection();
                        vw.Query.Where(vw.Query.TransactionNo == txtTransactionNo.Text);
                        vw.Query.OrderBy(vw.Query.NoUrut.Ascending);
                        if (vw.Query.Load())
                        {
                            foreach (var entity in vw)
                            {
                                var vhpv = dtb.NewRow();
                                vhpv["TransactionNo"] = entity.TransactionNo;
                                vhpv["NoUrut"] = entity.NoUrut ?? 0;
                                vhpv["KodePemeriksaan"] = entity.KodePemeriksaan;
                                vhpv["NamaPemeriksaan"] = entity.NamaPemeriksaan;
                                vhpv["Hasil"] = entity.Hasil;
                                vhpv["StandardValue"] = entity.StandardValue;
                                vhpv["RegistrationNo"] = entity.RegistrationNo;
                                vhpv["KodeTest"] = entity.KodeTest;
                                vhpv["Teks"] = string.Empty;
                                vhpv["Unit"] = string.Empty;
                                vhpv["Normal"] = string.Empty;
                                vhpv["Flag"] = entity.Flag;

                                vhpv["ValidateDateTime"] = DBNull.Value;
                                vhpv["ValidateBy"] = string.Empty;
                                vhpv["OrderTestId"] = string.Empty;

                                dtb.Rows.Add(vhpv);
                            }
                        }
                        else
                        {
                            var vhpv = dtb.NewRow();
                            vhpv["TransactionNo"] = string.Empty;
                            vhpv["NoUrut"] = 0;
                            vhpv["KodePemeriksaan"] = string.Empty;
                            vhpv["NamaPemeriksaan"] = string.Format("Record data is not available from {0}", AppSession.Parameter.LisInterop);
                            vhpv["Hasil"] = string.Empty;
                            vhpv["StandardValue"] = string.Empty;
                            vhpv["RegistrationNo"] = string.Empty;
                            vhpv["KodeTest"] = string.Empty;
                            vhpv["Teks"] = string.Empty;
                            vhpv["Unit"] = string.Empty;
                            vhpv["Normal"] = string.Empty;
                            vhpv["Flag"] = string.Empty;

                            vhpv["ValidateDateTime"] = DBNull.Value;
                            vhpv["ValidateBy"] = string.Empty;
                                                            vhpv["OrderTestId"] = string.Empty;

                            dtb.Rows.Add(vhpv);
                        }
                        break;
                    case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                        var sy = new VwHasilPasienCollection();
                        sy.Query.Where(sy.Query.OrderLabNo == txtTransactionNo.Text);
                        sy.Query.OrderBy(sy.Query.DispSeq.Ascending);
                        if (sy.Query.Load())
                        {
                            foreach (var entity in sy)
                            {
                                var vhpv = dtb.NewRow();
                                vhpv["TransactionNo"] = entity.OrderLabNo;
                                vhpv["NoUrut"] = entity.DispSeq.Replace("_", string.Empty).ToInt();
                                vhpv["KodePemeriksaan"] = entity.LabOrderCode;
                                vhpv["NamaPemeriksaan"] = (entity.LabOrderCode == entity.OrderTestid ? string.Empty : "&nbsp;&nbsp;") + entity.LabOrderSummary.Replace(" ", "&nbsp;");
                                vhpv["Hasil"] = entity.Result;
                                vhpv["StandardValue"] = entity.StandarValue;
                                vhpv["RegistrationNo"] = string.Empty;
                                vhpv["KodeTest"] = entity.OrderTestid;
                                vhpv["Teks"] = string.Empty;
                                vhpv["Unit"] = string.Empty;
                                vhpv["Normal"] = string.Empty;
                                if (entity.Flag == "L" || entity.Flag == "LL") vhpv["Flag"] = "LOW";
                                else if (entity.Flag == "H" || entity.Flag == "HH") vhpv["Flag"] = "HIGH";
                                else if (entity.Flag == "N") vhpv["Flag"] = "NORMAL";
                                else vhpv["Flag"] = string.Empty;

                                DateTime dt;
                                if (DateTime.TryParseExact(entity.ValidateOn, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) vhpv["ValidateDateTime"] = dt;
                                else vhpv["ValidateDateTime"] = DBNull.Value;
                                vhpv["ValidateBy"] = entity.ValidateBy;
                                vhpv["OrderTestId"] = entity.OrderTestid;

                                dtb.Rows.Add(vhpv);
                            }
                        }
                        else
                        {
                            var vhpv = dtb.NewRow();
                            vhpv["TransactionNo"] = string.Empty;
                            vhpv["NoUrut"] = 0;
                            vhpv["KodePemeriksaan"] = string.Empty;
                            vhpv["NamaPemeriksaan"] = string.Format("Record data is not available from {0}", AppSession.Parameter.LisInterop);
                            vhpv["Hasil"] = string.Empty;
                            vhpv["StandardValue"] = string.Empty;
                            vhpv["RegistrationNo"] = string.Empty;
                            vhpv["KodeTest"] = string.Empty;
                            vhpv["Teks"] = string.Empty;
                            vhpv["Unit"] = string.Empty;
                            vhpv["Normal"] = string.Empty;
                            vhpv["Flag"] = string.Empty;

                            vhpv["ValidateDateTime"] = DBNull.Value;
                            vhpv["ValidateBy"] = string.Empty;
                            vhpv["OrderTestId"] = string.Empty;

                            dtb.Rows.Add(vhpv);
                        }
                        break;
                }
                grdOnline.DataSource = dtb;
            }
        }

        protected void grdOnline_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                if (!string.IsNullOrEmpty(dataItem["flag"].Text))
                {
                    if (dataItem["flag"].Text == "&nbsp;") return;
                    if (dataItem["flag"].Text == "NORMAL")
                    {
                        dataItem.ForeColor = Color.Blue;
                        dataItem.Font.Bold = true;
                    }
                    else
                    {
                        dataItem.ForeColor = Color.Red;
                        dataItem.Font.Bold = true;
                    }

                    //if (dataItem["flag"].Text != "HIGH")
                    //{
                    //    dataItem.ForeColor = Color.Red;
                    //    dataItem.Font.Bold = true;
                    //}
                    //else if (dataItem["flag"].Text != "LOW")
                    //{
                    //    dataItem.ForeColor = Color.Blue;
                    //    dataItem.Font.Bold = true;
                    //}
                }
                //if (dataItem["KodePemeriksaan"].Text == dataItem["OrderTestId"].Text) dataItem.Font.Bold = true;
            }
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
    }
}
