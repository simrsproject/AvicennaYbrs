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
    public partial class SurgeryMonitoringCtl : BaseNosocomialCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override string Caption
        {
            get { return "Surgery"; }
        }
        public override string UrlHeaderEntry
        {
            get { return Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialSurgeryEntry.aspx"; }
        }
        public override string UrlDetailEntry
        {
            get { return Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialSurgeryDetailEntry.aspx"; }
        }
        public override string GridClientID
        {
            get { return grdSurgeryMonitoring.ClientID; }
        }
        public override string RegistrationNoClientID
        {
            get { return txtRegistrationNo.ClientID; }
        }
        public override string MonitoringNoClientID
        {
            get { return txtMonitoringNo.ClientID; }
        }


        public void GridSurgeryMonitorDatabind()
        {
            grdSurgeryMonitoring.DataBind();
        }


        public override void Rebind(string registrationNo, int monNo)
        {
            txtRegistrationNo.Text = registrationNo;
            txtMonitoringNo.Text = monNo.ToString();

            grdSurgeryMonitoring.Rebind();
        }

        #region Nosocomial
        protected void grdSurgeryMonitoring_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdSurgeryMonitoring_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var qr = new NosocomialMonitoringQuery("a");
            var nm = new NosocomialMonitoring();
            if (txtMonitoringNo.Text.ToInt() == 0)
            {
                qr.Where(qr.RegistrationNo.In(MergeRegistrations), qr.MonitoringType == "SUR");
                qr.es.Top = 1;
                qr.OrderBy(qr.LastUpdateDateTime.Descending);
            }
            else
            {
                qr.Where(qr.RegistrationNo == txtRegistrationNo.Text, qr.MonitoringNo == txtMonitoringNo.Text.ToInt());
            }

            if (nm.Load(qr))
            {
                txtRegistrationNo.Text = nm.RegistrationNo;
                txtMonitoringNo.Text = nm.MonitoringNo.ToString();
                txtSurgeryDate.Text =
                    Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                InstallationDate = Convert.ToDateTime(nm.InstallationDateTime);
                txtLocation.Text = nm.Location;

                txtAntibiotic.Text = nm.Antibiotic;
                txtOtherDrugs.Text = nm.OtherDrugs;
                txtMonitoring.Text = nm.Monitoring;

                PopulateBookingRoomInfo(nm.ReferenceNo);

            }

            grdSurgeryMonitoring.Columns[0].Visible = !IsHistoryMode;
            grdSurgeryMonitoring.Columns[grdSurgeryMonitoring.Columns.Count - 2].Visible = !IsHistoryMode;
            grdSurgeryMonitoring.DataSource = SurgeryMonitoringDataTable(nm.RegistrationNo?? string.Empty, nm.MonitoringNo ?? 0);

        }
        private void PopulateBookingRoomInfo(string bookingNo)
        {
            if (string.IsNullOrWhiteSpace(bookingNo)) return;

            var sur = new ServiceUnitBooking();
            sur.LoadByPrimaryKey(bookingNo);

            var ps = new PpiProcedureSurveillance();
            ps.LoadByPrimaryKey(bookingNo);

            txtReferenceNo.Text = bookingNo;
            txtSurgeryByName.Text = Paramedic.GetParamedicName(sur.ParamedicID);
            txtWoundClassification.Text =
                StandardReference.GetItemName(AppEnum.StandardReference.WoundClassification, ps.SRWoundClassification);
            txtAsaScore.Text = StandardReference.GetItemName(AppEnum.StandardReference.AsaScore, ps.SRAsaScore);
        }
        protected void grdSurgeryMonitoring_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var seqno = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]).ToInt();

            var nmd = new NosocomialMonitoringSurgery();
            if (nmd.LoadByPrimaryKey(txtRegistrationNo.Text, txtMonitoringNo.Text.ToInt(), seqno))
            {
                nmd.IsDeleted = true;
                nmd.Save();
            }

            grdSurgeryMonitoring.Rebind();
        }

        private DataTable SurgeryMonitoringDataTable(string registrationNo, int monitoringNo)
        {
            var query = new NosocomialMonitoringSurgeryQuery("a");
            var ppa = new AppUserQuery("p");
            query.LeftJoin(ppa).On(query.MonitoringByUserID == ppa.UserID);

            var stdi = new AppStandardReferenceItemQuery("stdi");
            query.LeftJoin(stdi).On(stdi.StandardReferenceID == "ExudateCharacter" && query.SRExudateCharacter == stdi.ItemID);

            query.Where(query.RegistrationNo == registrationNo, query.MonitoringNo == monitoringNo);
            query.OrderBy(query.MonitoringDateTime.Ascending);
            query.Select(query, ppa.UserName.As("MonitoringByName"), stdi.ItemName.As("SRExudateCharacterName"));
            var dtb = query.LoadDataTable();

            return dtb;
        }

        protected void grdSurgeryMonitoring_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("IsDeleted") == DBNull.Value) return;
                var isVoid = Convert.ToBoolean(item.GetDataKeyValue("IsDeleted"));
                //TableCell cell = item["ItemName"];
                if (isVoid)
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }
        }
        #endregion


 
    }
}