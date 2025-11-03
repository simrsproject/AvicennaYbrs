using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalAddressDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRAddressType, AppEnum.StandardReference.AddressType);
            StandardReference.InitializeIncludeSpace(cboSRState, AppEnum.StandardReference.Province);
            

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalAddressID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalAddressID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.PersonalAddressID));
            cboSRAddressType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.SRAddressType);
            txtAddress.Text = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.Address);

            try {
                var zc = new ZipCodeQuery();
                zc.Where(zc.ZipCode == (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.ZipCode));
                cboZipCode.DataSource = zc.LoadDataTable();
                cboZipCode.DataBind();
                cboZipCode.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.ZipCode);
            }
            catch (Exception exception)
            { 
            }

            txtDistrict.Text= (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.District);
            txtCounty.Text = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.County);
            txtCity.Text = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.City);
            cboSRState.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalAddressMetadata.ColumnNames.SRState);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalAddressCollection coll =
                    (PersonalAddressCollection)Session["collPersonalAddress" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string type = cboSRAddressType.SelectedValue;
                bool isExist = false;
                foreach (PersonalAddress item in coll)
                {
                    if (item.SRAddressType.Equals(type))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Address Type: {0} has exist", cboSRAddressType.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PersonalAddressID
        {
            get { return Convert.ToInt32(txtPersonalAddressID.Text); }
        }
        public String SRAddressType
        {
            get { return cboSRAddressType.SelectedValue; }
        }
        public String AddressTypeName
        {
            get { return cboSRAddressType.Text; }
        }
        public String Address
        {
            get { return txtAddress.Text; }
        }
        public String ZipCode
        {
            get { return cboZipCode.SelectedValue; }
        }
        public String ZipPortalCode
        {
            get { return cboZipCode.Text; }
        }
        public String District
        {
            get { return txtDistrict.Text; }
        }
        public String County
        {
            get { return txtCounty.Text; }
        }
        public String City
        {
            get { return txtCity.Text; }
        }
        public String SRState
        {
            get { return cboSRState.SelectedValue; }
        }
        public String StateName
        {
            get { return cboSRState.Text; }
        }
        public String SRCity
        {
            get { return string.Empty; }
        }
        #endregion
        #region Method & Event TextChanged
        protected void cboZipCode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var zip = new BusinessObject.ZipCode();
            if (zip.LoadByPrimaryKey(e.Value))
            {
                txtDistrict.Text = zip.District;
                txtCounty.Text = zip.County;
                txtCity.Text = zip.City;

                //txtDistrict.ReadOnly = true;
                //txtCounty.ReadOnly = true;
                //txtCity.ReadOnly = true;

                cboSRState.SelectedValue = zip.SRProvince;

                var item = new BusinessObject.AppStandardReferenceItem();
                if (item.LoadByPrimaryKey("Province", zip.SRProvince))
                {
                    cboSRState.SelectedValue = zip.SRProvince;
                    //cboSRState.Enabled = false;
                }
                else
                {
                    cboSRState.SelectedValue = string.Empty;
                    cboSRState.Text = string.Empty;
                    //cboSRState.Enabled = true;
                }
            }
            else
            {
                txtDistrict.Text = string.Empty;
                txtCounty.Text = string.Empty;
                txtCity.Text = string.Empty;
                cboSRState.Text = string.Empty;
                cboSRState.SelectedValue = string.Empty;
               
                //txtDistrict.ReadOnly = false;
                //txtCounty.ReadOnly = false;
                //txtCity.ReadOnly = false;
                //cboSRState.Enabled = true;
            }
        }

        protected void cboZipCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ZipPostalCode"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ZipCode"].ToString();
        }

        protected void cboZipCode_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BusinessObject.ZipCodeQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.ZipCode,
                    query.ZipPostalCode,
                    query.District,
                    query.County,
                    query.City
                );
            query.Where
                (
                    query.Or
                        (
                            query.ZipCode.Like(searchTextContain),
                            query.ZipPostalCode.Like(searchTextContain),
                            query.District.Like(searchTextContain),
                            query.County.Like(searchTextContain),
                            query.City.Like(searchTextContain)
                        )
                );

            cboZipCode.DataSource = query.LoadDataTable();
            cboZipCode.DataBind();
        }
        #endregion
    }
}
