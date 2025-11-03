using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class BedBookingList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.BedBooking;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboBedStatus, AppEnum.StandardReference.BedStatus);
                if (AppSession.Parameter.IsBookingBedCharged)
                    cboBedStatus.SelectedValue = AppSession.Parameter.BedStatusUnoccupied;
                
                txtReady.ReadOnly = true;
                txtReady.BackColor = System.Drawing.Color.Green;
                txtOccupied.ReadOnly = true;
                txtOccupied.BackColor = System.Drawing.Color.Red;
                txtBooked.ReadOnly = true;
                txtBooked.BackColor = System.Drawing.Color.Brown;
                txtPending.ReadOnly = true;
                txtPending.BackColor = System.Drawing.Color.Orange;
                txtCleaning.ReadOnly = true;
                txtCleaning.BackColor = System.Drawing.Color.Yellow;
                txtReserved.ReadOnly = true;
                txtReserved.BackColor = System.Drawing.Color.Blue;
                txtRepaired.ReadOnly = true;
                txtRepaired.BackColor = System.Drawing.Color.Purple;
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Beds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable Beds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboRoomID.SelectedValue) && string.IsNullOrEmpty(cboClassID.SelectedValue) && 
                    string.IsNullOrEmpty(cboBedStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new BedQuery("a");
                var srQ = new ServiceRoomQuery("b");
                var suQ = new ServiceUnitQuery("c");
                var cq = new ClassQuery("f");
                var std = new AppStandardReferenceItemQuery("e");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.BedID,
                        cq.ClassName,
                        query.RegistrationNo,
                        query.RoomID,
                        srQ.RoomName,
                        srQ.ServiceUnitID,
                        suQ.ServiceUnitName,
                        query.SRBedStatus,
                        std.ItemName.As("BedStatusName"), 
                        @"<CAST(1 AS BIT) AS IsEditable>"
                    );

                query.InnerJoin(srQ).On(query.RoomID == srQ.RoomID);
                query.InnerJoin(cq).On
                    (
                        query.ClassID == cq.ClassID &
                        cq.IsActive == true
                    );
                query.InnerJoin(suQ).On(srQ.ServiceUnitID == suQ.ServiceUnitID);
                query.InnerJoin(std).On(std.StandardReferenceID == "BedStatus" & std.ItemID == query.SRBedStatus);

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(srQ.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(srQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(query.RoomID == cboRoomID.SelectedValue);
                if (cboClassID.SelectedValue != string.Empty)
                    query.Where(query.ClassID == cboClassID.SelectedValue);
                if (!string.IsNullOrEmpty(cboBedStatus.SelectedValue))
                    query.Where(query.SRBedStatus == cboBedStatus.SelectedValue);
                
                query.Where
                    (
                        query.IsActive == true
                    );

                query.OrderBy(suQ.ServiceUnitName.Ascending, srQ.RoomName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    if (AppSession.Parameter.IsBookingBedCharged)
                    {
                        if (row["SRBedStatus"].ToString() == AppSession.Parameter.BedStatusUnoccupied)
                            row["IsEditable"] = true;
                        else 
                            row["IsEditable"] = false;
                    }
                    else
                    {
                        if (row["SRBedStatus"].ToString() == AppSession.Parameter.BedStatusRepaired)
                            row["IsEditable"] = false;
                        else
                            row["IsEditable"] = true;
                    }
                }

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        public System.Drawing.Color GetColor(object srBedStatus)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (srBedStatus.ToString())
            {
                case "BedStatus-01":
                    color = System.Drawing.Color.Green;
                    break;

                case "BedStatus-02":
                    color = System.Drawing.Color.Red;
                    break;

                case "BedStatus-03":
                    color = System.Drawing.Color.Brown;
                    break;

                case "BedStatus-04":
                    color = System.Drawing.Color.Orange;
                    break;

                case "BedStatus-05":
                    color = System.Drawing.Color.Yellow;
                    break;

                case "BedStatus-06":
                    color = System.Drawing.Color.Blue;
                    break;

                case "BedStatus-07":
                    color = System.Drawing.Color.Purple;
                    break;
            }

            return color;
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRoomID.Items.Clear();
            cboRoomID.Text = string.Empty;
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ServiceRoomQuery("a");
            var suq = new ServiceUnitQuery("b");
            query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID &&
                                    suq.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            query.Where(query.IsActive == true);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            query.Select(query.RoomID, query.RoomName);
            query.OrderBy(query.RoomID.Ascending);
            query.es.Top = 20;
            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        protected void cboRoomID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboClassID.Items.Clear();
            cboClassID.Text = string.Empty;
        }

        protected void cboClassID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ClassName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ClassID"].ToString();
        }

        protected void cboClassID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ClassQuery("a");
            var bedq = new BedQuery("b");
            query.InnerJoin(bedq).On(bedq.ClassID == query.ClassID);
            query.Where(query.IsActive == true, query.IsInPatientClass == true, bedq.IsActive == true);
            if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                query.Where(bedq.RoomID == cboRoomID.SelectedValue);
            query.Select(query.ClassID, query.ClassName);
            query.OrderBy(query.ClassID.Ascending);
            query.es.Distinct = true;

            cboClassID.DataSource = query.LoadDataTable();
            cboClassID.DataBind();
        }
    }
}
