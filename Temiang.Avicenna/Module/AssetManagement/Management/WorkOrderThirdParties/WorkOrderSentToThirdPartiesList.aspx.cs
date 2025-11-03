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
    public partial class WorkOrderSentToThirdPartiesList : BasePage
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

            ProgramID = AppConstant.Program.AssetWorkOrderSentToThirdParties;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboSearchToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, false);

                txtFromReceivedFromLogisticsDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToReceivedFromLogisticsDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtSentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToSentDate.SelectedDate = (new DateTime()).NowAtSqlServer();
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

        protected void grdListSend_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdListSend.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = AssetWorkOrderSends;
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
                    && txtFromReceivedFromLogisticsDateTime.IsEmpty && txtToReceivedFromLogisticsDateTime.IsEmpty 
                    && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Work Order Sent")) return null;

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
                        @"<'' AS 'ImplementedBy'>",
                        query.ProblemDescription,
                        query.ReceivedFromLogisticsDateTime,
                        @"<GETDATE() AS 'SentDate'>",
                        query.LetterNo
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
                query.Where(query.IsProceed == false, query.SentToThirdPartiesDateTime.IsNull(),
                            query.SRWorkStatus == AppSession.Parameter.WorkStatusThirdParties);

                if (!txtOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate >= txtOrderDate.SelectedDate, query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!txtFromReceivedFromLogisticsDateTime.IsEmpty && !txtToReceivedFromLogisticsDateTime.IsEmpty)
                    query.Where(query.ReceivedFromLogisticsDateTime >= txtFromReceivedFromLogisticsDateTime.SelectedDate, query.ReceivedFromLogisticsDateTime < txtToReceivedFromLogisticsDateTime.SelectedDate.Value.AddDays(1));
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

        private DataTable AssetWorkOrderSends
        {
            get
            {
                var isEmptyFilter = txtOrderDate.IsEmpty && txtToOrderDate.IsEmpty && string.IsNullOrEmpty(txtOrderNo.Text) && txtSentDate.IsEmpty 
                    && txtToSentDate.IsEmpty && string.IsNullOrEmpty(cboSearchFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSearchToServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "AssetWorkOrderSend")) return null;

                var query = new AssetWorkOrderQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var tounit = new ServiceUnitQuery("c");
                var wtype = new AppStandardReferenceItemQuery("d");
                var wstatus = new AppStandardReferenceItemQuery("e");
                var asset = new AssetQuery("f");
                var supp = new SupplierQuery("g");

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
                        query.SentToThirdPartiesDateTime.As("SentDate"),
                        query.LetterNo,
                        supp.SupplierName
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
                query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);
                query.Where(query.IsProceed == false, query.SentToThirdPartiesDateTime.IsNotNull(),
                            query.SRWorkStatus == AppSession.Parameter.WorkStatusThirdParties);

                if (!txtOrderDate.IsEmpty && !txtToOrderDate.IsEmpty)
                    query.Where(query.OrderDate >= txtOrderDate.SelectedDate, query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtOrderNo.Text))
                    query.Where(query.OrderNo == txtOrderNo.Text);
                if (!txtSentDate.IsEmpty && !txtToSentDate.IsEmpty)
                    query.Where(query.SentToThirdPartiesDateTime >= txtSentDate.SelectedDate, query.SentToThirdPartiesDateTime < txtToSentDate.SelectedDate.Value.AddDays(1));
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
            grdListSend.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "process")
            {
                pnlInfo.Visible = false;

                Validate();
                if (!IsValid)
                    return;

                var msg = Process();

                if (!string.IsNullOrEmpty(msg))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }

                grdList.Rebind();
                grdListSend.Rebind();
            }
            else if (eventArgument == "rebindo")
            {
                grdListSend.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var jobParameters = new PrintJobParameterCollection();

                var parameter = jobParameters.AddNew();
                parameter.Name = "p_OrderNo";
                parameter.ValueString = param[1];

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.SentToThirdPartiesSlp;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        private string Process()
        {
            var msg = string.Empty;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                {
                    string orderNo = dataItem.GetDataKeyValue("OrderNo").ToString();
                    DateTime sentDate = ((RadDatePicker)dataItem.FindControl("txtSentDate")).SelectedDate ?? DateTime.Now;
                    string letterNo = ((RadTextBox)dataItem.FindControl("txtLetterNo")).Text ?? string.Empty;
                    string supplierId = ((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue;
                    
                    if (string.IsNullOrEmpty(letterNo) || string.IsNullOrEmpty(supplierId))
                    {
                        if (string.IsNullOrEmpty(letterNo) && string.IsNullOrEmpty(supplierId))
                        {
                            if (msg == string.Empty)
                            {
                                msg = "Letter No and Supplier is required for " + orderNo;
                            }
                            else
                            {
                                msg += ", " + "Letter No and Supplier is required for " + orderNo;
                            }
                        }
                        else if (string.IsNullOrEmpty(letterNo))
                        {
                            if (msg == string.Empty)
                            {
                                msg = "Letter No is required for " + orderNo;
                            }
                            else
                            {
                                msg += ", " + "Letter No is required for " + orderNo;
                            }
                        }
                        else
                        {
                            if (msg == string.Empty)
                            {
                                msg = "Supplier is required for " + orderNo;
                            }
                            else
                            {
                                msg += ", " + "Supplier is required for " + orderNo;
                            }
                        }
                    }
                    else
                    {
                        Save(orderNo, sentDate, letterNo, supplierId);
                    }
                }
            }
            return msg;
        }

        private void Save(string orderNo, DateTime sentDate, string letterNo, string supplierId)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var wo = new AssetWorkOrder();
                wo.LoadByPrimaryKey(orderNo);
                wo.SentToThirdPartiesByUserID = AppSession.UserLogin.UserID;
                wo.SentToThirdPartiesDateTime = sentDate;
                wo.LetterNo = letterNo;
                wo.SupplierID = supplierId;
                wo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                wo.LastUpdateDateTime = DateTime.Now;

                wo.Save();
                trans.Complete();
            }
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            (o as RadComboBox).DataSource = PopulateSupplier(e.Text);
            (o as RadComboBox).DataBind();
        }

        private DataTable PopulateSupplier()
        {
            if (ViewState["supplier"] != null)
                return ViewState["supplier"] as DataTable;

            var supp = new SupplierQuery("b");
            supp.Select(
                "<b.SupplierID AS SupplierID>",
                supp.SupplierID.As("SuppliedIDFilter"),
                supp.SupplierName
                );
            supp.Where(supp.IsActive == true);

            ViewState["supplier"] = supp.LoadDataTable();
            return ViewState["supplier"] as DataTable;
        }

        private DataTable PopulateSupplier(string supplierName)
        {
            if (!string.IsNullOrEmpty(supplierName))
            {
                DataView dv = PopulateSupplier().DefaultView;
                dv.RowFilter = "SupplierName LIKE '%" + supplierName + "%'";
                return dv.ToTable();
            }

            var dtb = new DataTable();

            dtb.Columns.Add(new DataColumn("SupplierID"));
            dtb.Columns.Add(new DataColumn("SupplierName"));

            return dtb;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }
    }
}
