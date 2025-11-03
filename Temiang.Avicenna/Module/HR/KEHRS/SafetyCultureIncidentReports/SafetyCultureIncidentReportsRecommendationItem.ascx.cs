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

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsRecommendationItem : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (EmployeeSafetyCultureIncidentReportsRecommendationCollection)Session["collEmployeeSafetyCultureIncidentReportsRecommendation" + Request.UserHostName];
                if (coll.Count == 0)
                    hdnSequenceNo.Value = "001";
                else
                {
                    var seqNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int id = Convert.ToInt32(seqNo.Single()) + 1;

                    hdnSequenceNo.Value = string.Format("{0:000}", id);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            hdnSequenceNo.Value = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsRecommendationMetadata.ColumnNames.SequenceNo).ToString();
            txtRecommendation.Text = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsRecommendationMetadata.ColumnNames.Recommendation).ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public string SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public string Recommendation
        {
            get { return txtRecommendation.Text; }
        }
    }
}