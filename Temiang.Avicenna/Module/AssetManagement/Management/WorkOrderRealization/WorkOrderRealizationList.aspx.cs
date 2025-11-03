using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderRealizationList : BasePage
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

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

            ProgramID = getPageID == ""? AppConstant.Program.AssetWorkOrderRealization: AppConstant.Program.SanitationActivityWorkOrderRealization;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, true);

                if (getPageID == "")
                    StandardReference.InitializeIncludeSpace(cboSearchSRWorkStatus, AppEnum.StandardReference.WorkStatus);
                else
                {
                    string[] exc = (AppSession.Parameter.WorkStatusThirdParties.ToString() + "|" + AppSession.Parameter.WorkStatusWaitingForParts.ToString()).Split('|');
                    StandardReference.InitializeIncludeSpace(cboSearchSRWorkStatus, AppEnum.StandardReference.WorkStatus, exc, false);

                    grdListOutstanding.Columns[7].Visible = false;
                    grdList.Columns[8].Visible = false;
                }

                txtSearchRequiredDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtSearchToRequiredDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtSearchLastRealizationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtSearchToLastRealizationDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
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

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            //Load record
            var query = new AssetWorkOrderItemQuery("a");
            var iq = new ItemQuery("b");

            query.Select(
                query.OrderNo,
                query.SeqNo,
                query.ItemID,
                query.ItemName,
                query.Specification,
                query.SRItemUnit,
                query.Quantity,
                query.QuantityRealization,
                query.ConversionFactor,
                query.CostPrice
            );
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.OrderNo == dataItem.GetDataKeyValue("OrderNo").ToString());
            query.OrderBy(query.SeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable AssetWorkOrders
        {
            get
            {
                var isEmptyFilter = txtFromOrderDateRealization.IsEmpty && txtToOrderDateRealization.IsEmpty && string.IsNullOrEmpty(txtOrderNoRealization.Text) 
                    && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue) 
                    && txtSearchLastRealizationDateTime.IsEmpty && txtSearchToLastRealizationDateTime.IsEmpty && string.IsNullOrEmpty(cboSearchSRWorkStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Work Order Realization")) return null;

                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");
                var user = new AppUserServiceUnitQuery("g");
                var imp = new AssetWorkOrderImplementerQuery("h");

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
                        "<STRING_AGG(h.UserID, ',')  AS UserID>",
                        query.LastRealizationDateTime,
                        query.IsProceed,
                        "<CASE WHEN a.FirstResponseDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsFirstResponse>"
                    );

                if (getPageID == "")
                    query.Select(@"<'WorkOrderRealizationDetail.aspx?md=view&id='+a.OrderNo+'&type=' as WoUrl>");
                else
                    query.Select(@"<'WorkOrderRealizationDetail.aspx?md=view&id='+a.OrderNo+'&type=sa' as WoUrl>");

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
                query.LeftJoin(imp).On(imp.OrderNo == query.OrderNo);
                query.Where(query.IsApproved == true, query.LastRealizationDateTime.IsNotNull());

                if (getPageID == "")
                    query.Where(query.IsSanitation == false);
                else 
                    query.Where(query.IsSanitation == true);

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

                query.GroupBy(
                    "<a.OrderNo>",
                        query.OrderDate,
                        query.RequiredDate,
                        "<b.ServiceUnitName>",
                        "<c.ServiceUnitName>",
                        "<d.ItemName>",
                        "<e.ItemName>",
                        asset.AssetName,
                        query.ProblemDescription,
                        query.LastRealizationDateTime,
                        query.IsProceed,
                        "<CASE WHEN a.FirstResponseDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END >"
                        );
 
                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        private DataTable AssetWorkOrderOutstandings
        {
            get
            {
                var isEmptyFilter = txtFromOrderDate.IsEmpty && txtToOrderDate.IsEmpty && string.IsNullOrEmpty(txtOrderNo.Text) 
                    && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue) 
                    && txtSearchRequiredDate.IsEmpty && txtSearchToRequiredDate.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "Work Order Outstanding")) return null;

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
                        query.IsVoid
                    );

                if (getPageID == "")
                    query.Select(@"<'WorkOrderRealizationDetail.aspx?md=edit&id='+a.OrderNo+'&type=' as WoUrl>");
                else
                    query.Select(@"<'WorkOrderRealizationDetail.aspx?md=edit&id='+a.OrderNo+'&type=sa' as WoUrl>");

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
                query.Where(query.IsApproved == true, query.LastRealizationDateTime.IsNull());

                if (getPageID == "")
                    query.Where(query.IsSanitation == false);
                else
                    query.Where(query.IsSanitation == true);

                if (!txtFromOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate >= txtFromOrderDate.SelectedDate, query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitID.SelectedValue);
                if (!txtSearchRequiredDate.IsEmpty && !txtSearchToRequiredDate.IsEmpty)
                    query.Where(query.RequiredDate >= txtSearchRequiredDate.SelectedDate, query.RequiredDate < txtSearchToRequiredDate.SelectedDate.Value.AddDays(1));

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
                if (param[0] == "received")
                {
                    var awo = new AssetWorkOrder();
                    awo.LoadByPrimaryKey(param[1]);
                    awo.ReceivedDateTime = (new DateTime()).NowAtSqlServer();
                    awo.ReceivedByUserID = AppSession.UserLogin.UserID;

                    awo.LastRealizationByUserID = AppSession.UserLogin.UserID;
                    awo.LastRealizationDateTime = (new DateTime()).NowAtSqlServer();

                    awo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    awo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    awo.Save();

                    grdListOutstanding.Rebind();
                }
                else if (param[0] == "firstresponse")
                {
                    var awo = new AssetWorkOrder();
                    awo.LoadByPrimaryKey(param[1]);
                    awo.FirstResponseDateTime = (new DateTime()).NowAtSqlServer();
                    awo.FirstResponseByUserID = AppSession.UserLogin.UserID;

                    awo.LastRealizationByUserID = AppSession.UserLogin.UserID;
                    awo.LastRealizationDateTime = (new DateTime()).NowAtSqlServer();
                    awo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    awo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    awo.Save();

                    grdList.Rebind();
                }
            }
        }
    }
}
