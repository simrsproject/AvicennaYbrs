using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitCoaMatrixDetailProductAccount : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnit;

            if (IsPostBack) return;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["id"]);

            txtServiceUnitID.Text = unit.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var loc = new Location();
            loc.LoadByPrimaryKey(Request.QueryString["loc"]);

            txtLocationID.Text = loc.LocationID;
            lblLocationName.Text = loc.LocationName;

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsUnitBasedProductAccount) != "Yes")
            {
                for (int i = 0; i < grdList.MasterTableView.Columns.Count - 1; i++)
                {
                    grdList.MasterTableView.Columns[i].Visible = false;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["id"]);

            var maps = Session["ServiceUnitProductAccountMappingCollection"] as ServiceUnitProductAccountMappingCollection;

            foreach (GridDataItem item in grdList.MasterTableView.Items)
            {
                int chartOfAccountIdIncome = 0,
                    chartOfAccountIdInventory = 0,
                    chartOfAccountIdAccrual = 0,
                    chartOfAccountIdCOGS = 0,
                    chartOfAccountIdDiscount = 0,
                    chartOfAccountIdExpense = 0;
                int subLedgerIdIncome = 0,
                    subLedgerIdInventory = 0,
                    subLedgerIdAccrual = 0,
                    subLedgerIdCOGS = 0,
                    subLedgerIdDiscount = 0,
                    subLedgerIdExpense = 0;

                //coa
                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOAIncome") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOAIncome") as RadComboBox).SelectedValue, out chartOfAccountIdIncome);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOAAccrual") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOAAccrual") as RadComboBox).SelectedValue, out chartOfAccountIdAccrual);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOADiscount") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOADiscount") as RadComboBox).SelectedValue, out chartOfAccountIdDiscount);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOAInventory") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOAInventory") as RadComboBox).SelectedValue, out chartOfAccountIdInventory);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOACOGS") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOACOGS") as RadComboBox).SelectedValue, out chartOfAccountIdCOGS);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboCOAExpense") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboCOAExpense") as RadComboBox).SelectedValue, out chartOfAccountIdExpense);

                // subledger
                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLIncome") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLIncome") as RadComboBox).SelectedValue, out subLedgerIdIncome);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLAccrual") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLAccrual") as RadComboBox).SelectedValue, out subLedgerIdAccrual);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLDiscount") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLDiscount") as RadComboBox).SelectedValue, out subLedgerIdDiscount);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLInventory") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLInventory") as RadComboBox).SelectedValue, out subLedgerIdInventory);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLCOGS") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLCOGS") as RadComboBox).SelectedValue, out subLedgerIdCOGS);

                if (!string.IsNullOrEmpty((item["ProductAccountID"].FindControl("cboSLExprense") as RadComboBox).SelectedValue))
                    int.TryParse((item["ProductAccountID"].FindControl("cboSLExprense") as RadComboBox).SelectedValue, out subLedgerIdExpense);

                var map = maps.FindByPrimaryKey(txtLocationID.Text, txtServiceUnitID.Text, item["ProductAccountID"].Text, string.IsNullOrEmpty(unit.SRRegistrationType) ? "OPR" : (unit.SRRegistrationType == "MCU" ? "OPR" : unit.SRRegistrationType));
                if (map == null)
                {
                    map = maps.AddNew();
                    map.LocationId = txtLocationID.Text;
                    map.ServiceUnitId = txtServiceUnitID.Text;
                    map.ProductAccountId = item["ProductAccountID"].Text;
                    map.SRRegistrationType = string.IsNullOrEmpty(unit.SRRegistrationType) ? "OPR" : (unit.SRRegistrationType == "MCU" ? "OPR" : unit.SRRegistrationType);
                }

                map.ChartOfAccountIdIncome = chartOfAccountIdIncome;
                map.SubledgerIdIncome = subLedgerIdIncome;

                map.ChartOfAccountIdAccrual = chartOfAccountIdAccrual;
                map.SubledgerIdAccrual = subLedgerIdAccrual;

                map.ChartOfAccountIdDiscount = chartOfAccountIdDiscount;
                map.SubledgerIdDiscount = subLedgerIdDiscount;

                map.ChartOfAccountIdInventory = chartOfAccountIdInventory;
                map.SubledgerIdInventory = subLedgerIdInventory;

                map.ChartOfAccountIdCOGS = chartOfAccountIdCOGS;
                map.SubledgerIdCOGS = subLedgerIdCOGS;

                map.ChartOfAccountIdExpense = chartOfAccountIdExpense;
                map.SubledgerIdExpense = subLedgerIdExpense;

                map.LastUpdateByUserID = AppSession.UserLogin.UserID;
                map.LastUpdateDateTime = DateTime.Now;
            }

            if (Request.QueryString["type"] == "2")
                maps.Save();

            return true;
        }

        private DataTable PopulateChartOfAccount()
        {
            if (ViewState["chartOfAccount"] != null) return (ViewState["chartOfAccount"] as DataTable);

            var coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.IsDetail == true, coaQ.IsActive == true);
            ViewState["chartOfAccount"] = coaQ.LoadDataTable();

            return (ViewState["chartOfAccount"] as DataTable);
        }

        private DataTable PopulateSubLedger()
        {
            if (ViewState["subLedger"] != null) return (ViewState["subLedger"] as DataTable);

            var slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.GroupId == AppSession.Parameter.SubLedgerGroupIdServiceUnit);
            ViewState["subLedger"] = slQ.LoadDataTable();

            return (ViewState["subLedger"] as DataTable);
        }

        #region ComboBox ChartOfAccountIdExpense

        protected void cboCOAExpense_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var coa = PopulateChartOfAccount();
            if (string.IsNullOrEmpty(e.Text)) (sender as RadComboBox).DataSource = coa;
            else
            {
                var dv = PopulateChartOfAccount().DefaultView;
                dv.RowFilter = "ChartOfAccountCode LIKE '%" + e.Text + "%' OR ChartOfAccountName LIKE '%" + e.Text + "%'";
                if (dv.Count > 0)
                {
                    (sender as RadComboBox).DataSource = dv.ToTable().AsEnumerable().CopyToDataTable();
                }
                else
                {
                    (sender as RadComboBox).DataSource = null;
                }
            }
            (sender as RadComboBox).DataBind();
        }

        protected void cboCOAExpense_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        #endregion

        #region ComboBox SubledgerIdExpense

        protected void cboSLExprense_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) (sender as RadComboBox).DataSource = PopulateSubLedger();
            else
            {
                var dv = PopulateSubLedger().DefaultView;
                dv.RowFilter = "SubLedgerName LIKE '%" + e.Text + "%' OR Description LIKE '%" + e.Text + "%'";
                if (dv.Count > 0)
                {
                    (sender as RadComboBox).DataSource = dv.ToTable().AsEnumerable().Take(20).CopyToDataTable();
                }
                else {
                    (sender as RadComboBox).DataSource = null;
                }
            }
            (sender as RadComboBox).DataBind();
        }

        protected void cboSLExprense_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        #endregion

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private bool IsNotEmpty(string str) {
            str = str.Trim();
            return str != "&nbsp;" && str != "0" && !string.IsNullOrEmpty(str);
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null) return;

            if (IsNotEmpty(dataItem["ChartOfAccountIdIncome"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdIncome"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOAIncome") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdIncome"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdIncome"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdIncome"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLIncome") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdIncome"].Text;
                }
            }
            if (IsNotEmpty(dataItem["ChartOfAccountIdAccrual"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdAccrual"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOAAccrual") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdAccrual"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdAccrual"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdAccrual"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLAccrual") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdAccrual"].Text;
                }
            }
            if (IsNotEmpty(dataItem["ChartOfAccountIdDiscount"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdDiscount"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOADiscount") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdDiscount"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdDiscount"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdDiscount"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLDiscount") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdDiscount"].Text;
                }
            }
            if (IsNotEmpty(dataItem["ChartOfAccountIdInventory"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdInventory"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOAInventory") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdInventory"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdInventory"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdInventory"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLInventory") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdInventory"].Text;
                }
            }
            if (IsNotEmpty(dataItem["ChartOfAccountIdCOGS"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdCOGS"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOACOGS") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdCOGS"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdCOGS"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdCOGS"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLCOGS") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdCOGS"].Text;
                }
            }
            if (IsNotEmpty(dataItem["ChartOfAccountIdExpense"].Text))
            {
                var dtb = PopulateChartOfAccount().AsEnumerable().Where(p => p.Field<int>("ChartOfAccountId") == dataItem["ChartOfAccountIdExpense"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboCOAExpense") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["ChartOfAccountIdExpense"].Text;
                }
            }
            if (IsNotEmpty(dataItem["SubledgerIdExpense"].Text))
            {
                var dtb = PopulateSubLedger().AsEnumerable().Where(p => p.Field<int>("SubLedgerId") == dataItem["SubledgerIdExpense"].Text.ToInt()).CopyToDataTable();
                if (dtb.AsEnumerable().Any())
                {
                    var supplier = (dataItem["ProductAccountID"].FindControl("cboSLExprense") as RadComboBox);
                    supplier.DataSource = dtb;
                    supplier.DataBind();
                    supplier.SelectedValue = dataItem["SubledgerIdExpense"].Text;
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var pa = new ProductAccountCollection();
            var dtb = pa.GetProductAccountServiceUnitMapping(txtServiceUnitID.Text);

            var maps = Session["ServiceUnitProductAccountMappingCollection"] as ServiceUnitProductAccountMappingCollection;
            if (maps != null)
            {
                foreach (var map in maps)
                {
                    if (map.ServiceUnitId != txtServiceUnitID.Text) continue;
                    if (map.LocationId != txtLocationID.Text) continue;

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["ProductAccountId"].ToString() == map.ProductAccountId && 
                            row["SRRegistrationType"].ToString() == map.SRRegistrationType)
                        {
                            row["ChartOfAccountIdIncome"] = map.ChartOfAccountIdIncome;
                            row["SubledgerIdIncome"] = map.SubledgerIdIncome;

                            row["ChartOfAccountIdAccrual"] = map.ChartOfAccountIdAccrual;
                            row["SubledgerIdAccrual"] = map.SubledgerIdAccrual;

                            row["ChartOfAccountIdDiscount"] = map.ChartOfAccountIdDiscount;
                            row["SubledgerIdDiscount"] = map.SubledgerIdDiscount;

                            row["ChartOfAccountIdInventory"] = map.ChartOfAccountIdInventory;
                            row["SubledgerIdInventory"] = map.SubledgerIdInventory;

                            row["ChartOfAccountIdCOGS"] = map.ChartOfAccountIdCOGS;
                            row["SubledgerIdCOGS"] = map.SubledgerIdCOGS;

                            row["ChartOfAccountIdExpense"] = map.ChartOfAccountIdExpense;
                            row["SubledgerIdExpense"] = map.SubledgerIdExpense;

                            row.AcceptChanges();
                        }
                    }
                }
            }

            grdList.DataSource = dtb;
        }
    }
}
