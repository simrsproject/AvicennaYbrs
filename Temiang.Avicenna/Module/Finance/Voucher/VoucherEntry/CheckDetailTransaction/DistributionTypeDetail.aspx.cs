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

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction
{
    public partial class DistributionTypeDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["src"]))
                {
                    ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                    ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
                }

                ViewState["TransactionNo"] = string.Empty;
            }
        }

        private DataTable ItemTransactionItems
        {
            get
            {
                if (ViewState["ItemTransactionItems"] != null)
                    return (DataTable)ViewState["ItemTransactionItems"];

                DataTable tbl = null;

                var itq = new ItemTransactionQuery("it");
                var jtq = new JournalTransactionsQuery("jt");

                itq.InnerJoin(jtq).On(itq.TransactionNo == jtq.RefferenceNumber)
                    .Where(jtq.JournalId == Request.QueryString["ivd"]);

                var it = new ItemTransaction();
                if (it.Load(itq)) {
                    if (it.TransactionCode == TransactionCode.DistributionConfirm)
                    {
                        var query = new ItemTransactionItemQuery("a");
                        var header = new ItemTransactionQuery("b");
                        var item = new ItemQuery("c");
                        var journal = new JournalTransactionsQuery("d");
                        var su = new ServiceUnitQuery("e");
                        var su2 = new ServiceUnitQuery("f");
                        var hRef = new ItemTransactionQuery("hRef");
                        var journalId = Request.QueryString["ivd"];

                        query.Select
                            (
                                query.TransactionNo,
                                query.SequenceNo,
                                header.TransactionDate,
                                header.ToServiceUnitID,
                                su.ServiceUnitName.As("FromServiceUnitName"),
                                su2.ServiceUnitName.As("ToServiceUnitName"),
                                query.ItemID,
                                item.ItemName,
                                query.Quantity,
                                query.SRItemUnit,
                                query.CostPrice.As("Price"),
                                "<a.Quantity * a.CostPrice AS Amount>",
                                journal.RefferenceNumber
                            );

                        query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                        query.InnerJoin(item).On(query.ItemID == item.ItemID);
                        query.InnerJoin(su).On(header.FromServiceUnitID == su.ServiceUnitID);
                        query.InnerJoin(su2).On(header.ToServiceUnitID == su2.ServiceUnitID);
                        query.InnerJoin(hRef).On(hRef.ReferenceNo == header.TransactionNo);
                        query.InnerJoin(journal).On(hRef.TransactionNo == journal.RefferenceNumber);

                        query.Where
                            (
                                journal.JournalId == journalId
                            );
                        query.OrderBy
                            (
                                query.TransactionNo.Ascending,
                                query.SequenceNo.Ascending
                            );

                        tbl = query.LoadDataTable();
                    }
                    else {
                        var query = new ItemTransactionItemQuery("a");
                        var header = new ItemTransactionQuery("b");
                        var item = new ItemQuery("c");
                        var journal = new JournalTransactionsQuery("d");
                        var su = new ServiceUnitQuery("e");
                        var su2 = new ServiceUnitQuery("f");
                        var journalId = Request.QueryString["ivd"];

                        query.Select
                            (
                                query.TransactionNo,
                                query.SequenceNo,
                                header.TransactionDate,
                                header.ToServiceUnitID,
                                su.ServiceUnitName.As("FromServiceUnitName"),
                                su2.ServiceUnitName.As("ToServiceUnitName"),
                                query.ItemID,
                                item.ItemName,
                                query.Quantity,
                                query.SRItemUnit,
                                query.CostPrice.As("Price"),
                                "<a.Quantity * a.CostPrice AS Amount>",
                                journal.RefferenceNumber
                            );

                        query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                        query.InnerJoin(item).On(query.ItemID == item.ItemID);
                        query.InnerJoin(su).On(header.FromServiceUnitID == su.ServiceUnitID);
                        query.InnerJoin(su2).On(header.ToServiceUnitID == su2.ServiceUnitID);
                        query.InnerJoin(journal).On(query.TransactionNo == journal.RefferenceNumber);

                        query.Where
                            (
                                journal.JournalId == journalId
                            );
                        query.OrderBy
                            (
                                query.TransactionNo.Ascending,
                                query.SequenceNo.Ascending
                            );

                        tbl = query.LoadDataTable();
                    }
                }

                

                ViewState["ItemTransactionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["ItemTransactionItems"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = ItemTransactionItems;
        }
    }
}
