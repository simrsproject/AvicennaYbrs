using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsParamedicFeeUsingPercentage.Checked = true;
                chkIsDeductionFeeUsePercentage.Checked = true;
                txtParamedicFeeAmount.Value = 0D;
                txtParamedicFeeAmountReferral.Value = 0D;
                txtDeductionFeeAmount.Value = 0D;
                txtDeductionFeeAmountReferral.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;

            ItemQuery item = new ItemQuery();
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where(item.ItemID == (String)DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.ItemID));

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.ItemID);

            txtParamedicFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmount));
            txtParamedicFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmountReferral));
            chkIsParamedicFeeUsingPercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            txtDeductionFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmount));
            txtDeductionFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmountReferral));
            chkIsDeductionFeeUsePercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemMetadata.ColumnNames.IsDeductionFeeUsePercentage);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ParamedicFeeItemCollection coll = (ParamedicFeeItemCollection)Session["collParamedicFeeItem"];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ParamedicFeeItem item in coll)
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
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} already exist", itemID);
                }
            }
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");
            var sr = new AppStandardReferenceItemQuery("b");
            item.InnerJoin(sr).On(item.SRItemType == sr.ItemID && sr.StandardReferenceID == "ItemType" &&
                                  sr.ReferenceID == "Service");
            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        ),
                    item.IsActive == true
                );
            item.OrderBy(item.ItemID.Ascending);

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
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

        public Boolean IsFeeUsingPercentage
        {
            get { return chkIsParamedicFeeUsingPercentage.Checked; }
        }

        public Decimal FeeAmount
        {
            get { return Convert.ToDecimal(txtParamedicFeeAmount.Value); }
        }

        public decimal FeeAmountReferral
        {
            get { return Convert.ToDecimal(txtParamedicFeeAmountReferral.Value); }
        }

        public Boolean IsDeductionFeeUsePercentage
        {
            get { return chkIsDeductionFeeUsePercentage.Checked; }
        }

        public Decimal DeductionFeeAmount
        {
            get { return Convert.ToDecimal(txtDeductionFeeAmount.Value); }
        }

        public decimal DeductionFeeAmountReferral
        {
            get { return Convert.ToDecimal(txtDeductionFeeAmountReferral.Value); }
        }

        #endregion
    }
}