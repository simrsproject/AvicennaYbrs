using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class VisitDownPaymentList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClosingVisiteDownPayment;
            Title = "Visit Down Payment";
        }

        private DataTable DownPayments
        {
            get
            {
                return Helper.Payment.GetVisitDownPaymentOutstandingByPatientId(Request.QueryString["patid"]);
            }
        }

        protected void grdDownPaymentSummary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdDownPaymentSummary.DataSource = DownPayments;
        }

        protected void grdDownPayment_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string paymentNo = e.DetailTableView.ParentItem.GetDataKeyValue("PaymentNo").ToString();

            var query = new TransPaymentItemVisiteQuery("a");
            var item = new ItemQuery("b");
            var unit = new ServiceUnitQuery("c");

            query.Select(
                query,
                @"<a.VisiteQty - a.RealizationQty AS ClosedQty>",
                item.ItemName,
                unit.ServiceUnitName
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.Where(query.PaymentNo == paymentNo);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            if (grdDownPaymentSummary.MasterTableView.Items.Count == 0)
                return true;

            var coll = (ClosingVisiteDownPaymentItemCollection)Session["ClosingVisiteDownPaymentItems" + Request.UserHostName];
            coll.MarkAllAsDeleted();

            foreach (GridDataItem item in grdDownPaymentSummary.MasterTableView.Items.Cast<GridDataItem>())
            {
                if (((CheckBox)item.FindControl("detailChkbox")).Checked)
                {
                    double amount = ((RadNumericTextBox)item.FindControl("txtAmount")).Value ?? 0;

                    var tpi = coll.AddNew();
                    string payNo = item.GetDataKeyValue("PaymentNo").ToString();
                    tpi.PaymentNo = payNo;

                    var entity = new TransPayment();
                    entity.LoadByPrimaryKey(payNo);

                    tpi.PaymentDate = entity.PaymentDate;
                    tpi.PaymentTime = entity.PaymentTime;

                    tpi.Amount = Convert.ToDecimal(amount);
                }
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;
            foreach (GridDataItem dataItem in grdDownPaymentSummary.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}