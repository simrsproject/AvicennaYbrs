using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeByTeamDetail : BaseUserControl
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
                ComboBox.PopulateWithParamedic(cboParamedicMember);
                return;
            }
            ViewState["IsNewRecord"] = false;

            ComboBox.PopulateWithParamedic(cboParamedicMember);
            cboParamedicMember.SelectedValue = (string)DataBinder.Eval(DataItem, ParamedicFeeByTeamMetadata.ColumnNames.ParamedicMemberID);
            txtFeePercentage.Value = (double)DataBinder.Eval(DataItem, ParamedicFeeByTeamMetadata.ColumnNames.FeePercentage);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicFeeByTeamCollection)Session["collParamedicTeam"];

                string id = cboParamedicMember.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ParamedicMemberID.Equals(cboParamedicMember.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("member : {0} already exist", cboParamedicMember.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ParamedicMemberID
        {
            get { return cboParamedicMember.SelectedValue; }
        }

        public String ParamedicMemberName
        {
            get { return cboParamedicMember.Text; }
        }

        public Decimal FeePercentage
        {
            get { return Convert.ToDecimal(txtFeePercentage.Value); }
        }
        #endregion
    }
}