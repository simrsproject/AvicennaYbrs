using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class RlReport5_4Detail : BaseUserControl
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

                return;
            }
            ViewState["IsNewRecord"] = false;

            var qDiag = new DiagnoseQuery();
            qDiag.Where(qDiag.DiagnoseID == (String)DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.DiagnosaID));
            DataTable dtb = qDiag.LoadDataTable();

            cboDiagnoseID.DataSource = dtb;
            cboDiagnoseID.DataBind();

            cboDiagnoseID.SelectedValue = (String)DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.DiagnosaID);
            txtKasusBaruL.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.KasusBaruL));
            txtKasusBaruP.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.KasusBaruP));
            txtJumlahKasusBaru.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.JumlahKasusBaru));
            txtJumlahKunjungan.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RlTxReport54Metadata.ColumnNames.JumlahKunjungan));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                RlTxReport54Collection coll =
                    (RlTxReport54Collection)Session["collRlTxReport54"];

                string sValue = cboDiagnoseID.SelectedValue;
                bool isExist = false;
                foreach (RlTxReport54 item in coll)
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

        public Decimal KasusBaruL
        {
            get { return Convert.ToDecimal(txtKasusBaruL.Value); }
        }

        public Decimal KasusBaruP
        {
            get { return Convert.ToDecimal(txtKasusBaruP.Value); }
        }

        public Decimal JumlahKasusBaru
        {
            get { return Convert.ToDecimal(txtJumlahKasusBaru.Value); }
        }

        public Decimal JumlahKunjungan
        {
            get { return Convert.ToDecimal(txtJumlahKunjungan.Value); }
        }

        #endregion
    }
}