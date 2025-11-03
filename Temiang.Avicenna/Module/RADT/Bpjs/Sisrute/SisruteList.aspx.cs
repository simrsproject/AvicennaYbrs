using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class SisruteList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SisruteSearch.aspx";
            UrlPageDetail = "SisruteDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.Sisrute;
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
            string id = dataItem.GetDataKeyValue("RUJUKAN.NOMOR").ToString();
            Page.Response.Redirect("SisruteDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var svc = new Common.Sisrute.Service();
            Common.Sisrute.Rujukan.Get rsp;
            object obj = this.Session[SessionNameForQuery];
            if (obj == null) rsp = svc.GetRujukan(string.Empty, DateTime.Now.Date.ToString("yyyy-MM-dd"));
            else
            {
                var list = Session[SessionNameForQuery] as string[];
                rsp = svc.GetRujukan(list[0] ?? string.Empty, list[1] == null ? string.Empty : Convert.ToDateTime(list[1]).ToString("yyyy-MM-dd"));

            }
            if (rsp.Data == null) grdList.DataSource = null;
            else grdList.DataSource = rsp.Data;
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RUJUKAN.NOMOR"]);
            var svc = new Common.Sisrute.Service();
            var rsp = svc.BatalRujukan(itemID, new Common.Sisrute.Rujukan.SetRujukan.PETUGAS() { NIK = AppSession.UserLogin.UserID, NAMA = AppSession.UserLogin.UserName });
        }

    }
}
