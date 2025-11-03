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
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class RecruitmentWrittenTestScoreDetail : BaseUserControl
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

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                ViewState["PersonalRecruitmentTestID"] = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;

            ViewState["PersonalRecruitmentTestID"] = Convert.ToInt32(DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID));
            cboEvaluatorID.Text = Convert.ToString(DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID));
            txtPositionID.Text = Convert.ToString(DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID));
            txtScore.Text = Convert.ToString(DataBinder.Eval(DataItem, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score));

        }

        

        #region Properties for return entry value

        public int PersonalRecruitmentTestID
        {
            get { return Convert.ToInt32(ViewState["PersonalRecruitmentTestID"]); }
        }


        public int? EvaluatorID
        {
            get
            {
                int value;
                int.TryParse(cboEvaluatorID.SelectedValue, out value);
                return value;
            }
        }

        public String EvaluatorName
        {
            get { return cboEvaluatorID.Text; }
        }

        public int? PositionID
        {
            get
            {
                int value;
                int.TryParse(txtPositionID.Text, out value);
                return value;
            }
        }

        public String PositionName
        {
            get { return txtPositionID.Text; }
        }


        public Decimal Score
        {
            get { return Convert.ToDecimal(txtScore.Value); }
        }
        #endregion



        protected void cboEvaluatorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateEvaluatorInformation(e.Value);
        }

        protected void cboEvaluatorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new VwEmployeeTableQuery ("a");
            var pos = new PositionQuery("b");

            query.InnerJoin(pos).On(query.PositionID == pos.PositionID);
            query.Select(query.PersonID, query.EmployeeName, pos.PositionID, pos.PositionName);

            query.OrderBy(query.PersonID.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboEvaluatorID.DataSource = dtb;
            cboEvaluatorID.DataBind();
        }

        protected void cboEvaluatorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }


        private void PopulateEvaluatorInformation(string PositionID)
        {
            if (string.IsNullOrEmpty(PositionID))
                return;

            var patient = new Position();
            if (patient.LoadByPrimaryKey(PositionID.ToInt()))
            {
                txtPositionID.Text = patient.PositionName;
            }
        }



    }
}
