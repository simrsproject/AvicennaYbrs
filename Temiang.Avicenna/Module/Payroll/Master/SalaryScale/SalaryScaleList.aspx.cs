using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryScaleList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalaryScaleSearch.aspx";
            UrlPageDetail = "SalaryScaleDetail.aspx";

            ProgramID = AppConstant.Program.SalaryScale; //TODO: Isi ProgramID

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(SalaryScaleMetadata.ColumnNames.SalaryScaleID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SalaryScales;
        }

        private DataTable SalaryScales
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                SalaryScaleQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SalaryScaleQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SalaryScaleQuery("a");
                    var grade = new PositionGradeQuery("b");
                    var etype = new AppStandardReferenceItemQuery("c");
                    var pgroup = new AppStandardReferenceItemQuery("d");
                    var egroup = new AppStandardReferenceItemQuery("e");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.SalaryScaleID,
                        query.SalaryScaleCode,
                        query.SalaryScaleName,
                        query.PositionGradeID,
                        grade.PositionGradeCode,
                        grade.PositionGradeName,
                        query.SREmploymentType,
                        etype.ItemName.As("EmploymentTypeName"),
                        query.SRProfessionGroup,
                        pgroup.ItemName.As("ProfessionGroupName"),
                        query.SREducationGroup,
                        egroup.ItemName.As("EducationGroupName"),
                        query.Notes,
                        query.IsActive,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                                );
                    query.InnerJoin(grade).On(query.PositionGradeID == grade.PositionGradeID);
                    query.InnerJoin(etype).On(etype.StandardReferenceID == AppEnum.StandardReference.EmploymentType.ToString() && etype.ItemID == query.SREmploymentType);
                    query.InnerJoin(pgroup).On(pgroup.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() && pgroup.ItemID == query.SRProfessionGroup);
                    query.InnerJoin(egroup).On(egroup.StandardReferenceID == AppEnum.StandardReference.EducationGroup.ToString() && egroup.ItemID == query.SREducationGroup);

                    query.OrderBy(query.SalaryScaleCode.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}