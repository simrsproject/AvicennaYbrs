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
    public partial class InfusMonitoringCtl : BaseNosocomialCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string MonitoringType => !this.Request.QueryString["montype"].ToLower().Equals("infuscentral") ? "INF" : "INC";

        public override string Caption => !(this.MonitoringType == "INC") ? "Vena Perifier" : "Central (Infus)";

        public override string UrlHeaderEntry => Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialInfusEntry.aspx?tp=" + this.MonitoringType;

        public override string UrlDetailEntry => Helper.UrlRoot() + "/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialInfusDetailEntry.aspx?tp=" + this.MonitoringType;

        public override string GridClientID
        {
            get { return grdInfusMonitoring.ClientID; }
        }
        public override string RegistrationNoClientID
        {
            get { return txtRegistrationNo.ClientID; }
        }
        public override string MonitoringNoClientID
        {
            get { return txtMonitoringNo.ClientID; }
        }

        public void GridInfusMonitorDatabind()
        {
            grdInfusMonitoring.DataBind();
        }

        public override void Rebind(string registrationNo, int monNo)
        {
            txtRegistrationNo.Text = registrationNo;
            txtMonitoringNo.Text = monNo.ToString();

            grdInfusMonitoring.Rebind();
        }

        #region Nosocomial
        protected void grdInfusMonitoring_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdInfusMonitoring_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var qr = new NosocomialMonitoringQuery("a");
            var nm = new NosocomialMonitoring();

            if (txtMonitoringNo.Text.ToInt() == 0)
            {
                qr.Where(qr.RegistrationNo.In(MergeRegistrations), qr.MonitoringType == MonitoringType);
                qr.es.Top = 1;
                qr.OrderBy(qr.LastUpdateDateTime.Descending);
            }
            else
            {
                qr.Where(qr.RegistrationNo == txtRegistrationNo.Text, qr.MonitoringNo == txtMonitoringNo.Text.ToInt(), qr.MonitoringType == MonitoringType);
            }

            if (nm.Load(qr))
            {
                txtRegistrationNo.Text = nm.RegistrationNo;
                txtMonitoringNo.Text = nm.MonitoringNo.ToString();
                txtInstallationDate.Text =
                    Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
                InstallationDate = Convert.ToDateTime(nm.InstallationDateTime);
                txtLocation.Text = nm.Location;
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(nm.RoomID);
                txtRoomName.Text = sr.RoomName;

                if (!string.IsNullOrEmpty(nm.SRIVCatheter)) {
                    var stdr = new AppStandardReferenceItem();
                    if (stdr.LoadByPrimaryKey(AppEnum.StandardReference.IVChateter.ToString(), nm.SRIVCatheter)) {
                        txtChateterType.Text = stdr.ItemName;
                    }
                }
                if (!string.IsNullOrEmpty(nm.SRInfusSet))
                {
                    var stdr = new AppStandardReferenceItem();
                    if (stdr.LoadByPrimaryKey(AppEnum.StandardReference.InfusSet.ToString(), nm.SRInfusSet))
                    {
                        txtSetInfus.Text = stdr.ItemName;
                    }
                }
                chkIsSetBlood.Checked = nm.IsSetBlood ?? false;

                txtInstallationBy.Text = AppUser.GetUserName(nm.InstallationByUserID);

                txtTypeOfInfus.Text = nm.TypeOfInfus;
                txtAntibiotic.Text = nm.Antibiotic;
                txtOtherDrugs.Text = nm.OtherDrugs;
                txtProblem.Text = nm.Problem;
                txtMonitoring.Text = nm.Monitoring;

            }

            grdInfusMonitoring.Columns[0].Visible = !IsHistoryMode;
            grdInfusMonitoring.Columns[grdInfusMonitoring.Columns.Count - 2].Visible = !IsHistoryMode;
            grdInfusMonitoring.DataSource = InfusMonitoringDataTable(nm.RegistrationNo?? string.Empty, nm.MonitoringNo ?? 0);

        }

        protected void grdInfusMonitoring_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var seqno = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]).ToInt();

            var nmd = new NosocomialMonitoringInfus();
            if (nmd.LoadByPrimaryKey(txtRegistrationNo.Text, txtMonitoringNo.Text.ToInt(), seqno))
            {
                nmd.IsDeleted = true;
                nmd.Save();
            }

            grdInfusMonitoring.Rebind();
        }

        private DataTable InfusMonitoringDataTable(string registrationNo, int monitoringNo)
        {
            var query = new NosocomialMonitoringInfusQuery("a");
            var ppa = new AppUserQuery("p");
            query.LeftJoin(ppa).On(query.MonitoringByUserID == ppa.UserID);

            var ivCatheter = new AppStandardReferenceItemQuery("stdi1");
            query.LeftJoin(ivCatheter).On(ivCatheter.StandardReferenceID == AppEnum.StandardReference.IVChateter.ToString() && query.SRIVCatheter == ivCatheter.ItemID);

            var infusSet = new AppStandardReferenceItemQuery("stdi2");
            query.LeftJoin(infusSet).On(infusSet.StandardReferenceID == AppEnum.StandardReference.InfusSet.ToString() && query.SRInfusSet == infusSet.ItemID);

            var infusLocation = new AppStandardReferenceItemQuery("stdi3");
            query.LeftJoin(infusLocation).On(infusLocation.StandardReferenceID == AppEnum.StandardReference.InfusLocation.ToString() && query.SRInfusLocation == infusLocation.ItemID);

            query.Where(query.RegistrationNo == registrationNo, query.MonitoringNo == monitoringNo);
            query.OrderBy(query.MonitoringDateTime.Ascending);
            query.Select(query, ppa.UserName.As("MonitoringByName"), ivCatheter.ItemName.As("SRIVCatheterName"), infusSet.ItemName.As("SRInfusSetName"), infusLocation.ItemName.As("SRInfusLocationName"));
            var dtb = query.LoadDataTable();

            return dtb;
        }

        protected void grdInfusMonitoring_ItemDataBound(object sender, GridItemEventArgs e)
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