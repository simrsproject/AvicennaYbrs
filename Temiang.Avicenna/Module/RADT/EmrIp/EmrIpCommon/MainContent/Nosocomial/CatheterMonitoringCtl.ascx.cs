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
    public partial class CatheterMonitoringCtl : BaseNosocomialCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override string Caption
        {
            get { return "Catheter"; }
        }
        public override int HeightWindowDetailEntry
        {
            get { return 700; }
        }
        public override string UrlHeaderEntry
        {
            get { return Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialCatheterEntry.aspx"; }
        }
        public override string UrlDetailEntry
        {
            get { return Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialCatheterDetailEntry.aspx"; }
        }
        public override string GridClientID
        {
            get { return grdCatheterMonitoring.ClientID; }
        }
        public override string RegistrationNoClientID
        {
            get { return txtRegistrationNo.ClientID; }
        }
        public override string MonitoringNoClientID
        {
            get { return txtMonitoringNo.ClientID; }
        }

        public void GridCatheterMonitorDatabind()
        {
            grdCatheterMonitoring.DataBind();
        }

        public override void Rebind(string registrationNo, int monNo)
        {
            txtRegistrationNo.Text = registrationNo;
            txtMonitoringNo.Text = monNo.ToString();

            grdCatheterMonitoring.Rebind();
        }

        #region Fluid Balance
        protected void grdCatheterMonitoring_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdCatheterMonitoring_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var qr = new NosocomialMonitoringQuery("a");
            var nm = new NosocomialMonitoring();

            if (txtMonitoringNo.Text.ToInt() == 0)
            {
                qr.Where(qr.RegistrationNo.In(MergeRegistrations), qr.MonitoringType == "CAT");
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
                txtInstallationDate.Text =
                    Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);

                InstallationDate = Convert.ToDateTime(nm.InstallationDateTime);
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(nm.RoomID);
                txtRoomName.Text = sr.RoomName;

                txtReleaseDate.Text = nm.ReleaseDateTime == null ? string.Empty : Convert.ToDateTime(nm.ReleaseDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);

                txtInstallationBy.Text = AppUser.GetUserName(nm.InstallationByUserID);
                txtReleaseBy.Text = AppUser.GetUserName(nm.ReleaseByUserID);

                txtTypeOfCatheter.Text = nm.TypeOfCatheter;
                txtAntibiotic.Text = nm.Antibiotic;
                txtOtherDrugs.Text = nm.OtherDrugs;
                txtProblem.Text = nm.Problem;
                txtMonitoring.Text = nm.Monitoring;

            }

            grdCatheterMonitoring.Columns[0].Visible = !IsHistoryMode;
            grdCatheterMonitoring.Columns[grdCatheterMonitoring.Columns.Count-2].Visible = !IsHistoryMode;
            grdCatheterMonitoring.DataSource = CatheterMonitoringDataTable(nm.RegistrationNo?? string.Empty, nm.MonitoringNo ?? 0);

        }

        protected void grdCatheterMonitoring_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var seqno = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]).ToInt();

            var nmd = new NosocomialMonitoringCatheter();
            if (nmd.LoadByPrimaryKey(txtRegistrationNo.Text, txtMonitoringNo.Text.ToInt(), seqno))
            {
                nmd.IsDeleted = true;
                nmd.Save();
            }

            grdCatheterMonitoring.Rebind();
        }
        protected void grdCatheterMonitoring_ItemDataBound(object sender, GridItemEventArgs e)
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
        private DataTable CatheterMonitoringDataTable(string registrationNo, int monitoringNo)
        {
            var query = new NosocomialMonitoringCatheterQuery("a");
            var ppa = new AppUserQuery("p");
            query.LeftJoin(ppa).On(query.MonitoringByUserID == ppa.UserID);

            var generalCathe = new AppStandardReferenceItemQuery("stdi1");
            query.LeftJoin(generalCathe).On(generalCathe.StandardReferenceID == "GeneralChateterNo" && query.SRGeneralChateterNo == generalCathe.ItemID);

            var siliconCathe = new AppStandardReferenceItemQuery("stdi2");
            query.LeftJoin(siliconCathe).On(siliconCathe.StandardReferenceID == "SiliconChateterNo" && query.SRSiliconChateterNo == siliconCathe.ItemID);


            query.Where(query.RegistrationNo == registrationNo, query.MonitoringNo == monitoringNo);
            query.OrderBy(query.MonitoringDateTime.Ascending);
            query.Select(query, ppa.UserName.As("MonitoringByName"), generalCathe.ItemName.As("SRGeneralChateterNoName"), siliconCathe.ItemName.As("SRSiliconChateterNoName"));
            var dtb = query.LoadDataTable();

            return dtb;
        }

        #endregion



    }
}