using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance
{
    public partial class BkuList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "BkuSearch.aspx";
            UrlPageDetail = "BkuDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BkuMasuk;
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
            string id = dataItem.GetDataKeyValue("Nomor").ToString();
            Page.Response.Redirect("BkuDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TransaksiBkus;
        }

        private DataTable TransaksiBkus
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                TransaksiBkuQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (TransaksiBkuQuery)Session[SessionNameForQuery];
                else
                {
                    query = new TransaksiBkuQuery("a");
                    var detail = new TransaksiBkuDetailQuery("b");
                    query.InnerJoin(detail).On(query.Nomor == detail.Nomor);
                    var pelanggan = new VwPelangganBkuQuery("c");
                    query.LeftJoin(pelanggan).On(query.Pelanggan == pelanggan.Id && query.Jenis == pelanggan.Type);
                    var unit = new ServiceUnitQuery("d");
                    query.LeftJoin(unit).On(query.Unit == unit.ServiceUnitID);
                    var bank = new BankQuery("e");
                    query.InnerJoin(bank).On(query.KasBank == bank.BankID);

                    query.Select
                        (
                            query.Nomor,
                            query.Tanggal,
                            "<CASE WHEN a.Jenis = 1 THEN 'PENERIMAAN' ELSE 'PENGELUARAN' END AS NamaJenis>",
                            pelanggan.Name.As("NamaPelanggan"),
                            unit.ServiceUnitName.As("NamaUnit"),
                            bank.BankName.As("NamaKasBank"),
                            query.Uraian,
                            detail.Nominal.Sum().As("Total")
                        );
                    query.OrderBy(query.Nomor.Ascending, query.Tanggal.Ascending);
                    query.GroupBy("<a.Nomor>",
                        "<a.Tanggal>",
                        "<CASE WHEN a.Jenis = 1 THEN 'PENERIMAAN' ELSE 'PENGELUARAN' END>",
                        "<c.Name>",
                        "<d.ServiceUnitName>",
                        "<e.BankName>",
                        "<a.Uraian>");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}