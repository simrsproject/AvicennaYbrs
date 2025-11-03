using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class StockReceivedItemDetail : BaseUserControl
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
                txtVolumeBag.Value = 0;

                return;
            }
            ViewState["IsNewRecord"] = false;
            txtBagNo.ReadOnly = true;

            txtBagNo.Text = (String)DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.BagNo);
            cboSRBloodType.SelectedValue = (String)DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.SRBloodType);
            rblBloodRhesus.SelectedValue =
                (String) DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.SRBloodType) == "+" ? "0" : "1";
            cboSRBloodGroup.SelectedValue = (String)DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.SRBloodGroup);
            txtVolumeBag.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.VolumeBag));
            object expiredDate = DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime);
            if (expiredDate != null)
                txtExpiredDateTime.SelectedDate = (DateTime)DataBinder.Eval(DataItem, BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime);
            else
                txtExpiredDateTime.Clear();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (txtBagNo.Text.Contains("+"))//"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]"))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag no entered cannot contain punctuation +");
                    return;
                }
                if (txtBagNo.Text.Contains("&"))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag no entered cannot contain punctuation &");
                    return;
                }
                if (txtBagNo.Text.Contains("@"))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag no entered cannot contain punctuation @");
                    return;
                }
                if (txtBagNo.Text.Contains("#"))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag no entered cannot contain punctuation #");
                    return;
                }
                if (txtBagNo.Text.Contains("$"))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Bag no entered cannot contain punctuation $");
                    return;
                }

                var coll = (BloodReceivedItemCollection)Session["collBloodReceivedItem" + Request.UserHostName];

                string bagNo = txtBagNo.Text;
                bool isExist = false;

                foreach (BloodReceivedItem item in coll)
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
                        string.Format("Bag No : {0} already exist", txtBagNo.Text);
                    return;
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
            get { return txtBagNo.Text; }
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
            get { return rblBloodRhesus.SelectedItem == null ? string.Empty : rblBloodRhesus.SelectedItem.Text; }
        }
        public string SrBloodGroup
        {
            get { return cboSRBloodGroup.SelectedValue; }
        }
        public string BloodGroup
        {
            get { return cboSRBloodGroup.Text; }
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

        protected void txtBagNo_TextChanged(object sender, EventArgs e)
        {
            var bagno = new BloodBagNo();
            if (bagno.LoadByPrimaryKey(txtBagNo.Text))
            {
                cboSRBloodType.SelectedValue = bagno.SRBloodType;
                rblBloodRhesus.SelectedValue = (bagno.BloodRhesus == "-" ? "1" : "0");
                cboSRBloodGroup.SelectedValue = bagno.SRBloodGroup;
                txtVolumeBag.Value = Convert.ToDouble(bagno.VolumeBag);

                cboSRBloodType.Enabled = false;
                rblBloodRhesus.Enabled = false;
                cboSRBloodGroup.Enabled = false;
                txtVolumeBag.ReadOnly = true;

                if (bagno.ExpiredDateTime != null)
                    txtExpiredDateTime.SelectedDate = bagno.ExpiredDateTime;
                else txtExpiredDateTime.Clear();
            }
            else
            {
                cboSRBloodType.Enabled = true;
                rblBloodRhesus.Enabled = true;
                cboSRBloodGroup.Enabled = true;
                txtVolumeBag.ReadOnly = false;
                txtExpiredDateTime.Clear();
            }
        }
    }
}