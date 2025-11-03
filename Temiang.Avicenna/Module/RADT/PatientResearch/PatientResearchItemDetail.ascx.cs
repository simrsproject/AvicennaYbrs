using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientResearchItemDetail : BaseUserControl
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
                txtResearchID.Value = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtResearchID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.ResearchID));
            txtResearchTitle.Text = Convert.ToString(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.ResearchTitle));
            txtStartDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.StartDate));
            txtEndDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.EndDate));
            txtNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.Notes));

            var query = new ParamedicQuery("a");
            query.Where(query.ParamedicID == (String)DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.ParamedicID));
            DataTable tbl = query.LoadDataTable();
            cboParamedicID.DataSource = tbl;
            cboParamedicID.DataBind();

            cboParamedicID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, PatientResearchMetadata.ColumnNames.ParamedicID));

        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.Where(query.ParamedicName.Like(searchTextContain), 
                query.IsActive == true);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        #region Properties for return entry value

        public Int32 ResearchID
        {
            get { return Convert.ToInt32(txtResearchID.Value); }
        }
        public String ResearchTitle
        {
            get { return txtResearchTitle.Text; }
        }
        public DateTime StartDate
        {
            get { return Convert.ToDateTime(txtStartDate.SelectedDate); }
        }
        public DateTime EndDate
        {
            get { return Convert.ToDateTime(txtEndDate.SelectedDate); }
        }
        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }
        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion
    }
}