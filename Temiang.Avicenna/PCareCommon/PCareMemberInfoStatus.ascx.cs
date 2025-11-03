using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Bridging.PCare.BusinessObject;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.PCareCommon
{
    public partial class PCareMemberInfoStatus : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Attribute Peserta

        public string TglMulaiAktif
        {
            get { return txtTglMulaiAktif.Text; }
            set { txtTglMulaiAktif.Text = value; }
        }

        public string TglAkhirBerlaku
        {
            get { return txtTglAkhirBerlaku.Text; }
            set { txtTglAkhirBerlaku.Text = value; }
        }


        public bool Aktif
        {
            get { return chkAktif.Checked; }
            set { chkAktif.Checked = Convert.ToBoolean(value); }
        }

        public string KetAktif
        {
            get { return txtKetAktif.Text; }
            set { txtKetAktif.Text = value; }
        }

        public string KdProviderPst_kdProvider
        {
            get { return txtKdProviderPst_kdProvider.Text; }
            set { txtKdProviderPst_kdProvider.Text = value; }
        }
        public string KdProviderPst_nmProvider
        {
            get { return txtKdProviderPst_nmProvider.Text; }
            set { txtKdProviderPst_nmProvider.Text = value; }
        }
        #endregion

        public void Populate(string noKartu)
        {
            var peserta = new BpjsPeserta();
            if (!string.IsNullOrWhiteSpace(noKartu))
                peserta.LoadByPrimaryKey(noKartu);

            Populate(peserta);
        }

        private void Populate(BpjsPeserta peserta)
        {
            cpBpjsInfo.Title = string.Format("BPJS Status : [{0}] {1}", peserta.NoKartu, peserta.Nama);
            TglMulaiAktif = string.Empty;
            if (peserta.TglMulaiAktif != null)
                TglMulaiAktif = Convert.ToDateTime(peserta.TglMulaiAktif).ToString(AppConstant.DisplayFormat.Date);

            TglAkhirBerlaku = string.Empty;
            if (peserta.TglAkhirBerlaku != null)
                TglAkhirBerlaku = Convert.ToDateTime(peserta.TglAkhirBerlaku).ToString(AppConstant.DisplayFormat.Date);
            Aktif = peserta.Aktif ?? false;
            KetAktif = peserta.KetAktif;
            KdProviderPst_kdProvider = peserta.KdProviderPst_kdProvider;
            KdProviderPst_nmProvider = peserta.KdProviderPst_nmProvider;
        }
    }
}