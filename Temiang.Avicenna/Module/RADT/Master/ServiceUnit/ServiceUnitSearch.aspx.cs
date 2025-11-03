using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ServiceUnit;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ServiceUnitQuery("su");

            var qDept = new DepartmentQuery("d");
            query.InnerJoin(qDept).On(query.DepartmentID == qDept.DepartmentID);

            //var qLoc = new LocationQuery("c");
            //query.LeftJoin(qLoc).On(query.LocationID == qLoc.LocationID);

            var qSr = new AppStandardReferenceItemQuery("sr");
            query.LeftJoin(qSr).On(query.SRRegistrationType == qSr.ItemID & qSr.StandardReferenceID == string.Format("'{0}'", AppEnum.StandardReference.RegistrationType));

            query.Select
                (
                    query.DepartmentID,
                    qDept.DepartmentName,
                    query.ServiceUnitID,
                    query.ServiceUnitName,
                    query.ShortName,
                    query.ServiceUnitOfficer,
                    "<'' AS LocationName>",
                    query.SRRegistrationType,
                    qSr.ItemName.As("refToAppStandardReferenceItem_RegistrationType"),
                    query.IsUsingJobOrder,
                    query.IsPatientTransaction,
                    query.IsTransactionEntry,
                    query.IsActive
                );

            if (!string.IsNullOrEmpty(txtDepartmentID.Text))
            {
                if (cboFilterDepartmentID.SelectedIndex == 1)
                    query.Where(qDept.DepartmentName == txtDepartmentID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtDepartmentID.Text);
                    query.Where(qDept.DepartmentName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtServiceUnitID.Text))
            {
                if (cboFilterServiceUnitID.SelectedIndex == 1)
                    query.Where(query.ServiceUnitID == txtServiceUnitID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitID.Text);
                    query.Where(query.ServiceUnitID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtServiceUnitName.Text))
            {
                if (cboFilterServiceUnitName.SelectedIndex == 1)
                    query.Where(query.ServiceUnitName == txtServiceUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitName.Text);
                    query.Where(query.ServiceUnitName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ServiceUnitID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
