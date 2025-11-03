using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR
{
    public partial class RenkinList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RenkinSearch.aspx";
            UrlPageDetail = "RenkinDetail.aspx";

            ProgramID = AppConstant.Program.Renkin;
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(RenkinMetadata.ColumnNames.RenkinID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Renkins;
        }

        private DataTable Renkins
        {
            get
            {
                var query = new RenkinQuery("a");
                var item = new AppStandardReferenceItemQuery("b");
                var posQ = new PositionQuery("c");

                query.InnerJoin(item).On(query.SRRenkinJenisKegiatan == item.ItemID && item.StandardReferenceID == "RenkinJenisKegiatan");
                query.InnerJoin(posQ).On(query.PositionID == posQ.PositionID);
                query.Select(
                            query.RenkinID,
                            query.PositionID,
                            query.Kegiatan,
                            posQ.PositionName,
                            item.ItemName,
                            query.TargetPersen,
                            query.TargetBulan
                        );

                if (!string.IsNullOrEmpty(cboPositionID.SelectedValue))
                    query.Where(query.PositionID == cboPositionID.SelectedValue);

                query.OrderBy(query.RenkinID.Ascending);
                return query.LoadDataTable();


                //object obj = this.Session[SessionNameForList];
                //if (obj != null)
                //{
                //    return ((DataTable)(obj));
                //}

                //RenkinQuery query;
                //if (Session[SessionNameForQuery] != null)
                //{
                //    query = (RenkinQuery)Session[SessionNameForQuery];
                //}
                //else
                //{
                //    query = new RenkinQuery("a");
                //    var item = new AppStandardReferenceItemQuery("b");
                //    var posQ = new PositionQuery("c");
                //    //var pq = new PositionQuery("f");

                //    //query.InnerJoin(pq).On(query.PositionID == pq.PositionID);
                //    query.InnerJoin(item).On(query.SRRenkinJenisKegiatan == item.ItemID && item.StandardReferenceID == "RenkinJenisKegiatan");
                //    query.InnerJoin(posQ).On(query.PositionID == posQ.PositionID);
                //    query.Select(
                //                //pq.PositionName,
                //                query.RenkinID,
                //                query.PositionID,
                //                query.Kegiatan,
                //                posQ.PositionName,
                //                item.ItemName,
                //                query.TargetPersen,
                //                query.TargetBulan

                //            );

                //    if (!string.IsNullOrEmpty(cboPositionID.SelectedValue))
                //        query.Where(query.PositionID == cboPositionID.SelectedValue);

                //    query.OrderBy(query.RenkinID.Ascending);
                //}
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                //DataTable dtb = query.LoadDataTable();
                //this.Session[SessionNameForList] = dtb;
                //return dtb;
            }

        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            //grdList.DataSource = Renkins(cboPositionID.SelectedValue);
        }

        #region ComboBox PositionID

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new PositionQuery();
            query.es.Top = 15;
            query.Select(
                query.PositionID,
                query.PositionName
                );
            query.Where(
                query.PositionName.Like(string.Format("%{0}%", e.Text))
                );
            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        #endregion

    }
}

