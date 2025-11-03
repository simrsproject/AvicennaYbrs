using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class DocumentChecklistDefinitionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DocumentChecklistDefinitionSearch.aspx";
            UrlPageDetail = "DocumentChecklistDefinitionDetail.aspx";

            ProgramID = AppConstant.Program.GuarantorDocumentChecklistDefinition; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(AppStandardReferenceItemMetadata.ColumnNames.ItemID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DocumentChecklists;
        }

        private DataTable DocumentChecklists
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				AppStandardReferenceItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AppStandardReferenceItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AppStandardReferenceItemQuery("a");
                    query.Select(
                        query.StandardReferenceID,
                        query.ItemID,
                        query.ItemName,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                        );
                    query.Where(query.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist);
                    query.OrderBy(query.ItemID.Ascending);

                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string documentDefinitionId = dataItem.GetDataKeyValue("ItemID").ToString();

            var query = new DocumentChecklistDefinitionQuery("a");
            var qb = new DocumentFilesQuery("b");
            query.InnerJoin(qb).On(query.DocumentFilesID == qb.DocumentFilesID);
            query.Where(query.SRDocumentChecklist == documentDefinitionId);
            query.OrderBy(qb.DocumentNumber.Ascending);

            query.Select
                (
                    query.DocumentFilesID, qb.DocumentNumber, qb.DocumentName
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}

