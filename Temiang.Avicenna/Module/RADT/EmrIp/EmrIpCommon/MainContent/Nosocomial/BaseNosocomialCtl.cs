using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT.EmrIp.MainContent;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent
{
    public class BaseNosocomialCtl : BaseMainContentCtl
    {
        protected HiddenField hdnEditRegistrationNo
        {
            get
            {
                return (HiddenField)Helper.FindControlRecursive(this.Page, "hdnEditRegistrationNo");
            }
        }
        protected HiddenField hdnEditMonitoringNo
        {
            get
            {
                return (HiddenField)Helper.FindControlRecursive(this.Page, "hdnEditMonitoringNo");
            }
        }

        protected virtual List<string> MergeRegistrations
        {
            get
            {
                return AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            }
        }
        public virtual void Rebind(string registrationNo, int monNo)
        {

        }

        public virtual bool IsMonitoringEditable(GridItem gridItem)
        {
            var dataItem = gridItem.DataItem;
            if (!(DataBinder.Eval(dataItem, "MonitoringByUserID").Equals(AppSession.UserLogin.UserID)))
                return false;

            if ((DataBinder.Eval(dataItem, "IsDeleted") != DBNull.Value &&
                 DataBinder.Eval(dataItem, "IsDeleted").Equals(true)))
                return false;

            // Batas waktu
            if (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(dataItem, "MonitoringDateTime"))))
                return false;

            return true;
        }
        public virtual bool IsMonitoringDeleteable(GridItem gridItem)
        {
            return IsMonitoringEditable(gridItem);
        }

        public virtual string ScriptMonitoringEdit(GridItem gridItem, string monType)
        {
            // entryNosocomialDetail(mod, montype, regno, monno, seqno)
            var dataItem = gridItem.DataItem;
            return !IsMonitoringEditable(gridItem)
                ? string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />", Helper.UrlRoot())
                : string.Format(
                    "<a href=\"#\" onclick=\"javascript:entryNosocomialDetail('edit','{0}', '{1}','{2}','{3}'); return false;\"><img src=\"{4}/Images/Toolbar/edit16.png\"  alt=\"Edit\" /></a>",
                    monType,DataBinder.Eval(dataItem, "RegistrationNo"),DataBinder.Eval(dataItem, "MonitoringNo"),DataBinder.Eval(dataItem, "SequenceNo"), Helper.UrlRoot());
        }
        public virtual string Caption
        {
            get { return string.Empty; }
        }
        public virtual string UrlHeaderEntry
        {
            get { return string.Empty; }
        }
        public virtual string UrlDetailEntry
        {
            get { return string.Empty; }
        }
        public virtual int HeightWindowDetailEntry
        {
            get { return 750; }
        }
        public virtual string RegistrationNoClientID
        {
            get { return string.Empty; }
        }
        public virtual string MonitoringNoClientID
        {
            get { return string.Empty; }
        }
        public virtual string GridClientID
        {
            get { return string.Empty;}
        } 
        protected DateTime InstallationDate
        {
            get
            {
                if (ViewState["insdt"] == null) return DateTime.Today;
                return (DateTime)ViewState["insdt"];
            }
            set { ViewState["insdt"] = value; }
        }
        public bool IsHistoryMode
        {
            get
            {
                if (ViewState["histmod"] == null) return false;
                return (bool)ViewState["histmod"];
            }
            set { ViewState["histmod"] = value; }
        }
    }
}