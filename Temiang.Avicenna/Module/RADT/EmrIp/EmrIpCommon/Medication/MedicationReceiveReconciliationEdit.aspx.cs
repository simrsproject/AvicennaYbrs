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


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveReconciliationEdit : BasePageDialog
    {

        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["mrecno"].ToInt();
            }
        }
        private string AppropriateType
        {
            get
            {
                return Request.QueryString["rectype"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                Title = "Medication Reconciliation";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComboBox.StandardReferenceItem(cboSRMedicationNotAppropriate, "MedicationNotAppropriateReason",true);

                var stat = new MedicationReceiveAppropriate();
                if (stat.LoadByPrimaryKey(MedicationReceiveNo, AppropriateType))
                {
                    ComboBox.SelectedValue(cboSRMedicationNotAppropriate,stat.AppropriateType);
                    txtMedicationNotAppropriateReason.Text = stat.MedicationReason;
                }
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var stat = new MedicationReceiveAppropriate();
            if (!stat.LoadByPrimaryKey(MedicationReceiveNo, AppropriateType))
                stat = new MedicationReceiveAppropriate();

            stat.MedicationReceiveNo = MedicationReceiveNo;
            stat.SRMedicationNotAppropriateReason = cboSRMedicationNotAppropriate.SelectedValue;
            stat.MedicationReason = txtMedicationNotAppropriateReason.Text;

            var med = new MedicationReceive();
            med.LoadByPrimaryKey(MedicationReceiveNo);

            switch (Request.QueryString["rectype"])
            {
                case "adm":
                    stat.AppropriateType = "ADM";
                    med.IsAdmissionAppropriate = false;
                    break;
                case "trf":
                    stat.AppropriateType = "TRF";
                    med.IsTransferAppropriate = false;

                    break;
                case "dcg":
                    stat.AppropriateType = "DCG";
                    med.IsDischargeAppropriate = false;
                    break;
            }

            stat.Save();
            med.Save();
        }
    }
}
