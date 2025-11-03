using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class RiskManagementSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RiskManagement;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PatientIncidentQuery("a");
            var qsu = new ServiceUnitQuery("b");
            var qrefgr = new AppStandardReferenceItemQuery("f");

            query.InnerJoin(qsu).On(query.ServiceUnitIDInCharge == qsu.ServiceUnitID);
            query.LeftJoin(qrefgr).On(query.SRIncidentGroup == qrefgr.ItemID & qrefgr.StandardReferenceID == AppEnum.StandardReference.IncidentGroup.ToString());

            query.Where(query.IsRiskManagement == true);
            query.OrderBy
                (
                    query.IncidentDateTime.Descending
                );


            query.Select(
                        query.PatientIncidentNo,
                        query.IncidentDateTime,
                        
                        qsu.ServiceUnitName,
                        query.SRIncidentType,
                        query.SRIncidentGroup,
                        query.SRClinicalImpact,
                        query.SRIncidentFollowUp,
                        query.ReportingDateTime,
                        query.ReportedByUserID,
                        qrefgr.ItemName.As("IncidentGroupName"),
                        query.IsApproved,
                        query.IsVerified
                );

            if (!string.IsNullOrEmpty(txtPatientIncidentNo.Text))
            {
                if (cboFilterPatientIncidentNo.SelectedIndex == 1)
                    query.Where(query.PatientIncidentNo == txtPatientIncidentNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPatientIncidentNo.Text);
                    query.Where(query.PatientIncidentNo.Like(searchTextContain));
                }
            }
            if (!txtIncidentDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.IncidentDateTime.Date() == txtIncidentDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitIDInCharge == cboServiceUnitID.SelectedValue);
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
