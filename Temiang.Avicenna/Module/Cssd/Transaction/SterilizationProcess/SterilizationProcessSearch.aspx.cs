using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterilizationProcessSearch : BasePageDialog
    {
        private string IsDtt
        {
            get
            {
                return Request.QueryString["dtt"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = IsDtt == "0" ? AppConstant.Program.CssdSterilizationProcess : AppConstant.Program.CssdDttProcess;

            if (!IsPostBack)
            {
                pnlProcessType.Visible = IsDtt == "0";
                StandardReference.InitializeIncludeSpace(cboSRCssdProcessType, AppEnum.StandardReference.CssdProcessType);

                var machineColl = new CssdMachineCollection();
                machineColl.Query.Where(machineColl.Query.IsActive == true);
                machineColl.LoadAll();

                cboMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var machine in machineColl)
                {
                    cboMachineID.Items.Add(new RadComboBoxItem(machine.MachineName, machine.MachineID));
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdSterilizationProcessQuery("a");
            var machine = new CssdMachineQuery("b");
            var ptype = new AppStandardReferenceItemQuery("c");
            var usr = new AppUserQuery("d");

            query.Select
                (
                    query.ProcessNo,
                    query.ProcessDate,
                    query.ProcessStartTime,
                    query.ProcessEndTime,
                    query.MachineID,
                    machine.MachineName,
                    query.SRCssdProcessType,
                    ptype.ItemName.As("CssdProcessTypeName"),
                    query.OperatorByUserID,
                    usr.UserName.As("OperatorBy"),
                    @"<CASE WHEN ISNULL(a.ProcessTo, '') = '' THEN '' ELSE SUBSTRING(a.ProcessTo, 8, 4) END AS ProcessTo>",
                    query.IsApproved,
                    query.IsVoid
                );

            query.LeftJoin(machine).On(machine.MachineID == query.MachineID);
            query.LeftJoin(ptype).On(ptype.ItemID == query.SRCssdProcessType &&
                                      ptype.StandardReferenceID == AppEnum.StandardReference.CssdProcessType);
            query.InnerJoin(usr).On(usr.UserID == query.OperatorByUserID);
            if (IsDtt == "0")
            {
                query.Select("<'SterilizationProcessDetail.aspx?md=view&id='+a.ProcessNo+'&dtt=0' AS PUrl>");
                query.Where(query.IsDtt == false);
            }
            else
            {
                query.Select("<'SterilizationProcessDetail.aspx?md=view&id='+a.ProcessNo+'&dtt=1' AS PUrl>");
                query.Where(query.IsDtt == true);
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
            if (!string.IsNullOrEmpty(cboSRCssdProcessType.SelectedValue))
                query.Where(query.SRCssdProcessType == cboSRCssdProcessType.SelectedValue);
            if (!string.IsNullOrEmpty(cboMachineID.SelectedValue))
                query.Where(query.MachineID == cboMachineID.SelectedValue);

            query.OrderBy(query.ProcessDate.Descending, query.ProcessNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
