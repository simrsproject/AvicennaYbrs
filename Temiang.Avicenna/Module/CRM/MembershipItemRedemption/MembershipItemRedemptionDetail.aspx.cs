using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using DevExpress.XtraPrinting.BarCode.Native;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipItemRedemptionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "MembershipItemRedemptionSearch.aspx";
            UrlPageList = "MembershipItemRedemptionList.aspx";

            ProgramID = AppConstant.Program.MembershipItemRedemption;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRMembershipType, AppEnum.StandardReference.MembershipType);
                trSRMembershipType.Visible = false;
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];

            if (IsPostBack) return;
            if (DataModeCurrent != AppEnum.DataMode.Edit) return;
            txtTransactionDate.Enabled = false;
            cboMembershipNo.Enabled = false;
        }

        private void PopulatePatientInformation(string patientId, bool isMember)
        {
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                if (isMember)
                {
                    var std = new AppStandardReferenceItem();
                    txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                    txtPatientName.Text = patient.PatientName;
                    txtGender.Text = patient.Sex;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
                    txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
                    txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
                    txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);
                    txtAddress.Text = patient.Address;
                    txtPhoneNo.Text = patient.PhoneNo;
                    txtMobilePhone.Text = patient.MobilePhoneNo;
                    txtEmail.Text = patient.Email;
                }
                else {
                    txtRedeemedByAddress.Text = patient.Address;
                    txtRedeemedByPhoneNo.Text = patient.PhoneNo;
                    txtRedeemedByMobilePhoneNo.Text = patient.MobilePhoneNo;
                }
            }
            else
            {
                if (isMember)
                {
                    txtSalutation.Text = string.Empty;
                    txtPatientName.Text = string.Empty;
                    txtGender.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    txtAgeDay.Value = 0;
                    txtAgeMonth.Value = 0;
                    txtAgeYear.Value = 0;
                    txtAddress.Text = string.Empty;
                    txtPhoneNo.Text = string.Empty;
                    txtMobilePhone.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                }
                else
                {
                    txtRedeemedByAddress.Text = string.Empty;
                    txtRedeemedByPhoneNo.Text = string.Empty;
                    txtRedeemedByMobilePhoneNo.Text = string.Empty;
                }
            }
        }

        private void PopulateMembershipInformation(string membershipNo)
        {
            var membership = new Membership();
            if (membership.LoadByPrimaryKey(membershipNo))
            {
                cboSRMembershipType.SelectedValue = membership.SRMembershipType;
                txtJoinDate.SelectedDate = membership.JoinDate;
                PopulatePatientInformation(membership.PatientID, true);
                PopulateBalanceInformation(membershipNo);
            }
            else
            {
                cboSRMembershipType.SelectedValue = string.Empty;
                txtJoinDate.SelectedDate = DateTime.Now;
                PopulatePatientInformation(string.Empty, true);
                txtBalance.Value = 0;
            }
        }

        private void PopulateBalanceInformation(string membershipNo)
        {
            var bals = new MembershipDetailCollection();
            bals.Query.Where(bals.Query.MembershipNo == membershipNo, bals.Query.IsClosed == false);
            bals.LoadAll();

            decimal balance = 0;

            foreach (var b in bals)
            {
                balance += ((b.RewardPoint ?? 0) - (b.ClaimedPoint ?? 0));
            }

            txtBalance.Value = Convert.ToDouble(balance);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboMembershipNo, cboMembershipNo);
            ajax.AddAjaxSetting(cboMembershipNo, grdListDetail);
            //ajax.AddAjaxSetting(cboMembershipNo, cboSRMembershipType);
            ajax.AddAjaxSetting(cboMembershipNo, txtJoinDate);
            ajax.AddAjaxSetting(cboMembershipNo, txtSalutation);
            ajax.AddAjaxSetting(cboMembershipNo, txtPatientName);
            ajax.AddAjaxSetting(cboMembershipNo, txtGender);
            ajax.AddAjaxSetting(cboMembershipNo, txtPlaceDOB);
            ajax.AddAjaxSetting(cboMembershipNo, txtAgeYear);
            ajax.AddAjaxSetting(cboMembershipNo, txtAgeMonth);
            ajax.AddAjaxSetting(cboMembershipNo, txtAgeDay);
            ajax.AddAjaxSetting(cboMembershipNo, txtAddress);
            ajax.AddAjaxSetting(cboMembershipNo, txtPhoneNo);
            ajax.AddAjaxSetting(cboMembershipNo, txtMobilePhone);
            ajax.AddAjaxSetting(cboMembershipNo, txtEmail);


            ajax.AddAjaxSetting(cboMembershipNo, cboPatientID);
            ajax.AddAjaxSetting(cboMembershipNo, txtRedeemedByAddress);
            ajax.AddAjaxSetting(cboMembershipNo, txtRedeemedByPhoneNo);
            ajax.AddAjaxSetting(cboMembershipNo, txtRedeemedByMobilePhoneNo);
            ajax.AddAjaxSetting(cboMembershipNo, txtBalance);

            ajax.AddAjaxSetting(cboPatientID, cboPatientID);
            ajax.AddAjaxSetting(cboPatientID, txtRedeemedByAddress);
            ajax.AddAjaxSetting(cboPatientID, txtRedeemedByPhoneNo);
            ajax.AddAjaxSetting(cboPatientID, txtRedeemedByMobilePhoneNo);

            ajax.AddAjaxSetting(grdListItem, grdListItem);

            ajax.AddAjaxSetting(grdListDetail, grdListDetail);
            ajax.AddAjaxSetting(grdListDetail, txtBalance);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MembershipItemRedemption();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (MembershipItemRedemptionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new MembershipItemRedemption();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                decimal totalPointsUsed = 0;
                foreach (var item in MembershipItemRedemptionItems)
                {
                    totalPointsUsed += (item.TotalPointsUsed ?? 0);
                }

                var entitydet = new MembershipItemRedemptionDetailCollection();

                var coll = new MembershipDetailCollection();
                var query = new MembershipDetailQuery();
                query.Where(query.MembershipNo == entity.MembershipNo, query.RewardPoint > query.ClaimedPoint, query.IsClosed == false);
                query.OrderBy(coll.Query.StartDate.Ascending);
                coll.Load(query);
                
                foreach (var c in coll) 
                {
                    if (totalPointsUsed == 0) break;

                    decimal deduction = 0;
                    var saldo = (c.RewardPoint ?? 0) - (c.ClaimedPoint ?? 0);
                    if (saldo <= totalPointsUsed)
                        deduction = saldo;
                    else deduction = totalPointsUsed;

                    c.ClaimedPoint += deduction;

                    var det = entitydet.AddNew();
                    det.TransactionNo = entity.TransactionNo;
                    det.MembershipDetailID = c.MembershipDetailID;
                    det.ClaimedPoint = deduction;
                    det.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    det.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    totalPointsUsed -= deduction;
                }

                using (var trans = new esTransactionScope())
                {
                    entity.Save();
                    entitydet.Save();
                    coll.Save();

                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new MembershipItemRedemption();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;

                entity.IsVoid = true;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                entity.VoidByUserID = AppSession.UserLogin.UserID;

                entity.Save();
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(MembershipItemRedemption entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MembershipItemRedemption());
            PopulateMembershipInformation(cboMembershipNo.SelectedValue);
            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new MembershipItemRedemption();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MembershipItemRedemption();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.TableName = "MembershipItemRedemption";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
        }

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MembershipItemRedemption();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pm = (MembershipItemRedemption)entity;

            txtTransactionNo.Text = pm.TransactionNo;
            txtTransactionDate.SelectedDate = pm.TransactionDate ?? (new DateTime()).NowAtSqlServer();
            if (!string.IsNullOrEmpty(pm.MembershipNo))
            {
                var mem = new MembershipQuery("a");
                var pat = new PatientQuery("aa");
                mem.InnerJoin(pat).On(pat.PatientID == mem.PatientID);
                mem.Where(mem.MembershipNo == pm.MembershipNo);
                mem.Select(mem.MembershipNo, mem.JoinDate, pat.PatientName, pat.Address);
                cboMembershipNo.DataSource = mem.LoadDataTable();
                cboMembershipNo.DataBind();
                cboMembershipNo.SelectedValue = pm.MembershipNo;

                PopulateMembershipInformation(pm.MembershipNo);
            }
            else {
                cboMembershipNo.Items.Clear();
                cboMembershipNo.Text = string.Empty;
                cboMembershipNo.SelectedValue = string.Empty;

                PopulateMembershipInformation(string.Empty);
            }
            
            if (!string.IsNullOrEmpty(pm.str.PatientID))
            {
                var query = new PatientQuery("a");
                query.Where(query.PatientID == pm.PatientID);
                query.Select(query.PatientID, query.PatientName, query.DateOfBirth, query.Address);
                cboPatientID.DataSource = query.LoadDataTable();
                cboPatientID.DataBind();
                cboPatientID.SelectedValue = pm.PatientID;

                PopulatePatientInformation(pm.PatientID, false);
            }
            else
            {
                cboPatientID.Items.Clear();
                cboPatientID.Text = string.Empty;

                PopulatePatientInformation(string.Empty, false);
            }


            ViewState["IsApproved"] = pm.IsApproved ?? false;
            ViewState["IsVoid"] = pm.IsVoid ?? false;

            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MembershipItemRedemption entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.MembershipNo = cboMembershipNo.SelectedValue;
            entity.PatientID = cboPatientID.SelectedValue;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            foreach (var item in MembershipItemRedemptionItems)
            {
                item.TransactionNo = entity.TransactionNo;
            }
        }

        private void SaveEntity(MembershipItemRedemption entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                MembershipItemRedemptionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MembershipItemRedemptionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new MembershipItemRedemption();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdListItem.Columns[0].Visible = isVisible;
            grdListItem.Columns[grdListItem.Columns.Count - 1].Visible = isVisible;

            grdListDetail.Columns[grdListDetail.Columns.Count - 1].Visible = isVisible;

            grdListItem.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
            {
                MembershipItemRedemptionItems = null;
            }

            //Perbaharui tampilan dan data
            grdListItem.Rebind();
            grdListDetail.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            MembershipItemRedemptionItems = null; //Reset Record Detail
            grdListItem.DataSource = MembershipItemRedemptionItems;
            grdListItem.DataBind();

            grdListDetail.Rebind();
        }

        private DataTable MembershipDetails()
        {
            var query = new MembershipDetailQuery("a");
            
            query.Select(
                query.MembershipDetailID, 
                query.StartDate, 
                query.EndDate, 
                query.BalanceAmount,
                query.RewardPoint, 
                query.ClaimedPoint,
                @"<(a.RewardPoint-a.ClaimedPoint) AS Balance>");

            query.Where(query.MembershipNo == cboMembershipNo.SelectedValue, query.IsClosed == false);
            query.OrderBy(query.StartDate.Ascending);

            return query.LoadDataTable();
        }

        protected void grdListDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListDetail.DataSource = MembershipDetails();
        }

        private MembershipItemRedemptionItemCollection MembershipItemRedemptionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMembershipItemRedemptionItem"];
                    if (obj != null)
                        return ((MembershipItemRedemptionItemCollection)(obj));
                }

                var coll = new MembershipItemRedemptionItemCollection();
                var query = new MembershipItemRedemptionItemQuery("a");
                var itm = new MembershipItemRedeemQuery("b");
                var grp = new AppStandardReferenceItemQuery("c");
                query.InnerJoin(itm).On(itm.ItemReedemID == query.ItemReedemID);
                query.InnerJoin(grp).On(grp.StandardReferenceID == AppEnum.StandardReference.ItemReedemGroup && grp.ItemID == itm.SRItemReedemGroup);

                query.Select(query,
                    itm.ItemReedemName.As("refToItemRedeem_ItemReedemName"),
                    grp.ItemName.As("refToItemRedeem_ItemReedemGroup"));

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ItemReedemID.Ascending);
                coll.Load(query);

                Session["collMembershipItemRedemptionItem"] = coll;
                return coll;
            }
            set
            {
                Session["collMembershipItemRedemptionItem"] = value;
            }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListItem.DataSource = MembershipItemRedemptionItems;
        }

        protected void grdListItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID]);
            MembershipItemRedemptionItem entity = FindMembershipItemRedemptionItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdListItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID]);
            MembershipItemRedemptionItem entity = FindMembershipItemRedemptionItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdListItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            MembershipItemRedemptionItem entity = MembershipItemRedemptionItems.AddNew();
            SetEntityValue(entity, e);
        }

        private MembershipItemRedemptionItem FindMembershipItemRedemptionItem(String id)
        {
            MembershipItemRedemptionItemCollection coll = MembershipItemRedemptionItems;
            MembershipItemRedemptionItem retEntity = null;
            foreach (MembershipItemRedemptionItem rec in coll)
            {
                if (rec.ItemReedemID.ToString().Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(MembershipItemRedemptionItem entity, GridCommandEventArgs e)
        {
            var userControl = (MembershipItemRedemptionDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemReedemID = userControl.ItemReedemID;
                entity.ItemReedemName = userControl.ItemReedemName;
                entity.ItemReedemGroup = userControl.ItemReedemGroup;
                entity.Qty = userControl.Qty;
                entity.PointsUsed = userControl.PointsUsed;
                entity.TotalPointsUsed = userControl.TotalPointsUsed;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }
        #endregion

        #region Combobox
        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var membershipNo = string.IsNullOrEmpty(cboMembershipNo.SelectedValue) ? string.Empty : cboMembershipNo.SelectedValue;
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MembershipQuery("a");
            var pat = new PatientQuery("b");
            query.InnerJoin(pat).On(pat.PatientID == query.PatientID);
            query.Select(query.PatientID, pat.PatientName, pat.DateOfBirth, pat.Address);
            query.Where(
                query.MembershipNo == membershipNo, 
                query.Or(
                    pat.FirstName.Like(searchTextContain),
                    pat.MiddleName.Like(searchTextContain),
                    pat.LastName.Like(searchTextContain)
                    )
                );
            DataTable dtb = query.LoadDataTable();

            var query2 = new MembershipMemberQuery("a");
            pat = new PatientQuery("b");
            query2.InnerJoin(pat).On(pat.PatientID == query2.PatientID);
            query2.Select(query2.PatientID, pat.PatientName, pat.DateOfBirth, pat.Address);
            query2.Where(
                query2.MembershipNo == membershipNo,
                query2.IsActive == true,
                query2.Or(
                    pat.FirstName.Like(searchTextContain),
                    pat.MiddleName.Like(searchTextContain),
                    pat.LastName.Like(searchTextContain)
                    )
                );

            DataTable dtb2 = query2.LoadDataTable();

            dtb.Merge(dtb2);

            cboPatientID.DataSource = dtb;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulatePatientInformation(e.Value, false);
            }
            else
            {
                PopulatePatientInformation(string.Empty, false);
            }
        }

        protected void cboMembershipNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var mem = new MembershipQuery("a");
            var pat = new PatientQuery("aa");
            var memdet = new MembershipMemberQuery("b");
            var patdet = new PatientQuery("bb");
            mem.InnerJoin(pat).On(pat.PatientID == mem.PatientID);
            mem.LeftJoin(memdet).On(memdet.MembershipNo == mem.MembershipNo);
            mem.LeftJoin(patdet).On(patdet.PatientID == memdet.PatientID);

            mem.es.Top = 10;
            mem.Where(
                mem.SRMembershipType == "01",
                mem.IsActive == true,
                mem.Or(
                    mem.MembershipNo.Like(searchTextContain), 
                    pat.FirstName.Like(searchTextContain),
                    pat.MiddleName.Like(searchTextContain),
                    pat.LastName.Like(searchTextContain),
                    
                    patdet.FirstName.Like(searchTextContain),
                    patdet.MiddleName.Like(searchTextContain),
                    patdet.LastName.Like(searchTextContain)
                ));

            mem.Select(mem.MembershipNo, mem.JoinDate, pat.PatientName, pat.Address);

            cboMembershipNo.DataSource = mem.LoadDataTable();
            cboMembershipNo.DataBind();
        }

        protected void cboMembershipNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MembershipNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MembershipNo"].ToString();
        }

        protected void cboMembershipNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateMembershipInformation(e.Value);
            }
            else
            {
                PopulateMembershipInformation(string.Empty);
            }

            grdListDetail.Rebind();
        }
        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                if (param[0].ToString() == "calculation")
                {
                    var id = Convert.ToInt64(param[1]);

                    var div = AppSession.Parameter.MultipleForRewardPoints ;
                    var x = BusinessObject.MembershipDetail.UpdateRewardPointsBalanceAmount(id, div, AppSession.UserLogin.UserID);
                }
                grdListDetail.Rebind();
                PopulateBalanceInformation(cboMembershipNo.SelectedValue);
            }
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.ItemRedemptionNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}