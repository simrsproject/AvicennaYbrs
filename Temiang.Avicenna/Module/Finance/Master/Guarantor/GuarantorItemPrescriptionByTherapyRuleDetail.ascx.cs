using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemPrescriptionByTherapyRuleDetail : BaseUserControl
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

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtAmountValue.Value = 0D;
                txtAmountOPR.Value = 0D;
                txtAmountEMR.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var tgq = new AppStandardReferenceItemQuery();
            tgq.Where(tgq.StandardReferenceID == AppEnum.StandardReference.TherapyGroup, tgq.ItemID == (String)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup));
            cboSRTherapyGroup.DataSource = tgq.LoadDataTable();
            cboSRTherapyGroup.DataBind();
            cboSRTherapyGroup.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup);

            rblInclude.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsInclude) ? 0 : 1;
            cboSRGuarantorRuleType.SelectedValue = (String)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRGuarantorRuleType);
            txtAmountValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.AmountValue));
            txtAmountOPR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.OutpatientAmountValue) ?? 0);
            txtAmountEMR.Value = Convert.ToDouble(DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.EmergencyAmountValue) ?? 0);
            chkIsValueInPercent.Checked = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsValueInPercent);
            rblToGuarantor.SelectedIndex = (Boolean)DataBinder.Eval(DataItem, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsToGuarantor) ? 0 : 1;

            tblRuleType.Visible = rblInclude.SelectedIndex != 1;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                GuarantorItemPrescriptionByTherapyRuleCollection coll = (GuarantorItemPrescriptionByTherapyRuleCollection)Session["collGuarantorItemPrescriptionByTherapyRule"];

                string id = cboSRTherapyGroup.SelectedValue;
                bool isExist = false;
                foreach (GuarantorItemPrescriptionByTherapyRule item in coll)
                {
                    if (item.SRTherapyGroup.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Therapy Group: {0} has exist", cboSRTherapyGroup.Text);
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

        public String SRTherapyGroup
        {
            get { return cboSRTherapyGroup.SelectedValue; }
        }

        public String TherapyGroupName
        {
            get { return cboSRTherapyGroup.Text; }
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

        public Decimal OPRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountOPR.Value); }
        }

        public Decimal EMRAmountValue
        {
            get { return Convert.ToDecimal(txtAmountEMR.Value); }
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

        #region Method & Event TextChanged
        protected void cboSRTherapyGroup_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (query.StandardReferenceID == AppEnum.StandardReference.TherapyGroup, query.IsActive == true,
                 query.Or
                     (
                         query.ItemID.Like(searchTextContain),
                         query.ItemName.Like(searchTextContain)
                     )
                );
            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSRTherapyGroup.DataSource = dtb;
            cboSRTherapyGroup.DataBind();
        }

        protected void cboSRTherapyGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void rblInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblRuleType.Visible = rblInclude.SelectedIndex != 1;

            cboSRGuarantorRuleType.SelectedValue = string.Empty;
            txtAmountValue.Value = 0D;
            chkIsValueInPercent.Checked = false;
        }
        #endregion
    }
}