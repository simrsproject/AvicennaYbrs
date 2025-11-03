using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport.v2025

{
    public partial class RlReport4_2Detail : BaseUserControl
    {
        private object _dataItem2;

        public object DataItem
        {
            get { return _dataItem2; }
            set { _dataItem2 = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var qDiag = new DiagnoseQuery();
            qDiag.Where(qDiag.DiagnoseID == (String)DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.DiagnosaID));
            DataTable dtb = qDiag.LoadDataTable();

            cboDiagnoseID.DataSource = dtb;
            cboDiagnoseID.DataBind();

            cboDiagnoseID.SelectedValue = (String)DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.DiagnosaID);
            txtKeluarHidupL.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.KeluarHidupL));
            txtKeluarHidupP.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.KeluarHidupP));
            txtKeluarMatiL.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.KeluarMatiL));
            txtKeluarMatiP.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport42V2025Metadata.ColumnNames.KeluarMatiP));
        }
        
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            {
                RlTxReport42V2025Collection coll =
                    (RlTxReport42V2025Collection)Session["collRlTxReport42V2025"];

                string sValue = cboDiagnoseID.SelectedValue;
                bool isExist = false;
                foreach (RlTxReport42V2025 item in coll)
                {
                    if (item.DiagnosaID.Equals(sValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Kode ICD X: {0} has exist", sValue);
                }
            }
        }

        protected void cboDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        protected void cboDiagnoseID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DiagnoseQuery();

            query.es.Top = 10;
            query.Where
                (
                    query.Or
                        (
                            query.DiagnoseName.Like(searchTextContain),
                            query.DiagnoseID.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.DiagnoseName.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboDiagnoseID.DataSource = dtb;
            cboDiagnoseID.DataBind();
        }

        #region Properties for return entry value

        public String DiagnoseID
        {
            get { return cboDiagnoseID.SelectedValue; }
        }

        public String DiagnoseName
        {
            get { return cboDiagnoseID.Text; }
        }

        public Decimal KeluarHidupL
        {
            get { return Convert.ToDecimal(txtKeluarHidupL.Value); }
        }

        public Decimal KeluarHidupP
        {
            get { return Convert.ToDecimal(txtKeluarHidupP.Value); }
        }

        public Decimal KeluarMatiL
        {
            get { return Convert.ToDecimal(txtKeluarMatiL.Value); }
        }

        public Decimal KeluarMatiP
        {
            get { return Convert.ToDecimal(txtKeluarMatiP.Value); }
        }

        #endregion
    }
}