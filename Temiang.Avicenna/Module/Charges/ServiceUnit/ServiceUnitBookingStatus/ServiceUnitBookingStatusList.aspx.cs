using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ServiceUnitBookingStatusList : BasePage
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

            ProgramID = AppConstant.Program.ServiceUnitBookingStatus;

            if (!IsPostBack)
            {
                txtBookingDate.SelectedDate = DateTime.Now;

                var rooms = new ServiceRoomCollection();
                rooms.Query.Where(rooms.Query.IsActive == true, rooms.Query.IsOperatingRoom == true, rooms.Query.IsShowOnBookingOT == true);
                rooms.Query.OrderBy(rooms.Query.RoomID.Ascending);
                rooms.LoadAll();

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var r in rooms)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(r.RoomName, r.RoomID));
                }

                cboStatus.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboStatus.Items.Add(new RadComboBoxItem("Validate", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "2"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

        private DataTable ServiceUnitBookings
        {
            get
            {
                var isEmptyFilter = txtBookingDate.IsEmpty && string.IsNullOrEmpty(cboRoomID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && 
                    string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Booking")) return null;

                var query = new ServiceUnitBookingQuery("a");
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var unit = new ServiceUnitQuery("s");
                var unit2 = new ServiceUnitQuery("s2");
                var room = new ServiceRoomQuery("d");
                var sal = new AppStandardReferenceItemQuery("sal");

                var par1 = new ParamedicQuery("par1");
                var anes1 = new ParamedicQuery("anes1");
                var anes2 = new ParamedicQuery("anes2");
                var inst1 = new ParamedicQuery("inst1");
                var assit1 = new ParamedicQuery("assit1");
                var opr = new AppStandardReferenceItemQuery("opr");
                var opr2 = new AppStandardReferenceItemQuery("opr2");
                var anestype = new AppStandardReferenceItemQuery("anestype");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.BookingNo,
                        query.BookingDateTimeFrom,
                        query.BookingDateTimeTo,
                        @"<ISNULL(s2.ServiceUnitName, s.ServiceUnitName) AS ServiceUnitName>",
                        //unit.ServiceUnitName,
                        query.RegistrationNo,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qr.BedID,
                        sal.ItemName.As("SalutationName"),

                        query.Diagnose,
                        query.PostDiagnosis,
                        opr.ItemName.As("Procedure"),
                        opr2.ItemName.As("Procedure2"),
                        anestype.ItemName.As("AnestheticType"),

                        //query.IsCito,
                        @"<ISNULL(a.IsCito, 0) AS IsCito>",
                        par1.ParamedicName.As("Surgion"),
                        anes1.ParamedicName.As("Anesthetist"),
                        anes2.ParamedicName.As("AssistentAnesthetist"),
                        inst1.ParamedicName.As("Instrumentator1"),
                        assit1.ParamedicName.As("Assistent"),
                        query.Notes,
                        query.VoidReason,
                        query.IsApproved,
                        query.IsValidate,
                        query.IsVoid,

                        @"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) FROM EpisodeProcedure ep WHERE ep.RegistrationNo = a.RegistrationNo AND ep.BookingNo = a.BookingNo AND ep.IsVoid = 0), 0) AS IsEpisodeProcedure >",
                        @"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) FROM ServiceUnitBookingOperatingNotes nt WHERE nt.BookingNo = a.BookingNo AND nt.IsVoid = 0), 0) AS IsOperatingNotes >",
                        @"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) FROM PatientHealthRecord phr WHERE phr.RegistrationNo = a.RegistrationNo AND phr.ReferenceNo = a.BookingNo), 0) AS IsPatientHealthRecord >"
                    );
                query.LeftJoin(qr).On(query.RegistrationNo == qr.RegistrationNo);
                query.InnerJoin(qp).On(query.PatientID == qp.PatientID);
                query.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(unit2).On(query.FromServiceUnitID == unit2.ServiceUnitID);
                query.InnerJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                query.InnerJoin(par1).On(query.ParamedicID == par1.ParamedicID);
                query.LeftJoin(anes1).On(query.ParamedicIDAnestesi == anes1.ParamedicID);
                query.LeftJoin(anes2).On(query.AssistantIDAnestesi == anes2.ParamedicID);
                query.LeftJoin(inst1).On(query.Instrumentator1 == inst1.ParamedicID);
                query.LeftJoin(assit1).On(query.AssistantID1 == assit1.ParamedicID);
                query.LeftJoin(opr).On(query.SRProcedure1 == opr.ItemID && opr.StandardReferenceID == "Procedure");
                query.LeftJoin(opr2).On(query.SRProcedure2 == opr2.ItemID && opr2.StandardReferenceID == "Procedure");
                query.LeftJoin(anestype).On(query.SRAnestesiPlan == anestype.ItemID && anestype.StandardReferenceID == "Anestesi");

                query.Where(room.IsShowOnBookingOT == true);

                if (!txtBookingDate.IsEmpty)
                    query.Where(query.BookingDateTimeFrom >= txtBookingDate.SelectedDate, query.BookingDateTimeFrom < txtBookingDate.SelectedDate.Value.Date.AddDays(1));
                if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                    query.Where(query.RoomID == cboRoomID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where
                        (qr.Or
                             (
                                 string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                                 string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                                 string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                                 string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                 string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                             )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue) )
                {
                    switch (cboStatus.SelectedValue)
                    {
                        case "0":
                            query.Where(query.IsValidate == true);
                            break;
                        case "1":
                            query.Where(query.IsApproved == true);
                            break;
                        case "2":
                            query.Where(query.IsVoid == true);
                            break;
                    }
                }
               
                query.OrderBy(query.BookingDateTimeFrom.Ascending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ServiceUnitBookings;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void grdList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                grdList.DataSource = null;
                grdList.Rebind();
            }
        }

        private string _bookingNo = string.Empty;
        protected void grdList_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
                _bookingNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["BookingNo"]);

            if (e.Item is GridNestedViewItem)
            {
                // Populate
                var grd2 = (RadGrid)e.Item.FindControl("grdListTimeStamp");
                grd2.DataSource = ServiceUnitBookingDetails(_bookingNo);
                grd2.Rebind();

                _bookingNo = string.Empty;
            }
        }

        private DataTable ServiceUnitBookingDetails(string bookingNo)
        {
            var query = new ServiceUnitBookingQuery("a");
            query.Select(
                query.BookingNo,
                @"<CASE WHEN a.PreSurgeryDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsPreSurgery'>",
                @"<CONVERT(VARCHAR(20), ISNULL(a.PreSurgeryDateTime, GETDATE()), 113) AS PreSurgeryDateTimes>",

                @"<CASE WHEN a.AnesthesiaDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsAnesthesia'>",
                @"<CONVERT(VARCHAR(20), ISNULL(a.AnesthesiaDateTime, GETDATE()), 113) AS AnesthesiaDateTimes>",

                @"<CASE WHEN a.SurgeryDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsSurgery'>",
                @"<CONVERT(VARCHAR(20), ISNULL(a.SurgeryDateTime, GETDATE()), 113) AS SurgeryDateTimes>",

                @"<CASE WHEN a.PostSurgeryDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsPostSurgery'>",
                @"<CONVERT(VARCHAR(20), ISNULL(a.PostSurgeryDateTime, GETDATE()), 113) AS PostSurgeryDateTimes>",

                @"<CASE WHEN a.MoveToTheWardDateTime IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsMoveToTheWard'>",
                @"<CONVERT(VARCHAR(20), ISNULL(a.MoveToTheWardDateTime, GETDATE()), 113) AS MoveToTheWardDateTimes>",

                query.IsApproved,
                query.IsValidate,
                query.IsVoid
                );
            query.Where(query.BookingNo == bookingNo);

            DataTable dtb = query.LoadDataTable();

            return dtb;
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        private void UpdateStatus(string bookingNo, string status)
        {
            var sub = new ServiceUnitBooking();
            if (sub.LoadByPrimaryKey(bookingNo))
            {
                switch (status)
                {
                    case "1":
                        sub.PreSurgeryDateTime = (new DateTime()).NowAtSqlServer();
                        sub.PreSurgeryByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "2":
                        sub.AnesthesiaDateTime = (new DateTime()).NowAtSqlServer();
                        sub.AnesthesiaByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "3":
                        sub.SurgeryDateTime = (new DateTime()).NowAtSqlServer();
                        sub.SurgeryByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "4":
                        sub.PostSurgeryDateTime = (new DateTime()).NowAtSqlServer();
                        sub.PostSurgeryByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "5":
                        sub.MoveToTheWardDateTime = (new DateTime()).NowAtSqlServer();
                        sub.MoveToTheWardByUserID = AppSession.UserLogin.UserID;
                        break;
                    case "val":
                        sub.IsValidate = true;
                        sub.ValidateDateTime = (new DateTime()).NowAtSqlServer();
                        sub.ValidateByUserID = AppSession.UserLogin.UserID;
                        break;
                }
                sub.Save();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                UpdateStatus(param[0], param[1]);

                //grdList.DataSource = null;
                grdList.Rebind();
            }
            else if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}