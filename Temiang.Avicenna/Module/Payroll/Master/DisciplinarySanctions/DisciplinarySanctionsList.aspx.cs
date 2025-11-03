using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class DisciplinarySanctionsList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DisciplinarySanctionsSearch.aspx";
            UrlPageDetail = "DisciplinarySanctionsDetail.aspx";

            ProgramID = AppConstant.Program.DisciplinarySanctions; //TODO: Isi ProgramID

            // Quick Search
            if (!IsPostBack)
            {
            }
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
            string id = dataItem.GetDataKeyValue(DisciplinarySanctionsMetadata.ColumnNames.DisciplinarySanctionsID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = DisciplinarySanctionss;
        }

        private DataTable DisciplinarySanctionss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                DisciplinarySanctionsQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (DisciplinarySanctionsQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new DisciplinarySanctionsQuery("a");
                    var emptype = new AppStandardReferenceItemQuery("b");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query, 
                        emptype.ItemName.As("EmploymentTypeName")
                        );
                    query.InnerJoin(emptype).On
                           (
                               query.SREmploymentType == emptype.ItemID &
                               emptype.StandardReferenceID == AppEnum.StandardReference.EmploymentType
                           );
                    query.OrderBy(query.SREmploymentType.Ascending, query.StartValue.Ascending, query.ValidFromDate.Ascending);

                    //Quick Search
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}