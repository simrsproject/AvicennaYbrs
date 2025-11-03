using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class DistributionList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.Distribution;

            if (!IsPostBack)
            {
                cboSearchStatus.Items.Add(new RadComboBoxItem("", ""));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Still Open", "2"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Closed", "3"));
                cboSearchStatus.Items.Add(new RadComboBoxItem("Void", "4"));

                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnit, BusinessObject.Reference.TransactionCode.DistributionRequest, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToUnit, BusinessObject.Reference.TransactionCode.Distribution, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromUnitDist, BusinessObject.Reference.TransactionCode.Distribution, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToUnit, BusinessObject.Reference.TransactionCode.DistributionRequest, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemTypeReq);

                string healtcareInitial = AppSession.Parameter.HealthcareInitialAppsVersion;

                if (healtcareInitial == "RSCH")
                {
                    grdList.Columns[4].Visible = true;
                    grdListReq.Columns[4].Visible = true;
                    txtRequestDate.SelectedDate = DateTime.Now;
                    txtFromDate.SelectedDate = DateTime.Now;
                    txtToDate.SelectedDate = DateTime.Now;
                }
                else if (healtcareInitial == "RSSA")
                {
                    txtRequestDate.SelectedDate = DateTime.Now;
                    txtFromDate.SelectedDate = DateTime.Now;
                    txtToDate.SelectedDate = DateTime.Now;
                }
                else if (healtcareInitial == "RSUSKY")
                {
                    txtRequestDate.SelectedDate = DateTime.Now;
                    txtFromDate.SelectedDate = DateTime.Now;
                    txtToDate.SelectedDate = DateTime.Now;
                }
                else
                {
                    txtFromDate.SelectedDate = DateTime.Today.AddDays(0);
                    txtToDate.SelectedDate = DateTime.Today;
                }

                // menu new depends on user access
                
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //User Access
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                RadToolBar2.Items[0].Enabled = access.IsAddAble;
            }
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
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

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();
            //Load record
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
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtDistributionNo.Text) && string.IsNullOrEmpty(txtDistRequestNo.Text) && string.IsNullOrEmpty(cboSearchFromUnitDist.SelectedValue) && string.IsNullOrEmpty(cboSearchToUnit.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue) && string.IsNullOrEmpty(cboSearchStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Distribution")) return null;


                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var qryserviceunitto = new ServiceUnitQuery("e");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");
                var qcostunit = new ServiceUnitQuery("f");
                var locreq = new LocationQuery("g");
                var locTo = new LocationQuery("h");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qryserviceunitto).On(qryserviceunitto.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(locreq).On(locreq.LocationID == query.FromLocationID);
                query.InnerJoin(locTo).On(locTo.LocationID == query.ToLocationID);
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.Distribution);
                query.InnerJoin(qusr).On(query.FromServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(qcostunit).On(query.ServiceUnitCostID == qcostunit.ServiceUnitID);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                       locreq.LocationName.As("FLocationID"),
                       qryserviceunitto.ServiceUnitName.As("TServiceUnitID"),
                       locTo.LocationName.As("TLocationID"),
                       qcostunit.ServiceUnitName.As("CostForServiceUnit"),
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       "<'DistributionDetail.aspx?md=view&id=' + a.TransactionNo + '&drn=&rod=' as DoUrl>",
                       query.IsClosed,
                       query.LastUpdateByUserID
                   ) ;

                if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                    query.Where(query.TransactionDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
                if (!string.IsNullOrEmpty(cboSearchToUnit.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToUnit.SelectedValue);
                if (cboSearchStatus.SelectedValue == "0")
                    query.Where(query.IsApproved == false);
                if (cboSearchStatus.SelectedValue == "1")
                    query.Where(query.IsApproved == true);
                if (cboSearchStatus.SelectedValue == "2")
                    query.Where(query.IsClosed == false);
                if (cboSearchStatus.SelectedValue == "3")
                    query.Where(query.IsClosed == true);
                if (cboSearchStatus.SelectedValue == "4")
                    query.Where(query.IsVoid == true);
                if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemType.SelectedValue);
                if (!string.IsNullOrEmpty(txtDistributionNo.Text))
                    query.Where(query.TransactionNo == txtDistributionNo.Text);
                if (!string.IsNullOrEmpty(cboSearchFromUnitDist.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromUnitDist.SelectedValue);
                if (!string.IsNullOrEmpty(txtDistRequestNo.Text))
                    query.Where(query.ReferenceNo == txtDistRequestNo.Text);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdListReq_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = DistributionRequestPendings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        protected void grdListReq_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = DistributionRequestItemPendings(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        private DataTable DistributionRequestPendings
        {
            get
            {
                var isEmptyFilter = txtRequestDate.IsEmpty && string.IsNullOrEmpty(txtRequestNo.Text) && string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue) && string.IsNullOrEmpty(cboToUnit.SelectedValue) && string.IsNullOrEmpty(cboSRItemTypeReq.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Distribution")) return null;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qrItem = new ItemTransactionItemQuery("d");
                var qusr = new AppUserServiceUnitQuery("u");
                var qtounit = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("e");
                var qcostunit = new ServiceUnitQuery("f");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);
                query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);
                query.InnerJoin(qtounit).On(query.ToServiceUnitID == qtounit.ServiceUnitID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.LeftJoin(qcostunit).On(query.ServiceUnitCostID == qcostunit.ServiceUnitID);

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        query.FromServiceUnitID,
                        qryserviceunit.ServiceUnitName.As("FromServiceUnit"),
                        qtounit.ServiceUnitName.As("ToServiceUnit"),
                        qcostunit.ServiceUnitName.As("CostForServiceUnit"),
                        itemtype.ItemName.As("ItemType"),
                        query.Notes,
                        "<'DistributionDetail.aspx?md=new&id=&drn=' + a.TransactionNo as DoUrl>",
                        query.ApprovedByUserID,
                        query.ApprovedDate
                    );

                query.Where
                    (
                        query.TransactionCode == BusinessObject.Reference.TransactionCode.DistributionRequest,
                        query.IsApproved == true,
                        qrItem.IsClosed == false
                    );

                var isFilter = false;
                if (!txtRequestDate.IsEmpty)
                {
                    query.Where(query.TransactionDate == txtRequestDate.SelectedDate);
                    isFilter = true;
                }
                //else
                //{
                //    query.Where(query.TransactionDate >= DateTime.Today.AddDays(-15).Date, query.TransactionDate <= DateTime.Today.Date);
                //}
                if (!string.IsNullOrEmpty(cboSearchFromUnit.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromUnit.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRItemTypeReq.SelectedValue))
                    query.Where(query.SRItemType == cboSRItemTypeReq.SelectedValue);
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    query.Where(query.TransactionNo == txtRequestNo.Text);
                    isFilter = true;
                }
                if (!string.IsNullOrEmpty(cboToUnit.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboToUnit.SelectedValue);

                if (!isFilter)
                {
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    //query.Where(query.TransactionDate >= DateTime.Today.AddDays(-15).Date, query.TransactionDate <= DateTime.Today.Date);
                }

                query.GroupBy(query.TransactionNo,
                        query.TransactionDate,
                        query.FromServiceUnitID,
                        qryserviceunit.ServiceUnitName,
                        qtounit.ServiceUnitName,
                        qcostunit.ServiceUnitName,
                        itemtype.ItemName,
                        query.Notes,
                        query.ApprovedByUserID,
                        query.ApprovedDate);

                query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
                //query.es.Distinct = true;
                //query.es.Top = AppSession.Parameter.MaxResultRecord;

                var dtb = query.LoadDataTable();

                //foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => DistributionRequestItemPendings(row["TransactionNo"].ToString()).Rows.Count == 0))
                //{
                //    row.Delete();
                //}

                return dtb;
            }
        }

        private DataTable DistributionRequestItemPendings(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var hq = new ItemTransactionQuery("b");
            var bal = new ItemBalanceQuery("c");
            var su = new ServiceUnitQuery("d");
            var iq = new ItemQuery("e");

            query.InnerJoin(hq).On(query.TransactionNo == hq.TransactionNo);
            query.InnerJoin(su).On(hq.FromServiceUnitID == su.ServiceUnitID);
            query.LeftJoin(bal).On(query.ItemID == bal.ItemID && hq.FromLocationID == bal.LocationID);
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);

            query.Where
                (
                query.TransactionNo == transactionNo,
                query.IsClosed == false);
            query.OrderBy
                (
                    query.ItemID.Ascending
                );

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    hq.FromServiceUnitID,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.QuantityFinishInBaseUnit,
                    ((query.Quantity * query.ConversionFactor) - query.QuantityFinishInBaseUnit).As("QtyInput"),
                    iq.ItemName,
                    hq.SRItemType,
                    query.CostPrice,
                    query.ConversionFactor,
                    @"<ISNULL(c.Balance, 0) AS 'Balance'>",
                    @"<ISNULL(c.Minimum, 0) AS 'Minimum'>",
                    @"<ISNULL(c.Maximum, 0) AS 'Maximum'>"
                );
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void btnFilterDO_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void btnFilterDR_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListReq.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "new")
            {
                string url = string.Format("DistributionDetail.aspx?md={0}&drn=&rod=", eventArgument);
                Page.Response.Redirect(url, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}
