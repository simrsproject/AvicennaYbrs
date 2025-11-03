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

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingList : BasePageList
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeTrainingSearch.aspx?type=" + FormType;
            UrlPageDetail = "EmployeeTrainingDetail.aspx?type=" + FormType;

            this.WindowSearch.Height = 400;

            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeTraining : (FormType == "point" ? AppConstant.Program.EmployeeTrainingPoint : (FormType == "pps" ? AppConstant.Program.EmployeeTrainingProposal : AppConstant.Program.EmployeeTrainingProposal2));
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(EmployeeTrainingMetadata.ColumnNames.EmployeeTrainingID).ToString();
            string url = string.Format("EmployeeTrainingDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeTrainings;
        }

        private DataTable EmployeeTrainings
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeTrainingQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeTrainingQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeTrainingQuery("a");
                    query.Select(
                        query,
                        "<CAST(a.TargetAttendance AS VARCHAR) + '/' + CAST((SELECT COUNT(*) FROM EmployeeTrainingHistory eth WHERE eth.EmployeeTrainingID = a.EmployeeTrainingID AND eth.IsAttending = 1) AS VARCHAR) AS Attendance>"
                    );
                    query.Where(query.IsActive == true);
                    if (FormType == string.Empty || FormType == "point")
                        query.Where(query.IsProposal == false);
                    else
                    {
                        query.Where(query.IsProposal == true);
                        if (FormType == "pps")
                            query.Where(query.LastUpdateByUserID == AppSession.UserLogin.UserID);
                    }
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
