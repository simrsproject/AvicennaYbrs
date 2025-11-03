using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalaryComponentSearch.aspx";
            UrlPageDetail = "SalaryComponentDetail.aspx";

            ProgramID = AppConstant.Program.SalaryComponent; //TODO: Isi ProgramID

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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(SalaryComponentMetadata.ColumnNames.SalaryComponentID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SalaryComponents;
        }

        private DataTable SalaryComponents
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				SalaryComponentQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SalaryComponentQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery jamsostekType = new AppStandardReferenceItemQuery("f");
                    AppStandardReferenceItemQuery deductionType = new AppStandardReferenceItemQuery("e");
                    AppStandardReferenceItemQuery incomeTaxMethod = new AppStandardReferenceItemQuery("d");
                    AppStandardReferenceItemQuery salaryCategory = new AppStandardReferenceItemQuery("c");
                    AppStandardReferenceItemQuery salaryType = new AppStandardReferenceItemQuery("b");
                    query = new SalaryComponentQuery("a");

                    query.Select(
                                query.SalaryComponentID,
                                query.SalaryComponentCode,
                                query.SalaryComponentName,
                                query.SRSalaryType,
                                salaryType.ItemName.As("SalaryTypeName"),
                                query.SRSalaryCategory,
                                salaryCategory.ItemName.As("SalaryCategoryName"),
                                query.SRIncomeTaxMethod,
                                incomeTaxMethod.ItemName.As("IncomeTaxMethodName"),
                                query.SRDeductionType,
                                deductionType.ItemName.As("DeductionTypeName"),
                                query.SRJamsostekType,
                                jamsostekType.ItemName.As("JamsostekTypeName"),
                                query.ValidFrom,
                                query.ValidTo,
                                query.LastUpdateDateTime,
                                query.LastUpdateByUserID
                            );
                    query.LeftJoin(salaryType).On
                        (
                            query.SRSalaryType == salaryType.ItemID &
                            salaryType.StandardReferenceID == "SalaryType"
                        );
                    query.LeftJoin(salaryCategory).On
                        (
                            query.SRSalaryCategory == salaryCategory.ItemID &
                            salaryCategory.StandardReferenceID == "SalaryCategory"
                        );
                    query.LeftJoin(incomeTaxMethod).On
                        (
                            query.SRIncomeTaxMethod == incomeTaxMethod.ItemID &
                            incomeTaxMethod.StandardReferenceID == "IncomeTaxMethod"
                        );
                    query.LeftJoin(deductionType).On
                        (
                            query.SRDeductionType == deductionType.ItemID &
                            deductionType.StandardReferenceID == "DeductionType"
                        );
                    query.LeftJoin(jamsostekType).On
                        (
                            query.SRJamsostekType == jamsostekType.ItemID &
                            jamsostekType.StandardReferenceID == "JamsostekType"
                        );
                    query.OrderBy(query.SalaryComponentCode.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "SalaryComponentName", "SalaryComponentCode");
                }

				//query.es.Top = AppSession.Parameter.MaxResultRecord;				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

