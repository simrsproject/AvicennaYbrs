using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class BkuJournalList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["type"] == "0" ?  AppConstant.Program.BkuMasuk : AppConstant.Program.BkuKeluar;

            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = DateTime.Now.Date;
                txtToDate.SelectedDate = DateTime.Now.Date;

                var b = new BankCollection();
                b.Query.Where(b.Query.IsActive == true);
                b.Query.Load();

                cboKasBank.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach(var entity in b)
                {
                    cboKasBank.Items.Add(new Telerik.Web.UI.RadComboBoxItem(entity.BankName, entity.BankID));
                }

                cboPelanggan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                if (Request.QueryString["type"] == "0")
                {
                    var g = new GuarantorCollection();
                    g.Query.Where(g.Query.IsActive == true);
                    g.Query.Load();

                    foreach (var entity in g)
                    {
                        cboPelanggan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
                    }
                }
                else
                {
                    var g = new SupplierCollection();
                    g.Query.Where(g.Query.IsActive == true);
                    g.Query.Load();

                    foreach (var entity in g)
                    {
                        cboPelanggan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(entity.SupplierName, entity.SupplierID));
                    }
                }
            }
        }

        public DataTable BkuJournalTransaction()
        {
            if ((!txtFromDate.SelectedDate.HasValue) || (!txtToDate.SelectedDate.HasValue)) {
                return null;
            }

            var bjt = new BkuJournalTransactionsQuery("bjt");
            var bjtd = new BkuJournalTransactionDetailsQuery("bjtd");
            var b = new BankQuery("d");
            var coa = new ChartOfAccountsQuery("coa");
            var jt = new JournalTransactionsQuery("jt");

            bjt.InnerJoin(jt).On(bjt.JournalIdToCopy == jt.JournalId && jt.TransactionDate.Between(txtFromDate.SelectedDate.Value, txtToDate.SelectedDate.Value))
                .InnerJoin(bjtd).On(bjt.BkuJournalId == bjtd.BkuJournalId)
                .InnerJoin(b).On(bjtd.ChartOfAccountId == b.ChartOfAccountId)
                .InnerJoin(coa).On(bjtd.ChartOfAccountId == coa.ChartOfAccountId)
                .Select(
                    bjt.BkuJournalId, bjtd.ChartOfAccountId, coa.ChartOfAccountCode, coa.ChartOfAccountName, jt.TransactionDate, bjtd.Description, bjtd.Debit, bjtd.Credit, jt.RefferenceNumber
                );

            if (!string.IsNullOrEmpty(cboKasBank.SelectedValue)) {
                bjt.Where(b.BankID == cboKasBank.SelectedValue);
            }

            return bjt.LoadDataTable();
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = BkuJournalTransaction();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}