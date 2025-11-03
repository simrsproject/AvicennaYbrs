using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class RecruitmentTestScoringEvaluatorDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private HiddenField HdnSRRecruitmentTest
        {
            get
            { return (HiddenField)Helper.FindControlRecursive(Page, "hdnSRRecruitmentTest"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            trScore.Visible = !AppSession.Parameter.RecruitmentTestInterview.Contains(HdnSRRecruitmentTest.Value);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtScore.Value = 0;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboEvaluatorID.Enabled = false;

            if ((int)DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID) > 0)
            {
                PopulateCboEvaluatorID(cboEvaluatorID, (int)DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID));
                cboEvaluatorID.SelectedValue = DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID) > 0)
            {
                PopulateCboPositionID(cboPositionID, (int)DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID));
                cboPositionID.SelectedValue = DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID).ToString();
            }
            txtScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboEvaluatorID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Evaluator Name.";
                return;
            }

            if (string.IsNullOrEmpty(cboPositionID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Invalid Position Name.";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (PersonalRecruitmentTestEvaluatorCollection)Session["collPersonalRecruitmentTestEvaluator" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                Int32 id = cboEvaluatorID.SelectedValue.ToInt();
                bool isExist = false;
                foreach (PersonalRecruitmentTestEvaluator item in coll)
                {
                    if (item.EvaluatorID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Evaluator Name : {0} has exist", cboEvaluatorID.Text);
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

        protected void cboEvaluatorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var employeeInfo = new VwEmployeeTable();
            var employeeInfoQ = new VwEmployeeTableQuery();
            employeeInfoQ.es.Top = 1;
            employeeInfoQ.Where(employeeInfoQ.PersonID == e.Value.ToInt());
            employeeInfo.Load(employeeInfoQ);
            if (employeeInfo != null)
            {
                var pos = new PositionQuery();
                pos.Where(pos.PositionID == employeeInfo.PositionID.ToInt());
                var posDtb = pos.LoadDataTable();
                if (posDtb.Rows.Count > 0)
                {
                    cboPositionID.DataSource = posDtb;
                    cboPositionID.DataBind();
                    cboPositionID.SelectedValue = employeeInfo.PositionID.ToString();
                }
                else
                {
                    cboPositionID.Items.Clear();
                    cboPositionID.SelectedValue = string.Empty;
                    cboPositionID.Text = string.Empty;
                }
            }
            else
            {
                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;
            }
        }

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PositionQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionName
                );

            query.Where(query.PositionName.Like(searchTextContain));

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString() ;
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        private void PopulateCboPositionID(RadComboBox comboBox, int id)
        {
            var query = new PositionQuery();
            query.Select
                (
                    query.PositionID,
                    query.PositionName
                );
            query.Where(query.PositionID == id);
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
        public Int32 PositionID
        {
            get { return Convert.ToInt32(cboPositionID.SelectedValue); }
        }
        public String PositionName
        {
            get { return cboPositionID.Text; }
        }
        public Decimal Score
        {
            get { return Convert.ToDecimal(txtScore.Value); }
        }
        #endregion
    }
}