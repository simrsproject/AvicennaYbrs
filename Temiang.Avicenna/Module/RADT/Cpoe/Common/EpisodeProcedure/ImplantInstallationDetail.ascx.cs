using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ImplantInstallationDetail : BaseUserControl
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

                var coll = (ImplantInstallationCollection)Session["collImplantInstallation" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["SeqNo" + Request.UserHostName] = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SeqNo).Select(c => c.SeqNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    ViewState["SeqNo" + Request.UserHostName] = string.Format("{0:000}", seqNo);
                }
                txtQty.Value = 1;

                return;
            }

            ViewState["IsNewRecord"] = false;

            ViewState["SeqNo" + Request.UserHostName] = DataBinder.Eval(DataItem, ImplantInstallationMetadata.ColumnNames.SeqNo);
            txtImplantType.Text = (String)DataBinder.Eval(DataItem, ImplantInstallationMetadata.ColumnNames.ImplantType);
            txtSerialNo.Text = (String)DataBinder.Eval(DataItem, ImplantInstallationMetadata.ColumnNames.SerialNo);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ImplantInstallationMetadata.ColumnNames.Qty));
            txtPlacementSite.Text = (String)DataBinder.Eval(DataItem, ImplantInstallationMetadata.ColumnNames.PlacementSite);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQty.Value < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Qty value can't less then 0";
                return;
            }
        }

        #region Properties for return entry value

        public String SeqNo
        {
            get { return (string)ViewState["SeqNo" + Request.UserHostName]; }
        }

        public String ImplantType
        {
            get { return txtImplantType.Text; }
        }

        public String SerialNo
        {
            get { return txtSerialNo.Text; }
        }

        public Int16 Qty
        {
            get { return Convert.ToInt16(txtQty.Value); }
        }

        public String PlacementSite
        {
            get { return txtPlacementSite.Text; }
        }

        #endregion
    }
}