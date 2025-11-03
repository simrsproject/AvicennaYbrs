using System.Linq;
using Telerik.Reporting;
using System.Data;
using System;
using System.Text;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration
{
    using BusinessObject;
    //using Temiang.Avicenna.Common;

    public partial class ResumeMedisRawatInap : Report
    {
        public ResumeMedisRawatInap(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoAndTextBottom(this.reportHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("p_RegistrationNo").ValueString;

            string registrationNoIGD = string.Empty;
            string registrationNoOP = string.Empty;
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);


            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtPatientName.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;

            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);


            txtTglMasuk.Value = string.Format("{0:dd/MMM/yyyy}", reg.RegistrationDate);


            var medsum = new MedicalDischargeSummary();
            if (!medsum.LoadByPrimaryKey(registrationNo)) return;

            txtTglPulang.Value = string.Format("{0:dd/MMM/yyyy}", medsum.DischargeDate.Value);

            txtIndikasi.Value = medsum.TreatmentIndications;
            txtChiefComplaint.Value = medsum.ChiefComplaint;
            txtAnamnesis.Value = medsum.HistOfPresentIllness;
            txtKomorbiditas.Value = medsum.PastMedicalHistory;
            txtFisik.Value = medsum.PhysicalExam;
            txtPenunjang.Value = medsum.AncillaryExam;
            txtMedicalProcedures.Value = medsum.MedicalProcedures;

            txtKdICD9.Value = medsum.ProcedureID;
            txtProceduresName.Value = medsum.ProcedureName;

            txtICD101.Value = medsum.FinalDiagnoseID1;
            txtICD101Name.Value = medsum.FinalDiagnoseName1;
            txtICD102.Value = medsum.FinalDiagnoseID2;
            txtICD102Name.Value = medsum.FinalDiagnoseName2;
            txtICD103.Value = medsum.FinalDiagnoseID3;
            txtICD103Name.Value = medsum.FinalDiagnoseName3;

            txtMedications.Value = medsum.Medications;

            var ps = new AppStandardReferenceItem();
            ps.LoadByPrimaryKey("DischargeCondition", medsum.SRDischargeCondition);
            txtPresentStatus.Value = ps.ItemName;

            txtSuggestionFollowUp.Value = medsum.SuggestionFollowUp;

            var srDischMethod = new AppStandardReferenceItem();
            if (srDischMethod.LoadByPrimaryKey("DischargeMethod", medsum.SRDischargeMethod))
            {
                txtAlasanPulang.Value = srDischMethod.ItemName;
            }

            PopulateHomePrescription(registrationNo);

            // dokter
            if (!string.IsNullOrEmpty(medsum.ParamedicName))
            {
                txtParamedicName.Value = medsum.ParamedicName;
            }
            else
            {
                var parID = string.IsNullOrEmpty(medsum.ParamedicID) ? reg.ParamedicID : medsum.ParamedicID;
                var dktr = new Paramedic();
                if (dktr.LoadByPrimaryKey(parID))
                {
                    txtParamedicName.Value = dktr.ParamedicName;
                }
            }
        }

        private void PopulateHomePrescription(string registrationNo)
        {
            var mr = new MedicationReceiveQuery("mr");
            var cm = new ConsumeMethodQuery("cm");
            mr.InnerJoin(cm).On(mr.SRConsumeMethod == cm.SRConsumeMethod);
            mr.Where(mr.RegistrationNo == registrationNo, mr.IsBroughtHome == true);
            mr.Select(mr.ItemDescription, cm.SRConsumeMethodName, mr.ConsumeQty, mr.SRConsumeUnit, mr.Note, mr.RefTransactionNo, mr.RefSequenceNo);
            mr.OrderBy(mr.MedicationReceiveNo.Descending);
            var dtb = mr.LoadDataTable();
            dtb.Columns.Add("ConsumeMethod", typeof(string));

            foreach (DataRow row in dtb.Rows)
            {
                row["ConsumeMethod"] = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"],
                    row["SRConsumeUnit"]);

                if (row["RefTransactionNo"] != DBNull.Value && !string.IsNullOrEmpty((row["RefTransactionNo"]).ToString()))
                {
                    // Ambil ulang item descriptionnya spy tanpa format HTML
                    row["ItemDescription"] = ItemDescription(row["RefTransactionNo"].ToString(),
                        row["RefSequenceNo"].ToString());
                }
            }

            tblHomePrescription.DataSource = dtb;
        }

        private string ItemDescription(string prescriptionNo, string sequenceNo)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");

            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

            query.Select
            (
                query.ItemInterventionID,
                query.IsCompound,
                qItem.ItemName,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
            );

            query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0 && dtb.Rows[0]["IsCompound"].ToBoolean() == true)
            {
                // Racikan
                query = new TransPrescriptionItemQuery("a");
                qItem = new ItemQuery("b");
                qItemMedic = new ItemProductMedicQuery("im");
                qItemIntervention = new ItemQuery("c");

                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
                query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

                query.Select
                (
                    query.ItemInterventionID, query.ParentNo, query.IsRFlag,
                    qItem.ItemName, query.DosageQty, query.SRDosageUnit,
                    qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
                );

                query.Where(query.PrescriptionNo == prescriptionNo, query.Or(query.SequenceNo == sequenceNo, query.ParentNo == sequenceNo));
                query.OrderBy(query.SequenceNo.Ascending);

                dtb = query.LoadDataTable();
                var sbItem = new StringBuilder();
                foreach (DataRow row in dtb.Rows)
                {
                    var itemName = row["ItemName"].ToString();


                    if (row["ItemInterventionID"] != DBNull.Value &&
                        !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                    {
                        itemName = row["ItemNameIntervention"].ToString();
                    }

                    if (row["ParentNo"] != DBNull.Value && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                    {
                        //Header
                        sbItem = new StringBuilder();
                        sbItem.AppendFormat("{0} {1} @ {2} {3}{4}",
                            Convert.ToBoolean(row["IsRFlag"])
                                ? "R/ "
                                : string.Empty, itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} @ {2} {3}{4}",
                            Convert.ToBoolean(row["IsRFlag"])
                                ? "R/ "
                                : string.Empty,
                            itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                    }
                }
                return sbItem.ToString();

            }

            // Obat Paten
            var rowPaten = dtb.Rows[0];
            if (rowPaten["ItemInterventionID"] != DBNull.Value &&
                    !string.IsNullOrEmpty(rowPaten["ItemInterventionID"].ToString()))
            {
                return rowPaten["ItemNameIntervention"].ToString();
            }

            return rowPaten["ItemName"].ToString();
        }

    }

}