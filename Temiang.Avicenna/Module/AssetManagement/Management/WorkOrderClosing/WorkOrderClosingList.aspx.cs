using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderClosingList : BasePage
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

            ProgramID = getPageID == "" ? AppConstant.Program.AssetWorkOrderClosing : AppConstant.Program.SanitationActivityWorkOrderClosing;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, !this.IsUserCrossUnitAble);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, this.IsUserCrossUnitAble);

                txtFromLastRealizationDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToLastRealizationDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                if (getPageID != "")
                    grdList.Columns[10].Visible = false;
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
                var isEmptyFilter = txtFromOrderDate.IsEmpty && txtToOrderDate.IsEmpty && string.IsNullOrEmpty(txtOrderNo.Text) && txtFromLastRealizationDate.IsEmpty 
                    && txtToLastRealizationDate.IsEmpty && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Asset Work Order")) return null;

                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        fromunit.ServiceUnitName.As("FromServiceUnit"),
                        tounit.ServiceUnitName.As("ToServiceUnit"),
                        wtype.ItemName.As("WorkType"),
                        wstatus.ItemName.As("WorkStatus"),
                        asset.AssetName,
                        @"<'' AS ImplementedBy>",
                        query.ProblemDescription,
                        query.LastRealizationDateTime,
                        query.AcceptedBy
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

                if (this.IsUserCrossUnitAble)
                {
                    var user = new AppUserServiceUnitQuery("g");
                    query.InnerJoin(user).On(user.ServiceUnitID == query.ToServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                }
                else
                {
                    var user = new AppUserServiceUnitQuery("g");
                    query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                            query.IsProceed == true, query.AcceptedDateTime.IsNull(),
                            query.SRWorkStatus == AppSession.Parameter.WorkStatusDone
                            );
                if (getPageID == "")
                    query.Where(query.IsSanitation == false);
                else
                    query.Where(query.IsSanitation == true);

                if (!txtFromOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate >= txtFromOrderDate.SelectedDate,
                                query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!txtFromLastRealizationDate.IsEmpty && !txtToLastRealizationDate.IsEmpty)
                    query.Where(query.LastRealizationDateTime >= txtFromLastRealizationDate.SelectedDate,
                                query.LastRealizationDateTime < txtToLastRealizationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboSearchFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboSearchToServiceUnitID.SelectedValue);

                query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var awoi = new AssetWorkOrderImplementerQuery("a");
                    var au = new AppUserQuery("b");
                    awoi.InnerJoin(au).On(awoi.UserID == au.UserID);
                    awoi.Where(awoi.OrderNo == row["OrderNo"].ToString());
                    awoi.Select(au.UserName);
                    DataTable d = awoi.LoadDataTable();

                    var implemented = string.Empty;
                    if (d.Rows.Count > 0)
                    {
                        foreach (DataRow r in d.Rows)
                        {
                            if (implemented == string.Empty)
                                implemented = r["UserName"].ToString();
                            else
                                implemented += ", " + r["UserName"];
                        }
                    }

                    row["ImplementedBy"] = implemented;
                }

                dtb.AcceptChanges();

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

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;
            if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
            else if (eventArgument == "closed")
            {
                pnlInfo.Visible = false;

                Validate();
                if (!IsValid)
                    return;

                var msg = Closed();

                if (!string.IsNullOrEmpty(msg))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }

                grdList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                using (var trans = new esTransactionScope())
                {
                    var wo = new AssetWorkOrder();
                    wo.LoadByPrimaryKey(param[1]);
                    wo.SRWorkStatus = AppSession.Parameter.WorkStatusClosed;
                    wo.AcceptedByUserID = AppSession.UserLogin.UserID;
                    wo.AcceptedDateTime = DateTime.Now;
                    wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    wo.LastUpdateDateTime = DateTime.Now;

                    wo.Save();
                    trans.Complete();
                }

                grdList.Rebind();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        private string Closed()
        {
            var msg = string.Empty;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("detailChkbox") as CheckBox).Checked)
                {
                    string orderNo = dataItem.GetDataKeyValue("OrderNo").ToString();
                    string acceptedBy = ((RadTextBox)dataItem.FindControl("txtAcceptedBy")).Text ?? string.Empty;

                    if (string.IsNullOrEmpty(acceptedBy))
                    {
                        if (msg == string.Empty)
                        {
                            msg = "Accepted By is required for " + orderNo;
                        }
                        else
                        {
                            msg += ", " + "Accepted By is required for " + orderNo;
                        }
                    }
                    else if (acceptedBy.Length < 3)
                    {
                        if (msg == string.Empty)
                        {
                            msg = "Accepted By must be more than or equal 3 characters long for " + orderNo;
                        }
                        else
                        {
                            msg += ", " + "Accepted By must be more than or equal 3 characters long for " + orderNo;
                        }
                    }
                    else
                    {
                        var wo = new AssetWorkOrder();
                        wo.LoadByPrimaryKey(orderNo);
                        wo.SRWorkStatus = AppSession.Parameter.WorkStatusClosed;
                        wo.AcceptedByUserID = AppSession.UserLogin.UserID;
                        wo.AcceptedDateTime = (new DateTime()).NowAtSqlServer();
                        wo.AcceptedBy = acceptedBy;
                        wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        wo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        wo.Save();
                    }
                }
            }
            return msg;
        }
    }
}
