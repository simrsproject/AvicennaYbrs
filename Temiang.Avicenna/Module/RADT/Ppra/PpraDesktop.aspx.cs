using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using Google.Apis.Util;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using AppStandardReferenceItemQuery = Temiang.Avicenna.BusinessObject.AppStandardReferenceItemQuery;
using DateTime = System.DateTime;

namespace Temiang.Avicenna.Module.RADT.Ppra
{

    public partial class PpraDesktop : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Ppra;

            if (!IsPostBack)
            {
                //service unit
                PopulateServiceUnit();

                if (!string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
                {
                    ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
                }

                grdList.DataSource = string.Empty;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                // Month list
                for (int i = 1; i < 13; i++)
                    cboMonth.Items.Add(new RadComboBoxItem(Helper.MonthName(i), string.Format("{0:00}", i)));

                var nowYear = DateTime.Now.Year;
                for (int i = nowYear; i > nowYear - 15; i--)
                {
                    cboYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboYear.SelectedValue = nowYear.ToString();
                cboMonth.SelectedValue = DateTime.Now.ToString("MM");

                // Override with cookie value
                RestoreValueFromCookie();
            }
        }


        private DataTable LoadPatientRow()
        {

            var query = new RegistrationQuery("e");
            var unit = new ServiceUnitQuery("b");
            var room = new ServiceRoomQuery("c");
            var patient = new PatientQuery("p");
            var grr = new GuarantorQuery("g");
            var rg = new AppStandardReferenceItemQuery("rg");
            var sal = new AppStandardReferenceItemQuery("sal");

            var medic = new ParamedicQuery("medic");
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select
                (
                    room.RoomName,
                    query.RegistrationDate,
                    unit.ServiceUnitID,
                    query.ParamedicID,
                    medic.ParamedicName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    grr.GuarantorName,
                    query.PatientID,
                    query.IsConsul,
                    query.SRRegistrationType,
                    query.RoomID,
                    query.BedID,
                    query.RegistrationTime,
                    query.RegistrationQue,
                    query.FromRegistrationNo.Coalesce("''").As("FromRegistrationNo"),
                    patient.DateOfBirth,
                    rg.ItemName.As("ReferralGroupName"),
                    sal.ItemName.As("SalutationName"),
                    query.DischargeDate
                );

            query.LeftJoin(room).On(query.RoomID == room.RoomID);
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
            query.LeftJoin(rg).On(query.SRReferralGroup == rg.ItemID & rg.StandardReferenceID == "ReferralGroup");
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);

            query.Where(query.IsVoid == false);

            if (cboServiceUnitID.SelectedValue == string.Empty)
                query.Where(unit.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            else
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (txtRegistrationNo.Text.Contains("REG"))
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                    query.Where(patient.MedicalNo == txtRegistrationNo.Text);
            }

            if (txtPatientName.Text != string.Empty)
            {
                var searchPatient = "%" + txtPatientName.Text + "%";
                query.Where(string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient));
            }

            if (!chkIncludeNotClosed.Checked)
            {
                query.Where(query.IsClosed == true);
            }

            if (!chkIncludeNotDischarged.Checked)
            {
                query.Where(query.DischargeDate.IsNotNull());
            }

            // Subquery antibiotic prescription tx 
            var sqTpi = new TransPrescriptionItemQuery("tpi");
            var sqTp = new TransPrescriptionQuery("tp");
            sqTpi.InnerJoin(sqTp).On(sqTpi.PrescriptionNo == sqTp.PrescriptionNo);

            var sqIpm = new ItemProductMedicQuery("ipm");
            sqTpi.InnerJoin(sqIpm).On(sqTpi.ItemID == sqIpm.ItemID); // InterventionItemID abaikan karena kalau AB seharusnya gantinya AB juga

            sqTpi.es.Top = 1;
            sqTpi.Select(sqTp.RegistrationNo);

            var startDate = new DateTime(cboYear.Text.ToInt(), cboMonth.SelectedIndex + 1, 1).AddDays(-1);
            var endDate = new DateTime(cboYear.Text.ToInt(), cboMonth.SelectedIndex + 1, 1).AddMonths(1);
            sqTpi.Where(sqIpm.IsAntibiotic == true, sqTp.IsApproval == true,
                sqTp.PrescriptionDate > startDate,
                sqTp.PrescriptionDate < endDate);

            var abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);
            if (!string.IsNullOrWhiteSpace(abrForLine))
                sqTpi.Where(sqIpm.SRAntibioticLine == abrForLine);

            sqTpi.Where(sqTp.RegistrationNo == query.RegistrationNo);
            // End subquery

            query.Where(query.Exists(sqTpi));

            query.OrderBy
                (
                    query.RegistrationDate.Descending,
                    query.RegistrationTime.Ascending,
                    query.RegistrationNo.Descending
                );

            var dtbl = query.LoadDataTable();

            return dtbl;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                grdList.DataSource = LoadPatientRow();
            }
        }
        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
            SaveValueToCookie();
        }

        private void PopulateServiceUnit()
        {
            var units = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            query.Where(
                query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                query.IsActive == true
            );

            query.OrderBy(units.Query.ServiceUnitName.Ascending);
            units.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            if (!string.IsNullOrEmpty(AppSession.UserLogin.ServiceUnitID))
            {
                ComboBox.SelectedValue(cboServiceUnitID, AppSession.UserLogin.ServiceUnitID);
            }
        }


        //string _registrationNo = string.Empty;
        //string _patientID = string.Empty;
        //protected void grdList_OnItemDataBound(object sender, GridItemEventArgs e)
        //{

        //    if (e.Item is GridDataItem)
        //    {
        //        _registrationNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegistrationNo"]);
        //        _patientID = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PatientID"]);
        //    }

        //    if (e.Item is GridNestedViewItem)
        //    {
        //        // Populate Gyssens


        //        // Populate Raspro Form Hist
        //        var grdRasproForm = (RadGrid)e.Item.FindControl("grdRasproForm");
        //        InitializeCultureGrid(grdRasproForm); // Set date  format

        //        
        //        grdRasproForm.Rebind();

        //        _registrationNo = string.Empty; // reset
        //        _patientID = string.Empty;
        //    }
        //}


    }
}
