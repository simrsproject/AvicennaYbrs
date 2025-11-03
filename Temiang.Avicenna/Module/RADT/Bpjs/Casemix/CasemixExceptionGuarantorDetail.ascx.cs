using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixExceptionGuarantorDetail : BaseUserControl
    {

        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.Guarantor, txtGuarantorID);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtGuarantorID.Text = (String)DataBinder.Eval(DataItem, CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID);
            txtGuarantorID_TextChanged(null, null);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CasemixCoveredGuarantorCollection)Session["collCasemixCoveredGuarantor"];

                string guarId = txtGuarantorID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.GuarantorID.Equals(guarId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor ID : {0} {1} already exist", guarId, lblGuarantorName.Text);
                    return;
                }

                var coll2 = new CasemixCoveredGuarantorCollection();
                coll2.Query.Where(coll2.Query.GuarantorID == guarId);
                if (Request.QueryString["md"] != "new")
                    coll2.Query.Where(coll2.Query.CasemixCoveredID != Request.QueryString["id"].ToInt());
                coll2.LoadAll();
                if (coll2.Count > 0)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor ID : {0} {1} already set in other rule", guarId, lblGuarantorName.Text);
                }
            }
        }

        public string GuarantorID
        {
            get { return txtGuarantorID.Text; }
        }

        public string GuarantorName
        {
            get { return lblGuarantorName.Text; }
        }

        protected void txtGuarantorID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGuarantorID.Text)) lblGuarantorName.Text = string.Empty;
            else
            {
                var item = new Guarantor();
                if (item.LoadByPrimaryKey(txtGuarantorID.Text))
                    lblGuarantorName.Text = item.GuarantorName;
                else
                    lblGuarantorName.Text = string.Empty;
            }
        }
    }
}