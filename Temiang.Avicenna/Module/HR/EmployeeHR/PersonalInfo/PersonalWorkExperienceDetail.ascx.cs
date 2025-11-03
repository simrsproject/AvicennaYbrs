using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalWorkExperienceDetail : System.Web.UI.UserControl
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
			StandardReference.InitializeIncludeSpace(cboSRLineBisnis, AppEnum.StandardReference.LineBusiness);
            trDatePeriod.Visible = AppSession.Parameter.IsPersonalWorkExperienceUsingDatePeriod;
            trYearPeriod.Visible = !AppSession.Parameter.IsPersonalWorkExperienceUsingDatePeriod;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
				
				//TODO: Inisialisasi control untuk new row
				//misal --> chkIsActive.Checked = true;
                txtPersonalWorkExperienceID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;
			
			txtPersonalWorkExperienceID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID));
			cboSRLineBisnis.SelectedValue = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.SRLineBisnis);

            object startDate = DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.StartDate);
            if (startDate != null)
                txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.StartDate);
            else
                txtStartDate.Clear();

            object endDate = DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.EndDate);
            if (endDate != null)
                txtEndDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.EndDate);
            else
                txtEndDate.Clear();

            txtStartYear.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.StartYear);
            txtEndYear.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.EndYear);

            txtCompany.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.Company);		
			txtDivision.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.Division);		
			txtLocation.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.Location);		
			txtJobDesc.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.JobDesc);		
			txtSupervisorName.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.SupervisorName);		
			txtLastSalary.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.LastSalary));
			txtReasonOfLeaving.Text = (String)DataBinder.Eval(DataItem, PersonalWorkExperienceMetadata.ColumnNames.ReasonOfLeaving);		
    	}
		protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PersonalWorkExperienceCollection coll =
                    (PersonalWorkExperienceCollection) Session["collPersonalWorkExperience" + Request.UserHostName + PageId];
				
				//TODO: Betulkan cara pengecekannya
                string id = txtPersonalWorkExperienceID.Text;
                bool isExist = false;
                foreach (PersonalWorkExperience item in coll)
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
                    ((CustomValidator) source).ErrorMessage = string.Format("ID: {0} has exist", id);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtStartYear.Text))
            {
                int i;
                if (!Int32.TryParse(txtStartYear.Text, out i))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Invalid Year From");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtEndYear.Text))
            {
                int i;
                if (!Int32.TryParse(txtEndYear.Text, out i))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Invalid Year To");
                    return;
                }
            }
        }
		
		#region Properties for return entry value
	 	public Int32 PersonalWorkExperienceID
        {
            get { return Convert.ToInt32(txtPersonalWorkExperienceID.Text);}	 
	 	} 
	 	public String SRLineBisnis
        {
            get { return cboSRLineBisnis.SelectedValue;}		 
	 	}
        public String LineBisnisName
        {
            get { return cboSRLineBisnis.Text; }
        } 
	 	public DateTime? StartDate
        {
            get { return txtStartDate.SelectedDate;}	 
	 	} 
	 	public DateTime? EndDate
        {
            get { return txtEndDate.SelectedDate;}	 
	 	}
        public String StartYear
        {
            get { return txtStartYear.Text; }
        }
        public String EndYear
        {
            get { return txtEndYear.Text; }
        }
        public String Company
        {
            get { return txtCompany.Text;}	 
	 	} 
	 	public String Division
        {
            get { return txtDivision.Text;}	 
	 	} 
	 	public String Location
        {
            get { return txtLocation.Text;}	 
	 	} 
	 	public String JobDesc
        {
            get { return txtJobDesc.Text;}	 
	 	} 
	 	public String SupervisorName
        {
            get { return txtSupervisorName.Text;}	 
	 	} 
	 	public Decimal LastSalary
        {
            get { return Convert.ToDecimal(txtLastSalary.Value);}	 
	 	} 
	 	public String ReasonOfLeaving
        {
            get { return txtReasonOfLeaving.Text;}	 
	 	} 
        #endregion

		#region Method & Event TextChanged
		
		#endregion		
    }
    }
