using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.ControlPanel
{
    public partial class AuditLogList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.AuditLogView;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session.Remove(SessionNameForList);

            ((GridDateTimeColumn)grdList.Columns[5]).DataFormatString = "{0:" + AppConstant.DisplayFormat.DateTimeSecond + "}";
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            this.Session[SessionNameForList] = null;
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }
        private DateTime GetDateTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }
        private DataTable AuditLogs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
                //Pengecualian nama esession query untuk audit Log, lihat di basepage
                string sessionNameForQuery = string.Format("_que.{0}", AppConstant.Program.AuditLogView);
                AuditLogQuery query;

                query = new AuditLogQuery();
                if (!string.IsNullOrEmpty(txtTableName.Text))
                {
                    string searchText = string.Format("%{0}%", txtTableName.Text);
                    query.Where(query.TableName.Like(searchText));
                }
                if (!string.IsNullOrEmpty(cboAuditActionType.SelectedValue))
                {
                    query.Where(query.AuditActionType == cboAuditActionType.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtPrimaryKeyData.Text))
                {
                    string searchText = string.Format("%{0}%", txtPrimaryKeyData.Text);
                    query.Where(query.PrimaryKeyData.Like(searchText));
                }
                if (!string.IsNullOrEmpty(txtActionByUserID.Text))
                {
                    query.Where(query.ActionByUserID == txtActionByUserID.Text);
                }
                if (txtLogDateStart.SelectedDate != null && txtLogDateEnd.SelectedDate != null)
                {
                    query.Where(query.LogDateTime >= GetDateTime(txtLogDateStart.SelectedDate ?? DateTime.Now, txtLogTimeStart.SelectedDate ?? DateTime.Now));
                    query.Where(query.LogDateTime <= GetDateTime(txtLogDateEnd.SelectedDate ?? DateTime.Now, txtLogTimeEnd.SelectedDate ?? DateTime.Now));
                }
                else if (txtLogDateStart.SelectedDate != null)
                    query.Where(query.LogDateTime >= GetDateTime(txtLogDateStart.SelectedDate ?? DateTime.Now, txtLogTimeStart.SelectedDate ?? DateTime.Now));
                else if (txtLogDateEnd.SelectedDate == null)
                    query.Where(query.LogDateTime <= GetDateTime(txtLogDateEnd.SelectedDate ?? DateTime.Now, txtLogTimeEnd.SelectedDate ?? DateTime.Now));

                query.OrderBy(query.AuditLogID.Descending);


                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = AuditLogs;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "grdAuditLogData2":
                case "grdAuditLogData":
                    string id = dataItem.GetDataKeyValue("AuditLogID").ToString();
                    AuditLogDataQuery query = new AuditLogDataQuery();
                    query.Where(query.AuditLogID == id);
                    e.DetailTableView.DataSource = query.LoadDataTable();
                    break;
                case "grdDetailInSameTime":
                    DateTime dateTime = Convert.ToDateTime(dataItem.GetDataKeyValue("LogDateTime"));
                    dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
                    string auditLogID = dataItem.GetDataKeyValue("AuditLogID").ToString();
                    AuditLogQuery auditLogQuery = new AuditLogQuery();
                    auditLogQuery.Where
                        (
                            auditLogQuery.AuditLogID != auditLogID &
                            auditLogQuery.LogDateTime >= dateTime &
                            auditLogQuery.LogDateTime <= dateTime.AddSeconds(1)
                        );
                    auditLogQuery.OrderBy
                        (
                            auditLogQuery.AuditLogID,
                            esOrderByDirection.Descending
                        );
                    e.DetailTableView.DataSource = auditLogQuery.LoadDataTable();
                    break;
            }
        }
    }
}