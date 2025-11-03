using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukBalikList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RujukBalikSearch.aspx";
            UrlPageDetail = "RujukBalikDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRujukBalik;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string noSrb = dataItem.GetDataKeyValue("NoSRB").ToString();
            string noSep = dataItem.GetDataKeyValue("NoSEP").ToString();

            Page.Response.Redirect("RujukBalikDetail.aspx?md=" + mode + "&id=" + noSrb + "|" + noSep, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var noSRB = string.Empty;
            var noSep = string.Empty;
            var tglAwal = DateTime.Now;
            var tglAkhir = DateTime.Now;
            var query = Session[SessionNameForQuery];

            if (query != null)
            {
                var parameters = (List<string>)query;
                noSRB = parameters[0];
                noSep = parameters[1];
                if (!string.IsNullOrWhiteSpace(parameters[2])) tglAwal = Convert.ToDateTime(parameters[2]);
                if (!string.IsNullOrWhiteSpace(parameters[3])) tglAkhir = Convert.ToDateTime(parameters[3]);
            }
            var svc = new Common.BPJS.VClaim.v11.Service();
            if (!string.IsNullOrWhiteSpace(noSRB) && !string.IsNullOrWhiteSpace(noSep))
            {
                var response = svc.GetPrbByNo(noSRB, noSep);

                if (response.MetaData.IsValid)
                {
                    var list = new List<Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.List>();
                    var prb = response.Response.Prb;
                    list.Add(new Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.List()
                    {
                        NoSRB = prb.NoSRB,
                        TglSRB = prb.TglSRB,
                        ProgramPRB = new Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.ProgramPRB()
                        {
                            Nama = prb.ProgramPRB.Nama
                        },
                        NoSEP = prb.NoSEP,
                        Peserta = new Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.Peserta()
                        {
                            NoKartu = prb.Peserta.NoKartu,
                            Nama = prb.Peserta.Nama,
                        },
                        DPJP = new Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.DPJP()
                        {
                            Nama = prb.DPJP.Nama
                        },
                        Keterangan = prb.Keterangan,
                        Saran = prb.Saran
                    });

                    grdList.DataSource = GridDataSource(list);
                }
                else grdList.DataSource = GridDataSource(new List<Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.List>());
            }
            else
            {
                var response = svc.GetPrbByTanggal(tglAwal, tglAkhir);
                if (response.MetaData.IsValid) grdList.DataSource = GridDataSource(response.Response.Prb.List);
                else grdList.DataSource = GridDataSource(new List<Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.List>());
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var noSRB = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["NoSRB"]);
            var noSEP = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["NoSEP"]);

            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.Delete(new Common.BPJS.VClaim.v11.RujukanBalik.Delete.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.RujukanBalik.Delete.Request.TRequest()
                {
                    TPrb = new Common.BPJS.VClaim.v11.RujukanBalik.Delete.Request.TPrb()
                    {
                        NoSrb = noSRB,
                        NoSep = noSEP,
                        User = AppSession.UserLogin.UserID
                    }
                }
            });
            if (response.MetaData.IsValid)
            {
                grdList.Rebind();
            }
        }

        private DataTable GridDataSource(List<Common.BPJS.VClaim.v11.RujukanBalik.Select.ResponseList.List> lists)
        {
            var dtb = new DataTable();

            dtb.Columns.Add("NoSRB", typeof(string));
            dtb.Columns.Add("TglSRB", typeof(string));
            dtb.Columns.Add("ProgramPRBNama", typeof(string));
            dtb.Columns.Add("NoSEP", typeof(string));
            dtb.Columns.Add("PesertaNoKartu", typeof(string));
            dtb.Columns.Add("PesertaNama", typeof(string));
            dtb.Columns.Add("DPJPNama", typeof(string));
            dtb.Columns.Add("Keterangan", typeof(string));
            dtb.Columns.Add("Saran", typeof(string));

            foreach (var list in lists)
            {
                var row = dtb.NewRow();
                row[0] = list.NoSRB;
                row[1] = list.TglSRB;
                row[2] = list.ProgramPRB.Nama;
                row[3] = list.NoSEP;
                row[4] = list.Peserta.NoKartu;
                row[5] = list.Peserta.Nama;
                row[6] = list.DPJP.Nama;
                row[7] = list.Keterangan;
                row[8] = list.Saran;
                dtb.Rows.Add(row);
            }

            dtb.AcceptChanges();

            return dtb;
        }
    }
}