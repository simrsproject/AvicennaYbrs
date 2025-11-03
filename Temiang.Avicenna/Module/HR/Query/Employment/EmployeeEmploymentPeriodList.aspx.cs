using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeEmploymentPeriodList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeEmploymentPeriodSearch.aspx";
            UrlPageDetail = "EmployeeEmploymentPeriodDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.QueryEmploymentPeriod; //TODO: Isi ProgramID
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            //RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeEmploymentPeriods;
        }

        private DataTable EmployeeEmploymentPeriods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EmployeeEmploymentPeriodQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeEmploymentPeriodQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery employment = new AppStandardReferenceItemQuery("c");                    
                    PersonalInfoQuery personal = new PersonalInfoQuery("b");
                    query = new EmployeeEmploymentPeriodQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                           query.EmployeeEmploymentPeriodID,
                           query.PersonID,
                           personal.EmployeeNumber,
                           personal.EmployeeName,
                           query.SREmploymentType,
                           employment.ItemName.As("EmploymentTypeName"),
                           query.ValidFrom,
                           query.ValidTo,
                           query.Note
                        );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.LeftJoin(employment).On
                            (
                                query.SREmploymentType == employment.ItemID &
                                employment.StandardReferenceID == "EmploymentType"
                            );

                    query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya
                }				
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

