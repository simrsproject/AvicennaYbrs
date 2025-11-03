using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemRuleDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox txtGuarantorID
        {
            get { return Helper.FindControlRecursive(this.Page, "txtGuarantorID") as RadTextBox; }
        }

        private RadComboBox cboSRTariffType
        {
            get { return Helper.FindControlRecursive(this.Page, "cboSRTariffType") as RadComboBox; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Item, txtItemID);
            StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtAmountValue.Value = 0D;
                txtAmountOPR.Value = 0D;
                txtAmountEMR.Value = 0D;

                tblTariffComponent.Visible = false;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.Text = (String)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.ItemID);
            PopulateItemName(false);

            rblInclude.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsInclude) ? 0 : 1;
            cboSRGuarantorRuleType.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.SRGuarantorRuleType);
            txtAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.AmountValue));
            chkIsValueInPercent.Checked = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsValueInPercent);
            rblToGuarantor.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor) ? 0 : 1;

            txtAmountOPR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.OutpatientAmountValue) ?? 0);
            txtAmountEMR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.EmergencyAmountValue) ?? 0);
            try
            {
                chkIsByTariffComponent.Checked = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsByTariffComponent);
            }
            catch (Exception)
            {
                chkIsByTariffComponent.Checked =false;
            }

            tblRuleType.Visible = rblInclude.SelectedIndex != 1;

            tblTariffComponent.Visible = chkIsByTariffComponent.Checked;
            if (tblTariffComponent.Visible)
            {
                grdTariffComponent.DataSource = GuarantorItemRuleTariffComponents;
                grdTariffComponent.DataBind();
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                GuarantorItemRuleCollection coll = (GuarantorItemRuleCollection)Session["collGuarantorItemRule"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (GuarantorItemRule item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID: {0} has exist", itemID);
                }
            }

            if (rblInclude.SelectedIndex == 0)
            {
                if (cboSRGuarantorRuleType.SelectedIndex == 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Rule Type Name is required";
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return txtItemID.Text; }
        }

        public String ItemName
        {
            get { return lblItemName.Text; }
        }

        public String SRGuarantorRuleType
        {
            get { return cboSRGuarantorRuleType.SelectedValue; }
        }

        public String GuarantorRuleTypeName
        {
            get { return cboSRGuarantorRuleType.Text; }
        }

        public Decimal AmountValue
        {
            get { return Convert.ToDecimal(txtAmountValue.Value); }
        }

        public Boolean IsValueInPercent
        {
            get { return chkIsValueInPercent.Checked; }
        }

        public Boolean IsInclude
        {
            get { return rblInclude.SelectedIndex == 0 ? true : false; }
        }

        public Boolean IsToGuarantor
        {
            get
            {
                return rblToGuarantor.SelectedIndex == 0 ? true : false;
            }
        }

        public Decimal OPRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountOPR.Value); }
        }

        public Decimal EMRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountEMR.Value); }
        }

        public Boolean IsByTariffComponent
        {
            get { return chkIsByTariffComponent.Checked; }
        }

        public DataTable GetGuarantorItemRuleTariffComponent
        {
            get
            {
                var table = new DataTable();
                table.Columns.Add(new DataColumn("TariffComponentID", typeof(string)));
                table.Columns.Add(new DataColumn("AmountValue", typeof(decimal)));
                table.Columns.Add(new DataColumn("OutpatientAmountValue", typeof(decimal)));
                table.Columns.Add(new DataColumn("EmergencyAmountValue", typeof(decimal)));

                foreach (GridDataItem dataItem in grdTariffComponent.MasterTableView.Items)
                {
                    var ipr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtIPR") as RadNumericTextBox).Value);
                    var opr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtOPR") as RadNumericTextBox).Value);
                    var emr = Convert.ToDecimal((dataItem["TariffComponentID"].FindControl("txtEMR") as RadNumericTextBox).Value);
                    if (ipr == 0 && opr == 0 && emr == 0) continue;
                    var row = table.NewRow();
                    row["TariffComponentID"] = dataItem["TariffComponentID"].Text;
                    row["AmountValue"] = ipr;
                    row["OutpatientAmountValue"] = opr;
                    row["EmergencyAmountValue"] = emr;
                    table.Rows.Add(row);
                }

                return table;
            }
        }

        #endregion

        #region Method & Event TextChanged

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            PopulateItemName(true);
        }

        private void PopulateItemName(bool isResetIdIfNotExist)
        {
            if (txtItemID.Text == string.Empty)
            {
                lblItemName.Text = string.Empty;
                return;
            }
            Item entity = new Item();
            if (entity.LoadByPrimaryKey(txtItemID.Text))
                lblItemName.Text = entity.ItemName;
            else
            {
                lblItemName.Text = string.Empty;
                if (isResetIdIfNotExist)
                    txtItemID.Text = string.Empty;
            }
        }

        #endregion

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblRuleType.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }

        protected void chkIsByTariffComponent_CheckedChanged(object sender, EventArgs e)
        {
            txtAmountValue.Value = 0;
            txtAmountOPR.Value = 0;
            txtAmountEMR.Value = 0;

            tblTariffComponent.Visible = chkIsByTariffComponent.Checked;
            if (tblTariffComponent.Visible)
            {
                grdTariffComponent.DataSource = GuarantorItemRuleTariffComponents;
                grdTariffComponent.DataBind();
            }
        }

        private DataTable GuarantorItemRuleTariffComponents
        {
            get
            {
                var tc = new TariffComponentQuery("a");
                tc.Select(
                    tc.TariffComponentID,
                    tc.TariffComponentName,
                    "<0 AS AmountValue>",
                    "<0 AS OutpatientAmountValue>",
                    "<0 AS EmergencyAmountValue>"
                    );
                //tc.Where(tc.TariffComponentID.In(comps.Select(c => c.TariffComponentID)));
                tc.OrderBy(tc.TariffComponentID.Ascending);
                var table = tc.LoadDataTable();

                var comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, cboSRTariffType.SelectedValue, AppSession.Parameter.DefaultTariffClass, txtItemID.Text);
                if (comps == null || comps.Count() == 0)
                {
                    comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, txtItemID.Text);
                    if (comps == null || comps.Count() == 0)
                    {
                        table.Rows.Clear();
                        return table;
                    }
                }
               
                foreach (DataRow row in table.Rows)
                {
                    var comp = comps.Where(c => c.TariffComponentID == row["TariffComponentID"].ToString()).SingleOrDefault();
                    if (comp == null) row.Delete();
                }

                table.AcceptChanges();

                if (table.Rows.Count == 0) return table;

                var coll = (Session["collGuarantorItemRuleTariffComponent"] as GuarantorItemRuleTariffComponentCollection).Where(c => c.GuarantorID == txtGuarantorID.Text && c.ItemID == txtItemID.Text);
                if (coll != null && coll.Any() && coll.Count() > 0)
                {
                    foreach (var entity in coll)
                    {
                        var row = table.AsEnumerable().Where(t => t.Field<string>("TariffComponentID") == entity.TariffComponentID).SingleOrDefault();
                        if (row != null)
                        {
                            row["AmountValue"] = Convert.ToDouble(entity.AmountValue ?? 0);
                            row["OutpatientAmountValue"] = Convert.ToDouble(entity.OutpatientAmountValue ?? 0);
                            row["EmergencyAmountValue"] = Convert.ToDouble(entity.EmergencyAmountValue ?? 0);
                        }
                    }
                    table.AcceptChanges();
                }

                return table;
            }
        }
    }
}
