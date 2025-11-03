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
    public partial class MedicationStopConfirm : BasePageDialog
    {

        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["mrecno"].ToInt();
            }
        }
        private bool IsContinue
        {
            get
            {
                return "1".Equals(Request.QueryString["cont"]);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                if (IsContinue)
                {
                    Title = "Medication Continue Reason";
                    lblStopContinue.Text = "Continue Date";
                }
                else
                {
                    Title = "Medication Stop Reason";
                    lblStopContinue.Text = "Stop Date";
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsContinue)
                    ComboBox.StandardReferenceItem(cboSRMedicationStopReason, "MedicationContinueReason");
                    
                else
                    ComboBox.StandardReferenceItem(cboSRMedicationStopReason, "MedicationStopReason");

                txtStatusDateTime.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var stat = new MedicationReceiveStatus();
            stat.MedicationReceiveNo = MedicationReceiveNo;
            stat.StatusDateTime = txtStatusDateTime.SelectedDate;
            stat.SRMedicationStopReason = cboSRMedicationStopReason.SelectedValue;
            stat.MedicationReason = txtMedicationReason.Text;
            stat.IsMedicationStop = !IsContinue;
            stat.Save();

            var med = new MedicationReceive();
            if (med.LoadByPrimaryKey(MedicationReceiveNo))
            {
                med.IsContinue = IsContinue;
                med.Save();
            }
        }
    }
}
