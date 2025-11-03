using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using System.Xml;
using Telerik.Web.UI;
using System.Globalization;

namespace Temiang.Avicenna.Module.Finance.Integration.JasaRaharja
{
    public partial class DataQueryList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JasaRaharjaDataQuery;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var jr = new WebService.JasaRaharja();
            var list = jr.GET_KEJADIAN_RS(txtPatientName.Text);
            if (list.Count > 0)
            {
                var claim = list[0];

                var c = new BusinessObject.Interop.JasaRaharja.ClaimAndCoverage();
                c.es.Connection.Name = AppConstant.HIS_INTEROP.JASA_RAHAJA_INTEROP_CONNECTION_NAME;
                if (!c.LoadByPrimaryKey(txtPatientName.Text)) new BusinessObject.Interop.JasaRaharja.ClaimAndCoverage();
                c.RegistrationNo = txtPatientName.Text;
                c.IdRegister = claim.ID_REGISTER;
                c.SifatCedera = claim.SIFAT_CEDERA;
                c.JenisTindakan = claim.JENIS_TINDAKAN;
                c.DokterBerwenang = claim.DOKTER_BERWENANG;
                c.JmlBiaya = claim.BIAYA;
                c.JmlKlaim = claim.JUMLAH_DIBAYARKAN;
                //c.str.TglProses = string.IsNullOrEmpty(claim.TGL_PROSES) ? string.Empty : DateTime.ParseExact(claim.TGL_PROSES, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToShortDateString();
                c.str.TglProses = string.IsNullOrEmpty(claim.TGL_PROSES) ? string.Empty : DateTime.Parse(claim.TGL_PROSES).ToShortDateString();
                c.StatusJaminan = claim.STATUS_JAMINAN;
                c.StatusKlaim = claim.STATUS_KLAIM == "1" ? true : false;
                c.NoSuratJaminan = claim.NO_SURAT_JAMINAN;
                c.Save();

                JasaRaharjaClaims = list;
                JasaRaharjaUploads = claim.UploadClass;
            }
            else
            {
                JasaRaharjaClaims = null;
                JasaRaharjaUploads = null;
            }

            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = JasaRaharjaUploads;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = JasaRaharjaClaims;
        }

        private List<WebService.JasaRaharjaClass> JasaRaharjaClaims
        {
            get
            {
                if (ViewState["jasaRaharjaClaim"] == null) return new List<WebService.JasaRaharjaClass>();
                return ViewState["jasaRaharjaClaim"] as List<WebService.JasaRaharjaClass>;
            }
            set
            {
                ViewState["jasaRaharjaClaim"] = value;
            }
        }

        private List<WebService.JasaRaharjaUploadClass> JasaRaharjaUploads
        {
            get
            {
                if (ViewState["jasaRaharjaUpload"] == null) return new List<WebService.JasaRaharjaUploadClass>();
                return ViewState["jasaRaharjaUpload"] as List<WebService.JasaRaharjaUploadClass>;
            }
            set
            {
                ViewState["jasaRaharjaUpload"] = value;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind") btnFilter_Click(null, null);
        }
    }
}
