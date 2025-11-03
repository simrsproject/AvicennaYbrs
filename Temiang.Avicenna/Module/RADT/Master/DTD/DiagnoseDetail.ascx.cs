using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DiagnoseDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Diagnose, txtDiagnoseID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtDiagnoseID.Text = (String)DataBinder.Eval(DataItem, DiagnoseMetadata.ColumnNames.DiagnoseID);
            txtDiagnoseName.Text = (String)DataBinder.Eval(DataItem, DiagnoseMetadata.ColumnNames.DiagnoseName);
            chkIsChronicDisease.Checked = (Boolean)DataBinder.Eval(DataItem, DiagnoseMetadata.ColumnNames.IsChronicDisease);
            chkIsDisease.Checked = (Boolean)DataBinder.Eval(DataItem, DiagnoseMetadata.ColumnNames.IsDisease);
            chkIsActive.Checked = (Boolean)DataBinder.Eval(DataItem, DiagnoseMetadata.ColumnNames.IsActive);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                DiagnoseCollection coll =
                    (DiagnoseCollection)Session["collDiagnose"];

                string diagnoseID = txtDiagnoseID.Text;
                bool isExist = false;
                foreach (Diagnose item in coll)
                {
                    if (item.DiagnoseID.Equals(diagnoseID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Diagnose ID: {0} has exist", diagnoseID);
                }
            }
        }

        #region Properties for return entry value

        public String DiagnoseID
        {
            get { return txtDiagnoseID.Text; }
        }

        public String DiagnoseName
        {
            get { return txtDiagnoseName.Text; }
        }

        public Boolean IsChronicDisease
        {
            get { return chkIsChronicDisease.Checked; }
        }

        public Boolean IsDisease
        {
            get { return chkIsDisease.Checked; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion

        protected void txtDiagnoseID_TextChanged(object sender, EventArgs e)
        {
            Diagnose d = new Diagnose();
            if (d.LoadByPrimaryKey(txtDiagnoseID.Text))
                txtDiagnoseName.Text = d.DiagnoseName;
            //else
            //{
            //    txtDiagnoseID.Text = string.Empty;
            //    txtDiagnoseName.Text = string.Empty;
            //}
        }
    }
}
