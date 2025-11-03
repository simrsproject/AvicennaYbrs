using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeVerificationByDischargeDateNotes : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var feeC = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                var feeQ = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
                feeQ.Where(feeQ.TransactionNo == Request.QueryString["trno"],
                           feeQ.SequenceNo == Request.QueryString["seqno"],
                           feeQ.TariffComponentID == Request.QueryString["compid"]);
                if (feeC.Load(feeQ)) {
                    txtNotes.Text = feeC.First().Notes ?? string.Empty;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (false)
            {
                ShowInformationHeader("Raise Error here.");
                return false;
            }
            //-- update notes
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(feeColl.Query.TransactionNo == Request.QueryString["trno"],
                            feeColl.Query.SequenceNo == Request.QueryString["seqno"],
                            feeColl.Query.TariffComponentID == Request.QueryString["compid"]);
            feeColl.LoadAll();
            foreach (var fee in feeColl)
            {
                fee.Notes = txtNotes.Text;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                feeColl.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
