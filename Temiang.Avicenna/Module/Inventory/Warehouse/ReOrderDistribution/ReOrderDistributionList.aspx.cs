using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ReOrderDistributionList : BasePage
    {
        private AppAutoNumberLast _autoNumber; 
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.ReOrderDistribution;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.Distribution, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.DistributionConfirm, false);
                
                if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboFromServiceUnitID.SelectedValue = Request.QueryString["su"];
                
                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.Distribution);
             
                if (!string.IsNullOrEmpty(Request.QueryString["it"]))
                    cboSRItemType.SelectedValue = Request.QueryString["it"];

                txtDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                cboFromServiceUnitID.Text = string.Empty;
                cboToServiceUnitID.Text = string.Empty;
                cboSRItemType.Text = string.Empty;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListDist.Rebind();
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboFromLocationID, e.Value);
            cboFromLocationID.SelectedIndex = 1;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboFromServiceUnitID.SelectedValue, TransactionCode.Distribution);
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboToLocationID, e.Value);
            cboToLocationID.SelectedIndex = 1;
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        private DataTable ItemBalances()
        {
            var isEmptyFilter = string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboFromLocationID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboItemGroupID.SelectedValue) && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboToLocationID.SelectedValue) && txtDate.IsEmpty;
            if (!ValidateSearch(isEmptyFilter, "Re-Order Distribution")) return null;

            var floc = cboFromLocationID.SelectedValue;
            var tloc = cboToLocationID.SelectedValue;

            var query = new ItemBalanceQuery("a");
            var qfbal = new ItemBalanceQuery("b");
            var qit = new ItemQuery("c");

            query.Select(
                query.ItemID,
                qit.ItemName,
                query.Balance,
                query.Minimum,
                query.Maximum,
                @"<ISNULL(b.Balance, 0) AS BalanceFrom>",
                @"<(a.Maximum - a.Balance) AS RoP>"
                );
            query.InnerJoin(qit).On(query.ItemID == qit.ItemID & qit.IsActive == true &
                                    qit.SRItemType == cboSRItemType.SelectedValue & query.LocationID == tloc);
            query.LeftJoin(qfbal).On(query.ItemID == qfbal.ItemID & qfbal.LocationID == floc);

            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var qipm = new ItemProductMedicQuery("d");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit);
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var qipm = new ItemProductNonMedicQuery("d");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit);
            }
            else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
            {
                var qipm = new ItemKitchenQuery("d");
                query.InnerJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(qipm.SRItemUnit);
            }
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                query.Where(qit.ItemGroupID == cboItemGroupID.SelectedValue);

            query.Where(query.Balance <= query.Minimum, query.Maximum > 0);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemBalances();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdListDist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;

        }

        protected void grdListDist_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();
            
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.SequenceNo,
                    query.IsClosed,
                    iq.ItemName.As("ItemName")
                );

            DataTable dtb = query.LoadDataTable();
            
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboFromLocationID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboItemGroupID.SelectedValue) && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboToLocationID.SelectedValue) && txtDate.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Re-Order Distribution")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var qryserviceunitto = new ServiceUnitQuery("e");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(qusr).On(query.FromServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                       qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.FromServiceUnitID,
                       query.SRItemType,
                       "<'../Distribution/DistributionDetail.aspx?md=view&id=' + a.TransactionNo + '&drn=&rod=1&su=' + a.FromServiceUnitID + '&it=' + a.SRItemType as DoUrl>"
                   );

                query.Where(query.TransactionCode == TransactionCode.Distribution, query.IsBySystem == true);
                query.Where(query.TransactionDate == txtDate.SelectedDate,
                            query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue,
                            query.ToServiceUnitID == cboToServiceUnitID.SelectedValue,
                            query.SRItemType == cboSRItemType.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Descending);
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            pnlInfo.Visible = false;
            bool isValid = true;
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "From Unit required.";
                isValid = false;
            }
            else if (string.IsNullOrEmpty(cboFromLocationID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "From Location required.";
                isValid = false;
            }
            else if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "To Unit required.";
                isValid = false;
            }
            else if (string.IsNullOrEmpty(cboToLocationID.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "To Location required.";
                isValid = false;
            }
            else if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "Item Type required.";
                isValid = false;
            }

            if (isValid)
            {
                if (eventArgument == "generate")
                {
                    var transNo = string.Empty;
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(cboFromServiceUnitID.SelectedValue))
                    {
                        using (var trans = new esTransactionScope())
                        {
                            _autoNumber = Helper.GetNewAutoNumber(txtDate.SelectedDate.Value.Date, TransactionCode.Distribution, su.DepartmentID);
                            
                            var entity = new ItemTransaction();
                            entity.AddNew();

                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                            entity.TransactionCode = TransactionCode.Distribution;
                            entity.TransactionDate = txtDate.SelectedDate;
                            entity.ReferenceNo = string.Empty;
                            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
                            entity.FromLocationID = cboFromLocationID.SelectedValue;
                            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
                            entity.ToLocationID = cboToLocationID.SelectedValue;
                            entity.ServiceUnitCostID = cboToServiceUnitID.SelectedValue;
                            entity.SRItemType = cboSRItemType.SelectedValue;
                            entity.Notes = string.Empty;
                            entity.IsBySystem = true;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;

                            transNo = entity.TransactionNo;

                            var coll = new ItemTransactionItemCollection();
                            int seqNo = 0;

                            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                            {
                                seqNo += 1;

                                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0;

                                var c = coll.AddNew();
                                c.TransactionNo = entity.TransactionNo;
                                c.ItemID = dataItem["ItemID"].Text;
                                c.SequenceNo = string.Format("{0:000}", seqNo);
                                c.Quantity = Convert.ToDecimal(qty);
                                c.ItemName = dataItem["ItemName"].Text;
                                c.SRItemUnit = dataItem["SRItemUnit"].Text;
                                c.ConversionFactor = 1;

                                if (cboSRItemType.SelectedValue == ItemType.Medical)
                                {
                                    var ipm = new ItemProductMedic();
                                    if (ipm.LoadByPrimaryKey(c.ItemID))
                                    {
                                        c.CostPrice = ipm.CostPrice;
                                        c.Price = ipm.PriceInBaseUnit;
                                        c.PriceInCurrency = ipm.PriceInBaseUnit;
                                    }
                                    else
                                    {
                                        c.CostPrice = 0;
                                        c.Price = 0;
                                        c.PriceInCurrency = 0;
                                    }
                                }
                                else
                                {
                                    var ipm = new ItemProductNonMedic();
                                    if (ipm.LoadByPrimaryKey(c.ItemID))
                                    {
                                        c.CostPrice = ipm.CostPrice;
                                        c.Price = ipm.PriceInBaseUnit;
                                        c.PriceInCurrency = ipm.PriceInBaseUnit;
                                    }
                                    else
                                    {
                                        c.CostPrice = 0;
                                        c.Price = 0;
                                        c.PriceInCurrency = 0;
                                    }
                                }

                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                            }

                            _autoNumber.Save();
                            entity.Save();
                            coll.Save();

                            trans.Complete();
                        }
                        
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Generate Distribution Succeed with No. : " + transNo;
                    }
                }

                grdList.Rebind();
                grdListDist.Rebind();
            }
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Text = string.Empty;
            cboItemGroupID.SelectedValue = null;
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ItemGroupID,
                    query.ItemGroupName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                        query.IsActive == true,
                        query.SRItemType == cboSRItemType.SelectedValue
                );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }
    }
}
