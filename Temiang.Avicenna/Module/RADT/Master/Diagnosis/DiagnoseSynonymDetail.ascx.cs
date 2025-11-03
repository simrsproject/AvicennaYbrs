using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DiagnoseSynonymDetail : BaseUserControl
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

                var coll = (DiagnoseSynonymCollection)Session["collDiagnoseSynonym"];
                if (coll.Count == 0) ViewState["SequenceNo"] = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }
                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, DiagnoseSynonymMetadata.ColumnNames.SequenceNo);
            txtSynonym.Text = (String)DataBinder.Eval(DataItem, DiagnoseSynonymMetadata.ColumnNames.SynonymText);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (DiagnoseSynonymCollection)Session["collDiagnoseSynonym"];

                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.SynonymText.Equals(txtSynonym.Text))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Synonym : {0} already exist", txtSynonym.Text);
                }
            }
        }

        #region Properties for return entry value
        public string SequenceNo
        {
            get { return ViewState["SequenceNo"].ToString(); }
        }
        public String SynonymText
        {
            get { return txtSynonym.Text; }
        }
        #endregion
    }
}