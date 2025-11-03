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
    public partial class CredentialingCompetencyAssessmentEvaluatorItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCompetencyAssessmentEvalRole, AppEnum.StandardReference.CompetencyAssessmentEvalRole);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboEvaluatorID.Enabled = false;
            
            if ((int)DataBinder.Eval(DataItem, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID) > 0)
            {
                PopulateCboEvaluatorID(cboEvaluatorID, (int)DataBinder.Eval(DataItem, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID));
                cboEvaluatorID.SelectedValue = DataBinder.Eval(DataItem, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.EvaluatorID).ToString();
            }
            cboSRCompetencyAssessmentEvalRole.SelectedValue = DataBinder.Eval(DataItem, CredentialCompetencyAssessmentEvaluatorMetadata.ColumnNames.SRCompetencyAssessmentEvalRole).ToString();
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboEvaluatorID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Evaluator Name.";
                return;
            }
            if (string.IsNullOrEmpty(cboSRCompetencyAssessmentEvalRole.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Role.";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (CredentialCompetencyAssessmentEvaluatorCollection)Session["collCredentialCompetencyAssessmentEvaluator" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                Int32 id = cboEvaluatorID.SelectedValue.ToInt();
                bool isExist1 = false;
                bool isExist2 = false;
                foreach (CredentialCompetencyAssessmentEvaluator item in coll)
                {
                    if (item.EvaluatorID.Equals(id))
                    {
                        isExist1 = true;
                        break;
                    }
                    if (item.SRCompetencyAssessmentEvalRole.Equals(cboSRCompetencyAssessmentEvalRole.SelectedValue))
                    {
                        isExist2 = true;
                        break;
                    }
                }
                if (isExist1 || isExist2)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = isExist1 == true ? string.Format("Evaluator Name : {0} has exist", cboEvaluatorID.Text) : string.Format("Role : {0} has exist", cboSRCompetencyAssessmentEvalRole.Text);
                }
            }
        }

        #region ComboBox
        protected void cboEvaluatorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
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
                query.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive,
                query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboEvaluatorID.DataSource = query.LoadDataTable();
            cboEvaluatorID.DataBind();
        }

        protected void cboEvaluatorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        private void PopulateCboEvaluatorID(RadComboBox comboBox, int personId)
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

        #region Properties for return entry value
        public Int32 EvaluatorID
        {
            get { return Convert.ToInt32(cboEvaluatorID.SelectedValue); }
        }
        public String EvaluatorName
        {
            get { return cboEvaluatorID.Text; }
        }
        public String SRCompetencyAssessmentEvalRole
        {
            get { return cboSRCompetencyAssessmentEvalRole.SelectedValue; }
        }
        public String CompetencyAssessmentEvalRoleName
        {
            get { return cboSRCompetencyAssessmentEvalRole.Text; }
        }
        #endregion
    }
}