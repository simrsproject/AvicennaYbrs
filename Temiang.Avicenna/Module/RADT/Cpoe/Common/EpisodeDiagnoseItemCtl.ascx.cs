using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe.Common
{
    public partial class EpisodeDiagnoseItemCtl : System.Web.UI.UserControl
    {
        public int RowNo
        {
            get
            {
                if (ViewState["rn"] == null)
                    ViewState["rn"] = 1;

                return (int)ViewState["rn"];
            }
            set
            {
                ViewState["rn"] = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                if (ViewState["dgro"] == null)
                    ViewState["dgro"] = false;

                return (bool)ViewState["dgro"];                
            }
            set
            {
                ViewState["dgro"] = value;
                var enabled = !value;
                cboDiagnoseID.Enabled = enabled;
                cboSRDiagnoseType.Enabled = enabled;
                txtDiagnoseText.ReadOnly = !enabled;
                chkIsOldCase.Enabled = enabled;
            }
        }
        public string DiagnoseID
        {
            get { return cboDiagnoseID.Text; }
        }

        public void SetDiagnoseID(string seqNo, string id, string diagnoseText)
        {
            cboDiagnoseID.Text = id;
            txtDiagnoseText.Text = diagnoseText;
        }

        public string DiagnosisText
        {
            get
            {
                return txtDiagnoseText.Text;
            }
        }
        public string SRDiagnoseType
        {
            get { return cboSRDiagnoseType.SelectedValue; }
            set
            {
                ComboBox.SelectedValue(cboSRDiagnoseType, value);
            }
        }
        public bool IsOldCase
        {
            get { return chkIsOldCase.Checked; }
            set { chkIsOldCase.Checked = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var diagnoseTypeMain = AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain);

            cboSRDiagnoseType.Items.Clear();
            var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.DiagnoseType);
            foreach (var item in coll)
            {
                if (diagnoseTypeMain.ToLower() == item.ItemID.ToLower() && RowNo == 1)
                {
                    // Baris pertama untuk Main Diagnose
                    cboSRDiagnoseType.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                    ComboBox.SelectedValue(cboSRDiagnoseType, diagnoseTypeMain);
                    break;
                }

                if (diagnoseTypeMain.ToLower() != item.ItemID.ToLower())
                    cboSRDiagnoseType.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblNo.Text = RowNo.ToString();
        }
    }
}