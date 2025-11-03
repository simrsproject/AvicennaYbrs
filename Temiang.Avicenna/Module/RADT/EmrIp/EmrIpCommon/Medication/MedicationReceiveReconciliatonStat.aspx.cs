using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Form ini tidak dipakai lagi dan diganti dgn MedicationReceiveReconciliatonConfirm
    /// </summary>
    public partial class MedicationReceiveReconciliatonStat : BasePageDialog
    {

        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["mrecno"].ToInt();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                Title = "Medication Reconciliation Status";

                optReconStatus.Items.Add(new ButtonListItem("Continue with consume method not changed","CN"));
                optReconStatus.Items.Add(new ButtonListItem("Continue with consume method changed", "CC"));
                optReconStatus.Items.Add(new ButtonListItem("Stop", "ST"));
                //if (Request.QueryString["rectype"]== "dcg") 
                    optReconStatus.Items.Add(new ButtonListItem("New Therapies", "NT"));

                StandardReference.InitializeIncludeSpace(cboSRMedicationConsume, AppEnum.StandardReference.MedicationConsume);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateEntryControl();

                if (txtStatusDateTime.IsEmpty)
                    txtStatusDateTime.SelectedDate = DateTime.Now;
                else
                    txtStatusDateTime.Enabled = false;
            }
        }

        private void PopulateEntryControl()
        {
            var med = new MedicationReceive();
            if (!med.LoadByPrimaryKey(MedicationReceiveNo)) return;
            lblItemDescription.Text = med.ItemDescription;
            txtItemUnit.Text = med.SRConsumeUnit;
            txtItemUnit2.Text = med.SRConsumeUnit;

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(med.SRConsumeMethod);
            txtConsumeMethod.Text = string.Format("{0} {1} {2}", cm.SRConsumeMethodName, med.ConsumeQty, med.SRConsumeUnit);
            txtConsumeQty.Text = med.ConsumeQtyInString;
            ComboBox.SelectedValue(cboSRMedicationConsume, med.SRMedicationConsume);

            txtBalanceRealQty.Value = Convert.ToDouble(med.BalanceRealQty);

            var reg = new Registration();
            reg.LoadByPrimaryKey(med.RegistrationNo);
            txtBed.Text = reg.BedID;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtPatientName.Text = pat.PatientName;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitName.Text = su.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            txtRoomName.Text = room.RoomName;

            switch (Request.QueryString["rectype"])
            {
                case "adm":
                    {
                        txtStatusDateTime.SelectedDate = med.AdmissionAppropriateDateTime;
                        optReconStatus.SelectedValue = med.ReconStatusAdm;
                        if (!string.IsNullOrEmpty(med.SRConsumeMethodAdm))
                            ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, med.SRConsumeMethodAdm);
                        break;
                    }
                case "trf":
                    {
                        txtStatusDateTime.SelectedDate = med.TransferAppropriateDateTime;
                        optReconStatus.SelectedValue = med.ReconStatusTrf;
                        if (!string.IsNullOrEmpty(med.SRConsumeMethodTrf))
                            ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, med.SRConsumeMethodTrf);
                        break;
                    }
                case "dcg":
                    {
                        txtStatusDateTime.SelectedDate = med.DischargeAppropriateDateTime;
                        optReconStatus.SelectedValue = med.ReconStatusDis;
                        if (!string.IsNullOrEmpty(med.SRConsumeMethodDis))
                            ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, med.SRConsumeMethodDis);
                        break;
                    }
            }

        }

        private void Save(ValidateArgs args)
        {
            using (var tr = new esTransactionScope())
            {
                var med = new MedicationReceive();
                if (med.LoadByPrimaryKey(MedicationReceiveNo))
                {
                    var isStop = optReconStatus.SelectedValue.Equals("ST");
                    var reconConsumeMethod = cboConsumeMethod.SelectedValue;
                    med = SetReconStatus(med, optReconStatus.SelectedValue, reconConsumeMethod, isStop);
                    med.Save();
                }

                // Create New MedicationReceive()
                if (optReconStatus.SelectedValue.Equals("CC")) //Continue with consume method changed
                    CreateNewMedicationReceive(med);

                tr.Complete();
            }
        }
        private MedicationReceive SetReconStatus(MedicationReceive med, string reconStatusType,string reconConsumeMethod, bool isStop)
        {
            switch (Request.QueryString["rectype"])
            {
                case "adm":
                    {
                        med.AdmissionAppropriateDateTime = txtStatusDateTime.SelectedDate;
                        med.ReconStatusAdm = reconStatusType;
                        med.IsAdmissionAppropriate = !isStop;
                        if (reconStatusType.Equals("CC")) //Continue with consume method changed
                            med.SRConsumeMethodAdm = reconConsumeMethod;
                        else
                            med.str.SRConsumeMethodAdm = String.Empty;
                        break;
                    }
                case "trf":
                    {
                        med.TransferAppropriateDateTime = txtStatusDateTime.SelectedDate;
                        med.ReconStatusTrf = reconStatusType;
                        med.IsTransferAppropriate = !isStop;
                        if (reconStatusType.Equals("CC")) //Continue with consume method changed
                            med.SRConsumeMethodTrf = reconConsumeMethod;
                        else
                            med.str.SRConsumeMethodTrf = String.Empty;
                        break;
                    }
                case "dcg":
                    {
                        med.DischargeAppropriateDateTime = txtStatusDateTime.SelectedDate;
                        med.ReconStatusDis = reconStatusType;
                        med.IsDischargeAppropriate = !isStop;
                        if (reconStatusType.Equals("CC")) //Continue with consume method changed
                            med.SRConsumeMethodDis = reconConsumeMethod;
                        else
                            med.str.SRConsumeMethodDis = String.Empty;
                        break;
                    }
            }
            med.IsContinue = !(reconStatusType.Equals("ST")); // Not Stop
            return med;
        }
        private int NewSequenceNo()
        {
            var qr = new MedicationReceiveUsedQuery("a");
            var fb = new MedicationReceiveUsed();
            qr.es.Top = 1;
            qr.Where(qr.MedicationReceiveNo == MedicationReceiveNo);
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }

        private void CreateNewMedicationReceive(MedicationReceive fromMedicationReceive)
        {
            // Create Used untuk menghabiskan stok di item asal
            var ent = new MedicationReceiveUsed();
            ent.MedicationReceiveNo = MedicationReceiveNo;
            ent.SequenceNo = NewSequenceNo();
            ent.Qty = Convert.ToDecimal(txtBalanceRealQty.Value); // Habiskan balance

            var date = txtStatusDateTime.SelectedDate.Value;
            ent.RealizedDateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            ent.RealizedByUserID = AppSession.UserLogin.UserID;

            ent.SetupDateTime = ent.RealizedDateTime;
            ent.SetupByUserID = ent.RealizedByUserID;

            ent.VerificationDateTime = ent.RealizedDateTime;
            ent.VerificationByUserID = ent.RealizedByUserID;

            ent.ScheduleDateTime = ent.RealizedDateTime;

            ent.Note = "Generate from Recon admisi with status Continue with consume method not changed";
            ent.IsNotConsume = true;
            ent.Save();

            var newMedRec = new MedicationReceive
            {
                RegistrationNo = fromMedicationReceive.RegistrationNo,
                MedicationReceiveNo = NewMedicationReceiveNo(),
                BalanceQty = Convert.ToDecimal(txtBalanceRealQty.Value),
                BalanceRealQty = Convert.ToDecimal(txtBalanceRealQty.Value),
                ReceiveDateTime = txtStatusDateTime.SelectedDate,
                StartDateTime = txtStatusDateTime.SelectedDate,
                ItemID = fromMedicationReceive.ItemID,
                ItemDescription = fromMedicationReceive.ItemDescription,
                ReceiveQty = Convert.ToDecimal(txtBalanceRealQty.Value),
                ConsumeQty = Convert.ToDecimal(new BusinessObject.Common.Fraction(txtConsumeQty.Text)),
                ConsumeQtyInString = txtConsumeQty.Text,
                SRConsumeUnit = fromMedicationReceive.SRConsumeUnit,
                SRConsumeMethod = cboConsumeMethod.SelectedValue,
                RefTransactionNo = fromMedicationReceive.RefTransactionNo,
                RefSequenceNo = fromMedicationReceive.RefSequenceNo,
                SRMedicationConsume = cboSRMedicationConsume.SelectedValue,
                IsVoid = false,
                IsContinue = true
            };

            var reconConsumeMethod = cboConsumeMethod.SelectedValue;
            newMedRec = SetReconStatus(newMedRec, "NT", reconConsumeMethod, false); // set as new therapy

            newMedRec.Save();
        }

        private int NewMedicationReceiveNo()
        {
            var qr = new MedicationReceiveQuery("a");
            var fb = new MedicationReceive();
            qr.es.Top = 1;
            qr.OrderBy(qr.MedicationReceiveNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MedicationReceiveNo.ToInt() + 1;
            }
            return 1;
        }



        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (string.IsNullOrWhiteSpace(optReconStatus.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Please choose one of Reconciliaton Status";
                return;
            }

            if (optReconStatus.SelectedValue.Equals("CC") && string.IsNullOrWhiteSpace(cboConsumeMethod.SelectedValue))
            {
                args.IsCancel = true;
                args.MessageText = "Please choose one of consume method";
                return;
            }

            Save(args);
        }
    }
}
