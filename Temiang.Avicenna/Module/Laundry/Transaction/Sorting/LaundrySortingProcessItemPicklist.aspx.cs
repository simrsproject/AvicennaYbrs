using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundrySortingProcessItemPicklist : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                var retval = string.Empty;
                var p = new AppProgram();
                if (p.LoadByPrimaryKey("15.01.01"))
                {
                    string lastOne = p.NavigateUrl.Substring(p.NavigateUrl.Length - 1);
                    if (lastOne == "n")
                        retval = "n";
                }

                return retval;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (getPageID == "")
                {
                    trTypes.Visible = false;
                    rblTypes.SelectedIndex = 1;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.tno = '" + grdList.SelectedValue + "'";
        }

        private DataTable LaunderedProcesses
        {
            get
            {
                var query = new LaunderedProcessItemInfectiousQuery("q");
                var hq = new LaunderedProcessQuery("h");
                var rq = new LaundryReceivedItemInfectiousQuery("r");
                var rhq = new LaundryReceivedQuery("rh");
                var suq = new ServiceUnitQuery("su");
                var iq = new ItemLinenQuery("i");
                var iuq = new AppStandardReferenceItemQuery("iu");
                var machineq = new LaundryWashingMachineQuery("mc");
                var sortingq = new LaundrySortingProcessQuery("d");

                query.InnerJoin(hq).On(hq.ProcessNo == query.ProcessNo);
                query.InnerJoin(rq).On(rq.ReceivedNo == query.ReceivedNo && rq.ReceivedSeqNo == query.ReceivedSeqNo);
                query.InnerJoin(rhq).On(rhq.ReceivedNo == rq.ReceivedNo);
                query.InnerJoin(suq).On(suq.ServiceUnitID == rhq.FromServiceUnitID);
                query.InnerJoin(iq).On(iq.ItemID == rq.ItemID);
                query.InnerJoin(iuq).On(iuq.StandardReferenceID == "ItemUnit" && iuq.ItemID == query.SRItemUnit);
                query.InnerJoin(machineq).On(machineq.MachineID == hq.MachineID);
                query.LeftJoin(sortingq).On(sortingq.ProcessNo == query.ProcessNo && sortingq.ProcessSeqNo == query.ProcessSeqNo && sortingq.IsVoid == false);

                query.Select
                (
                    @"<q.ProcessNo + '|' + q.ProcessSeqNo AS ListKey>",
                    @"<q.ProcessNo + ' [ ' + CONVERT(VARCHAR(11), h.ProcessDate, 113) + ' ' + h.ProcessTime + ' | ' + mc.MachineName + ' ]' AS ListGroup>",

                    hq.ProcessDate,
                    hq.ProcessTime,
                    hq.MachineID,
                    machineq.MachineName,

                    rhq.FromServiceUnitID,
                    suq.ServiceUnitName.As("FromServiceUnitName"),
                    query.ProcessNo,
                    query.ProcessSeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,
                    rq.ItemID,
                    iq.ItemName,
                    query.Qty,
                    query.SRItemUnit,
                    iuq.ItemName.As("ItemUnit"),
                    rq.Notes
                );

                query.Where(
                    hq.IsApproved == true,
                    sortingq.TransactionNo.IsNull()
                    );

                if (getPageID == "")
                {
                    query.Where(hq.SRLaundryProcessType == "02");
                }
                else
                {
                    if (rblTypes.SelectedIndex == 0)
                        query.Where(hq.SRLaundryProcessType == "01");
                    else
                        query.Where(hq.SRLaundryProcessType == "02");
                }
                    
                if (!txtDate.IsEmpty)
                    query.Where(hq.ProcessDate == txtDate.SelectedDate);
                
                if (!string.IsNullOrEmpty(cboMachineID.SelectedValue))
                    query.Where(hq.MachineID == cboMachineID.SelectedValue);

                query.OrderBy(query.ProcessNo.Ascending, query.ProcessSeqNo.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = LaunderedProcesses;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}