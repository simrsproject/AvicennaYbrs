using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent
{
    public partial class NosocomialCtl : BaseMainContentCtl
    {
        #region Properties
        public string TabStripClientID
        {
            get { return tspNosocomial.ClientID; }
        }
        public string InfusMonitoringGridClientID
        {
            get { return infusMonitoringCtl.GridClientID; }
        }
        public string NgtMonitoringGridClientID
        {
            get { return ngtMonitoringCtl.GridClientID; }
        }
        public string CatheterMonitoringGridClientID
        {
            get { return catheterMonitoringCtl.GridClientID; }
        }
        public string SurgeryMonitoringGridClientID
        {
            get { return surgeryMonitoringCtl.GridClientID; }
        }
        public string EttMonitoringGridClientID
        {
            get { return ettMonitoringCtl.GridClientID; }
        }
        public string BedRestMonitoringGridClientID
        {
            get { return bedRestMonitoringCtl.GridClientID; }
        }

        public string InfusMonitoringNoClientID
        {
            get { return infusMonitoringCtl.MonitoringNoClientID; }
        }
        public string NgtMonitoringNoClientID
        {
            get { return ngtMonitoringCtl.MonitoringNoClientID; }
        }
        public string CatheterMonitoringNoClientID
        {
            get { return catheterMonitoringCtl.MonitoringNoClientID; }
        }
        public string SurgeryMonitoringNoClientID
        {
            get { return surgeryMonitoringCtl.MonitoringNoClientID; }
        }
        public string EttMonitoringNoClientID
        {
            get { return ettMonitoringCtl.MonitoringNoClientID; }
        }        
        public string BedRestMonitoringNoClientID
        {
            get { return bedRestMonitoringCtl.MonitoringNoClientID; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetSelectedIndexTab(int index)
        {
            tspNosocomial.SelectedIndex = index;
            mpgNosocomial.PageViews[index].Selected = true;
        }
 
    }
}