using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Credential.Process
{
    public partial class CredentialingTeam : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCredentialingTeam, AppEnum.StandardReference.CredentialingTeamPosition);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            if ((int)DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PersonID) > 0)
            {
                PopulateCboPersonID(cboPersonID, (int)DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PersonID));
                cboPersonID.SelectedValue = DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PersonID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PositionID) > 0)
            {
                PopulateCboPositionID(cboPositionID, (int)DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PositionID));
                cboPositionID.SelectedValue = DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.PositionID).ToString();
            }
            cboSRCredentialingTeam.SelectedValue = DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.SRCredentialingTeamPosition).ToString();
            txtAreasOfExpertise.Text = DataBinder.Eval(DataItem, CredentialProcessTeamMetadata.ColumnNames.AreasOfExpertise).ToString();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (CredentialProcessTeamCollection)Session["collCredentialProcessTeam" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                Int32 id = cboPersonID.SelectedValue.ToInt();
                bool isExist = false;
                foreach (CredentialProcessTeam item in coll)
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
                    ((CustomValidator)source).ErrorMessage = string.Format("Team Member : {0} has exist", cboPersonID.Text);
                }
            }
        }

        #region ComboBox
        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboPositionID.Items.Clear();
            cboPositionID.SelectedValue = string.Empty;
            cboPositionID.Text = string.Empty;

            var emp = new EmployeePosition();
            var empq = new EmployeePositionQuery();
            empq.Where(empq.PersonID == e.Value.ToInt(), empq.IsPrimaryPosition == true);
            emp.Load(empq);
            if (emp != null)
            {
                if (emp.PositionID > 0)
                {
                    PopulateCboPositionID(cboPositionID, emp.PositionID.ToInt());
                    cboPositionID.SelectedValue = emp.PositionID.ToString();
                }
            }
        }
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PersonalInfoQuery("a");
            var ewi = new EmployeeWorkingInfoQuery("b");
            query.InnerJoin(ewi).On(ewi.PersonID == query.PersonID);
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where(
                query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            string.Format("< OR LTRIM(a.PreTitle + ' ') + RTRIM(a.FirstName + ' ' + a.MiddleName) + RTRIM(' ' + a.LastName) + RTRIM(' ' + a.PostTitle) LIKE '{0}'>", searchTextContain)
                        ),
                ewi.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        private void PopulateCboPersonID(RadComboBox comboBox, int personId)
        {
            var query = new PersonalInfoQuery();
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where(query.PersonID == personId);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        private void PopulateCboPositionID(RadComboBox comboBox, int textSearch)
        {
            var query = new PositionQuery();

            query.Where(query.PositionID == textSearch);

            query.Select(query.PositionID, query.PositionCode, query.PositionName);
            query.OrderBy(query.PositionCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }
        #endregion

        #region Properties for return entry value
        public Int32 PersonID
        {
            get { return Convert.ToInt32(cboPersonID.SelectedValue); }
        }
        public String TeamMemberName
        {
            get { return cboPersonID.Text; }
        }
        public Int32 PositionID
        {
            get 
            {
                if (string.IsNullOrEmpty(cboPositionID.SelectedValue))
                    return -1;
                return Convert.ToInt32(cboPositionID.SelectedValue); 
            }
        }
        public String PositionName
        {
            get { return cboPositionID.Text; }
        }
        public String SRCredentialingTeam
        {
            get { return cboSRCredentialingTeam.SelectedValue; }
        }
        public String CredentialingTeamName
        {
            get { return cboSRCredentialingTeam.Text; }
        }
        public String AreasOfExpertise
        {
            get { return txtAreasOfExpertise.Text; }
        }
        #endregion
    }
}