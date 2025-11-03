using System;
using System.Collections.Generic;
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
    public partial class NosocomialHist : BasePageDialog
    {

        public virtual string LastEditRegistrationNo
        {
            get
            {
                return Request.QueryString["regnoedt"];
            }
        }
        private string MonitoringNo
        {
            get
            {
                return Request.QueryString["monno"];
            }
        }

        public string MonitoringType
        {
            get
            {
                return Request.QueryString["montype"];
            }
        }

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
                    }
                }

                _nosocomialCtl.ID = "monitoringDetail";
                _nosocomialCtl.IsHistoryMode = true;
                return _nosocomialCtl;
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
                    this.Title = "HAIs Monitoring of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }


            paneRight.Controls.Add(nosocomialCtl);

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Text = "Edit";

            if (!IsPostBack)
            {
                hdnEditRegistrationNo.Value = LastEditRegistrationNo;
                hdnEditMonitoringNo.Value = MonitoringNo;
                var monitoringDetail = (BaseNosocomialCtl)Helper.FindControlRecursive(paneRight, "monitoringDetail");
                monitoringDetail.Rebind(hdnEditRegistrationNo.Value, hdnEditMonitoringNo.Value.ToInt());
            }
        }

        protected void grdHeader_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var qr = new NosocomialMonitoringQuery();
            qr.Where(qr.RegistrationNo.In(MergeRegistrations));
            qr.OrderBy(qr.MonitoringNo.Descending);

            var montype = MonitoringType;
            switch (montype)
            {
                case "infus":
                    qr.Where(qr.MonitoringType == "INF");
                    break;
                case "infuscentral":
                    qr.Where(qr.MonitoringType == "INC");
                    break;
                case "ngt":
                    qr.Where(qr.MonitoringType == "NGT");
                    break;
                case "catheter":
                    qr.Where(qr.MonitoringType == "CAT");
                    break;
                case "surgery":
                    qr.Where(qr.MonitoringType == "SUR");
                    break;
                case "ett":
                    qr.Where(qr.MonitoringType == "ETT");
                    break;
                case "bedrest":
                    qr.Where(qr.MonitoringType == "BDR");
                    break;
            }

            grdHeader.DataSource = qr.LoadDataTable();
        }

        protected void grdHeader_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var dataItem = grdHeader.SelectedItems[0] as GridDataItem;
            hdnEditRegistrationNo.Value = Convert.ToString(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["RegistrationNo"]);
            hdnEditMonitoringNo.Value = Convert.ToString(dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["MonitoringNo"]);
            var monitoringDetail = (BaseNosocomialCtl)Helper.FindControlRecursive(paneRight, "monitoringDetail");
            monitoringDetail.Rebind(hdnEditRegistrationNo.Value, hdnEditMonitoringNo.Value.ToInt());
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {

            var script = string.Format(@"oArg.editRegNo = document.getElementById('{0}').value;
            oArg.editMonNo = document.getElementById('{1}').value;", hdnEditRegistrationNo.ClientID,
                hdnEditMonitoringNo.ClientID);
            return script;
        }

        protected void grdHeader_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EditHist")
            {
                var monitoringNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MonitoringNo"]).ToInt();


            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var items = grdHeader.MasterTableView.GetSelectedItems();
            if (items.Length == 0 && grdHeader.MasterTableView.DetailTables.Count > 0)
                items = grdHeader.MasterTableView.DetailTables[0].GetSelectedItems();

            if (items.Length > 0)
            {
                var dataItem = items[0];
                var registrationNo = dataItem.GetDataKeyValue("RegistrationNo").ToString();
                var monitoringNo = dataItem.GetDataKeyValue("MonitoringNo").ToInt();

                var nos = new NosocomialMonitoring();
                if (nos.LoadByPrimaryKey(registrationNo, monitoringNo))
                {
                    // Set LastUpdateDateTime supaya menjadi yg terakhir dan ini akan dijadikan sbg default monitoring
                    nos.LastUpdateDateTime = DateTime.Now;
                    nos.Save();
                }
            }
        }
    }
}
