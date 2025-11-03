using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionBenchmarkDetail : BaseUserControl
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
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtBenchmarkName.Text = (String)DataBinder.Eval(DataItem, PositionBenchmarkMetadata.ColumnNames.BenchmarkName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionBenchmarkMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionBenchmarkCollection coll = (PositionBenchmarkCollection)Session["collPositionBenchmark"];

                //TODO: Betulkan cara pengecekannya
                string benchmarkName = txtBenchmarkName.Text;
                bool isExist = false;
                foreach (PositionBenchmark item in coll)
                {
                    if (item.BenchmarkName.Equals(BenchmarkName))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Benchmark Name: {0} has exist", benchmarkName);
                }
            }
        }

        #region Properties for return entry value
        public String BenchmarkName
        {
            get { return txtBenchmarkName.Text; }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }
        #endregion
    }
}