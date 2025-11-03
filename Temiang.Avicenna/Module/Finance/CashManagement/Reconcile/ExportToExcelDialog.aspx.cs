using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

namespace Temiang.Avicenna.Module.Finance.CashManagement.Reconcile
{
    public partial class ExportToExcelDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

            }

            if (Session["ExportBankReconcile"] != null)
            {
                var dtb = Session["ExportBankReconcile"] as DataTable;
                var cNames = new List<string>();
                foreach (DataColumn col in dtb.Columns)
                {
                    if (!cNames.Contains(col.ColumnName))
                    {
                        cNames.Add(col.ColumnName);
                    }
                }
                foreach (string n in cNames)
                {
                    var a = dtb.Columns[n];
                    //if (a != null)
                    //{
                    //    tbl.Columns.Remove(a);
                    //}l.Columns.Remove("DischargeDate");
                    //}
                    if (dtb.Columns.Contains("TransactionId"))
                        dtb.Columns.Remove("TransactionId");
                    if (dtb.Columns.Contains("Amount"))
                        dtb.Columns.Remove("Amount");
                    if (dtb.Columns.Contains("TxnBalanceId"))
                        dtb.Columns.Remove("TxnBalanceId");
                    if (dtb.Columns.Contains("iRowCount"))
                        dtb.Columns.Remove("iRowCount");
                }

                Common.CreateExcelFile.CreateExcelDocument(dtb, "BankReconcileDetail.xls", this.Response);
                Session["ExportBankReconcile"] = null;
            }

        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
