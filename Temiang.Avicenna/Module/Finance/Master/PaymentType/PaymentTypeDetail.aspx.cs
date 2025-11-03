using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class PaymentTypeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PaymentTypeSearch.aspx";
            UrlPageList = "PaymentTypeList.aspx";

            ProgramID = AppConstant.Program.PAYMENTTYPE;

            //StandardReference Initialize
            if (!IsPostBack)
            {

            }

            //PopUp Search
            if (!IsCallback)
            {
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new PaymentType());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            PaymentType entity = new PaymentType();
            entity.LoadByPrimaryKey(txtSRPaymentTypeID.Text);
            entity.MarkAsDeleted();

            PaymentMethodCollection coll = new PaymentMethodCollection();
            string srPaymentTypeID = txtSRPaymentTypeID.Text;
            coll.Query.Where(coll.Query.SRPaymentTypeID == srPaymentTypeID);
            coll.LoadAll();
            coll.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                coll.Save();
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            PaymentType entity = new PaymentType();
            if (entity.LoadByPrimaryKey(txtSRPaymentTypeID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new PaymentType();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            PaymentType entity = new PaymentType();
            if (entity.LoadByPrimaryKey(txtSRPaymentTypeID.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("SRPaymentTypeID='{0}'", txtSRPaymentTypeID.Text.Trim());
            auditLogFilter.TableName = "PaymentType";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtSRPaymentTypeID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PaymentType entity = new PaymentType();
            if (parameters.Length > 0)
            {
                String srPaymentTypeID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(srPaymentTypeID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtSRPaymentTypeID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var paymentType = (PaymentType)entity;
            txtSRPaymentTypeID.Text = paymentType.SRPaymentTypeID;
            txtPaymentTypeName.Text = paymentType.PaymentTypeName;
            chkIsCashierFO.Checked = paymentType.IsCashierFrontOffice ?? false;
            chkIsApPayment.Checked = paymentType.IsApPayment ?? false;
            chkIsArPayment.Checked = paymentType.IsArPayment ?? false;
            chkIsFeePayment.Checked = paymentType.IsFeePayment ?? false;
            chkIsAssetAuctionPayment.Checked = paymentType.IsAssetAuctionPayment ?? false;

            if (txtSRPaymentTypeID.Text != string.Empty)
            {
                int chartOfAccountId = (paymentType.ChartOfAccountID.HasValue ? paymentType.ChartOfAccountID.Value : 0);
                int subLedgerId = (paymentType.SubledgerID.HasValue ? paymentType.SubledgerID.Value : 0);
                if (chartOfAccountId != 0)
                {
                    ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
                    coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
                    coaQ.Where(coaQ.ChartOfAccountId == chartOfAccountId);
                    DataTable dtbCoa = coaQ.LoadDataTable();
                    cboChartOfAccountId.DataSource = dtbCoa;
                    cboChartOfAccountId.DataBind();
                    cboChartOfAccountId.SelectedValue = chartOfAccountId.ToString();
                    if (subLedgerId != 0)
                    {
                        SubLedgersQuery slQ = new SubLedgersQuery();
                        slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
                        slQ.Where(slQ.SubLedgerId == subLedgerId);
                        DataTable dtbSl = slQ.LoadDataTable();
                        cboSubledgerId.DataSource = dtbSl;
                        cboSubledgerId.DataBind();
                        cboSubledgerId.SelectedValue = subLedgerId.ToString();
                    }
                    else
                    {
                        cboSubledgerId.Items.Clear();
                        cboSubledgerId.Text = string.Empty;
                    }
                }
                else
                {
                    cboChartOfAccountId.Items.Clear();
                    cboSubledgerId.Items.Clear();
                    cboChartOfAccountId.Text = string.Empty;
                    cboSubledgerId.Text = string.Empty;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboSubledgerId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                cboSubledgerId.Text = string.Empty;
            }

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(PaymentType entity)
        {
            foreach (PaymentMethod methode in PaymentMethods)
            {
                methode.SRPaymentTypeID = txtSRPaymentTypeID.Text;
                //Last Update Status
                if (methode.es.IsAdded || methode.es.IsModified)
                {
                    methode.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    methode.LastUpdateDateTime = DateTime.Now;
                }
            }
            entity.SRPaymentTypeID = txtSRPaymentTypeID.Text;
            entity.PaymentTypeName = txtPaymentTypeName.Text;
            int chartOfAccountId = 0;
            int subLedgerId = 0;
            int.TryParse(cboChartOfAccountId.SelectedValue, out chartOfAccountId);
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.ChartOfAccountID = chartOfAccountId;
            entity.SubledgerID = subLedgerId;
            entity.IsCashierFrontOffice = chkIsCashierFO.Checked;
            entity.IsApPayment = chkIsApPayment.Checked;
            entity.IsArPayment = chkIsArPayment.Checked;
            entity.IsFeePayment = chkIsFeePayment.Checked;
            entity.IsAssetAuctionPayment = chkIsAssetAuctionPayment.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(PaymentType entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Save detil
                PaymentMethods.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {

            PaymentTypeQuery que = new PaymentTypeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.SRPaymentTypeID > txtSRPaymentTypeID.Text);
                que.OrderBy(que.SRPaymentTypeID.Ascending);
            }
            else
            {
                que.Where(que.SRPaymentTypeID < txtSRPaymentTypeID.Text);
                que.OrderBy(que.SRPaymentTypeID.Descending);
            }
            PaymentType entity = new PaymentType();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region Record Detail Method Function
        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPaymentTypeMethode.Columns[0].Visible = isVisible;
            
            //grdPaymentTypeMethode.MasterTableView.CommandItemDisplay = isVisible
            //                                                                  ? GridCommandItemDisplay.Top
            //                                                                  : GridCommandItemDisplay.None;
            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                PaymentMethods = null;

            //Perbaharui tampilan dan data
            grdPaymentTypeMethode.Rebind();
        }

        private PaymentMethodCollection PaymentMethods
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPaymentMethod"];
                    if (obj != null)
                    {
                        return ((PaymentMethodCollection)(obj));
                    }
                }

                PaymentMethodCollection coll = new PaymentMethodCollection();
                PaymentMethodQuery query = new PaymentMethodQuery("a");
                ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery("b");
                SubLedgersQuery slQ = new SubLedgersQuery("c");
                query.LeftJoin(coaQ).On(query.ChartOfAccountID == coaQ.ChartOfAccountId);
                query.LeftJoin(slQ).On(query.SubledgerID == slQ.SubLedgerId);
                query.Select
                        (
                        query.SRPaymentTypeID,
                        query.SRPaymentMethodID,
                        query.PaymentMethodName,
                        query.ChartOfAccountID,
                        query.SubledgerID,
                        @"<
                            RTRIM(b.ChartOfAccountCode) + ' - ' + RTRIM(b.ChartOfAccountName) AS refToChartOfAccounts_ChartOfAccountName
                        >",
                        @"<
                            RTRIM(c.SubLedgerName) + ' - ' + RTRIM(c.Description) AS refToSubLedgers_SubLedgerName
                        >",
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                        );
                string srPaymentTypeID = txtSRPaymentTypeID.Text;
                query.Where(query.SRPaymentTypeID == srPaymentTypeID);
                query.OrderBy(query.SRPaymentMethodID.Ascending);
                coll.Load(query);

                Session["collPaymentMethod"] = coll;
                return coll;
            }
            set { Session["collPaymentMethod"] = value; }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            PaymentMethods = null; //Reset Record Detail
            grdPaymentTypeMethode.DataSource = PaymentMethods;
            grdPaymentTypeMethode.DataBind();
        }

        protected void grdPaymentTypeMethode_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPaymentTypeMethode.DataSource = PaymentMethods;
        }

        protected void grdPaymentTypeMethode_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String srPaymentMethodID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PaymentMethodMetadata.ColumnNames.SRPaymentMethodID]);
            PaymentMethod entity = FindPaymentMethod(srPaymentMethodID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        private PaymentMethod FindPaymentMethod(String srPaymentMethodID)
        {
            PaymentMethodCollection coll = PaymentMethods;
            PaymentMethod retEntity = null;
            foreach (PaymentMethod rec in coll)
            {
                if (rec.SRPaymentMethodID.Equals(srPaymentMethodID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(PaymentMethod entity, GridCommandEventArgs e)
        {
            PaymentTypeMethodeDetail userControl = (PaymentTypeMethodeDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRPaymentTypeID = txtSRPaymentTypeID.Text;
                entity.SRPaymentMethodID = userControl.SRPaymentMethodID;
                entity.PaymentMethodName = userControl.PaymentMethodName;
                int chartOfAccountId = 0;
                int subLedgerId = 0;
                int.TryParse(userControl.ChartOfAccountID, out chartOfAccountId);
                int.TryParse(userControl.SubLedgerID, out subLedgerId);
                entity.ChartOfAccountID = chartOfAccountId;
                entity.SubledgerID = subLedgerId;
                entity.ChartOfAccountName = userControl.ChartOfAccountName;
                entity.SubLedgerName = userControl.SubLedgerName;
            }
        }

        #endregion

        #region ComboBox ChartOfAccountId
        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }
        #endregion

        #region ComboBox SubledgerId
        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (cboChartOfAccountId.SelectedValue == string.Empty)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountId.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion
    }
}
