using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NonPatientCustomerChargesSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.NonPatientCustomerCharges;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new TransChargesQuery("a");
            var unit = new ServiceUnitQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");
            var sal = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query.RegistrationNo,
                    reg.RegistrationDate,
                    query.TransactionNo,
                    unit.ServiceUnitName,
                    patient.PatientName,
                    query.FromServiceUnitID,
                    sal.ItemName.As("SalutationName")
                );

            query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
            query.InnerJoin(unit).On(query.FromServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && sal.ItemID == patient.SRSalutation);

            query.Where
           (
               reg.IsClosed == false,
               reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
               reg.IsHoldTransactionEntry == false,
               reg.IsVoid == false,
               reg.IsFromDispensary == false,
               reg.IsNonPatient == true
               //,
               //query.IsNonPatient == true
           );

      
            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (cboFilterRegistrationNo.SelectedIndex == 1)
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
                    query.Where(query.RegistrationNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtServiceUnit.Text))
            {
                if (cboFilterServiceUnit.SelectedIndex == 1)
                    query.Where(unit.ServiceUnitName == txtServiceUnit.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnit.Text);
                    query.Where(unit.ServiceUnitName.Like(searchTextContain));
                }
            }
 
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
