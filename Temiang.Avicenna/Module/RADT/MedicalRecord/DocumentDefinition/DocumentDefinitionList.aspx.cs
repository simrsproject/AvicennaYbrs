using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentDefinitionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DocumentDefinitionSearch.aspx";
            UrlPageDetail = "DocumentDefinitionDetail.aspx";

            ProgramID = AppConstant.Program.DocumentDefinition; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(DocumentDefinitionMetadata.ColumnNames.DocumentDefinitionID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DocumentDefinitions;
        }

        private DataTable DocumentDefinitions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				DocumentDefinitionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (DocumentDefinitionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new DocumentDefinitionQuery("a");
                    var files = new AppStandardReferenceItemQuery("c");
                    var department = new DepartmentQuery("b");

                    query.LeftJoin(department).On(query.DepartmentID == department.DepartmentID);
                    query.InnerJoin(files).On
                        (
                            query.SRFilesAnalysis == files.ItemID &
                            files.StandardReferenceID == "FilesAnalysis"
                        );
                    query.Select(
                                    query.DocumentDefinitionID,
                                    query.DepartmentID,
                                    department.DepartmentName,
                                    files.ItemName.As("NameFilesType"),
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID,
                                    query.IsActive
                                );
                    query.OrderBy(department.DepartmentName.Ascending);

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
            string documentDefinitionId = dataItem.GetDataKeyValue("DocumentDefinitionID").ToString();

            var query = new DocumentDefinitionItemQuery("a");
            var qb = new DocumentFilesQuery("b");
            query.InnerJoin(qb).On(query.DocumentFilesID == qb.DocumentFilesID);
            query.Where(query.DocumentDefinitionID == documentDefinitionId.ToInt());
            query.OrderBy(qb.DocumentNumber.Ascending);

            query.Select
                (
                    query.DocumentFilesID, qb.DocumentNumber, qb.DocumentName
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}

