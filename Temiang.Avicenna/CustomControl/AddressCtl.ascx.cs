using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.CustomControl
{
    public partial class AddressCtl : System.Web.UI.UserControl
    {
        public String StreetName
        {
            get { return txtStreetName.Text; }
            set { txtStreetName.Text = value; }
        }
        public RequiredFieldValidator RFVStreetName
        {
            get { return rfvStreetName; }
            set { rfvStreetName = value; }
        }

        public String ZipCode
        {
            get { return cboZipCode.SelectedValue; }
            set { cboZipCode.SelectedValue = value; }
        }
        public RadComboBox ZipCodeCombo
        {
            get { return cboZipCode; }
        }
        public RequiredFieldValidator RFVZipCode
        {
            get { return rfvZipCode; }
            set { rfvZipCode = value; }
        }

        public String District
        {
            get { return txtDistrict.Text; }
            set { txtDistrict.Text = value; }
        }
        public RadTextBox TxtDistrict
        {
            get { return txtDistrict; }
            set { txtDistrict = value; }
        }

        public String City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }
        public RadTextBox TxtCity
        {
            get { return txtCity; }
            set { txtCity = value; }
        }

        public String County
        {
            get { return txtCounty.Text; }
            set { txtCounty.Text = value; }
        }
        public RadTextBox TxtCounty
        {
            get { return txtCounty; }
            set { txtCounty = value; }
        }

        public String State
        {
            get { return txtState.Text; }
            set { txtState.Text = value; }
        }
        public RadTextBox TxtState
        {
            get { return txtState; }
            set { txtState = value; }
        }

        public String PhoneNo
        {
            get { return txtPhoneNo.Text; }
            set { txtPhoneNo.Text = value; }
        }
        public RequiredFieldValidator RFVPhoneNo
        {
            get { return rfvPhoneNo; }
            set { rfvPhoneNo = value; }
        }

        public String FaxNo
        {
            get { return txtFaxNo.Text; }
            set { txtFaxNo.Text = value; }
        }

        public String Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public String MobilePhoneNo
        {
            get { return txtMobilePhoneNo.Text; }
            set { txtMobilePhoneNo.Text = value; }
        }
        public RequiredFieldValidator RFVMobilePhoneNo
        {
            get { return rfvMobilePhoneNo; }
            set { rfvMobilePhoneNo = value; }
        }

        protected void cboZipCode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var zip = new BusinessObject.ZipCode();
            if (zip.LoadByPrimaryKey(e.Value))
            {
                txtDistrict.Text = zip.District;
                txtCounty.Text = zip.County;
                txtCity.Text = zip.City;

                txtDistrict.ReadOnly = true;
                txtCounty.ReadOnly = true;
                txtCity.ReadOnly = true;

                var item = new BusinessObject.AppStandardReferenceItem();
                if (item.LoadByPrimaryKey("Province", zip.SRProvince))
                {
                    txtState.Text = item.ItemName;
                    txtState.ReadOnly = true;
                }
                else
                {
                    txtState.Text = string.Empty;
                    txtState.ReadOnly = false;
                }
            }
            else
            {
                txtDistrict.Text = string.Empty;
                txtCounty.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;

                txtDistrict.ReadOnly = false;
                txtCounty.ReadOnly = false;
                txtCity.ReadOnly = false;
                txtState.ReadOnly = false;
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
    }
}