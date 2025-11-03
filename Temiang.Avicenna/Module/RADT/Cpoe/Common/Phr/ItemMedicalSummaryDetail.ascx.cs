using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ItemMedicalSummaryDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (ExamSummaryResultCollection)Session["collExamSummaryResult" + Request.UserHostName];

                var sequenceNo = (coll.OrderByDescending(c => c.SequenceNo)
                                      .Select(c => c.SequenceNo)).Take(1).SingleOrDefault();
                if (sequenceNo == null)
                    ViewState["SequenceNo" + Request.UserHostName] = "001";
                else
                {
                    var seqNo = int.Parse(sequenceNo) + 1;
                    ViewState["SequenceNo" + Request.UserHostName] = string.Format("{0:000}", seqNo);
                }

                return;
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo" + Request.UserHostName] = DataBinder.Eval(DataItem, "SequenceNo");
            txtDescription.Text = (string)DataBinder.Eval(DataItem, "Description");
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
        }

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo" + Request.UserHostName]; }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }
    }
}