using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master.Referralv2
{
    public partial class ReferralDetailItem : BaseUserControl
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
                txtReferralID.ReadOnly = (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralIdAutomatic) == "Yes");
                if (txtReferralID.ReadOnly)
                {
                    txtReferralName.Focus();
                    rfvReferralID.Visible = false;
                }

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtReferralID.ReadOnly = true;
            txtReferralID.Text = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ReferralID);
            txtReferralName.Text = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ReferralName);
            txtShortName.Text = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ShortName);
            txtDepartmentName.Text = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.DepartmentName);
            txtTaxRegistrationNo.Text = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.TaxRegistrationNo);
            chkIsPKP.Checked = (bool)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.IsPKP);
            AddressCtl1.StreetName = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.StreetName);
            AddressCtl1.District = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.District);
            AddressCtl1.City = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.City);
            AddressCtl1.County = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.County);
            AddressCtl1.State = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.State);

            var zipCode = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ZipCode);
            ZipCodeQuery zip = new ZipCodeQuery();

            //zip.es.Top = 1;
            //zip.Where(zip.Or(zip.ZipCode.Like("%" + zipCode + "%"), zip.ZipPostalCode.Like("%" + zipCode + "%")));
            //zip.OrderBy(zip.ZipPostalCode.Ascending, zip.ZipCode.Ascending);

            if (!string.IsNullOrEmpty(zipCode))
                zipCode = "?";
            zip.Where(zip.ZipCode == zipCode);

            var dtb = zip.LoadDataTable();
            AddressCtl1.ZipCodeCombo.DataSource = dtb;
            AddressCtl1.ZipCodeCombo.DataBind();
            if (dtb.Rows.Count > 0) AddressCtl1.ZipCodeCombo.SelectedValue = zipCode;

            //AddressCtl1.ZipCode = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ZipCode);
            AddressCtl1.PhoneNo = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.PhoneNo);
            AddressCtl1.FaxNo = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.FaxNo);
            AddressCtl1.Email = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.Email);
            AddressCtl1.MobilePhoneNo = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.MobilePhoneNo);
            chkIsRefferalFrom.Checked = (bool)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.IsRefferalFrom);
            chkIsRefferalTo.Checked = (bool)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.IsRefferalTo);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.IsActive);

            var parId = (String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ParamedicID);
            if (!string.IsNullOrEmpty(parId))
            {
                var pq = new ParamedicQuery();
                pq.Where(pq.ParamedicID == parId);
                cboParamedicID.DataSource = pq.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = parId;
            }

            var pCareValidation = ConfigurationManager.AppSettings["PCareValidation"];
            if (!string.IsNullOrEmpty(pCareValidation) && pCareValidation.ToUpper().Equals("YES"))
            {
                trPcare.Visible = true;

                object obj = Session["collPcare"];
                if (obj != null)
                {
                    var pcareMaps = (PCareReferenceItemMappingCollection)obj;
                    var pcareMap = pcareMaps.Where(r => r.MappingWithID == txtReferralID.Text).FirstOrDefault();
                    if (pcareMap != null)
                    {
                        txtPCareItemID.Text = pcareMap.ItemID;
                        PopulatePCareItemName(pcareMap.ItemID);
                    }
                }
                else
                    PopulatePCareMap((String)DataBinder.Eval(DataItem, ReferralMetadata.ColumnNames.ReferralID));
            }
            else
                trPcare.Visible = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ReferralCollection coll = (ReferralCollection)Session["collReferral"];

                string id = txtReferralID.Text;
                bool isExist = false;
                foreach (Referral item in coll)
                {
                    if (item.ReferralID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Referral ID : {0} already exist", id);
                    return;
                }

                var colls = new ReferralCollection();
                colls.Query.Where(colls.Query.ReferralID == id);
                colls.LoadAll();
                if (colls.Count > 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Referral ID : {0} already exist", id);
                    return;
                }
            }
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Where
                (
                    query.Or
                        (
                             query.ParamedicName.Like(searchTextContain),
                             query.ParamedicID.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);
            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();
        }

        #region Properties for return entry value



        public String ReferralID
        {
            get { return txtReferralID.Text; }
        }
        public String ReferralName
        {
            get { return txtReferralName.Text; }
        }

        public string ShortName
        {
            get { return txtShortName.Text; }
        }

        public string DepartmentName
        {
            get { return txtDepartmentName.Text; }
        }

        public string TaxRegistrationNo
        {
            get { return txtTaxRegistrationNo.Text; }
        }

        public Boolean IsPKP
        {
            get { return chkIsPKP.Checked; }
        }

        public string StreetName
        {
            get { return AddressCtl1.StreetName; }
        }

        public string District
        {
            get { return AddressCtl1.District; }
        }

        public string City
        {
            get { return AddressCtl1.City; }
        }

        public string County
        {
            get { return AddressCtl1.County; }
        }

        public string State
        {
            get { return AddressCtl1.State; }
        }

        public string ZipCode
        {
            get { return AddressCtl1.ZipCode; }
        }

        public string PhoneNo
        {
            get { return AddressCtl1.PhoneNo; }
        }

        public string FaxNo
        {
            get { return AddressCtl1.FaxNo; }
        }

        public string Email
        {
            get { return AddressCtl1.Email; }
        }

        public string MobilePhoneNo
        {
            get { return AddressCtl1.MobilePhoneNo; }
        }

        public string ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public Boolean IsRefferalFrom
        {
            get { return chkIsRefferalFrom.Checked; }
        }

        public Boolean IsRefferalTo
        {
            get { return chkIsRefferalTo.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public string PCareItemID
        {
            get { return txtPCareItemID.Text; }
        }
        #endregion

        #region PCare
        protected void txtPCareItemID_TextChanged(object sender, EventArgs e)
        {
            PopulatePCareItemName(txtPCareItemID.Text);
        }

        private void PopulatePCareItemName(string itemID)
        {
            lblPCareItemName.Text = string.Empty;
            if (string.IsNullOrEmpty(itemID))
                return;

            var stdi = new PCareReferenceItem();
            if (stdi.LoadByPrimaryKey("Provider", itemID))
                lblPCareItemName.Text = stdi.ItemName;
            else
                lblPCareItemName.Text = string.Empty;
        }

        private void PopulatePCareMap(string mappingWithId)
        {
            txtPCareItemID.Text = string.Empty;
            lblPCareItemName.Text = string.Empty;

            if (!string.IsNullOrEmpty(mappingWithId))
            {
                var pcareMap = new PCareReferenceItemMapping();
                if (pcareMap.LoadByPrimaryKey("Provider", mappingWithId))
                {
                    txtPCareItemID.Text = pcareMap.ItemID;

                    PopulatePCareItemName(pcareMap.ItemID);
                }
            }
        }
        #endregion
    }
}