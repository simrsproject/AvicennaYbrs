using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class WorkOrderPointList : BasePage
    {        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AssetWorkOrderPoint;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, true);
                StandardReference.InitializeIncludeSpace(cboSearchSRWorkStatus, AppEnum.StandardReference.WorkStatus);

                txtSearchLastRealizationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtSearchToLastRealizationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
            }
        }       

        protected void grdListOutstanding_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListOutstanding.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetWorkOrderOutstandings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
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
                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");
                var user = new AppUserServiceUnitQuery("g");
                var wpoint = new AppStandardReferenceItemQuery("h");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        query.RequiredDate,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        tounit.ServiceUnitName.As("ToServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wstatus.ItemName.As("WorkStatus"),
                        asset.AssetName,
                        query.ProblemDescription,
                        query.LastRealizationDateTime,
                        query.IsProceed,
                        query.SRWorkOrderPoint,
                        wpoint.ItemName.As("WorkOrderPointName"),
                        query.WorkOrderPoint,
                        "<CASE WHEN a.FirstResponseDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsFirstResponse>",
                        @"<'WorkOrderPointDetail.aspx?md=view&id='+a.OrderNo as WoUrl>"
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
                query.InnerJoin(user).On(user.ServiceUnitID == query.ToServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                query.InnerJoin(wpoint).On
                    (
                        wpoint.ItemID == query.SRWorkOrderPoint &&
                        wpoint.StandardReferenceID == "WorkOrderPoint"
                    );
                query.Where(query.IsApproved == true, query.IsProceed == true, query.IsSanitation == false, query.SRWorkOrderPoint.IsNotNull());

                if (!txtFromOrderDateRealization.IsEmpty && !txtToOrderDateRealization.IsEmpty)
                    query.Where(query.OrderDate >= txtFromOrderDateRealization.SelectedDate, query.OrderDate < txtToOrderDateRealization.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNoRealization.Text))
                    query.Where(query.OrderNo == txtOrderNoRealization.Text);
                if (!string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitID.SelectedValue);
                if (!txtSearchLastRealizationDateTime.IsEmpty && !txtSearchToLastRealizationDateTime.IsEmpty)
                    query.Where(query.LastRealizationDateTime >= txtSearchLastRealizationDateTime.SelectedDate, query.LastRealizationDateTime < txtSearchToLastRealizationDateTime.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboSearchSRWorkStatus.SelectedValue))
                    query.Where(query.SRWorkStatus == cboSearchSRWorkStatus.SelectedValue);

                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var query = new AssetWorkOrderImplementerQuery("a");
            var usrq = new AppUserQuery("b");

            query.Select(
                query.OrderNo,
                query.UserID,
                usrq.UserName,
                query.Notes
            );
            query.LeftJoin(usrq).On(usrq.UserID == query.UserID);
            query.Where(query.OrderNo == dataItem.GetDataKeyValue("OrderNo").ToString());
            query.OrderBy(usrq.UserName.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable AssetWorkOrderOutstandings
        {
            get
            {
                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");
                var user = new AppUserServiceUnitQuery("g");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        query.RequiredDate,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        tounit.ServiceUnitName.As("ToServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wstatus.ItemName.As("WorkStatus"),
                        asset.AssetName,
                        query.ProblemDescription,
                        query.IsApproved,
                        query.IsVoid,
                        @"<'WorkOrderPointDetail.aspx?md=edit&id='+a.OrderNo as WoUrl>"
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
                query.InnerJoin(user).On(user.ServiceUnitID == query.ToServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                query.Where(query.IsApproved == true, query.IsProceed == true, query.IsSanitation == false, query.SRWorkOrderPoint.IsNull());

                if (!txtFromOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate >= txtFromOrderDate.SelectedDate, query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitID.SelectedValue);

                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                var dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdListOutstanding.Rebind();
            grdList.Rebind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdListOutstanding.Rebind();
        }

    }
}