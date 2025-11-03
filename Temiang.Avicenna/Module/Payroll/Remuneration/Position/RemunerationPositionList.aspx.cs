using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.RemunerationPosition
{
    public partial class RemunerationPositionList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RemunerationPositionSearch.aspx";
            UrlPageDetail = "RemunerationPositionDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.EmployeeRemunerationPosition;
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
            string id = dataItem.GetDataKeyValue(EmployeeWageStructureAndScalePositionMetadata.ColumnNames.WageStructureAndScalePositionID).ToString();
            string url = string.Format("RemunerationPositionDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeWageStructureAndScalePositions;
        }

        private DataTable EmployeeWageStructureAndScalePositions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeWageStructureAndScalePositionQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeWageStructureAndScalePositionQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeWageStructureAndScalePositionQuery("a");
                    var personalq = new PersonalInfoQuery("b");
                    var wtq = new EmployeeWageStructureAndScalePositionQuery("c");
                    var wgroupq = new AppStandardReferenceItemQuery("d");
                    var wsubgroupq = new AppStandardReferenceItemQuery("e");
                    var jobposq = new AppStandardReferenceItemQuery("f");

                    query.Select
                        (query,
                            personalq.EmployeeNumber,
                            personalq.EmployeeName,
                            wgroupq.ItemName.As("EmployeeWorkGroupName"),
                            wsubgroupq.ItemName.As("EmployeeWorkSubGroupName"),
                            jobposq.ItemName.As("EmployeeJobPositionName")
                        );
                    query.InnerJoin(personalq).On(personalq.PersonID == query.PersonID);
                    query.InnerJoin(wtq).On(wtq.WageStructureAndScalePositionID == query.WageStructureAndScalePositionID);
                    query.InnerJoin(wgroupq).On(wgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkGroup.ToString() && wgroupq.ItemID == wtq.SREmployeeWorkGroup);
                    query.InnerJoin(wsubgroupq).On(wsubgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkSubGroup.ToString() && wsubgroupq.ItemID == wtq.SREmployeeWorkSubGroup);
                    query.InnerJoin(jobposq).On(jobposq.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition.ToString() && jobposq.ItemID == wtq.SREmployeeJobPosition);
                    query.OrderBy(query.WageStructureAndScalePositionID.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}