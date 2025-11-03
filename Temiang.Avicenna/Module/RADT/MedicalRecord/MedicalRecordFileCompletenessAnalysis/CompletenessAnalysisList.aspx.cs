using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileCompletenessAnalysis
{
    public partial class CompletenessAnalysisList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            ProgramID = AppConstant.Program.MedicalRecordFileCompletenessAnalysis;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient, query.IsActive == true);
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                txtDischargeDate.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        private DataTable Registrations
        {
            get
            {
                var qa = new RegistrationQuery("a");
                var qc = new PatientQuery("c");
                var qg = new ParamedicQuery("g");
                var qh = new ServiceUnitQuery("h");
                var qad = new MedicalRecordFileCompletenessQuery("i");
                var asr = new AppStandardReferenceItemQuery("j");

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                    (
                        qa.RegistrationNo,
                        qa.RegistrationDate,
                        qa.RegistrationTime,
                        qa.DischargeDate,
                        qa.DischargeTime,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qa.PatientID
                    );

                qa.InnerJoin(qc).On(qc.PatientID == qa.PatientID);
                qa.InnerJoin(qg).On(qg.ParamedicID == qa.ParamedicID);
                qa.InnerJoin(qh).On(qh.ServiceUnitID == qa.ServiceUnitID);
                qa.LeftJoin(qad).On(qad.RegistrationNo == qa.RegistrationNo);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & asr.ItemID == qc.SRSalutation);

                qa.Where(qa.SRRegistrationType == AppConstant.RegistrationType.InPatient);

                if (!txtDischargeDate.IsEmpty)
                    qa.Where(qa.DischargeDate == txtDischargeDate.SelectedDate);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qa.Where(
                    //    qa.Or(
                    //        qa.RegistrationNo == searchMedNo,
                    //        qc.MedicalNo == searchMedNo,
                    //        qc.OldMedicalNo == searchMedNo,
                    //        string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                    //        string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qa, qc, searchMedNo, "registration");
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qa.Where(qa.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    qa.Where(qa.ParamedicID == cboParamedicID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                qa.Where(qa.DischargeDate.IsNotNull(), qad.TransactionDate.IsNull());

                qa.OrderBy(qa.DischargeDate.Descending, qa.DischargeTime.Descending);

                DataTable tbl = qa.LoadDataTable();

                return tbl;
            }
        }

        protected void grdRegistration_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return;
            } 
            
            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void grdAnalysis_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = MedicalRecordFileAnalysis;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable MedicalRecordFileAnalysis
        {
            get
            {
                var qa = new MedicalRecordFileCompletenessQuery("a");
                var qb = new RegistrationQuery("b");
                var qc = new PatientQuery("c");
                var qg = new ParamedicQuery("g");
                var qh = new ServiceUnitQuery("h");
                var asr = new AppStandardReferenceItemQuery("j");

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                    (
                        qa.RegistrationNo,
                        
                        qb.RegistrationDate,
                        qb.RegistrationTime,
                        qb.DischargeDate,
                        qb.DischargeTime,
                        qa.TransactionDate,
                        qa.LastReturnDate,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qa.IsApproved,
                        qb.PatientID
                    );

                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.InnerJoin(qg).On(qb.ParamedicID == qg.ParamedicID);
                qa.InnerJoin(qh).On(qb.ServiceUnitID == qh.ServiceUnitID);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & qc.SRSalutation == asr.ItemID);

                if (!txtTransactionDate.IsEmpty)
                    qa.Where(qa.TransactionDate == txtTransactionDate.SelectedDate);

                if (!txtReturnDate.IsEmpty)
                    qa.Where(qa.LastReturnDate == txtReturnDate.SelectedDate);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtRegistrationNo.Text);
                    qa.Where(
                        qa.Or(
                            qa.RegistrationNo == searchMedNo,
                            qc.MedicalNo == searchMedNo,
                            qc.OldMedicalNo == searchMedNo,
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                            )
                        );
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qa.Where(qb.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    qa.Where(qb.ParamedicID == cboParamedicID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }


                qa.OrderBy(qa.TransactionDate.Descending);

                DataTable tbl = qa.LoadDataTable();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdRegistration.Rebind();
            grdAnalysis.Rebind();
        }
    }
}