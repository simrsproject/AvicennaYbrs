using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Kemkes
{
    public partial class SitbList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.KemenkesSitb;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.InPatient),
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationDate1.SelectedDate = DateTime.Now.Date;
                txtRegistrationDate2.SelectedDate = DateTime.Now.Date;

                RestoreValueFromCookie();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var sitb = new KemenkesSitbQuery("b");
            var pat = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var epi = new EpisodeDiagnoseQuery("e");
            var diag = new DiagnoseQuery("f");
            var pmedic = new ParamedicQuery("g");

            reg.Select(
                reg.RegistrationDate,
                reg.RegistrationNo,
                pat.MedicalNo,
                pat.PatientName,
                unit.ServiceUnitName,
                pmedic.ParamedicName,
                diag.DiagnoseID,
                (diag.DiagnoseID + " " + diag.DiagnoseName).As("Diagnose"),
                sitb.SitbNo
                );
            reg.LeftJoin(sitb).On(reg.RegistrationNo == sitb.RegistrationNo);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.InnerJoin(pmedic).On(reg.ParamedicID == pmedic.ParamedicID);
            reg.InnerJoin(epi).On(reg.RegistrationNo == epi.RegistrationNo && epi.IsVoid == false);
            reg.InnerJoin(diag).On(epi.DiagnoseID == diag.DiagnoseID);

            reg.Where(epi.DiagnoseID.Substring(3).In(AppSession.Parameter.SitbDiagnoseList));
            if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
                reg.Where(reg.Or(reg.RegistrationNo == txtRegistrationNo.Text, pat.MedicalNo == txtRegistrationNo.Text));
            if (!string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                reg.Where(string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient));
            }
            if (!string.IsNullOrWhiteSpace(cboServiceUnitID.SelectedValue))
                reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!txtRegistrationDate1.IsEmpty && !txtRegistrationDate2.IsEmpty)
                reg.Where(reg.RegistrationDate.Date().Between(txtRegistrationDate1.SelectedDate.Value.Date, txtRegistrationDate2.SelectedDate.Value.Date));

            reg.OrderBy(unit.ServiceUnitName.Ascending, reg.RegistrationNo.Ascending);
            grdList.DataSource = reg.LoadDataTable();
        }
    }
}