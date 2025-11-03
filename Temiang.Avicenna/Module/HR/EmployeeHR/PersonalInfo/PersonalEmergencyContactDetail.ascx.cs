using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalEmergencyContactDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSRFamilyRelation, AppEnum.StandardReference.FamilyRelation);
            StandardReference.InitializeIncludeSpace(cboSRState, AppEnum.StandardReference.Province);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalEmergencyContactID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPersonalEmergencyContactID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID));
            cboSRFamilyRelation.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.SRFamilyRelation);
            txtContactName.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.ContactName);
            txtAddress.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.Address);
            try
            {
                var zc = new ZipCodeQuery();
                zc.Where(zc.ZipCode == (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.ZipCode));
                cboZipCode.DataSource = zc.LoadDataTable();
                cboZipCode.DataBind();
                cboZipCode.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.ZipCode);
            }
            catch (Exception exception)
            {
            }

            txtDistrict.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.District);
            txtCounty.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.County);
            txtCity.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.City);
            cboSRState.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.SRState);
            txtPhone.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.Phone);
            txtMobile.Text = (String)DataBinder.Eval(DataItem, PersonalEmergencyContactMetadata.ColumnNames.Mobile);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalEmergencyContactCollection coll =
                    (PersonalEmergencyContactCollection)Session["collPersonalEmergencyContact" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtPersonalEmergencyContactID.Text;
                bool isExist = false;
                foreach (PersonalEmergencyContact item in coll)
                {
                    if (item.PersonID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 PersonalEmergencyContactID
        {
            get { return Convert.ToInt32(txtPersonalEmergencyContactID.Text); }
        }

        public String SRFamilyRelation
        {
            get { return cboSRFamilyRelation.SelectedValue; }
        }
        public String FamilyRelationName
        {
            get { return cboSRFamilyRelation.Text; }
        }
        public String ContactName
        {
            get { return txtContactName.Text; }
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
        public String Phone
        {
            get { return txtPhone.Text; }
        }
        public String Mobile
        {
            get { return txtMobile.Text; }
        }
        #endregion
        #region Method & Event TextChanged

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

                txtDistrict.ReadOnly = true;
                txtCounty.ReadOnly = true;
                txtCity.ReadOnly = true;

                cboSRState.SelectedValue = zip.SRProvince;

                var item = new BusinessObject.AppStandardReferenceItem();
                if (item.LoadByPrimaryKey("Province", zip.SRProvince))
                {
                    cboSRState.SelectedValue = zip.SRProvince;
                    cboSRState.Enabled = false;
                }
                else
                {
                    cboSRState.SelectedValue = string.Empty;
                    cboSRState.Text = string.Empty;
                    cboSRState.Enabled = true;
                }
            }
            else
            {
                txtDistrict.Text = string.Empty;
                txtCounty.Text = string.Empty;
                txtCity.Text = string.Empty;
                cboSRState.Text = string.Empty;

                txtDistrict.ReadOnly = false;
                txtCounty.ReadOnly = false;
                txtCity.ReadOnly = false;
                cboSRState.SelectedValue = string.Empty;
                cboSRState.Text = string.Empty;
                cboSRState.Enabled = true;
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