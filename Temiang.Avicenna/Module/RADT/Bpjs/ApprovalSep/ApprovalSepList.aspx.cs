using System;
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
    public partial class ApprovalSepList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ApprovalSepSearch.aspx";
            UrlPageDetail = "ApprovalSepDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsApproval;
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
            string id = dataItem.GetDataKeyValue(BpjsApprovalMetadata.ColumnNames.NoKartu).ToString();
            string id2 = Convert.ToDateTime(dataItem.GetDataKeyValue(BpjsApprovalMetadata.ColumnNames.TglSep).ToString()).ToString("yyyy-MM-dd");
            string id3 = dataItem.GetDataKeyValue(BpjsApprovalMetadata.ColumnNames.JnsPelayanan).ToString();

            Page.Response.Redirect("ApprovalSepDetail.aspx?md=" + mode + "&id=" + id + "&id2=" + id2 + "&id3=" + id3, true);
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

                BpjsApprovalQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BpjsApprovalQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BpjsApprovalQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    var std2 = new AppStandardReferenceItemQuery("c");

                    query.InnerJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JnsPelayanan);
                    query.InnerJoin(std2).On(std2.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfApproval.ToString() && std2.ItemID == query.JnsPengajuan);

                    query.Select(
                        query.NoKartu,
                        query.TglSep,
                        query.Keterangan,
                        query.JnsPelayanan,
                        std.ItemName.As("TypeOfService"),
                        query.JnsPengajuan,
                        std2.ItemName.As("TypeOfApproval"),
                        "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
                        query.IsApproved
                        );
                    query.Where(query.TglSep == DateTime.Now.Date);
                    query.OrderBy(query.NoKartu.Descending, query.TglSep.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}