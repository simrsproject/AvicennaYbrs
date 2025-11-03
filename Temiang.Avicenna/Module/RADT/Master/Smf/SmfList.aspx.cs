using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class SmfList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SmfSearch.aspx";
            UrlPageDetail = "SmfDetail.aspx";

            ProgramID = AppConstant.Program.Smf;
            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(SmfMetadata.ColumnNames.SmfID).ToString();
            string url = string.Format("SmfDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Smfs;
        }

        private DataTable Smfs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SmfQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (SmfQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SmfQuery("a");
                    var stdFeeType = new AppStandardReferenceItemQuery("b");
                    var stdAssType = new AppStandardReferenceItemQuery("c");

                    query.LeftJoin(stdFeeType).On(query.SRParamedicFeeCaseType == stdFeeType.ItemID &&
                                                  stdFeeType.StandardReferenceID ==
                                                  AppEnum.StandardReference.ParamedicFeeCaseType);
                    query.LeftJoin(stdAssType).On(query.SRAssessmentType == stdAssType.ItemID &&
                                                  stdAssType.StandardReferenceID ==
                                                  AppEnum.StandardReference.AssessmentType);

                    query.Select
                        (
                            query.SmfID,
                            query.SmfName,
                            stdFeeType.ItemName.As("ParamedicFeeCaseTypeName"),
                            stdAssType.ItemName.As("AssessmentTypeName")
                        );
                    query.OrderBy(query.SmfID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "SmfName", "SmfID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
