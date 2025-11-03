using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class WorkOrderSearch : BasePageDialog
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
            ProgramID = getPageID == "" ? AppConstant.Program.AssetWorkOrder : AppConstant.Program.SanitationActivityWorkOrder;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrder, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.AssetWorkOrderRealization, false);
                StandardReference.InitializeIncludeSpace(cboSRWorkType, AppEnum.StandardReference.WorkType);
                StandardReference.InitializeIncludeSpace(cboSRWorkStatus, AppEnum.StandardReference.WorkStatus);

                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "4"));
                cboStatus.Items.Add(new RadComboBoxItem("Preventive Maintenance", "5"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AssetWorkOrderQuery("a");
            var fromunit = new ServiceUnitQuery("b");
            var tounit = new ServiceUnitQuery("c");
            var wtype = new AppStandardReferenceItemQuery("d");
            var wstatus = new AppStandardReferenceItemQuery("e");
            var asset = new AssetQuery("f");
            var user = new AppUserServiceUnitQuery("g");

            query.Select
                (
                    query.OrderNo,
                    query.OrderDate,
                    query.RequiredDate,
                    fromunit.ServiceUnitName.As("FromServiceUnit"),
                    tounit.ServiceUnitName.As("ToServiceUnit"),
                    wtype.ItemName.As("WorkType"),
                    wstatus.ItemName.As("WorkStatus"),
                    asset.AssetName,
                    query.ProblemDescription,
                    query.IsPreventiveMaintenance,
                    query.IsApproved,
                    query.IsVoid
                );

            if (getPageID == "")
                query.Select(@"<'WorkOrderDetail.aspx?md=view&id='+a.OrderNo+'&type=' as WoUrl>");
            else
                query.Select(@"<'WorkOrderDetail.aspx?md=view&id='+a.OrderNo+'&type=sa' as WoUrl>");

            query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
            query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
            query.InnerJoin(wtype).On
                (
                    wtype.ItemID == query.SRWorkType &&
                    wtype.StandardReferenceID == "WorkType"
                );
            query.InnerJoin(wstatus).On
                (
                    wstatus.ItemID == query.SRWorkStatus &&
                    wstatus.StandardReferenceID == "WorkStatus"
                );
            query.LeftJoin(asset).On(asset.AssetID == query.AssetID);
            query.InnerJoin(user).On(user.ServiceUnitID == query.FromServiceUnitID & user.UserID == AppSession.UserLogin.UserID);

            if (getPageID == "")
                query.Where(query.IsSanitation == false);
            else
                query.Where(query.IsSanitation == true);

            if (!string.IsNullOrEmpty(txtOrderNo.Text))
            {
                if (cboFilterOrderNo.SelectedIndex == 1)
                    query.Where(query.OrderNo == txtOrderNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrderNo.Text);
                    query.Where(query.OrderNo.Like(searchTextContain));
                }
            }
            if (!txtFromOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToOrderDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.OrderDate >= txtFromOrderDate.SelectedDate, query.OrderDate < txtToOrderDate.SelectedDate.Value.AddDays(1));
            if (!txtFromRequiredDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToRequiredDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.RequiredDate >= txtFromRequiredDate.SelectedDate, query.RequiredDate < txtToRequiredDate.SelectedDate.Value.AddDays(1));
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRWorkType.SelectedValue))
                query.Where(query.SRWorkType == cboSRWorkType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRWorkStatus.SelectedValue))
                query.Where(query.SRWorkStatus == cboSRWorkStatus.SelectedValue);
            if (cboStatus.SelectedValue == "0")
                query.Where(query.IsApproved == false);
            if (cboStatus.SelectedValue == "1")
                query.Where(query.IsApproved == true);
            if (cboStatus.SelectedValue == "4")
                query.Where(query.IsVoid == true);
            if (cboStatus.SelectedValue == "5")
                query.Where(query.IsPreventiveMaintenance == true);

            query.OrderBy(query.OrderDate.Descending, query.OrderNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
