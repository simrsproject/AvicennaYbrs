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

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class BloodExterminationItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
            StandardReference.InitializeIncludeSpace(cboSRBloodGroup, AppEnum.StandardReference.BloodGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBagNo.Enabled = false;
            PopulateCboBagNo(cboBagNo, (String)DataBinder.Eval(DataItem, "BagNo"), false);
            cboBagNo.Text = (String)DataBinder.Eval(DataItem, "BagNo");
            cboBagNo.SelectedValue = (String)DataBinder.Eval(DataItem, BloodExterminationItemMetadata.ColumnNames.BagNo);
            cboSRBloodGroup.SelectedValue = (String)DataBinder.Eval(DataItem, BloodExterminationItemMetadata.ColumnNames.SRBloodGroup);
            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(cboBagNo.SelectedValue))
            {
                cboSRBloodType.SelectedValue = bagno.SRBloodType;
                rblBloodRhesus.SelectedValue = (bagno.BloodRhesus == "+" ? "0" : "1");
                txtVolumeBag.Value = Convert.ToDouble(bagno.VolumeBag);
                if (bagno.ExpiredDateTime != null)
                    txtExpiredDateTime.SelectedDate = bagno.ExpiredDateTime;
                else txtExpiredDateTime.Clear();
            }
            else
            {
                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
                txtVolumeBag.Value = 0;
                txtExpiredDateTime.Clear();
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BloodExterminationItemCollection)Session["collBloodExterminationItem"];

                string bagNo = cboBagNo.SelectedValue;
                bool isExist = false;

                foreach (BloodExterminationItem item in coll)
                {
                    if (item.BagNo.Equals(bagNo))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag No : {0} already exist", bagNo);
                }
            }
        }

        #region Properties for return entry value

        public String BagNo
        {
            get { return cboBagNo.SelectedValue; }
        }
        public string SrBloodType
        {
            get { return cboSRBloodType.SelectedValue; }
        }
        public string BloodType
        {
            get { return cboSRBloodType.Text; }
        }
        public string BloodRhesus
        {
            get { return rblBloodRhesus.SelectedValue; }
        }
        public string SrBloodGroup
        {
            get { return cboSRBloodGroup.SelectedValue; }
        }
        public string BloodGroup
        {
            get { return cboSRBloodGroup.Text; }
        }
        public decimal VolumeBag
        {
            get { return Convert.ToDecimal(txtVolumeBag.Value); }
        }
        public string ExpiredDate
        {
            get
            {
                if (txtExpiredDateTime.IsEmpty)
                    return string.Empty;
                return string.Format("{0:dd-MMM-yyyy HH:mm}", txtExpiredDateTime.SelectedDate);
            }
        }
        #endregion

        #region ComboBox
        protected void cboBagNo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboBagNo((RadComboBox)sender, e.Text, true);
        }
        private void PopulateCboBagNo(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new BloodBalanceQuery("a");
            var bagno = new BloodBagNoQuery("b");
            query.Select(query.BagNo, bagno.VolumeBag, bagno.ExpiredDateTime);
            query.InnerJoin(bagno).On(bagno.BagNo == query.BagNo);
            query.Where(
                query.BagNo.Like(searchTextContain));
            if (isNew)
                query.Where(query.Balance > 0, bagno.IsCrossMatching == false, bagno.IsExtermination == false);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboBagNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BagNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BagNo"].ToString();
        }
        protected void cboBagNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(e.Value))
            {
                cboSRBloodType.SelectedValue = bagno.SRBloodType;
                rblBloodRhesus.SelectedValue = (bagno.BloodRhesus == "-" ? "1" : "0");
                cboSRBloodGroup.SelectedValue = bagno.SRBloodGroup;
                txtVolumeBag.Value = Convert.ToDouble(bagno.VolumeBag);
                if (bagno.ExpiredDateTime != null)
                    txtExpiredDateTime.SelectedDate = bagno.ExpiredDateTime;
                else txtExpiredDateTime.Clear();
            }
            else
            {
                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
                cboSRBloodGroup.SelectedValue = string.Empty;
                cboSRBloodGroup.Text = string.Empty;
                txtVolumeBag.Value = 0;
                txtExpiredDateTime.Clear();
            }
        }
        #endregion
    }
}