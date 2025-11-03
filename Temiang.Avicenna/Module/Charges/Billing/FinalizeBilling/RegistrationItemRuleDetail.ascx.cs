using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class RegistrationItemRuleDetail : UserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRGuarantorRuleType, AppEnum.StandardReference.GuarantorRuleType);

            rblInclude.Items[1].Enabled = !(((RadComboBox)Helper.FindControlRecursive(Page, "cboGuarantorID")).SelectedValue == AppSession.Parameter.SelfGuarantor);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtAmountValue.Value = 0D;
                txtOutpatientAmountValue.Value = 0D;
                txtEmergencyAmountValue.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;

            ItemQuery item = new ItemQuery();
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where(item.ItemID == (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.ItemID));

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.ItemID);

            cboSRGuarantorRuleType.SelectedValue = (String)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType);
            txtAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.AmountValue));
            txtOutpatientAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue));
            txtEmergencyAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue));
            chkIsValueInPercent.Checked = (bool)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent);

            rblToGuarantor.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor) ? 0 : 1;

            rblInclude.SelectedIndex = (bool)DataBinder.Eval(DataItem, RegistrationItemRuleMetadata.ColumnNames.IsInclude) ? 0 : 1;

            tblInclude.Visible = rblInclude.SelectedIndex != 1;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                //RegistrationItemRuleCollection coll = (RegistrationItemRuleCollection)Session["collRegistrationItemRuleBilling" + Request.UserHostName];
                RegistrationItemRuleCollection coll = (RegistrationItemRuleCollection)Session["BillingVerification:collRegistrationItemRule" + Request.QueryString["regNo"]];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (BusinessObject.RegistrationItemRule item in coll)
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
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
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

        public Decimal OutpatientAmountValue
        {
            get { return Convert.ToDecimal(txtOutpatientAmountValue.Value); }
        }

        public Decimal EmergencyAmountValue
        {
            get { return Convert.ToDecimal(txtEmergencyAmountValue.Value); }
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

        #endregion

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");
            var transHd = new TransChargesQuery("b");
            var transDt = new TransChargesItemQuery("c");

            item.es.Top = 10;
            item.es.Distinct = true;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.InnerJoin(transDt).On
                (
                    item.ItemID == transDt.ItemID &&
                    transDt.IsApprove == true
                );
            item.InnerJoin(transHd).On
                (
                    transDt.TransactionNo == transHd.TransactionNo &&
                    transHd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])) &&
                    transHd.IsApproved == true
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        )
                );
            item.OrderBy(item.ItemID.Ascending);

            DataTable dtbt = item.LoadDataTable();

            if (dtbt.Rows.Count < 10)
            {
                var itemp = new ItemQuery("a");
                var presHd = new TransPrescriptionQuery("b");
                var presDt = new TransPrescriptionItemQuery("c");

                itemp.es.Top = 10;
                itemp.es.Distinct = true;
                itemp.Select
                    (
                        itemp.ItemID,
                        itemp.ItemName
                    );
                itemp.InnerJoin(presDt).On
                    (
                        itemp.ItemID == presDt.ItemID &&
                        presDt.IsApprove == true
                    );
                itemp.InnerJoin(presHd).On
                    (
                        presDt.PrescriptionNo == presHd.PrescriptionNo &&
                        presHd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])) &&
                        presHd.IsApproval == true
                    );
                itemp.Where
                    (
                        itemp.Or
                            (
                                itemp.ItemID.Like(searchTextContain),
                                itemp.ItemName.Like(searchTextContain)
                            )
                    );
                itemp.OrderBy(itemp.ItemName.Ascending);

                dtbt.Merge(itemp.LoadDataTable());
            }

            if (dtbt.Rows.Count < 10)
            {
                var itemp = new ItemQuery("a");
                var presHd = new TransPrescriptionQuery("b");
                var presDt = new TransPrescriptionItemQuery("c");

                itemp.es.Top = 10;
                itemp.es.Distinct = true;
                itemp.Select
                    (
                        itemp.ItemID,
                        itemp.ItemName
                    );
                itemp.InnerJoin(presDt).On
                    (
                        itemp.ItemID == presDt.ItemInterventionID &&
                        presDt.IsApprove == true
                    );
                itemp.InnerJoin(presHd).On
                    (
                        presDt.PrescriptionNo == presHd.PrescriptionNo &&
                        presHd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])) &&
                        presHd.IsApproval == true
                    );
                itemp.Where
                    (
                        itemp.Or
                            (
                                itemp.ItemID.Like(searchTextContain),
                                itemp.ItemName.Like(searchTextContain)
                            )
                    );
                itemp.OrderBy(itemp.ItemName.Ascending);

                dtbt.Merge(itemp.LoadDataTable());
            }

            var dtb = dtbt.DefaultView.ToTable(true, "ItemID", "ItemName");

            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblInclude.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0;
            txtOutpatientAmountValue.Value = 0D;
            txtEmergencyAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }
    }
}