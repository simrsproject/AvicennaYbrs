using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class FluidBalanceSchemaInfusDetail : BaseUserControl
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

                var coll = (PatientFluidBalanceSchemaInfusCollection)Session["collSchemaInfus"];

                int lastNo = 0;
                foreach (PatientFluidBalanceSchemaInfus row in coll)
                {
                    if (row.SchemaInfusNo.ToInt()>lastNo)
                    {
                        lastNo = row.SchemaInfusNo.ToInt();
                    }
                }
                hdnSchemaInfusNo.Value = (lastNo + 1).ToString();
                return;
            }
            ViewState["IsNewRecord"] = false;

            hdnSchemaInfusNo.Value = Convert.ToString(DataBinder.Eval(DataItem, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusNo));
            txtSchemaInfusName.Text = Convert.ToString(DataBinder.Eval(DataItem, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.SchemaInfusName));
            txtQtyVolume.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyVolume));
            txtQtyPerHour.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PatientFluidBalanceSchemaInfusMetadata.ColumnNames.QtyPerHour));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientFluidBalanceSchemaInfusCollection)Session["collSchemaInfus"];

                string name = txtSchemaInfusName.Text;
                bool isExist = false;
                foreach (PatientFluidBalanceSchemaInfus row in coll)
                {
                    if (row.SchemaInfusName.Equals(name))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Schema Infus Name: {0} has exist", name);
                }
            }
        }

        #region Properties for return entry value
        public int SchemaInfusNo
        {
            get { return hdnSchemaInfusNo.Value.ToInt(); }
        }
        public String SchemaInfusName
        {
            get { return txtSchemaInfusName.Text; }
        }
        public decimal QtyVolume
        {
            get { return txtQtyVolume.Value.ToDecimal(); }
        }
        public decimal QtyPerHour
        {
            get { return txtQtyPerHour.Value.ToDecimal(); }
        }
        #endregion
    }
}