using System;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs.VClaim
{
    public partial class UpdateTanggalPulangDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var sep = new BusinessObject.BpjsSEP();
                sep.LoadByPrimaryKey(Request.QueryString["sepno"]);

                txtNoSep.Text = sep.NoSEP;
                txtTglPulang.SelectedDate = DateTime.Now.Date;
            }
        }

        public override bool OnButtonOkClicked()
        {
            HideInformationHeader();

            if (cboStatusPulang.SelectedValue == "4")
            {
                if (txtTanggalMeninggal.IsEmpty)
                {
                    ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", "404", "Tanggal Meninggal belum diisi."));
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtNoSuratMeninggal.Text))
                {
                    ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", "404", "No Surat Meninggal belum diisi."));
                    return false;
                }
            }

            var co = new Common.BPJS.VClaim.v11.Service();
            var response = co.UpdateTglPulang(new Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.TSep
            {
                NoSep = Request.QueryString["sepno"],
                StatusPulang = cboStatusPulang.SelectedValue,
                NoSuratMeninggal = txtNoSuratMeninggal.Text,
                TglMeninggal = txtTanggalMeninggal.IsEmpty ? string.Empty : txtTanggalMeninggal.SelectedDate?.ToString("yyyy-MM-dd"),
                NoLPManual = txtNoLP.Text,
                User = AppSession.UserLogin.UserID,
                TglPulang = txtTglPulang.SelectedDate?.ToString("yyyy-MM-dd")
            });
            if (!response.MetaData.IsValid)
            {
                ShowInformationHeader(string.Format("Code : {0}, Message : Bridging BPJS, {1}", response.MetaData.Code, response.MetaData.Message));
                return false;
            }
            return true;
        }

        protected void cboStatusPulang_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "4")
            {
                txtTanggalMeninggal.DatePopupButton.Enabled = true;
                txtTanggalMeninggal.DateInput.ReadOnly = false;
                txtTanggalMeninggal.SelectedDate = DateTime.Now.Date;
                txtNoSuratMeninggal.ReadOnly = false;
                txtNoSuratMeninggal.Text = string.Empty;
            }
            else
            {
                txtTanggalMeninggal.DatePopupButton.Enabled = false;
                txtTanggalMeninggal.DateInput.ReadOnly = true;
                txtTanggalMeninggal.Clear();
                txtNoSuratMeninggal.ReadOnly = true;
                txtNoSuratMeninggal.Text = string.Empty;
            }
        }
    }
}