using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitCoaMatrixDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnit;

            if (IsPostBack)
                return;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["id"]);

            txtServiceUnitID.Text = unit.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var cls = new ClassCollection();
            cls.Query.Where(cls.Query.IsActive == true);
            cls.LoadAll();

            cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var cl in cls)
            {
                cboClass.Items.Add(new RadComboBoxItem(cl.ClassName, cl.ClassID));
            }

            var tariffs = new TariffComponentCollection();
            tariffs.LoadAll();

            cboTariffComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var tariff in tariffs)
            {
                cboTariffComponent.Items.Add(new RadComboBoxItem(tariff.TariffComponentName, tariff.TariffComponentID));
            }
        }

        protected void cboChartOfAccountIdIncome_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                return;

            cboSubledgerIdIncome.Items.Clear();
            cboSubledgerIdIncome.Text = string.Empty;
        }

        protected void cboChartOfAccountIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.es.Top = 20;
            query.Select(
                query.ChartOfAccountId,
                query.ChartOfAccountCode,
                query.ChartOfAccountName
                );
            query.Where(
                query.Or(
                    query.ChartOfAccountCode.Like(searchTextContain),
                    query.ChartOfAccountName.Like(searchTextContain)
                    )
                );
            query.Where(
                query.IsDetail == true,
                query.IsActive == true
                );

            cboChartOfAccountIdIncome.DataSource = query.LoadDataTable();
            cboChartOfAccountIdIncome.DataBind();
        }

        protected void cboChartOfAccountIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"] + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboSubledgerIdIncome_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            if (string.IsNullOrEmpty(cboChartOfAccountIdIncome.SelectedValue))
                groupID = 0;
            else
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(cboChartOfAccountIdIncome.SelectedValue));
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.es.Top = 20;
            query.Select(
                query.SubLedgerId,
                query.SubLedgerName,
                query.Description
                );
            query.Where(
                query.GroupId == groupID,
                query.Or(
                    query.SubLedgerName.Like(searchTextContain),
                    query.Description.Like(searchTextContain)
                    )
                );

            cboSubledgerIdIncome.DataSource = query.LoadDataTable();
            cboSubledgerIdIncome.DataBind();
        }

        protected void cboSubledgerIdIncome_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"] + " - " + ((DataRowView)e.Item.DataItem)["Description"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        protected void btnLoadList_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);

            DataTable dtb;
            ItemQuery item;
            var svc = new ServiceUnitItemServiceQuery("c");

            var comp = new ServiceUnitItemServiceClassQuery("a");
            item = new ItemQuery("b");

            comp.Select(
                comp.ItemID,
                item.ItemName,
                comp.ChartOfAccountIdIncome,
                comp.SubledgerIdIncome,
                comp.ChartOfAccountIdDiscount,
                comp.SubledgerIdDiscount
                );
            comp.InnerJoin(item).On(comp.ItemID == item.ItemID);
            comp.InnerJoin(svc).On(
                comp.ItemID == svc.ItemID &&
                svc.ServiceUnitID == unit.ServiceUnitID
                );
            comp.Where(
                comp.ServiceUnitID == txtServiceUnitID.Text,
                comp.ClassID == cboClass.SelectedValue,
                comp.TariffComponentID == cboTariffComponent.SelectedValue
                );

            dtb = comp.LoadDataTable();

            foreach (DataRow row in from DataRow row in dtb.Rows let i = GetItemTariffComponent(txtTransactionDate.SelectedDate.HasValue ? txtTransactionDate.SelectedDate.Value : DateTime.Now.Date, cboClass.SelectedValue, cboTariffComponent.SelectedValue, row["ItemID"].ToString()) where i == 0 select row)
            {
                row.Delete();
            }

            dtb.AcceptChanges();

            listRight.Items.Clear();

            foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => !string.IsNullOrEmpty(row[rblCOAType.SelectedValue == "income" ? "ChartOfAccountIdIncome" : "ChartOfAccountIdDiscount"].ToString()) &&
                                                                      !string.IsNullOrEmpty(row[rblCOAType.SelectedValue == "income" ? "SubledgerIdIncome" : "SubledgerIdDiscount"].ToString())))
            {
                var coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(Convert.ToInt32(row[rblCOAType.SelectedValue == "income" ? "ChartOfAccountIdIncome" : "ChartOfAccountIdDiscount"]));

                var sub = new SubLedgers();
                sub.LoadByPrimaryKey(Convert.ToInt32(row[rblCOAType.SelectedValue == "income" ? "SubledgerIdIncome" : "SubledgerIdDiscount"]));

                var name = row["ItemID"] + " - " + row["ItemName"] + "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + coa.ChartOfAccountName + " - " + sub.Description;
                listRight.Items.Add(new RadListBoxItem(name, row["ItemID"].ToString()));
            }

            listLeft.Items.Clear();

            foreach (var row in dtb.Rows.Cast<DataRow>().Where(row => string.IsNullOrEmpty(row[rblCOAType.SelectedValue == "income" ? "ChartOfAccountIdIncome" : "ChartOfAccountIdDiscount"].ToString()) &&
                                                                      string.IsNullOrEmpty(row[rblCOAType.SelectedValue == "income" ? "SubledgerIdIncome" : "SubledgerIdDiscount"].ToString())))
            {
                listLeft.Items.Add(new RadListBoxItem(row["ItemID"] + " - " + row["ItemName"], row["ItemID"].ToString()));
            }

            var tariff = new ItemTariffComponentQuery("a");
            item = new ItemQuery("b");
            var suis = new ServiceUnitItemServiceQuery("c");

            tariff.es.Distinct = true;
            tariff.Select(
                tariff.ItemID,
                item.ItemName
                );
            tariff.InnerJoin(item).On(tariff.ItemID == item.ItemID);
            tariff.InnerJoin(suis).On(tariff.ItemID == suis.ItemID);
            tariff.Where(
                suis.ServiceUnitID == txtServiceUnitID.Text,            
                tariff.TariffComponentID == cboTariffComponent.SelectedValue
                );
            tariff.Where(
                   tariff.ClassID == cboClass.SelectedValue || tariff.ClassID == AppSession.Parameter.DefaultTariffClass
                );

            if (listLeft.Items.Any())
                tariff.Where(
                    tariff.ItemID.NotIn(listLeft.Items.Select(l => l.Value)) 
                    );
            if (listRight.Items.Any())
                tariff.Where(
                    tariff.ItemID.NotIn(listRight.Items.Select(l => l.Value)) 
                    );
            var tariffs = tariff.LoadDataTable();

            foreach (var row in tariffs.Rows.Cast<DataRow>())
            {
                listLeft.Items.Add(new RadListBoxItem(row["ItemID"] + " - " + row["ItemName"], row["ItemID"].ToString()));
              
            }

            //var tariff = new ItemTariffComponentQuery("a");
            //item = new ItemQuery("b");

            //tariff.es.Distinct = true;
            //tariff.Select(
            //    tariff.ItemID,
            //    item.ItemName
            //    );
            //tariff.InnerJoin(item).On(tariff.ItemID == item.ItemID);
            //tariff.Where(
            //    tariff.ClassID == cboClass.SelectedValue &&
            //    tariff.TariffComponentID == cboTariffComponent.SelectedValue
            //    );

            //if (listLeft.Items.Any())
            //    tariff.Where(
            //        tariff.ItemID.NotIn(listLeft.Items.Select(l => l.Value)) &&
            //        tariff.ItemID.In(dtb.AsEnumerable().Select(d => d.Field<string>("ItemID")))
            //        );
            //if (listRight.Items.Any())
            //    tariff.Where(
            //        tariff.ItemID.NotIn(listRight.Items.Select(l => l.Value)) &&
            //        tariff.ItemID.In(dtb.AsEnumerable().Select(d => d.Field<string>("ItemID")))
            //        );

            //var tariffs = tariff.LoadDataTable();

            //if (!tariffs.AsEnumerable().Any())
            //    return;

            //foreach (var row in tariffs.Rows.Cast<DataRow>().Select(row => new
            //                                                            {
            //                                                                row,
            //                                                                t = GetItemTariffComponent(DateTime.Now.Date,
            //                                                                                           cboClass.SelectedValue,
            //                                                                                           cboTariffComponent.SelectedValue,
            //                                                                                           row["ItemID"].ToString())
            //                                                            })
            //                                                .Where(@t1 => @t1.t > 0)
            //                                                .Select(@t1 => @t1.row))
            //{
            //    listLeft.Items.Add(new RadListBoxItem(row["ItemID"] + " - " + row["ItemName"], row["ItemID"].ToString()));
            //}
        }

        protected void btnLeftToRight_Click(object sender, EventArgs e)
        {
            if (!listLeft.Items.Any(l => l.Checked))
                return;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);

            using (var trans = new esTransactionScope())
            {
                foreach (var item in listLeft.Items.Where(l => l.Checked))
                {


                    var comp = new BusinessObject.ServiceUnitItemServiceClass();
                    if (comp.LoadByPrimaryKey(txtServiceUnitID.Text, item.Value, cboClass.SelectedValue, cboTariffComponent.SelectedValue))
                    {
                        if (rblCOAType.SelectedValue == "income")
                        {
                            comp.ChartOfAccountIdIncome = int.Parse(cboChartOfAccountIdIncome.SelectedValue);
                            comp.SubledgerIdIncome = int.Parse(cboSubledgerIdIncome.SelectedValue);
                        }
                        else
                        {
                            comp.ChartOfAccountIdDiscount = int.Parse(cboChartOfAccountIdIncome.SelectedValue);
                            comp.SubledgerIdDiscount = int.Parse(cboSubledgerIdIncome.SelectedValue);
                        }
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = DateTime.Now;
                    }
                    else
                    {
                        comp = new BusinessObject.ServiceUnitItemServiceClass();
                        comp.ServiceUnitID = txtServiceUnitID.Text;
                        comp.ItemID = item.Value;
                        comp.ClassID = cboClass.SelectedValue;
                        comp.TariffComponentID = cboTariffComponent.SelectedValue;
                        if (rblCOAType.SelectedValue == "income")
                        {
                            comp.ChartOfAccountIdIncome = int.Parse(cboChartOfAccountIdIncome.SelectedValue);
                            comp.SubledgerIdIncome = int.Parse(cboSubledgerIdIncome.SelectedValue);
                        }
                        else
                        {
                            comp.ChartOfAccountIdDiscount = int.Parse(cboChartOfAccountIdIncome.SelectedValue);
                            comp.SubledgerIdDiscount = int.Parse(cboSubledgerIdIncome.SelectedValue);
                        }
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = DateTime.Now;
                    }

                    comp.Save();

                }

                trans.Complete();
            }

            LoadList();
        }

        protected void btnRightToLeft_Click(object sender, EventArgs e)
        {
            if (!listRight.Items.Any(l => l.Checked))
                return;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);

            using (var trans = new esTransactionScope())
            {
                foreach (var item in listRight.Items.Where(l => l.Checked))
                {

                    var comp = new BusinessObject.ServiceUnitItemServiceClass();
                    if (comp.LoadByPrimaryKey(txtServiceUnitID.Text, item.Value, cboClass.SelectedValue, cboTariffComponent.SelectedValue))
                    {
                        if (rblCOAType.SelectedValue == "income")
                        {
                            comp.ChartOfAccountIdIncome = null;
                            comp.SubledgerIdIncome = null;
                        }
                        else
                        {
                            comp.ChartOfAccountIdDiscount = null;
                            comp.SubledgerIdDiscount = null;
                        }
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = DateTime.Now;
                    }
                    else
                    {
                        comp = new BusinessObject.ServiceUnitItemServiceClass();

                    }

                    comp.Save();

                }

                trans.Complete();
            }

            LoadList();
        }

        protected static int GetItemTariffComponent(DateTime transactionDate, string classID, string tariffComponentID, string itemID)
        {
            var query = new ItemTariffComponentQuery("a");
            var tariff = new TariffComponentQuery("b");
            var itq = new ItemTariffQuery("c");

            query.es.Top = 1;
            query.Select(
                query.TariffComponentID,
                query.Price,
                query.IsAllowDiscount,
                query.IsAllowVariable,
                tariff.TariffComponentName,
                tariff.IsTariffParamedic
                );
            query.InnerJoin(tariff).On(query.TariffComponentID == tariff.TariffComponentID);
            query.InnerJoin(itq).On(
                query.SRTariffType == itq.SRTariffType &&
                query.ItemID == itq.ItemID &&
                query.ClassID == itq.ClassID &&
                query.StartingDate.Date() <= itq.StartingDate.Date()
                );
            query.Where(
                query.ItemID == itemID,
                query.ClassID == classID,
                query.TariffComponentID == tariffComponentID,
                query.StartingDate.Date() <= transactionDate,
                query.Price > 0
                );
            query.OrderBy(
                query.TariffComponentID.Ascending,
                query.StartingDate.Descending
                );

            if (query.LoadDataTable().Rows.Count > 0)
            {
                return query.LoadDataTable().Rows.Count;
            }
            else
            {
                var query2 = new ItemTariffComponentQuery("a");
                var tariff2 = new TariffComponentQuery("b");
                var itq2 = new ItemTariffQuery("c");

                query2.es.Top = 1;
                query2.Select(
                    query2.TariffComponentID,
                    query2.Price,
                    query2.IsAllowDiscount,
                    query2.IsAllowVariable,
                    tariff2.TariffComponentName,
                    tariff2.IsTariffParamedic
                    );
                query2.InnerJoin(tariff).On(query2.TariffComponentID == tariff.TariffComponentID);
                query2.InnerJoin(itq).On(
                    query2.SRTariffType == itq.SRTariffType &&
                    query2.ItemID == itq.ItemID &&
                    query2.ClassID == itq.ClassID &&
                    query2.StartingDate.Date() <= itq.StartingDate.Date()
                    );
                query2.Where(
                    query2.ItemID == itemID,
                    query2.ClassID == AppSession.Parameter.DefaultTariffClass,
                    query2.TariffComponentID == tariffComponentID,
                    query2.StartingDate.Date() <= transactionDate,
                    query2.Price > 0
                    );
                query2.OrderBy(
                    query2.TariffComponentID.Ascending,
                    query2.StartingDate.Descending
                    );
                return query2.LoadDataTable().Rows.Count;
            }
        }
    }
}
