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
    public partial class BloodTransformationItemDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRBloodGroupFrom, AppEnum.StandardReference.BloodGroup);
            StandardReference.InitializeIncludeSpace(cboSRBloodGroupTo, AppEnum.StandardReference.BloodGroup);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtVolumeBag.Value = 0;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBagNo.Enabled = false;
            PopulateCboBagNo(cboBagNo, (String)DataBinder.Eval(DataItem, "BagNo"), false);
            cboBagNo.Text = (String)DataBinder.Eval(DataItem, "BagNo");
            cboBagNo.SelectedValue = (String)DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.BagNo);
            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(cboBagNo.SelectedValue))
            {
                cboSRBloodType.SelectedValue = bagno.SRBloodType;
                rblBloodRhesus.SelectedValue = (bagno.BloodRhesus == "+" ? "0" : "1");
            }
            else
            {
                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
            }
            cboSRBloodGroupFrom.SelectedValue = (String)DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.SRBloodGroupFrom);
            cboSRBloodGroupTo.SelectedValue = (String)DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.SRBloodGroupTo);
            txtVolumeBag.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.VolumeBag));
            object expiredDate = DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime);
            if (expiredDate != null)
                txtExpiredDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime);
            else
                txtExpiredDateTime.Clear();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BloodTransformationItemCollection)Session["collBloodTransformationItem" + Request.UserHostName];

                string bagNo = cboBagNo.SelectedValue;
                bool isExist = false;

                foreach (BloodTransformationItem item in coll)
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
            if (txtExpiredDateTime.IsEmpty)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage =
                    string.Format("Blood ED required.");
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
        public string SrBloodGroupFrom
        {
            get { return cboSRBloodGroupFrom.SelectedValue; }
        }
        public string BloodGroupFrom
        {
            get { return cboSRBloodGroupFrom.Text; }
        }
        public string SrBloodGroupTo
        {
            get { return cboSRBloodGroupTo.SelectedValue; }
        }
        public string BloodGroupTo
        {
            get { return cboSRBloodGroupTo.Text; }
        }
        public Decimal VolumeBag
        {
            get { return Convert.ToDecimal(txtVolumeBag.Value); }
        }
        public DateTime? ExpiredDateTime
        {
            get { return txtExpiredDateTime.SelectedDate; }
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
            query.Select(query.BagNo, bagno.ExpiredDateTime);
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
                cboSRBloodGroupFrom.SelectedValue = bagno.SRBloodGroup;
                if (bagno.ExpiredDateTime != null)
                    txtExpiredDateTime.SelectedDate = bagno.ExpiredDateTime;
                else txtExpiredDateTime.Clear();
            }
            else
            {
                cboSRBloodType.SelectedValue = string.Empty;
                cboSRBloodType.Text = string.Empty;
                rblBloodRhesus.SelectedValue = "0";
                cboSRBloodGroupFrom.SelectedValue = string.Empty;
                cboSRBloodGroupFrom.Text = string.Empty;
                txtExpiredDateTime.Clear();
            }
        }
        #endregion
    }
}