using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FilmConsumptionItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRFilmID, AppEnum.StandardReference.Film);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtQty.Value = 1D;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboSRFilmID.Enabled = false;
            cboSRFilmID.SelectedValue = (String)DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.Qty));
            txtKv.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.Kv));
            txtMa.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.Ma));
            txtS.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.S));
            txtMas.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransChargesItemFilmConsumptionMetadata.ColumnNames.Mas));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (TransChargesItemFilmConsumptionCollection)Session["collTransChargesItemFilmConsumption" + Request.UserHostName];

                string filmID = cboSRFilmID.SelectedValue;
                bool isExist = false;
                foreach (TransChargesItemFilmConsumption item in coll)
                {
                    if (item.SRFilmID.Equals(filmID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Film ID : {0} already exist", filmID);
                }
            }
        }

        #region Properties for return entry value

        public String SRFilmID
        {
            get { return cboSRFilmID.SelectedValue; }
        }

        public String FilmName
        {
            get { return cboSRFilmID.Text; }
        }

        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQty.Value); }
        }

        public Decimal Kv
        {
            get { return Convert.ToDecimal(txtKv.Value); }
        }

        public Decimal Ma
        {
            get { return Convert.ToDecimal(txtMa.Value); }
        }

        public Decimal S
        {
            get { return Convert.ToDecimal(txtS.Value); }
        }

        public Decimal Mas
        {
            get { return Convert.ToDecimal(txtMas.Value); }
        }

        #endregion
    }
}