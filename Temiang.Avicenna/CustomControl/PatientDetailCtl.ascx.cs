using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.CustomControl
{
    public partial class PatientDetailCtl : UserControl
    {
        public string RegistrationNo
        {
            get { return txtRegistrationNo.Text; }
            set { txtRegistrationNo.Text = value; }
        }

        public string MedicalNo
        {
            get { return txtMedicalNo.Text; }
            set { txtMedicalNo.Text = value; }
        }

        public string PatientName
        {
            get { return txtPatientName.Text; }
            set { txtPatientName.Text = value; }
        }

        public string ParamedicID
        {
            get { return txtParamedicID.Text; }
            set { txtParamedicID.Text = value; }
        }

        public string ParamedicName
        {
            get { return lblParamedicName.Text; }
            set { lblParamedicName.Text = value; }
        }

        public string DepartmentID
        {
            get { return txtDepartmentID.Text; }
            set { txtDepartmentID.Text = value; }
        }

        public string DepartmentName
        {
            get { return lblDepartmentName.Text; }
            set { lblDepartmentName.Text = value; }
        }

        public string ServiceUnitID
        {
            get { return txtServiceUnitID.Text; }
            set { txtServiceUnitID.Text = value; }
        }

        public string ServiceUnitName
        {
            get { return lblServiceUnitName.Text; }
            set { lblServiceUnitName.Text = value; }
        }

        public string RoomID
        {
            get { return txtRoomID.Text; }
            set { txtRoomID.Text = value; }
        }

        public string RoomName
        {
            get { return lblRoomName.Text; }
            set { lblRoomName.Text = value; }
        }

        public string BedID
        {
            get { return txtBedID.Text; }
            set { txtBedID.Text = value; }
        }

        public string Sex
        {
            get { return optSexMale.Checked ? "M" : "F"; }
            set
            {
                optSexMale.Checked = (value == "M");
                optSexMale.Enabled = (value == "M");
                optSexFemale.Checked = (value == "F");
                optSexFemale.Enabled = (value == "F");
            }
        }

        public double AgeYear
        {
            get { return txtAgeYear.Value.Value; }
            set { txtAgeYear.Value = value; }
        }

        public double AgeMonth
        {
            get { return txtAgeMonth.Value.Value; }
            set { txtAgeMonth.Value = value; }
        }

        public double AgeDay
        {
            get { return txtAgeDay.Value.Value; }
            set { txtAgeDay.Value = value; }
        }

        public RadComboBox GuarantorComboBox
        {
            get
            {
                return cboGuarantorID;
            }
        }

        public string GuarantorID
        {
            get { return cboGuarantorID.SelectedValue; }
            set { cboGuarantorID.SelectedValue = value; }
        }

        public string BusinessMethodName
        {
            get { return txtBusinessMethod.Text; }
            set { txtBusinessMethod.Text = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["edit"] == null)
                    cboGuarantorID.Enabled = true;
                else if (Request.QueryString["edit"] == "grr")
                {
                    cboGuarantorID.AutoPostBack = true;
                    cboGuarantorID.Enabled = true;
                    cboGuarantorID.EnableLoadOnDemand = true;
                    cboGuarantorID.HighlightTemplatedItems = true;
                    cboGuarantorID.MarkFirstMatch = true;
                    cboGuarantorID.NoWrap = true;

                    cboGuarantorID.ItemsRequested += cboGuarantorID_ItemsRequested;
                    cboGuarantorID.ItemDataBound += cboGuarantorID_ItemDataBound;
                    cboGuarantorID.SelectedIndexChanged += cboGuarantorID_SelectedIndexChanged;
                }
            }
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 10;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(e.Value);
            var std = new AppStandardReferenceItem();
            txtBusinessMethod.Text = std.LoadByPrimaryKey("BusinessMethod", grr.SRBusinessMethod) ? std.ItemName : string.Empty;
        }
    }
}