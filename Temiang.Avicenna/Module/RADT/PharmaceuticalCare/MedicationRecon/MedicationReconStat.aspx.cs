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

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    /// <summary>
    /// Form ini tidak dipakai lagi dan diganti dgn MedicationReceiveReconciliatonConfirm
    /// </summary>
    public partial class MedicationReconStat : BasePageDialog
    {
        private DataTable ReconDataSource
        {
            get { return (DataTable)Session["dtbrecon_" + FromGridID]; }
        }

        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["mrecno"].ToInt();
            }
        }
        private string ReconType => Request.QueryString["rectype"].ToUpper();
        private string FromGridID => Request.QueryString["fgid"].ToUpper();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                Title = "Medication Reconciliation Status";

                optReconStatus.Items.Add(new ButtonListItem("Continue with consume method not changed", "CN"));
                optReconStatus.Items.Add(new ButtonListItem("Continue with consume method changed", "CC"));
                optReconStatus.Items.Add(new ButtonListItem("Stop", "ST"));
                if (ReconType.Equals("DCG")) 
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
            var row = ReconDataSource.Rows.Find(MedicationReceiveNo);
            lblItemDescription.Text = row["ItemDescription"].ToString();
            txtItemUnit.Text = row["SRConsumeUnit"].ToString();
            txtItemUnit2.Text = row["SRConsumeUnit"].ToString();

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());
            txtConsumeMethod.Text = string.Format("{0} {1} {2}", cm.SRConsumeMethodName, row["ConsumeQty"], row["SRConsumeUnit"]);
            txtConsumeQty.Value = Convert.ToDouble(row["ConsumeQty"]);
            ComboBox.SelectedValue(cboSRMedicationConsume, row["SRMedicationConsume"].ToString());

            txtBalanceRealQty.Value = Convert.ToDouble(row["BalanceRealQty"]);

            var reg = new Registration();
            reg.LoadByPrimaryKey(row["RegistrationNo"].ToString()); // Recon dalam 1 episode shg bisa dari bbrp RegistrationNo
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

            optReconStatus.SelectedValue = row["ReconStatus"].ToString();
            if (row["NewConsumeMethodName"] != DBNull.Value)
                ComboBox.PopulateWithOneConsumeMethod(cboConsumeMethod, row["NewConsumeMethodName"].ToString());
        }

        private void Save(ValidateArgs args)
        {
            var row = ReconDataSource.Rows.Find(MedicationReceiveNo);
            var isStop = optReconStatus.SelectedValue.Equals("ST");
            var reconStatusType = optReconStatus.SelectedValue;
            row["ReconStatus"] = reconStatusType;
            row["IsContinue"] = !("ST".Equals(reconStatusType)); // Not Stop
            row["ReconStatusName"] = ReconStatus(row["ReconStatus"].ToString());
            if (reconStatusType.Equals("CC")) //Continue with consume method changed
            {
                row["NewConsumeMethodName"] = string.Format("{0} @{1} {2}", cboConsumeMethod.SelectedValue, txtConsumeQty.Text, txtItemUnit2.Text);
                row["NewSRConsumeMethod"] = cboConsumeMethod.SelectedValue;
                row["NewConsumeQty"] = txtConsumeQty.Text;
                row["NewSRConsumeUnit"] = txtItemUnit2.Text;
                row["NewSRMedicationConsume"] = cboSRMedicationConsume.SelectedValue;
            }
            else
            {
                row["NewConsumeMethodName"] = String.Empty;
                row["NewSRConsumeMethod"] = String.Empty;
                row["NewConsumeQty"] = String.Empty;
                row["NewSRConsumeUnit"] = String.Empty;
                row["NewSRMedicationConsume"] = String.Empty;
            }
        }
        private string ReconStatus(string statCode)
        {
            switch (statCode)
            {
                case "CN":
                    return "Continue with consume method not changed";
                case "CC":
                    return "Continue with consume method changed";
                case "ST":
                    return "Stop";
                case "NT":
                    return "New Therapies";
            }
            return string.Empty;
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
