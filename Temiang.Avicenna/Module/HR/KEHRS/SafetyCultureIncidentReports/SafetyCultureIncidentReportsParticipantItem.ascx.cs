using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsParticipantItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtTransactionNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtTransactionNo");
            }
        }

        private RadComboBox CboVictimID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboPersonID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRParticipantStatus, AppEnum.StandardReference.ParticipantStatus);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            if ((int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.ParticipantPersonID) > 0)
            {
                PopulateCboPersonID(cboPersonID, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.ParticipantPersonID));
                cboPersonID.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.ParticipantPersonID).ToString();
            }
            cboSRParticipantStatus.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.SRParticipantStatus).ToString();
            txtNotes.Text = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.Notes).ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Participant Name required.");
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (EmployeeSafetyCultureIncidentReportsParticipantCollection)Session["collEmployeeSafetyCultureIncidentReportsParticipant" + Request.UserHostName];

                //bool isExist = false;
                var id = cboPersonID.SelectedValue.ToInt();

                //string id = cboLocationID.SelectedValue;
                bool isExist = coll.Any(item => item.ParticipantPersonID.Equals(id));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Participant: {0} has exist", cboPersonID.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 ParticipantPersonID
        {
            get { return Convert.ToInt32(cboPersonID.SelectedValue); }
        }

        public String ParticipantName
        {
            get { return cboPersonID.Text; }
        }

        public String SRParticipantStatus
        {
            get { return cboSRParticipantStatus.SelectedValue; }
        }

        public String ParticipantStatusName
        {
            get { return cboSRParticipantStatus.Text; }
        }

        public string Notes
        {
            get { return txtNotes.Text; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var subjects = (EmployeeSafetyCultureIncidentReportsSubjects.Select(i => i.SubjectPersonID)).Distinct();
            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where(
                query.PersonID != CboVictimID.SelectedValue.ToInt(),
                query.PersonID.NotIn(subjects),
                query.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive,
                query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
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
            var query = new VwEmployeeTableQuery();
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
        #endregion

        private EmployeeSafetyCultureIncidentReportsSubjectCollection EmployeeSafetyCultureIncidentReportsSubjects
        {
            get
            {
                var obj = Session["collEmployeeSafetyCultureIncidentReportsSubject" + Request.UserHostName];
                var coll = ((EmployeeSafetyCultureIncidentReportsSubjectCollection)(obj));
                coll.Where(c => c.TransactionNo == TxtTransactionNo.Text);
                return coll;
            }
        }
    }
}