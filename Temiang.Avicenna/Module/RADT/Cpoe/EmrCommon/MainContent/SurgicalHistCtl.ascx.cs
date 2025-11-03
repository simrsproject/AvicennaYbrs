using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public partial class SurgicalHistCtl : BaseMainContentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GrdEpisodeProcedureClientID
        {
            get { return grdEpisodeProcedure.ClientID; }
        }

        internal String ReferFromRegistrationNo
        {
            set { ViewState["freg"] = value; }
            get { return Convert.ToString(ViewState["freg"]); }
        }
        internal RadGrid GrdEpisodeProcedure
        {
            get { return grdEpisodeProcedure; }
        }
        #region Record Detail Method Function Episode Procedure

        
        protected bool IsUserEntrySurgical(string x)
        {
            if (x == "add")
            {
                //if (RegistrationType != AppConstant.RegistrationType.OutPatient)
                //    return false;

                //var bookings = new ServiceUnitBookingCollection();
                //bookings.Query.Where(bookings.Query.RegistrationNo == RegistrationNo, bookings.Query.IsVoid == false);
                //bookings.LoadAll();
                //if (bookings.Count > 0)
                //    return false;

                //db:20240521 - direct surgery tidak hanya u/ outpatient, tp bisa jg di inpatient (icu: cito, pasien tidak memungkinkan u/ dipindahkan ke ruang bedah sentral) & igd
                // u/ outpatient hanya bisa 1 x per noreg, tp kalo inpatient bisa > 1
                if (RegistrationType != AppConstant.RegistrationType.InPatient)
                {
                    var bookings = new ServiceUnitBookingCollection();
                    bookings.Query.Where(bookings.Query.RegistrationNo == RegistrationNo, bookings.Query.IsVoid == false);
                    bookings.LoadAll();
                    if (bookings.Count > 0)
                        return false;
                }

                var isValidRoom = false;
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(RegistrationNo))
                {
                    var room = new ServiceRoom();
                    /*if (room.LoadByPrimaryKey(reg.RoomID) && room.IsOperatingRoom == true ) */
                    //db:20240521 - tambah filter room.IsShowOnBookingOT == false (direct hanya u/ room2 yg bukan termasuk unit bedah sentral)
                    if (room.LoadByPrimaryKey(reg.RoomID) && room.IsOperatingRoom == true && room.IsShowOnBookingOT == false)
                        isValidRoom = true;
                }

                return (isValidRoom &&
                    AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor &&
                    !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID) &&
                    AppSession.UserLogin.ParamedicID == ParamedicID);
            }
            
            // Team dokter operasi
            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor && !string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID) && Eval("ParamedicSurgical").ToString().Contains(AppSession.UserLogin.ParamedicID));
        }

        private DataTable ServiceUnitBookings()
        {
            //// 1. Query ServiceUnitBooking untuk keperluan pengisian form2 Pra Operasi 
            var query = new ServiceUnitBookingQuery("a");
            var param = new ParamedicQuery("b");
            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

            var reg = new RegistrationQuery("r");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var su = new ServiceUnitQuery("s");
            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);

            var rm = new ServiceRoomQuery("rm");
            query.InnerJoin(rm).On(query.RoomID == rm.RoomID);

            query.Select
                 (
                     query.BookingNo, query.OperatingNotes, query.IsApproved,
                     @"<a.ParamedicID + '_' + ISNULL(a.ParamedicID2, '') + '_' + ISNULL(a.ParamedicID3, '') + '_' + ISNULL(a.ParamedicID4, '') + '_' + ISNULL(a.ParamedicIDAnestesi, '') AS 'ParamedicSurgical'>",
                     //(query.ParamedicID + "_" + query.ParamedicID2 + "_" + query.ParamedicID3 + "_" + query.ParamedicID4 + "_" + query.ParamedicIDAnestesi).As("ParamedicSurgical"),
                     query.RegistrationNo, "<'' as SequenceNo>", query.RealizationDateTimeFrom.As("ProcedureDate"), "<LEFT(CONVERT(VARCHAR, a.RealizationDateTimeFrom, 8), 5) as ProcedureTime>",
                     param.ParamedicName, "<'' as ProcedureName>", query.AnestesyNotes, query.AnestPostSurgeryInstructions, query.Diagnose, query.PostDiagnosis, su.ServiceUnitName, rm.RoomName,
                     reg.PatientID, query.ParamedicIDAnestesi, @"<CAST(1 AS BIT) 'IsNew'>"
                 );

            query.Where(reg.PatientID == PatientID, query.Or(reg.IsVoid.IsNull(), reg.IsVoid == false), query.Or(query.IsVoid.IsNull(), query.IsVoid == false));
            //query.Where(reg.RegistrationNo.In(RelatedRegistrations)); //TODO: Mungkinkah dimunculkan semua tidak per episode
            query.Where(reg.PatientID.In(PatientRelateds));
            query.OrderBy(query.BookingNo.Descending);

            DataTable dtb = query.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                var isAnesthetist = (row["ParamedicIDAnestesi"].ToString() == AppSession.UserLogin.ParamedicID);
                if (!isAnesthetist)
                {
                    var notes = new ServiceUnitBookingOperatingNotesCollection();
                    notes.Query.Where(
                        notes.Query.BookingNo == row["BookingNo"].ToString(), 
                        notes.Query.CreatedByUserID == AppSession.UserLogin.UserID, 
                        notes.Query.IsVoid == false
                        );
                    notes.LoadAll();
                    row["IsNew"] = notes.Count == 0;
                }
                else
                {
                    //row["IsNew"] = string.IsNullOrEmpty(row["AnestesyNotes"].ToString());
                }
            }
            dtb.AcceptChanges();

            return dtb;
        }


        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeProcedure.DataSource = ServiceUnitBookings();
        }
        protected void grdEpisodeProcedure_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                grdEpisodeProcedure.DataSource = null;
                grdEpisodeProcedure.Rebind();
            }
        }
        protected void grdEpisodeProcedure_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var dataKeyValues = item.OwnerTableView.DataKeyValues[item.ItemIndex];
            var bookingNo = Convert.ToString(dataKeyValues[ServiceUnitBookingOperatingNotesMetadata.ColumnNames.BookingNo]);
            var seqNo = Convert.ToString(dataKeyValues[ServiceUnitBookingOperatingNotesMetadata.ColumnNames.OpNotesSeqNo]);

            var entity = new ServiceUnitBookingOperatingNotes();
            if (entity.LoadByPrimaryKey(bookingNo, seqNo) && entity.ParamedicID == ParamedicID)
            {
                entity.IsVoid = true;
                entity.Save();

                var epColl = new EpisodeProcedureCollection();
                epColl.Query.Where(epColl.Query.BookingNo == bookingNo, epColl.Query.OpNotesSeqNo == seqNo);
                epColl.LoadAll();
                foreach (var ep in epColl)
                {
                    ep.IsVoid = true;
                }
                epColl.Save();
            }

            grdEpisodeProcedure.DataSource = null;
            grdEpisodeProcedure.Rebind();
        }

        private string _bookingNo = string.Empty;
        protected void grdEpisodeProcedure_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
                _bookingNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["BookingNo"]);

            if (e.Item is GridNestedViewItem)
            {
                // Show Visible tab document
                var tabsEpisodeProcedure = (RadTabStrip)e.Item.FindControl("tabsEpisodeProcedure");
                tabsEpisodeProcedure.Tabs[2].Visible = !string.IsNullOrEmpty(_bookingNo);

                // Populate
                var grd2 = (RadGrid)e.Item.FindControl("grdEpisodeProcedureDetail");
                grd2.DataSource = EpisodeProcedureDetails(_bookingNo);
                grd2.Rebind();

                var grd3 = (RadGrid)e.Item.FindControl("grdEpisodeProcedureDetailAns");
                grd3.DataSource = EpisodeProcedureAnsDetails(_bookingNo);
                grd3.Rebind();

                if (tabsEpisodeProcedure.Tabs[2].Visible)
                {
                    var grd = (RadGrid)e.Item.FindControl("grdForm");
                    grd.InitializeCultureGrid();
                    grd.DataSource = ServiceUnitBookingForms(_bookingNo);
                    grd.Rebind();
                }

                _bookingNo = string.Empty;
            }
        }

        private DataTable EpisodeProcedureDetails(string bookingNo)
        {
            var query = new ServiceUnitBookingOperatingNotesQuery("a");
            var sub = new ServiceUnitBookingQuery("b");
            var par = new ParamedicQuery("c");
            query.InnerJoin(sub).On(sub.BookingNo == query.BookingNo);
            query.InnerJoin(par).On(par.ParamedicID == query.ParamedicID);
            query.Select(sub.RegistrationNo, query.BookingNo, query.OpNotesSeqNo.As("SequenceNo"), query.ParamedicID,
                par.ParamedicName, query.Regio, "<'' AS ProcedureName>",
                query.OperatingNotes, query.PostSurgeryInstructions, "<CAST(0 AS BIT) as IsEditable>", query.CreatedByUserID, query.OpNotesSeqNo);
            query.Where(query.BookingNo == bookingNo, query.IsVoid == false);
            query.OrderBy(par.ParamedicName.Ascending, query.OpNotesSeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                //if (row["ParamedicID"].ToString() == ParamedicID) //ParamedicID dari List, jadinya non dokter akan bisa edit
                if (row["CreatedByUserID"].ToString() == AppSession.UserLogin.UserID)
                    row["IsEditable"] = true;

                var epcoll = new EpisodeProcedureCollection();
                epcoll.Query.Where(epcoll.Query.BookingNo == _bookingNo,
                    epcoll.Query.OpNotesSeqNo == row["SequenceNo"].ToString(), epcoll.Query.IsVoid == false);
                epcoll.LoadAll();
                var procedureName = string.Empty;
                foreach (var ep in epcoll)
                {
                    if (procedureName == string.Empty)
                        procedureName = "[" + ep.ProcedureID + "] " + ep.ProcedureName + ", Synonym: " + ep.ProcedureSynonym;
                    else
                        procedureName = procedureName + "; [" + ep.ProcedureID + "] " + ep.ProcedureName + ", Synonym: " + ep.ProcedureSynonym;
                }
                row["ProcedureName"] = procedureName;
            }
            dtb.AcceptChanges();

            return dtb;
        }

        private DataTable EpisodeProcedureAnsDetails(string bookingNo)
        {
            var query = new EpisodeProcedureQuery("a");
            var booking = new ServiceUnitBookingQuery("b");
            var usr = new AppUserQuery("c");
            query.InnerJoin(booking).On(booking.BookingNo == query.BookingNo);
            query.InnerJoin(usr).On(usr.UserID == query.CreateByUserID);
            query.Where(query.BookingNo == bookingNo, query.OpNotesSeqNo == "000", usr.ParamedicID == booking.ParamedicIDAnestesi, query.IsVoid == false);

            query.Select(query);
            return query.LoadDataTable();
        }

        private DataTable ServiceUnitBookingForms(string bookingNo)
        {
            // Bermasalah jika di PHR ada yg dihapus dan tidak support multi form
            //var form = new QuestionFormQuery("a");
            //var bform = new ServiceUnitBookingFormQuery("b");
            //form.LeftJoin(bform).On(form.QuestionFormID == bform.QuestionFormID && bform.BookingNo == bookingNo);

            //form.Where(form.IsActive == true, form.SRQuestionFormType == QuestionForm.QuestionFormType.ServiceUnitBooking);
            //form.Select("<'" + bookingNo + "' as BookingNo>", form.QuestionFormID, form.RmNO, form.QuestionFormName, bform.TransactionNo, bform.CreatedDateTime,
            //    form.RestrictionUserType );

            //var dtb = form.LoadDataTable();
            //dtb.Columns.Add("ServiceUnitID", typeof(string));
            //dtb.Columns.Add("registrationNo", typeof(string));
            //dtb.Columns.Add("FormType", typeof(string));
            //dtb.Columns.Add("CreateByUserName", typeof(string));
            //dtb.Columns.Add("IsApproved", typeof(bool));
            //dtb.Columns.Add("ApprovedDatetime", typeof(DateTime));
            //dtb.Columns.Add("ApprovedByUserName", typeof(string));

            //foreach (DataRow row in dtb.Rows)
            //{
            //    var phr = new PatientHealthRecord();
            //    phr.Query.es.Top = 1;
            //    phr.Query.Where(phr.Query.TransactionNo == row["TransactionNo"].ToString());

            //    if (phr.Query.Load())
            //    {

            //        row["ServiceUnitID"] = phr.ServiceUnitID;
            //        row["RegistrationNo"] = phr.RegistrationNo;

            //        if (phr.IsApproved??false)
            //            row["IsApproved"] = phr.IsApproved;

            //        if (phr.ApprovedDatetime!=null)
            //            row["ApprovedDatetime"] = phr.ApprovedDatetime;

            //        if (phr.ApprovedByUserID!=null)
            //            row["ApprovedByUserName"] = AppUser.GetUserName(phr.ApprovedByUserID);

            //        row["CreateByUserName"] = AppUser.GetUserName(phr.CreateByUserID);
            //    }
            //    row["FormType"] = "phr";

            //}

            //// Add Non PHR Form
            //var newRow = dtb.NewRow();
            //newRow["FormType"] = "medhist";
            //newRow["BookingNo"] = bookingNo;
            //newRow["QuestionFormName"] = "MONITORING ANESTESI INTRA OPERATIF OBAT-OBATAN";
            //dtb.Rows.Add(newRow);

            //var newRow2 = dtb.NewRow();
            //newRow2["FormType"] = "vitalsign";
            //newRow2["BookingNo"] = bookingNo;

            //newRow2["QuestionFormName"] = "MONITORING ANESTESI INTRA OPERATIF PEMERIKSAAN FISIK";
            //dtb.Rows.Add(newRow2);


            //return dtb;


            var form = new QuestionFormQuery("a");
            var phr = new PatientHealthRecordQuery("b");
            form.LeftJoin(phr).On(form.QuestionFormID == phr.QuestionFormID & phr.ReferenceNo == bookingNo);


            form.Where(form.IsActive == true, form.SRQuestionFormType == QuestionForm.QuestionFormType.ServiceUnitBooking);
            form.Select("<'" + bookingNo + "' as BookingNo>", form.QuestionFormID, form.RmNO, form.QuestionFormName,
                phr.TransactionNo, phr.CreateDateTime, phr.CreateByUserID,
                phr.ServiceUnitID, phr.RegistrationNo, phr.IsApproved, phr.ApprovedDatetime, phr.ApprovedByUserID,
                form.RestrictionUserType, form.IsSingleEntry);

            var dtb = form.LoadDataTable();
            dtb.Columns.Add("FormType", typeof(string));
            dtb.Columns.Add("CreateByUserName", typeof(string));
            dtb.Columns.Add("ApprovedByUserName", typeof(string));

            foreach (DataRow row in dtb.Rows)
            {
                if (row["ApprovedByUserID"] != null)
                    row["ApprovedByUserName"] = AppUser.GetUserName(row["ApprovedByUserID"].ToString());

                if (row["CreateByUserID"] != null)
                    row["CreateByUserName"] = AppUser.GetUserName(row["CreateByUserID"].ToString());
                row["FormType"] = "phr";
            }

            // Add Non PHR Form
            var newRow = dtb.NewRow();
            newRow["FormType"] = "medhist";
            newRow["BookingNo"] = bookingNo;
            newRow["QuestionFormName"] = "MONITORING ANESTESI INTRA OPERATIF OBAT-OBATAN";
            newRow["RestrictionUserType"] = string.Empty;
            dtb.Rows.Add(newRow);

            var newRow2 = dtb.NewRow();
            newRow2["FormType"] = "vitalsign";
            newRow2["BookingNo"] = bookingNo;
            newRow2["QuestionFormName"] = "MONITORING ANESTESI INTRA OPERATIF PEMERIKSAAN FISIK";
            newRow["RestrictionUserType"] = string.Empty;
            dtb.Rows.Add(newRow2);
            return dtb;
        }

        protected string AddBookingFormLink(GridItem container)
        {
            if (!"phr".Equals(Eval("FormType"))) return string.Empty;
            if (Eval("IsSingleEntry") != DBNull.Value && true.Equals(Eval("IsSingleEntry"))) return string.Empty;
            if (Eval("TransactionNo") == DBNull.Value) return string.Empty;
            if (!Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType)) return string.Empty;

            var newLink = string.Format(
                "<a href=\"#\" onclick=\"entryPhrFromBookingNo('new', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\"><img src=\"{5}/Images/Toolbar/insert16.png\" border=\"0\" /></a>&nbsp;&nbsp;",
                string.Empty, RegistrationNo, Eval("QuestionFormID"), ServiceUnitID, Eval("BookingNo"), Helper.UrlRoot(), container.OwnerGridID);

            return newLink;
        }

        protected string BookingFormLink(GridItem container)
        {
            var retval = string.Empty;
            if ("phr".Equals(Eval("FormType")))
            {
                var newDisableLink = string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\" />", Helper.UrlRoot());

                var viewLink = BookingFormViewLink(container, "icon");

                if (Eval("RestrictionUserType") == null || string.IsNullOrWhiteSpace(Eval("RestrictionUserType").ToString()) || Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType))
                {
                    var newLink = string.Format(
                        "<a href=\"#\" onclick=\"entryPhrFromBookingNo('new', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\"><img src=\"{5}/Images/Toolbar/new16.png\" border=\"0\" /></a>",
                        string.Empty, RegistrationNo, Eval("QuestionFormID"), ServiceUnitID, Eval("BookingNo"), Helper.UrlRoot(), container.OwnerGridID);

                    if (Eval("TransactionNo") == DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString()))
                        retval = this.IsUserAddAble ? newLink : newDisableLink;
                    else
                        retval = viewLink;
                }
                else
                {
                    if (Eval("TransactionNo") == DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString()))
                        retval = newDisableLink;
                    else
                        retval = viewLink;
                }
            }
            else if ("medhist".Equals(Eval("FormType")))
            {
                retval = string.Format(
                    "<a href=\"#\" onclick=\"openMedicationAnesthesiaHist('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/views16.png\" border=\"0\" /></a>", Helper.UrlRoot(), Eval("BookingNo"));
            }
            else if ("vitalsign".Equals(Eval("FormType")))
            {
                retval = string.Format(
                    "<a href=\"#\" onclick=\"openVitalSignChartAnesthesia('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/views16.png\" border=\"0\" /></a>", Helper.UrlRoot(), Eval("BookingNo"));
            }

            return retval;
        }

        protected string BookingFormViewLink(GridItem container, string type)
        {
            string caption;
            if (type == "icon")
                caption = string.Format("<img src=\"{0}/Images/Toolbar/views16.png\" border=\"0\" />",
                    Helper.UrlRoot());
            else
                caption = Eval("TransactionNo").ToString();

            var viewLink = string.Format(
                "<a href=\"#\" onclick=\"entryPhrFromBookingNo('view', '{0}','{1}','{2}','{3}','{4}','{6}'); return false;\">{5}</a>",
                Eval("TransactionNo"), Eval("registrationNo"), Eval("QuestionFormID"),
                Eval("serviceUnitID"),
                Eval("BookingNo"), caption, container.OwnerGridID);
            return viewLink;
        }

        #endregion

    }
}