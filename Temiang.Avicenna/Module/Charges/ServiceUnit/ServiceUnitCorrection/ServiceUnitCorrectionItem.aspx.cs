using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitCorrectionItem : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["disch"] == "0")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrection;
            else if (Request.QueryString["disch"] == "1")
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerification;
            else
                ProgramID = AppConstant.Program.ServiceUnitTransactionCorrectionVerificationAncillary;

            if (!IsPostBack)
            {
                ViewState["TransactionNo" + Request.UserHostName] = string.Empty;
                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["verif"]) || (Request.QueryString["verif"] == "0"))
                    {
                        grdTransChargesItem.Columns.FindByUniqueName("Price").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("DiscountAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("CitoAmount").Visible = false;
                        grdTransChargesItem.Columns.FindByUniqueName("Total").Visible = false;
                    }
                }
            }
        }

        private DataTable TransChargesItems
        {
            get
            {
                var hd = new TransCharges();
                hd.LoadByPrimaryKey((string)ViewState["TransactionNo" + Request.UserHostName]);

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var param = new ParamedicQuery("c");
                var cost = new CostCalculationQuery("d");

                query.Select
                    (
                        query.TransactionNo,
                        query.SequenceNo,
                        query.ItemID,
                        item.ItemName,
                        query.Notes,
                        param.ParamedicName,
                        query.ParamedicCollectionName,
                        @"<a.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                      FROM TransChargesItem 
                                                      WHERE ReferenceNo = a.TransactionNo
                                                            AND ReferenceSequenceNo = a.SequenceNo
                                                            AND IsVoid = 0), 0) AS 'ChargeQuantity'>",
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.CitoAmount,
                        query.IsOrderRealization,
                        cost.IntermBillNo,
                        @"<(
                               (
                                   (
                                       a.ChargeQuantity + ISNULL(
                                           (
                                               SELECT SUM(ChargeQuantity)
                                               FROM   TransChargesItem
                                               WHERE  ReferenceNo = a.TransactionNo
                                                      AND ReferenceSequenceNo = a.SequenceNo
                                                      AND IsVoid = 0
                                           ),
                                           0
                                       )
                                   ) * a.Price
                               ) - (
                                   ABS(
                                       (
                                           ISNULL(
                                               (
                                                   SELECT SUM(ChargeQuantity)
                                                   FROM   TransChargesItem
                                                   WHERE  TransactionNo = a.TransactionNo
                                                          AND SequenceNo = a.SequenceNo
                                                          AND IsApprove = 1
                                               ),
                                               0
                                           )
                                       ) / a.ChargeQuantity
                                   ) * a.DiscountAmount
                               )
                           ) + a.CitoAmount AS Total>"
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(cost).On(query.TransactionNo == cost.TransactionNo &&
                                         query.SequenceNo == cost.SequenceNo);
                query.Where
                    (
                        query.TransactionNo == (string)ViewState["TransactionNo" + Request.UserHostName],
                        query.IsApprove == true,
                        query.IsVoid == false,
                        query.Or(
                            query.ParentNo == string.Empty,
                            query.ParentNo.IsNull()
                            )
                    );

                if (!string.IsNullOrEmpty((string)ViewState["TransactionNo" + Request.UserHostName]))
                    query.Where(cost.RegistrationNo == hd.RegistrationNo);

                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(query.ItemID == cboItemID.SelectedValue);

                query.OrderBy(query.SequenceNo.Ascending);

                var tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    //item yg sudah dikoreksi full tidak ditampilkan lg baik order ataupun bukan
                    if ((decimal)row["ChargeQuantity"] == 0)
                        row.Delete();
                    else if (hd.IsOrder ?? false)
                    {
                        if (!(bool)row["IsOrderRealization"])
                            row.Delete();
                    }

                    //if (hd.IsOrder ?? false)
                    //{
                    //    if (!(bool)row["IsOrderRealization"])
                    //        row.Delete();
                    //}
                    //else
                    //{
                    //    if ((decimal)row["ChargeQuantity"] == 0)
                    //        row.Delete();
                    //}
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        private static int TransChargesItemCollection(string transactionNo, string verif)
        {
            var hd = new TransCharges();
            hd.LoadByPrimaryKey(transactionNo);

            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");
            var param = new ParamedicQuery("c");

            query.Select(
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.Notes,
                    query.ParamedicCollectionName,
                    item.ItemName,
                    param.ParamedicName,
                    @"<a.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                FROM TransChargesItem 
                                                WHERE ReferenceNo = a.TransactionNo
                                                    AND ReferenceSequenceNo = a.SequenceNo
                                                    AND IsVoid = 0), 0) AS 'ChargeQuantity'>",
                    query.SRItemUnit,
                    query.Price,
                    query.DiscountAmount,
                    query.CitoAmount,
                    @"<(a.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                FROM TransChargesItem 
                                                WHERE ReferenceNo = a.TransactionNo
                                                    AND ReferenceSequenceNo = a.SequenceNo
                                                    AND IsVoid = 0), 0)) * ((a.Price - a.DiscountAmount) + a.CitoAmount) AS 'Total'>",
                    query.IsOrderRealization
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.Where
                (
                    query.TransactionNo == transactionNo,
                    query.IsVoid == false,
                    query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        ),
                    query.Or(
                        query.IsApprove == true,
                        query.IsOrderRealization == true
                        )
                );

            query.OrderBy(query.SequenceNo.Ascending);

            var tbl = query.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                if (hd.IsOrder ?? false)
                {
                    if (!(bool)row["IsOrderRealization"])
                        row.Delete();
                    else if ((decimal)row["ChargeQuantity"] == 0)
                        row.Delete();
                }
                else
                {
                    if ((decimal)row["ChargeQuantity"] == 0)
                        row.Delete();
                }
            }

            tbl.AcceptChanges();

            return tbl.Rows.Count;
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        private DataTable TransCharges
        {
            get
            {
                var charges = new TransChargesQuery("a");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var usrunit = new AppUserServiceUnitQuery("d");

                charges.es.Distinct = true;
                charges.Select
                    (
                        charges.TransactionNo,
                        charges.TransactionDate,
                        charges.ExecutionDate,
                        unit.ServiceUnitName,
                        room.RoomName,
                        charges.IsAutoBillTransaction,
                        charges.LastUpdateByUserID,
                        charges.IsOrder
                    );
                charges.InnerJoin(unit).On(charges.ToServiceUnitID == unit.ServiceUnitID);
                charges.LeftJoin(room).On(charges.RoomID == room.RoomID);
                if (Request.QueryString["disch"] != "1")
                    charges.InnerJoin(usrunit).On(
                        charges.ToServiceUnitID == usrunit.ServiceUnitID &&
                        usrunit.UserID == AppSession.UserLogin.UserID
                        );

                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                {
                    var chargesitem = new TransChargesItemQuery("e");
                    charges.InnerJoin(chargesitem).On(charges.TransactionNo == chargesitem.TransactionNo);
                    charges.Where(chargesitem.ItemID == cboItemID.SelectedValue);
                }
                if (!txtTransactionDate.IsEmpty)
                    charges.Where(charges.TransactionDate.Date() == txtTransactionDate.SelectedDate);

                charges.Where(
                    charges.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"])),
                    charges.IsCorrection == false,
                    charges.IsApproved == true,
                    charges.IsVoid == false,
                    charges.PackageReferenceNo.IsNull(),
                    charges.Or(charges.IsPackage == false, charges.IsPackage.IsNull())
                    );
                charges.OrderBy(charges.TransactionDate.Ascending);

                var dtb = charges.LoadDataTable();

                foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => TransChargesItemCollection(row["TransactionNo"].ToString(), Request.QueryString["verif"].ToString()) == 0))
                {
                    row.Delete();
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void grdTransCharges_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtb = TransCharges.Copy();

            foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => TransChargesItemCollection(row["TransactionNo"].ToString(), Request.QueryString["verif"].ToString()) == 0))
            {
                row.Delete();
            }

            dtb.AcceptChanges();

            grdTransCharges.DataSource = TransCharges;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdTransChargesItem":
                    ViewState["TransactionNo" + Request.UserHostName] = eventArgument;
                    grdTransChargesItem.Rebind();
                    break;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.refno = '" + grdTransCharges.SelectedValue + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var details = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
            details.MarkAllAsDeleted();

            var component = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];
            component.MarkAllAsDeleted();

            var consumption = (TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName];
            consumption.MarkAllAsDeleted();

            foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    var pycoll = new TransPaymentItemOrderCollection();
                    pycoll.Query.Where(
                        pycoll.Query.TransactionNo == dataItem["TransactionNo"].Text,
                        pycoll.Query.SequenceNo == dataItem["SequenceNo"].Text,
                        pycoll.Query.IsPaymentProceed == true,
                        pycoll.Query.IsPaymentReturned == false
                        );
                    pycoll.LoadAll();

                    if (pycoll.Count > 0)
                    {
                        ShowInformationHeader("Transaction already paid. Correction Data is not allowed.");
                        return false;
                    }

                    if (AppSession.Parameter.IsUsingIntermBill && !AppSession.Parameter.IsAllowCorrectionOfIntermBillsTransaction)
                    {
                        var ibcoll = new CostCalculationCollection();
                        ibcoll.Query.Where(
                            ibcoll.Query.TransactionNo == dataItem["TransactionNo"].Text,
                            ibcoll.Query.SequenceNo == dataItem["SequenceNo"].Text,
                            ibcoll.Query.IntermBillNo.IsNotNull()
                            );
                        ibcoll.LoadAll();

                        if (ibcoll.Count > 0)
                        {
                            ShowInformationHeader("Transaction is already on interm bill. Correction Data is not allowed.");
                            return false;
                        }
                    }

                    if (((RadNumericTextBox)dataItem.FindControl("txtQty")).Value > 0)
                    {
                        var detail = new TransChargesItem();
                        detail.LoadByPrimaryKey(dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                        detail.MarkAllColumnsAsDirty(DataRowState.Added);

                        #region item component
                        var components = new TransChargesItemCompCollection();
                        components.Query.Where(
                                components.Query.TransactionNo == dataItem["TransactionNo"].Text,
                                components.Query.SequenceNo == dataItem["SequenceNo"].Text
                            );
                        components.Query.OrderBy(components.Query.TariffComponentID.Ascending);
                        components.LoadAll();

                        foreach (var c in components)
                        {
                            c.MarkAllColumnsAsDirty(DataRowState.Added);
                            component.AttachEntity(c);
                        }
                        #endregion

                        #region detail
                        decimal chargeQuantityTx = detail.ChargeQuantity ?? 0;

                        detail.ReferenceNo = detail.TransactionNo;
                        detail.ReferenceSequenceNo = detail.SequenceNo;

                        decimal stockQty = (detail.StockQuantity ?? 0) / (detail.ChargeQuantity ?? 1);
                        detail.StockQuantity = 0 - (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) * stockQty);
                        //detail.StockQuantity = 0 - ((Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) / detail.ChargeQuantity) * detail.StockQuantity);

                        //if (detail.AutoProcessCalculation < 0)
                        detail.DiscountAmount = (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) / detail.ChargeQuantity) * detail.DiscountAmount;
                        //else if (detail.AutoProcessCalculation > 0)
                        //    detail.Price += detail.AutoProcessCalculation;
                        detail.CitoAmount = (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) / detail.ChargeQuantity) * detail.CitoAmount;
                        detail.ChargeQuantity = 0 - Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value);
                        detail.IsBillProceed = false;
                        detail.IsApprove = false;
                        detail.IsVoid = false;

                        details.AttachEntity(detail);
                        #endregion

                        #region item consumption
                        var consumptions = new TransChargesItemConsumptionCollection();
                        consumptions.Query.Where(
                                consumptions.Query.TransactionNo == dataItem["TransactionNo"].Text,
                                consumptions.Query.SequenceNo == dataItem["SequenceNo"].Text
                            );
                        consumptions.Query.OrderBy(consumptions.Query.DetailItemID.Ascending);
                        consumptions.LoadAll();

                        foreach (var c in consumptions)
                        {
                            c.MarkAllColumnsAsDirty(DataRowState.Added);

                            c.Qty = 0 - (c.Qty * (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) / chargeQuantityTx));
                            c.QtyRealization = 0 - (c.QtyRealization * (Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value) / chargeQuantityTx));

                            consumption.AttachEntity(c);
                        }
                        #endregion
                    }
                }
            }

            return true;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdTransCharges.Rebind();

            grdTransChargesItem.DataSource = null;
            grdTransChargesItem.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            var tciq = new TransChargesItemQuery("b");
            var tcq = new TransChargesQuery("c");

            query.InnerJoin(tciq).On(query.ItemID == tciq.ItemID);
            query.InnerJoin(tcq).On(tciq.TransactionNo == tcq.TransactionNo);

            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                );
            
            query.Where(
                    query.Or(
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        ),
                    tcq.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]))
                );
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();
            var item2 = string.Empty;

            foreach (DataRow row in tbl.Rows)
            {
                var item1 = (string)row["ItemID"];
                if (item1 != item2)
                    item2 = (string)row["ItemID"];
                else
                    row.Delete();
            }

            tbl.AcceptChanges();

            return tbl;
        }
    }
}
