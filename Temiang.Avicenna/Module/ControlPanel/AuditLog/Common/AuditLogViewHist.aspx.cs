using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.ControlPanel
{
    /// <summary>
    /// View History Edit per Field
    /// </summary>
    public partial class AuditLogViewHist : BasePageDialog
    {
        protected String TableName => Request.QueryString["tbnm"];
        protected string FieldName => Request.QueryString["fnm"];
        protected String PrimaryKeyData
        {
            get { return HttpUtility.UrlDecode(Request.QueryString["pkd"]); }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Title = "Edit History";

            Footer.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                timelineEdit.DataSource = FieldEditHist();
                timelineEdit.DataBind();
            }
        }
        private List<FieldEdit> FieldEditHist()
        {
            var retVal = new List<FieldEdit>();
            var au = new AuditLogQuery("a");
            var aud = new AuditLogDataQuery("b");

            au.InnerJoin(aud).On(au.AuditLogID == aud.AuditLogID);
            au.Where(au.TableName == TableName, au.AuditActionType == "U", au.PrimaryKeyData == PrimaryKeyData, aud.ColumnName == FieldName);
            au.Select(au.ActionByUserID, au.LogDateTime, aud.OldValue, aud.NewValue);
            var dtbEdited = au.LoadDataTable();

            if (dtbEdited.Rows.Count > 0)
            {
                // Create Record
                var auc = new AuditLog();
                auc.Query.Where(auc.Query.TableName == TableName, auc.Query.AuditActionType == "C", auc.Query.PrimaryKeyData == PrimaryKeyData);
                if (auc.Query.Load())
                {
                    var row = dtbEdited.Rows[0];
                    var fe = new FieldEdit();
                    fe.LogDateTime = auc.LogDateTime ?? DateTime.Now;
                    fe.CurrentValue = Convert.ToString(row["OldValue"]).Replace(Environment.NewLine,"<br />");
                    fe.UserName = auc.ActionByUserID;
                    fe.UserName = string.Concat(fe.UserName," - ", AppUser.GetUserName(fe.UserName));
                    fe.Title = fe.LogDateTime.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                    retVal.Add(fe);
                }
            }
            foreach (DataRow row in dtbEdited.Rows)
            {
                var fe = new FieldEdit();
                fe.LogDateTime = Convert.ToDateTime(row["LogDateTime"]);
                fe.CurrentValue = Convert.ToString(row["NewValue"]).Replace(Environment.NewLine,"<br />");
                fe.UserName = Convert.ToString(row["ActionByUserID"]);
                fe.UserName = string.Concat(fe.UserName," - ", AppUser.GetUserName(fe.UserName));
                fe.Title = fe.LogDateTime.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                retVal.Add(fe);
            }

            return retVal.OrderByDescending (o => o.LogDateTime).ToList();
        }

        private class FieldEdit
        {
            public string Title { get; set; }
            public DateTime LogDateTime { get; set; }
            public string UserName { get; set; }
            public string CurrentValue { get; set; }
        }
    }
}