using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalFamilyDetail : BaseUserControl
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
            StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);
            StandardReference.InitializeIncludeSpace(cboSRState, AppEnum.StandardReference.Province);
            //StandardReference.InitializeIncludeSpace(cboSRCity, AppEnum.StandardReference.City);
            StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.MaritalStatus);
            StandardReference.InitializeIncludeSpace(cboSRCoverageType, AppEnum.StandardReference.EmployeeCoverageType);
            StandardReference.InitializeIncludeSpace(cboSRFamilyOccupation, AppEnum.StandardReference.FamilyOccupation);

            var cs = new ClassCollection();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                
                cs.Query.Where(cs.Query.IsActive == true);
                if (cs.LoadAll())
                {
                    if (cboCoverageClass.Items.Count > 0) cboCoverageClass.Items.Clear();
                    if (cboCoverageClassBPJS.Items.Count > 0) cboCoverageClassBPJS.Items.Clear();

                    cboCoverageClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    foreach (var c in cs)
                    {
                        cboCoverageClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                        cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                    }
                }

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPersonalFamilyID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            if (cs.LoadAll())
            {
                if (cboCoverageClass.Items.Count > 0) cboCoverageClass.Items.Clear();
                if (cboCoverageClassBPJS.Items.Count > 0) cboCoverageClassBPJS.Items.Clear();

                cboCoverageClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var c in cs)
                {
                    cboCoverageClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                    cboCoverageClassBPJS.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }
            }

            txtPersonalFamilyID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.PersonalFamilyID));
            PopulateCboPatientID(cboPatientID, (String)DataBinder.Eval(DataItem, "PatientID"));
            cboSRFamilyRelation.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRFamilyRelation);
            txtFamilyName.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.FamilyName);
            txtPlaceBirth.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.CityOfBirth);
            txtDateBirth.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.DateBirth);
            cboSREducationLevel.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SREducationLevel);
            txtAddress.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.Address);
            cboSRState.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRState);
            //cboSRCity.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRCity);
            try
            {
                var zc = new ZipCodeQuery();
                zc.Where(zc.ZipCode == (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.ZipCode));
                cboZipCode.DataSource = zc.LoadDataTable();
                cboZipCode.DataBind();
                cboZipCode.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.ZipCode);
            }
            catch (Exception exception)
            {
            }

            txtDistrict.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.District);
            txtCounty.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.County);
            txtCity.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.City);

            txtPhone.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.Phone);
            cboSRMaritalStatus.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRMaritalStatus);

            var sex = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRGenderType);
            if (!string.IsNullOrEmpty(sex))
                rbtSex.SelectedValue = sex;

            PopulatePatientInformation(cboPatientID.SelectedValue, false);
            chkIsGuaranteed.Checked = (bool)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.IsGuaranteed);
            cboSRCoverageType.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRCoverageType);
            txtBpjsKesehatanNo.Text = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.BPJSKesehatanNo);
            cboSRFamilyOccupation.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.SRFamilyOccupation);
            object weddingDate = DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.WeddingDate);
            if (weddingDate != null)
                txtWeddingDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.WeddingDate);
            else
                txtWeddingDate.Clear();
            cboCoverageClass.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.CoverageClass);
            cboCoverageClassBPJS.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalFamilyMetadata.ColumnNames.CoverageClassBPJS);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalFamilyCollection coll =
                    (PersonalFamilyCollection)Session["collPersonalFamily" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                string id = txtFamilyName.Text;
                bool isExist = false;
                foreach (PersonalFamily item in coll)
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
        public Int32 PersonalFamilyID
        {
            get { return Convert.ToInt32(txtPersonalFamilyID.Text); }
        }
        public String PatientID
        {
            get { return cboPatientID.SelectedValue; }
        }
        public String MedicalNo
        {
            get { return txtMedicalNo.Text; }
        }
        public String SRFamilyRelation
        {
            get { return cboSRFamilyRelation.SelectedValue; }
        }
        public String FamilyRelationName
        {
            get { return cboSRFamilyRelation.Text; }
        }
        public String FamilyName
        {
            get { return txtFamilyName.Text; }
        }
        public String CityOfBirth
        {
            get { return txtPlaceBirth.Text; }
        }
        public DateTime DateBirth
        {
            get { return Convert.ToDateTime(txtDateBirth.SelectedDate); }
        }
        public String SREducationLevel
        {
            get { return cboSREducationLevel.SelectedValue; }
        }
        public String Address
        {
            get { return txtAddress.Text; }
        }
        public String SRState
        {
            get { return cboSRState.SelectedValue; }
        }
        public String SRCity
        {
            get { return string.Empty; }
        }
        public String ZipCode
        {
            get { return cboZipCode.SelectedValue; }
        }
        public String ZipPortalCode
        {
            get { return cboZipCode.Text; }
        }
        public String Phone
        {
            get { return txtPhone.Text; }
        }
        public String SRMaritalStatus
        {
            get { return cboSRMaritalStatus.SelectedValue; }
        }
        public String MaritalStatusName
        {
            get { return cboSRMaritalStatus.Text; }
        }
        public String SRGenderType
        {
            get { return rbtSex.SelectedValue; }
        }
        public String GenderTypeName
        {
            get { return rbtSex.SelectedValue == "M" ? "Male" : "Female"; }
        }
        public Boolean IsGuaranteed
        {
            get { return chkIsGuaranteed.Checked; }
        }
        public String SRCoverageType
        {
            get { return cboSRCoverageType.SelectedValue; }
        }
        public String CoverageTypeName
        {
            get { return cboSRCoverageType.Text; }
        }
        public String BpjsKesehatanNo
        {
            get { return txtBpjsKesehatanNo.Text; }
        }
        public DateTime? WeddingDate
        {
            get { return txtWeddingDate.SelectedDate; }
        }
        public String SRFamilyOccupation
        {
            get { return cboSRFamilyOccupation.SelectedValue; }
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
        public String CoverageClass
        {
            get { return cboCoverageClass.SelectedValue; }
        }
        public String CoverageClassName
        {
            get { return cboCoverageClass.Text; }
        }
        public String CoverageClassBpjs
        {
            get { return cboCoverageClassBPJS.SelectedValue; }
        }
        public String CoverageClassBpjsName
        {
            get { return cboCoverageClassBPJS.Text; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox ItemID
        protected void cboPatientID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        private void PopulateCboPatientID(RadComboBox comboBox, string textSearch)
        {
            var query = new PatientQuery("a");
            query.Where(query.PatientID == textSearch);
            query.Select(query.PatientID,
                query.MedicalNo,
                query.PatientName,
                query.Sex,
                query.Address,
                query.PhoneNo,
                query.MobilePhoneNo,
                query.DateOfBirth);

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PatientID"].ToString();
            }
        }
        
        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value, true);
        }

        private void PopulatePatientInformation(string patientId, bool isNew)
        {
            if (string.IsNullOrEmpty(patientId))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientId))
            {
                if (isNew)
                {
                    cboPatientID.SelectedValue = patient.PatientID;
                    cboPatientID.Text = patient.PatientID;
                    txtFamilyName.Text = patient.PatientName;
                    rbtSex.SelectedValue = patient.Sex;
                    txtDateBirth.SelectedDate = patient.DateOfBirth;
                }
                txtMedicalNo.Text = patient.MedicalNo;
            }
        }

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
