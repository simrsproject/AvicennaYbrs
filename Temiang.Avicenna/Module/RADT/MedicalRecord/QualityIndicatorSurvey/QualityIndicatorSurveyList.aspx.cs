using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class QualityIndicatorSurveyList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QualityIndicatorSurvey;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = StandardReferences;
        }

        private DataTable StandardReferences
        {
            get
            {
                var query = new AppStandardReferenceQuery();
                query.Select(query.StandardReferenceID, query.StandardReferenceName);
                query.Where(query.StandardReferenceGroup == "IndikatorMutu" && query.IsActive == true);
                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new QualityIndicatorSurveyQuery("a");
            var qsu = new ServiceUnitQuery("d");
            var user = new AppUserServiceUnitQuery("e");
            query.InnerJoin(qsu).On(query.ServiceUnitID == qsu.ServiceUnitID);
            query.InnerJoin(user).On(query.ServiceUnitID == user.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);
            query.Select
                (
                query.StandardReferenceID,
                query.SurveyID,
                qsu.ServiceUnitName,
                query.PeriodDate,
                query.IsApprove,
                query.IsVoid
                );
            query.Where(query.StandardReferenceID == e.DetailTableView.ParentItem.GetDataKeyValue("StandardReferenceID"));

            query.OrderBy(query.PeriodDate.Descending);

            e.DetailTableView.DataSource = query.LoadDataTable();
            
        }
    }
}