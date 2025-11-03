using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipItemRedemptionDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadNumericTextBox TxtBalance
        {
            get
            { return (RadNumericTextBox)Helper.FindControlRecursive(Page, "txtBalance"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtQty.Value = 1;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ItemRedeemsRequested(cboItemReedemID, (String)DataBinder.Eval(DataItem, MembershipItemRedemptionItemMetadata.ColumnNames.ItemReedemID), true);
            PopulateItemRedeem(cboItemReedemID.SelectedValue);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MembershipItemRedemptionItemMetadata.ColumnNames.Qty));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (MembershipItemRedemptionItemCollection)Session["collMembershipItemRedemptionItem"];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                bool isExist = false;
                double totalPointsUsed = 0;
                foreach (MembershipItemRedemptionItem item in coll)
                {
                    if (item.ItemReedemID.Equals(cboItemReedemID.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }

                    totalPointsUsed += Convert.ToDouble(item.TotalPointsUsed ?? 0);
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", cboItemReedemID.Text);
                    return;
                }
                
                double remainingPoints = (TxtBalance.Value ?? 0) - totalPointsUsed;
                double totalPoint = (txtPointsUsed.Value ?? 0) * (txtQty.Value ?? 0);

                if (totalPoint > remainingPoints)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Insufficient points. Remaining points : " + string.Format("{0:n0}", remainingPoints));
                    return;
                }
            }
            else
            {
                double totalPointsUsed = 0;
                foreach (MembershipItemRedemptionItem item in coll)
                {
                    if (item.ItemReedemID != cboItemReedemID.SelectedValue)
                        totalPointsUsed += Convert.ToDouble(item.TotalPointsUsed ?? 0);
                }
                double remainingPoints = (TxtBalance.Value ?? 0) - totalPointsUsed;
                double totalPoint = (txtPointsUsed.Value ?? 0) * (txtQty.Value ?? 0);

                if (totalPoint > remainingPoints)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Insufficient points. Remaining points : " + string.Format("{0:n0}", remainingPoints));
                }
            }
        }

        private void PopulateItemRedeem(string id)
        {
            var itm = new MembershipItemRedeem();
            if (itm.LoadByPrimaryKey(id))
            {
                var itmgr = new AppStandardReferenceItem();
                if (itmgr.LoadByPrimaryKey(AppEnum.StandardReference.ItemReedemGroup.ToString(), itm.SRItemReedemGroup))
                    txtItemReedemGroup.Text = itmgr.ItemName;
                else txtItemReedemGroup.Text = string.Empty;
                txtPointsUsed.Value = Convert.ToDouble(itm.PointsUsed);
            }
            else
            {
                txtItemReedemGroup.Text = string.Empty;
                txtPointsUsed.Value = 0;
            }
        }

        protected void cboItemReedemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var itm = new MembershipItemRedeem();
            if (!itm.LoadByPrimaryKey(e.Value))
            {
                cboItemReedemID.Text = string.Empty;
                txtItemReedemGroup.Text = string.Empty;
                txtPointsUsed.Value = 0;
                return;
            }
            PopulateItemRedeem(e.Value);
        }

        protected void cboItemReedemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ItemRedeemsRequested((RadComboBox)o, e.Text, true);
        }

        private void ItemRedeemsRequested(RadComboBox comboBox, string textSearch, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new MembershipItemRedeemQuery("a");
            var itmgr = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(itmgr).On(itmgr.StandardReferenceID == AppEnum.StandardReference.ItemReedemGroup && itmgr.ItemID == query.SRItemReedemGroup);
            query.Select(query.ItemReedemID, query.ItemReedemName, itmgr.ItemName.As("ItemReedemGroup"));

            if (isNew)
            {
                query.Where
                  (
                      query.Or
                        (
                            query.ItemReedemID.Like(searchTextContain),
                            query.ItemReedemName.Like(searchTextContain)
                        ),
                      query.IsActive == true
                  );
            }
            else
            {
                query.Where(query.ItemReedemID == textSearch);
            }

            query.es.Top = 10;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemReedemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemReedemID"].ToString();
            }
        }

        protected void cboItemReedemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemReedemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemReedemID"].ToString();
        }

        #region Properties for return entry value

        public String ItemReedemID
        {
            get { return cboItemReedemID.SelectedValue; }
        }

        public String ItemReedemName
        {
            get { return cboItemReedemID.Text; }
        }

        public String ItemReedemGroup
        {
            get { return txtItemReedemGroup.Text; }
        }

        public decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }

        public decimal PointsUsed
        {
            get { return Convert.ToDecimal(txtPointsUsed.Value); }
        }

        public decimal TotalPointsUsed
        {
            get { return Convert.ToDecimal(txtPointsUsed.Value) * Convert.ToDecimal(txtQty.Value); }
        }

        #endregion
    }
}