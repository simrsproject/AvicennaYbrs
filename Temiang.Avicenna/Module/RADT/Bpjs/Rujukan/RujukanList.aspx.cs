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

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukanList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RujukanSearch.aspx";
            UrlPageDetail = "RujukanDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRujukan;
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
            string id = dataItem.GetDataKeyValue(BpjsRujukanMetadata.ColumnNames.NoSep).ToString();
            Page.Response.Redirect("RujukanDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = BpjsSeps;
        }

        private DataTable BpjsSeps
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                BpjsRujukanQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BpjsRujukanQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BpjsRujukanQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    var diag = new DiagnoseQuery("c");
                    var reg = new BpjsSEPQuery("e");
                    
                    query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JnsPelayanan);
                    query.InnerJoin(diag).On(query.DiagRujukan == diag.DiagnoseID);
                    query.InnerJoin(reg).On(query.NoSep == reg.NoSEP);
                    query.Select(
                        query,
                        std.ItemName.As("TypeOfService"),
                        diag.DiagnoseName,
                        reg.NomorKartu,
                        "<e.NamaPasien + ' (' + e.JenisKelamin + ')' AS NamaPasienJK>"
                        );
                    query.Where(query.TglRujukan == DateTime.Now.Date);
                    query.OrderBy(query.NoRujukan.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}