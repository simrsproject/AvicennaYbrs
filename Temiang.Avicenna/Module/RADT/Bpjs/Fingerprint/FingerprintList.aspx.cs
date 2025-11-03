using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class FingerprintList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Common.AppConstant.Program.BpjsFingerprint;

            if (!IsPostBack) txtTglPelayanan.SelectedDate = DateTime.Now.Date;
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var data = new List<Common.BPJS.VClaim.v20.Fingerprint.Select.List>();
            if (!txtTglPelayanan.IsEmpty)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var respose = svc.GetFingerprints(txtTglPelayanan.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
                if (respose.MetaData.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(txtNoKartu.Text))
                    {
                        data = respose.Response.List.Where(x => x.NoKartu == txtNoKartu.Text).ToList();
                        if (!data.Any())
                        {
                            data.Add(new Common.BPJS.VClaim.v20.Fingerprint.Select.List()
                            {
                                NoKartu = "000",
                                NoSEP = "Peserta belum melakukan validasi finger print."
                            });
                        }
                        grdList.DataSource = GridDataSource(data);
                    }
                    else
                    {
                        data = respose.Response.List;
                        if (!data.Any())
                        {
                            data.Add(new Common.BPJS.VClaim.v20.Fingerprint.Select.List()
                            {
                                NoKartu = "000",
                                NoSEP = "Data tidak ditemukan."
                            });
                        }
                        grdList.DataSource = GridDataSource(data);
                    }
                }
                else
                {
                    data.Add(new Common.BPJS.VClaim.v20.Fingerprint.Select.List()
                    {
                        NoKartu = respose.MetaData.Code,
                        NoSEP = respose.MetaData.Message
                    });
                    grdList.DataSource = GridDataSource(data);
                }
            }
            else
            {
                data.Add(new Common.BPJS.VClaim.v20.Fingerprint.Select.List()
                {
                    NoKartu = "000",
                    NoSEP = "Tanggal pelayanan kosong."
                });
                grdList.DataSource = GridDataSource(data);
            }
        }

        protected void btnFilterNoKartu_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable GridDataSource(List<Common.BPJS.VClaim.v20.Fingerprint.Select.List> lists)
        {
            var dtb = new DataTable();

            dtb.Columns.Add("NoKartu", typeof(string));
            dtb.Columns.Add("NoSEP", typeof(string));

            foreach (var list in lists)
            {
                var row = dtb.NewRow();
                row[0] = list.NoKartu;
                row[1] = list.NoSEP;
                dtb.Rows.Add(row);
            }

            dtb.AcceptChanges();

            return dtb;
        }
    }
}