using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Module.RADT.EmrIp.MainContent;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NosocomialMain : BasePageDialog
    {
        private BaseNosocomialCtl _nosocomialCtl = null;

        protected BaseNosocomialCtl nosocomialCtl
        {

            get
            {
                if (_nosocomialCtl == null)
                {
                    switch (MonitoringType)
                    {
                        case "infus":
                        case "infuscentral":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/InfusMonitoringCtl.ascx");
                            break;
                        case "catheter":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/CatheterMonitoringCtl.ascx");
                            break;
                        case "ngt":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/NgtMonitoringCtl.ascx");
                            break;
                        case "surgery":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/SurgeryMonitoringCtl.ascx");
                            break;
                        case "ett":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/EttMonitoringCtl.ascx");
                            break;
                        case "bedrest":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/BedRestMonitoringCtl.ascx");
                            break;
                        case "hap":
                            _nosocomialCtl =
                                (BaseNosocomialCtl)LoadControl("~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/HapMonitoringCtl.ascx");
                            break;
                    }
                }

                return _nosocomialCtl;
            }
        }
        public string MonitoringType
        {
            get
            {
                return Request.QueryString["montype"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    var caption = nosocomialCtl.Caption;

                    var hmQ = new AppStandardReferenceItemQuery();
                    hmQ.Where(hmQ.StandardReferenceID == AppEnum.StandardReference.HaisMonitoring, hmQ.ReferenceID == MonitoringType);
                    hmQ.es.Top = 1;
                    var hm = new AppStandardReferenceItem();
                    if (hm.Load(hmQ))
                    {
                        var prgName = AppSession.Parameter.HaisMonitoringProgramName;
                        switch (prgName)
                        {
                            case "NAT":
                                caption = hm.CustomField;
                                break;
                            case "INT":
                                caption = hm.CustomField2;
                                break;
                            default:
                                caption = hm.ItemName;
                                break;
                        }
                    }
                    
                    this.Title = string.Format("HAIs Monitoring - {0} of : {1}", caption, pat.PatientName + " (MRN: " + pat.MedicalNo + ")");
                }

                tbarNosocomial.Items[0].Enabled = IsUserAddAble;
                tbarNosocomial.Items[1].Enabled = IsUserAddAble;
                tbarNosocomial.Items[2].Enabled = IsUserEditAble;

                // tampilan mode view (Fajri - 2023/11/06)
                if (Request.QueryString["mod"] == "view")
                {
                    tbarNosocomial.Items[0].Enabled = false;
                    tbarNosocomial.Items[1].Enabled = false;
                    tbarNosocomial.Items[2].Enabled = false;
                }
            }
            this.pnlNosocomial.Controls.Add(nosocomialCtl);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }



    }
}
