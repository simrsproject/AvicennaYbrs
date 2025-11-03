using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingAttendanceDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSREmployeeTrainingRole, AppEnum.StandardReference.EmployeeTrainingRole);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeeTrainingHistoryID.Value = 1;
                chkIsAttending.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeTrainingHistoryID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.EmployeeTrainingHistoryID));

            var query = new VwEmployeeTableQuery();
            query.Select(
                query.PersonID,
                query.EmployeeNumber,
                query.EmployeeName
                );
            query.Where(query.PersonID == Convert.ToInt32(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.PersonID)));
            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.PersonID));
            cboSREmployeeTrainingRole.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.SREmployeeTrainingRole));
            chkIsAttending.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.IsAttending));
            txtNote.Text = (String)DataBinder.Eval(DataItem, EmployeeTrainingHistoryMetadata.ColumnNames.Note);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeTrainingHistoryCollection coll = (EmployeeTrainingHistoryCollection)Session["collEmployeeTrainingHistory" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string personId = cboPersonID.SelectedValue;
                bool isExist = false;
                foreach (EmployeeTrainingHistory item in coll)
                {
                    if (item.PersonID.ToString().Equals(personId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Employee: {0} has exist", cboPersonID.Text);
                }
            }
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.Select(
                query.PersonID,
                query.EmployeeNumber,
                query.EmployeeName
                );
            query.Where(
                query.Or(
                    query.EmployeeNumber.Like(searchTextContain),
                    query.EmployeeName.Like(searchTextContain)
                    )
                );
            if (Request.QueryString["type"] == "pps")
            {
                query.Where(query.Or(query.PersonID == AppSession.UserLogin.PersonID, 
                    query.SupervisorId == AppSession.UserLogin.PersonID, 
                    query.ManagerID == AppSession.UserLogin.PersonID));
            }

            (o as RadComboBox).DataSource = query.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        #region Properties for return entry value

        public Int32 EmployeeTrainingHistoryID
        {
            get { return Convert.ToInt32(txtEmployeeTrainingHistoryID.Value); }
        }

        public Int32 PersonID
        {
            get { return Convert.ToInt32(cboPersonID.SelectedValue); }
        }

        public String EmployeeName
        {
            get { return cboPersonID.Text; }
        }

        public bool IsAttending
        {
            get { return chkIsAttending.Checked; }
        }

        public String Note
        {
            get { return txtNote.Text; }
        }

        public String SREmployeeTrainingRole
        {
            get { return cboSREmployeeTrainingRole.SelectedValue; }
        }

        public String EmployeeTrainingRoleName
        {
            get { return cboSREmployeeTrainingRole.Text; }
        }
        #endregion
    }
}