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
    public partial class BedBookingReleaseList : BasePage
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

            ProgramID = AppConstant.Program.BedBookingRelease;

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
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable Beds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboRoomID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Bed Booking Release")) return null;

                var query = new BedQuery("a");
                var srQ = new ServiceRoomQuery("b");
                var suQ = new ServiceUnitQuery("c");
                var regQ = new RegistrationQuery("e");
                var patQ = new PatientQuery("f");
                var parQ = new ParamedicQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                esQueryItem group = new esQueryItem(query, "Group", esSystemType.String);
                group = suQ.ServiceUnitName + " - " + srQ.RoomName;

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.BedID,
                        query.RegistrationNo,
                        query.RoomID,
                        srQ.RoomName,
                        srQ.ServiceUnitID,
                        suQ.ServiceUnitName,
                        query.RoomID,
                        group.As("Group"),
                        patQ.MedicalNo,
                        patQ.PatientName,
                        parQ.ParamedicName,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(srQ).On(query.RoomID == srQ.RoomID);
                query.InnerJoin(suQ).On(srQ.ServiceUnitID == suQ.ServiceUnitID);
                query.InnerJoin(regQ).On(query.RegistrationNo == regQ.RegistrationNo);
                query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
                query.InnerJoin(parQ).On(regQ.ParamedicID == parQ.ParamedicID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patQ.SRSalutation == sal.ItemID);

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(srQ.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.Text != string.Empty)
                    query.Where(srQ.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(query.RoomID == cboRoomID.SelectedValue);
                query.Where
                    (
                        query.IsActive == true,
                        query.SRBedStatus == AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusBooked)
                    );

                query.OrderBy(query.BedID.Ascending);

                return query.LoadDataTable();
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

            //if (eventArgument == "rebind")
            //    grdList.Rebind();

            string command = eventArgument.Split(':')[0];
            switch (command)
            {
                case "rebind":
                    grdList.Rebind();
                    break;
                case "release":
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(eventArgument.Split(':')[1]))
                        {
                            var regNo = bed.RegistrationNo;
                            //if (AppSession.Parameter.IsBookingBedCharged)
                            {
                                bed.RegistrationNo = string.Empty;
                                bed.SRBedStatus = AppParameter.GetParameterValue(AppParameter.ParameterItem.BedStatusUnoccupied);
                                bed.BedStatusUpdatedBy = AppSession.UserLogin.UserID;
                                bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                bed.BookingDateTime = null;

                                bed.Save();
                            }

                            if (!string.IsNullOrEmpty(regNo))
                            {
                                var bedmanag = new BedManagementCollection();
                                bedmanag.Query.Where(bedmanag.Query.BedID == bed.BedID, 
                                                     bedmanag.Query.RegistrationNo == regNo,
                                                     bedmanag.Query.IsReleased == false,
                                                     bedmanag.Query.IsVoid == false);
                                bedmanag.LoadAll();
                                foreach (var b in bedmanag)
                                {
                                    b.IsReleased = true;
                                    b.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                                    b.ReleasedByUserID = AppSession.UserLogin.UserID;
                                    b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                    b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                }
                                bedmanag.Save();
                            }
                        }
                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                    
                    grdList.Rebind();
                    break;
            }
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
    }
}
