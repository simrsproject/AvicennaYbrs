using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "" ? AppConstant.Program.LaundererProcess : (getPageID == "i" ? AppConstant.Program.LaundererProcessInfectious : AppConstant.Program.LaundererProcessRewashing);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new LaunderedProcessQuery("a");
            var m = new LaundryWashingMachineQuery("b");
            var p = new AppStandardReferenceItemQuery("c");
            var t = new AppStandardReferenceItemQuery("d");

            query.LeftJoin(m).On(m.MachineID == query.MachineID);
            query.LeftJoin(p).On(p.StandardReferenceID == "LaundryProgram" && p.ItemID == query.SRLaundryProgram);
            query.LeftJoin(t).On(t.StandardReferenceID == "LaundryType" && t.ItemID == query.SRLaundryType);

            query.Select
                (
                    query.ProcessNo,
                    query.ProcessDate,
                    query.ProcessTime,
                    m.MachineName,
                    p.ItemName.As("LaundryProgramName"),
                    t.ItemName.As("LaundryTypeName"),
                    query.IsApproved,
                    query.IsVoid
                );

            if (getPageID == "")
            {
                query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=' AS PUrl>");
                query.Where(query.SRLaundryProcessType == "01");
            }
            else if (getPageID == "i")
            {
                query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=i' AS PUrl>");
                query.Where(query.SRLaundryProcessType == "02");
            }
            else
            {
                query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=r' AS PUrl>");
                query.Where(query.SRLaundryProcessType == "03");
            }

            if (!string.IsNullOrEmpty(txtProcessNo.Text))
            {
                if (cboFilterProcessNo.SelectedIndex == 1)
                    query.Where(query.ProcessNo == txtProcessNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtProcessNo.Text);
                    query.Where(query.ProcessNo.Like(searchTextContain));
                }
            }
            if (!txtProcessDate.IsEmpty)
                query.Where(query.ProcessDate == txtProcessDate.SelectedDate);

            query.OrderBy(query.ProcessDate.Descending, query.ProcessNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
