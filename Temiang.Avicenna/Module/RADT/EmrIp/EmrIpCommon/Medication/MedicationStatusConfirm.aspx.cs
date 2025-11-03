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
    public partial class MedicationStatusConfirm : BasePageDialog
    {

        private int MedicationReceiveNo
        {
            get
            {
                return Request.QueryString["mrecno"].ToInt();
            }
        }

        private string MedicationStatType
        {
            get
            {
                return Request.QueryString["medsttp"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                switch (MedicationStatType)
                {
                    case "continue":
                        Title = "Medication Continue Reason";
                        lblDate.Text = "Continue Date";
                        break;
                    case "stop":
                        Title = "Medication Stop Reason";
                        lblDate.Text = "Stop Date";
                        break;
                    case "void":
                        Title = "Medication Void Reason";
                        lblDate.Text = "Void Date";
                        break;
                    case "unvoid":
                        Title = "Medication Unvoid Reason";
                        lblDate.Text = "Unvoid Date";
                        break;
                    default:
                        break;
                }

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComboBox.StandardReferenceItem(cboSRMedicationStatusReason, "MedicationStatusReason", false, MedicationStatType);
                txtStatusDateTime.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var stat = new MedicationReceiveStatus();
            stat.MedicationReceiveNo = MedicationReceiveNo;
            stat.StatusDateTime = txtStatusDateTime.SelectedDate;
            stat.SRMedicationStatusReason = cboSRMedicationStatusReason.SelectedValue;
            stat.MedicationReason = txtMedicationReason.Text;

            switch (MedicationStatType)
            {
                case "continue":
                    stat.SRMedicationStatusType = "CONT";
                    stat.IsMedicationStop = false;
                    break;
                case "stop":
                    stat.SRMedicationStatusType = "STOP";
                    stat.IsMedicationStop = true;
                    break;
                case "void":
                    stat.SRMedicationStatusType = "VOID";
                    stat.IsMedicationStop = true;
                    break;
                case "unvoid":
                    stat.SRMedicationStatusType = "UNVD";
                    stat.IsMedicationStop = false;
                    break;
            }

            stat.Save();

            var med = new MedicationReceive();
            if (med.LoadByPrimaryKey(MedicationReceiveNo))
            {
                switch (MedicationStatType)
                {
                    case "continue":
                        med.IsContinue = true;
                        break;
                    case "stop":
                        med.IsContinue = false;
                        break;
                    case "void":
                        med.IsVoid = true;
                        break;
                    case "unvoid":
                        med.IsVoid = false;
                        break;
                    default:
                        break;
                }
                med.Save();
            }

            if (chkIsApplyInUddItem.Checked)
            {
                var uddItem = new UddItem();
                var qr = new UddItemQuery();
                qr.Where(qr.RegistrationNo == med.RegistrationNo
                    , qr.ItemID == med.ItemID
                    , qr.SRConsumeMethod == med.SRConsumeMethod
                    , qr.ConsumeQty == med.ConsumeQtyInString
                    , qr.SRConsumeUnit == med.SRConsumeUnit);
                qr.es.Top = 1;
                if (uddItem.Load(qr))
                {
                    uddItem.IsStop = !med.IsContinue;
                    uddItem.Save();
                }
            }
        }
    }
}
