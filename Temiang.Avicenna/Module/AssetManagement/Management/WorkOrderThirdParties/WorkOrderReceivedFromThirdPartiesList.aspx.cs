using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderReceivedFromThirdPartiesList : BasePage
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

            ProgramID = AppConstant.Program.AssetWorkOrderReceivedFromThirdParties;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, false);

                txtSentToThirdPartiesDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToSentToThirdPartiesDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetWorkOrders;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable AssetWorkOrders
        {
            get
            {
                var isEmptyFilter = txtOrderDate.IsEmpty && txtToOrderDate.IsEmpty && string.IsNullOrEmpty(txtOrderNo.Text) 
                    && txtSentToThirdPartiesDateTime.IsEmpty && txtToSentToThirdPartiesDateTime.IsEmpty && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) 
                    && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Work Order Received")) return null;

                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");
                var supplier = new SupplierQuery("g");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        tounit.ServiceUnitName.As("ToServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wstatus.ItemName.As("WorkStatus"),
                        asset.AssetName,
                        @"<'' AS 'ImplementedBy'>",
                        query.ProblemDescription,
                        query.ReceivedFromLogisticsDateTime,
                        @"<GETDATE() AS 'ReceivedDate'>",
                        query.SentToThirdPartiesDateTime.As("SentDate"),
                        query.LetterNo,
                        supplier.SupplierName,
                        @"<'Purchase Request' AS 'PrNo'>",
                        "<'../../../../Module/Inventory/Procurement/RequestOrder/RequestOrderDetail.aspx?md=new&id=&wo=' + a.OrderNo AS PrUrl>"
                    );

                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(wtype).On
                    (
                        wtype.ItemID == query.SRWorkType &&
                        wtype.StandardReferenceID == "WorkType"
                    );
                query.InnerJoin(wstatus).On
                    (
                        wstatus.ItemID == query.SRWorkStatus &&
                        wstatus.StandardReferenceID == "WorkStatus"
                    );
                query.LeftJoin(asset).On(asset.AssetID == query.AssetID);
                query.LeftJoin(supplier).On(query.SupplierID == supplier.SupplierID);
                query.Where(query.IsProceed == false, query.SentToThirdPartiesDateTime.IsNotNull(), query.ReceivedFromThirdPartiesDateTime.IsNull(),
                            query.SRWorkStatus == AppSession.Parameter.WorkStatusThirdParties);

                if (!txtOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate.Date().Between(txtOrderDate.SelectedDate, txtToOrderDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!txtSentToThirdPartiesDateTime.IsEmpty && !txtToSentToThirdPartiesDateTime.IsEmpty)
                    query.Where(query.SentToThirdPartiesDateTime.Date().Between(txtSentToThirdPartiesDateTime.SelectedDate, txtToSentToThirdPartiesDateTime.SelectedDate));
                if (!string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitID.SelectedValue);

                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                //foreach (DataRow row in dtb.Rows)
                //{
                //    var awoi = new AssetWorkOrderImplementerQuery("a");
                //    var au = new AppUserQuery("b");
                //    awoi.InnerJoin(au).On(awoi.UserID == au.UserID);
                //    awoi.Where(awoi.OrderNo == row["OrderNo"].ToString());
                //    awoi.Select(au.UserName);
                //    DataTable d = awoi.LoadDataTable();

                //    var implemented = string.Empty;
                //    if (d.Rows.Count > 0)
                //    {
                //        foreach (DataRow r in d.Rows)
                //        {
                //            if (implemented == string.Empty)
                //                implemented = r["UserName"].ToString();
                //            else
                //                implemented += ", " + r["UserName"];
                //        }
                //    }

                //    row["ImplementedBy"] = implemented;
                //}

                //dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "process")
            {
                Validate();
                if (!IsValid)
                    return;

                Process();

                grdList.Rebind();
            }
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                {
                    string orderNo = dataItem.GetDataKeyValue("OrderNo").ToString();
                    DateTime receiveDate = ((RadDatePicker)dataItem.FindControl("txtReceivedDate")).SelectedDate ?? (new DateTime()).NowAtSqlServer();

                    Save(orderNo, receiveDate);
                }
            }
        }

        private void Save(string orderNo, DateTime receiveDate)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var wo = new AssetWorkOrder();
                wo.LoadByPrimaryKey(orderNo);
                wo.ReceivedFromThirdPartiesByUserID = AppSession.UserLogin.UserID;
                wo.ReceivedFromThirdPartiesDateTime = receiveDate;
                wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                wo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                wo.Save();
                trans.Complete();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}
