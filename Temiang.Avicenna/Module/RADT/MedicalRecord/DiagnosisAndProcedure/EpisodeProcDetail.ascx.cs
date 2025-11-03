using DevExpress.Web.Internal.XmlProcessor;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Reports.OptionControl;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class EpisodeProcDetail : BaseUserControl
    {
        private RadTextBox TxtRegistrationNo
        {
            get
            { return (RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo"); }
        }

        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRProcedureCategory, AppEnum.StandardReference.ProcedureCategory);
            StandardReference.InitializeIncludeSpace(cboSRAnestesi, AppEnum.StandardReference.Anestesi);

            var qRoom = new ServiceRoomQuery();
            qRoom.Select
                (
                    qRoom.RoomID,
                    qRoom.RoomName
                );
            qRoom.Where
                (
                    qRoom.IsActive == true,
                    qRoom.IsOperatingRoom == true
                );
            DataTable dtbRoom = qRoom.LoadDataTable();

            cboRoomID.Items.Clear();
            cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            foreach (DataRow rowRoom in dtbRoom.Rows)
            {
                cboRoomID.Items.Add(new RadComboBoxItem(rowRoom["RoomName"].ToString(), rowRoom["RoomID"].ToString()));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var collProcedure = (EpisodeProcedureCollection)Session["collEpisodeProcedure" + Request.UserHostName];
                if (!collProcedure.Any())
                    ViewState["SequenceNo"] = "001";
                else
                {
                    int seqNo = 0;
                    foreach (EpisodeProcedure item in collProcedure)
                    {
                        if (int.Parse(item.SequenceNo) > seqNo)
                            seqNo = int.Parse(item.SequenceNo);
                    }
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo + 1);
                }

                txtProcedureDate.SelectedDate = DateTime.Now;
                txtProcedureTime.Text = DateTime.Now.ToString("HH:mm");
                txtProcedureDate2.SelectedDate = DateTime.Now;
                txtProcedureTime2.Text = DateTime.Now.ToString("HH:mm");
                txtIncisionDate.SelectedDate = DateTime.Now;
                txtIncisionTime.Text = DateTime.Now.ToString("HH:mm");

                var reg = new Registration();
                reg.LoadByPrimaryKey(TxtRegistrationNo.Text);
                if (!string.IsNullOrEmpty(reg.ParamedicID))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == reg.ParamedicID);
                    cboParamedicID.DataSource = mq.LoadDataTable();
                    cboParamedicID.DataBind();
                    cboParamedicID.SelectedValue = reg.ParamedicID;
                }

                var bookQ = new ServiceUnitBookingQuery("a");
                var bookColl = new ServiceUnitBookingCollection();
                bookQ.Where(bookQ.RegistrationNo.In(MergeRegistrationList()), bookQ.IsVoid == false,
                    bookQ.Or(bookQ.IsApproved == true, bookQ.RealizationDateTimeFrom.IsNotNull()));
                bookColl.Load(bookQ);
                cboBookingNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var book in bookColl)
                {
                    cboBookingNo.Items.Add(new RadComboBoxItem(book.BookingNo, book.BookingNo));
                }
                EnableAllInput(true);
                
                cboBookingNo.Enabled = true;
                rfvBookingNo.Visible = bookColl.Count > 0;// true;
                cboOpNotesNo.Enabled = true;
                cboOpNotesNo.Text = string.Empty;
                rfvOpNotesNo.Visible = false;

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SequenceNo);

            //
            cboBookingNo.Items.Add(new RadComboBoxItem((String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.BookingNo), (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.BookingNo)));
            cboBookingNo.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.BookingNo);

            var procId = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureID);
            if (!string.IsNullOrEmpty(procId))
            {
                var qProc = new ProcedureQuery();
                qProc.Where(qProc.ProcedureID == procId);
                var tab = qProc.LoadDataTable();
                cboProcedureID.DataSource = tab;
                cboProcedureID.DataBind();

                cboProcedureID.SelectedValue = procId;
                PopulateProcedureName(true);
            }
            else
            {
                txtProcedureText.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureName);
            }
            
            //txtProcedureText.Text = tab.Rows[0]["ProcedureName"].ToString();
            txtProcedureDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureDate);
            txtProcedureDate2.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureDate2);
            txtProcedureTime.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureTime);
            txtProcedureTime2.Text = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ProcedureTime2);

            object incisionDate = DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.IncisionDateTime);
            if (incisionDate != null)
            {
                txtIncisionDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.IncisionDateTime);
                txtIncisionTime.Text = ((DateTime)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.IncisionDateTime)).ToString("HH:mm");
            }
            else
                txtIncisionDate.Clear();

            var surgeon1 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID);
            if (!string.IsNullOrEmpty(surgeon1))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == surgeon1);
                cboParamedicID.DataSource = mq.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = surgeon1;
            }

            var surgeon2= (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID2a);
            if (!string.IsNullOrEmpty(surgeon2))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == surgeon2);
                cboParamedicID2a.DataSource = mq.LoadDataTable();
                cboParamedicID2a.DataBind();
                cboParamedicID2a.SelectedValue = surgeon2;
            }

            var surgeon3 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID3a);
            if (!string.IsNullOrEmpty(surgeon3))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == surgeon3);
                cboParamedicID3a.DataSource = mq.LoadDataTable();
                cboParamedicID3a.DataBind();
                cboParamedicID3a.SelectedValue = surgeon3;
            }

            var surgeon4 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID4a);
            if (!string.IsNullOrEmpty(surgeon4))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == surgeon4);
                cboParamedicID4a.DataSource = mq.LoadDataTable();
                cboParamedicID4a.DataBind();
                cboParamedicID4a.SelectedValue = surgeon4;
            }

            var anesthesiologist = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.ParamedicID2);
            if (!string.IsNullOrEmpty(anesthesiologist))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == anesthesiologist);
                cboParamedicID2.DataSource = mq.LoadDataTable();
                cboParamedicID2.DataBind();
                cboParamedicID2.SelectedValue = anesthesiologist;
            }

            var assAnesthesiologist = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi);
            if (!string.IsNullOrEmpty(assAnesthesiologist))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == assAnesthesiologist);
                cboAssistantIDAnestesi.DataSource = mq.LoadDataTable();
                cboAssistantIDAnestesi.DataBind();
                cboAssistantIDAnestesi.SelectedValue = assAnesthesiologist;
            }

            var assAnesthesiologist2 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.AssistantIDAnestesi2);
            if (!string.IsNullOrEmpty(assAnesthesiologist2))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == assAnesthesiologist2);
                cboAssistantIDAnestesi2.DataSource = mq.LoadDataTable();
                cboAssistantIDAnestesi2.DataBind();
                cboAssistantIDAnestesi2.SelectedValue = assAnesthesiologist2;
            }

            var assistant1 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.AssistantID1);
            if (!string.IsNullOrEmpty(assistant1))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == assistant1);
                cboAssistantID1.DataSource = mq.LoadDataTable();
                cboAssistantID1.DataBind();
                cboAssistantID1.SelectedValue = assistant1;
            }

            var assistant2 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.AssistantID2);
            if (!string.IsNullOrEmpty(assistant2))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == assistant2);
                cboAssistantID2.DataSource = mq.LoadDataTable();
                cboAssistantID2.DataBind();
                cboAssistantID2.SelectedValue = assistant2;
            }

            var instrumentator1 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.InstrumentatorID1);
            if (!string.IsNullOrEmpty(instrumentator1))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == instrumentator1);
                cboInstrumentatorID1.DataSource = mq.LoadDataTable();
                cboInstrumentatorID1.DataBind();
                cboInstrumentatorID1.SelectedValue = instrumentator1;
            }

            var instrumentator2 = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.InstrumentatorID2);
            if (!string.IsNullOrEmpty(instrumentator2))
            {
                var mq = new ParamedicQuery();
                mq.Where(mq.ParamedicID == instrumentator2);
                cboInstrumentatorID2.DataSource = mq.LoadDataTable();
                cboInstrumentatorID2.DataBind();
                cboInstrumentatorID2.SelectedValue = instrumentator2;
            }

            cboSRProcedureCategory.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SRProcedureCategory);
            cboSRAnestesi.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.SRAnestesi);
            cboRoomID.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.RoomID);
            chkIsCito.Checked = (Boolean)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.IsCito);
            chkIsVoid.Checked = (Boolean)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.IsVoid);

            if (!string.IsNullOrEmpty(cboBookingNo.SelectedValue))
            {
                PopulateCboOpNotesNo(cboBookingNo.SelectedValue, cboParamedicID2.Text, false);
                cboOpNotesNo.SelectedValue = (String)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.OpNotesSeqNo);
            }
            else
            {
                cboOpNotesNo.Items.Clear();
                cboOpNotesNo.SelectedValue = string.Empty;
                cboOpNotesNo.Text = string.Empty;
            }
            rfvOpNotesNo.Visible = !string.IsNullOrEmpty(cboOpNotesNo.SelectedValue);

            var bookingNo = (string)DataBinder.Eval(DataItem, EpisodeProcedureMetadata.ColumnNames.BookingNo);

            if (!string.IsNullOrEmpty(bookingNo))
            {
                var booking = new ServiceUnitBooking();
                if (booking.LoadByPrimaryKey(bookingNo))
                {
                    EnableAllInput(false);
                    if (string.IsNullOrEmpty(booking.SRProcedureCategory))
                        cboSRProcedureCategory.Enabled = true;

                    PopulateTxtOperatingNotes(cboOpNotesNo.SelectedValue);
                }
                else
                {
                    EnableAllInput(false);
                }
            }
            else
            {
                EnableAllInput(true);
            }

            cboBookingNo.Enabled = false;
            rfvBookingNo.Visible = false;
            cboOpNotesNo.Enabled = false;
            rfvOpNotesNo.Visible = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboProcedureID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Procedure required.");
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EpisodeProcedureCollection)Session["collEpisodeProcedure" + Request.UserHostName];

                //TODO: Betulkan cara pengecekannya
                string itemID = cboProcedureID.SelectedValue;
                string procedureTime = txtProcedureTime.Text;
                bool isExist = coll.Any(item => item.ProcedureID.Equals(itemID) && item.ProcedureTime.Equals(procedureTime));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Procedure ID: {0} has exist.", itemID);
                }
            }

            DateTime start = DateTime.Parse(txtProcedureDate.SelectedDate.Value.ToShortDateString() + " " + txtProcedureTime.TextWithLiterals);
            DateTime end = DateTime.Parse(txtProcedureDate2.SelectedDate.Value.ToShortDateString() + " " + txtProcedureTime2.TextWithLiterals);
            DateTime incision = DateTime.Parse(txtIncisionDate.SelectedDate.Value.ToShortDateString() + " " + txtIncisionTime.TextWithLiterals);
            if (start >= end)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Procedure date range is invalid.";
            }
            if (start > incision)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Incision date/time can't less then procedure start date/time.";
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public String BookingNo
        {
            get { return cboBookingNo.SelectedValue; }
        }

        public DateTime ProcedureDate
        {
            get { return txtProcedureDate.SelectedDate.Value; }
        }

        public String ProcedureTime
        {
            get { return txtProcedureTime.TextWithLiterals; }
        }

        public DateTime ProcedureDate2
        {
            get { return txtProcedureDate2.SelectedDate.Value; }
        }

        public String ProcedureTime2
        {
            get { return txtProcedureTime2.TextWithLiterals; }
        }

        public String ParamedicID
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public String ParamedicID2a
        {
            get { return cboParamedicID2a.SelectedValue; }
        }

        public String ParamedicName2a
        {
            get { return cboParamedicID2a.Text; }
        }

        public String ParamedicID3a
        {
            get { return cboParamedicID3a.SelectedValue; }
        }

        public String ParamedicName3a
        {
            get { return cboParamedicID3a.Text; }
        }

        public String ParamedicID4a
        {
            get { return cboParamedicID4a.SelectedValue; }
        }

        public String ParamedicName4a
        {
            get { return cboParamedicID4a.Text; }
        }

        public String ParamedicID2
        {
            get { return cboParamedicID2.SelectedValue; }
        }

        public String ParamedicNama2
        {
            get { return cboParamedicID2.Text; }
        }

        public String AssistantIDAnestesi
        {
            get { return cboAssistantIDAnestesi.SelectedValue; }
        }

        public String AssistantNameAnestesi
        {
            get { return cboAssistantIDAnestesi.Text; }
        }

        public String AssistantIDAnestesi2
        {
            get { return cboAssistantIDAnestesi2.SelectedValue; }
        }

        public String AssistantNameAnestesi2
        {
            get { return cboAssistantIDAnestesi2.Text; }
        }

        public String AssistantID1
        {
            get { return cboAssistantID1.SelectedValue; }
        }

        public String AssistantID2
        {
            get { return cboAssistantID2.SelectedValue; }
        }

        public String InstrumentatorID1
        {
            get { return cboInstrumentatorID1.SelectedValue; }
        }

        public String InstrumentatorID2
        {
            get { return cboInstrumentatorID2.SelectedValue; }
        }

        public String ProcedureID
        {
            get { return cboProcedureID.SelectedValue; }
        }

        public String ProcedureName
        {
            get { return txtProcedureText.Text; }
        }

        public String SRProcedureCategory
        {
            get { return cboSRProcedureCategory.SelectedValue; }
        }

        public String SRAnestesi
        {
            get { return cboSRAnestesi.SelectedValue; }
        }

        public String RoomID
        {
            get { return cboRoomID.SelectedValue; }
        }

        public Boolean IsCito
        {
            get { return chkIsCito.Checked; }
        }

        public Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }

        public String OpNotesSeqNo
        {
            get { return cboOpNotesNo.SelectedValue; }
        }

        public DateTime IncisionDateTime
        {
            get { return DateTime.Parse(txtIncisionDate.SelectedDate.Value.ToShortDateString() + " " + txtIncisionTime.TextWithLiterals); }
        }

        #endregion

        protected void cboBookingNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string bookingNo = e.Value;
            if (e.Text == string.Empty)
            {
                EmptyingControl();
                EnableAllInput(true);
                cboOpNotesNo.Items.Clear();
                cboOpNotesNo.Text = string.Empty;
                return;
            }

            var b = new ServiceUnitBooking();
            if (b.LoadByPrimaryKey(bookingNo))
            {
                if (!string.IsNullOrEmpty(b.ParamedicID))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.ParamedicID);
                    cboParamedicID.DataSource = mq.LoadDataTable();
                    cboParamedicID.DataBind();
                    cboParamedicID.SelectedValue = b.ParamedicID;
                }
                if (!string.IsNullOrEmpty(b.ParamedicID2))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.ParamedicID2);
                    cboParamedicID2a.DataSource = mq.LoadDataTable();
                    cboParamedicID2a.DataBind();
                    cboParamedicID2a.SelectedValue = b.ParamedicID2;
                }
                if (!string.IsNullOrEmpty(b.ParamedicID3))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.ParamedicID3);
                    cboParamedicID3a.DataSource = mq.LoadDataTable();
                    cboParamedicID3a.DataBind();
                    cboParamedicID3a.SelectedValue = b.ParamedicID3;
                }
                if (!string.IsNullOrEmpty(b.ParamedicID4))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.ParamedicID4);
                    cboParamedicID4a.DataSource = mq.LoadDataTable();
                    cboParamedicID4a.DataBind();
                    cboParamedicID4a.SelectedValue = b.ParamedicID4;
                }
                if (!string.IsNullOrEmpty(b.ParamedicIDAnestesi))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.ParamedicIDAnestesi);
                    cboParamedicID2.DataSource = mq.LoadDataTable();
                    cboParamedicID2.DataBind();
                    cboParamedicID2.SelectedValue = b.ParamedicIDAnestesi;
                }
                if (!string.IsNullOrEmpty(b.AssistantIDAnestesi))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.AssistantIDAnestesi);
                    cboAssistantIDAnestesi.DataSource = mq.LoadDataTable();
                    cboAssistantIDAnestesi.DataBind();
                    cboAssistantIDAnestesi.SelectedValue = b.AssistantIDAnestesi;
                }
                if (!string.IsNullOrEmpty(b.AssistantID1))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.AssistantID1);
                    cboAssistantID1.DataSource = mq.LoadDataTable();
                    cboAssistantID1.DataBind();
                    cboAssistantID1.SelectedValue = b.AssistantID1;
                }
                if (!string.IsNullOrEmpty(b.AssistantID2))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.AssistantID2);
                    cboAssistantID2.DataSource = mq.LoadDataTable();
                    cboAssistantID2.DataBind();
                    cboAssistantID2.SelectedValue = b.AssistantID2;
                }
                if (!string.IsNullOrEmpty(b.Instrumentator1))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.Instrumentator1);
                    cboInstrumentatorID1.DataSource = mq.LoadDataTable();
                    cboInstrumentatorID1.DataBind();
                    cboInstrumentatorID1.SelectedValue = b.Instrumentator1;
                }
                if (!string.IsNullOrEmpty(b.Instrumentator2))
                {
                    var mq = new ParamedicQuery();
                    mq.Where(mq.ParamedicID == b.Instrumentator2);
                    cboInstrumentatorID2.DataSource = mq.LoadDataTable();
                    cboInstrumentatorID2.DataBind();
                    cboInstrumentatorID2.SelectedValue = b.Instrumentator2;
                }
                
                if (b.RealizationDateTimeFrom.HasValue)
                {
                    txtProcedureDate.SelectedDate = b.RealizationDateTimeFrom.Value;
                    txtProcedureTime.Text = b.RealizationDateTimeFrom.Value.ToShortTimeString();
                }
                else
                {
                    txtProcedureDate.SelectedDate = null;
                    txtProcedureTime.Text = string.Empty;
                }
                if (b.RealizationDateTimeTo.HasValue)
                {
                    txtProcedureDate2.SelectedDate = b.RealizationDateTimeTo.Value;
                    txtProcedureTime2.Text = b.RealizationDateTimeTo.Value.ToShortTimeString();
                }
                else
                {
                    txtProcedureDate2.SelectedDate = null;
                    txtProcedureTime2.Text = string.Empty;
                }

                cboSRAnestesi.SelectedValue = b.SRAnestesiPlan;
                cboSRProcedureCategory.SelectedValue = b.SRProcedureCategory;
                chkIsCito.Checked = b.IsCito.HasValue ? b.IsCito.Value : false;
                cboRoomID.SelectedValue = b.RoomID;

                EnableAllInput(false);
                if (string.IsNullOrEmpty(b.SRProcedureCategory))
                    cboSRProcedureCategory.Enabled = true;

                var anesthetist = string.Empty;
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(b.ParamedicIDAnestesi))
                    anesthetist = par.ParamedicName;

                PopulateCboOpNotesNo(bookingNo, anesthetist, true);
            }
            else
            {
                EmptyingControl();
                EnableAllInput(true);
                cboOpNotesNo.Items.Clear();
                cboOpNotesNo.Text = string.Empty;
            }
        }

        protected void cboProcedureID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProcedureID"].ToString();
        }

        protected void cboProcedureID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ProcedureQuery query = new ProcedureQuery();
           
            if (AppSession.Parameter.HealthcareInitial == "RSMP")
                query.Where
                    (
                        query.Or
                            (
                                 query.ProcedureName.Like(searchTextContain),
                                 query.ProcedureID.Equal(e.Text)
                            )
                    );
            else
                query.Where
                    (
                        query.Or
                            (
                                 query.ProcedureName.Like(searchTextContain),
                                 query.ProcedureID.Like(searchTextContain)
                            )
                    );
            query.OrderBy(query.ProcedureName.Ascending);
            query.es.Top = 50;

            cboProcedureID.DataSource = query.LoadDataTable();
            cboProcedureID.DataBind();
        }

        protected void cboProcedureID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(e.Value))
            //{
            //    txtProcedureText.Text = string.Empty;
            //    return;
            //}
            //var proc = new Procedure();
            //proc.LoadByPrimaryKey(e.Value);
            //txtProcedureText.Text = proc.ProcedureName;
            PopulateProcedureName(true);
        }

        private void PopulateProcedureName(bool isResetIdIfNotExist)
        {
            if (cboProcedureID.SelectedValue == string.Empty)
            {
                txtProcedureText.Text = string.Empty;
                return;
            }

            var proc = new Procedure();
            if (proc.LoadByPrimaryKey(cboProcedureID.SelectedValue))
                txtProcedureText.Text = proc.ProcedureName;
            else
            {
                txtProcedureText.Text = string.Empty;
                if (isResetIdIfNotExist)
                    cboProcedureID.SelectedValue = string.Empty;
            }
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

            string searchTextContain = string.Format("%{0}%", e.Text);

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors), ptype.SRParamedicType.In(AppSession.Parameter.ParamedicTypeDoctors))
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboAssistantID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

            string searchTextContain = string.Format("%{0}%", e.Text);

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboPhysicianIDAnestesi_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboParamedicID2.Items.Clear();

            string searchTextContain = string.Format("%{0}%", e.Text);

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAnesthetic, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAnesthetic)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cboParamedicID2.DataSource = medic.LoadDataTable();
            cboParamedicID2.DataBind();
        }

        protected void cboAssistantIDAnestesi_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;
            cbo.Items.Clear();

            string searchTextContain = string.Format("%{0}%", e.Text);

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant,
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeAssAnesthesia,
                ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssistant,
                ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeAssAnesthesia)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboInstrumentatorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var cbo = (RadComboBox)o;

            cbo.Items.Clear();

            string searchTextContain = string.Format("%{0}%", e.Text);

            var medic = new ParamedicQuery("a");
            var ptype = new ParamedicOtherTypeQuery("b");
            medic.LeftJoin(ptype).On(ptype.ParamedicID == medic.ParamedicID);

            medic.es.Top = 15;
            medic.es.Distinct = true;
            medic.Where(
                medic.ParamedicName.Like(searchTextContain),
                medic.IsActive == true,
                medic.Or(
                medic.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator, ptype.SRParamedicType == AppSession.Parameter.PhysicianTypeInstrumentator)
                );
            medic.Select(medic.ParamedicID, medic.ParamedicName);

            cbo.DataSource = medic.LoadDataTable();
            cbo.DataBind();
        }

        private void EmptyingControl()
        {
            cboParamedicID.SelectedValue = string.Empty;
            cboParamedicID2a.SelectedValue = string.Empty;
            cboParamedicID3a.SelectedValue = string.Empty;
            cboParamedicID4a.SelectedValue = string.Empty;
            cboParamedicID2.SelectedValue = string.Empty;
            cboAssistantIDAnestesi.SelectedValue = string.Empty;
            cboAssistantIDAnestesi2.SelectedValue = string.Empty;
            cboAssistantID1.SelectedValue = string.Empty;
            cboAssistantID2.SelectedValue = string.Empty;
            cboInstrumentatorID1.SelectedValue = string.Empty;
            cboInstrumentatorID2.SelectedValue = string.Empty;

            cboSRAnestesi.SelectedValue = string.Empty;
            cboSRProcedureCategory.SelectedValue = string.Empty;
            chkIsCito.Checked = false;
            cboRoomID.SelectedValue = string.Empty;
        }

        private void EnableAllInput(bool b)
        {
            cboParamedicID.Enabled = b;
            cboParamedicID2a.Enabled = b;
            cboParamedicID3a.Enabled = b;
            cboParamedicID4a.Enabled = b;
            cboParamedicID2.Enabled = b;
            cboAssistantIDAnestesi.Enabled = b;
            cboAssistantIDAnestesi2.Enabled = b;
            cboAssistantID1.Enabled = b;
            cboAssistantID2.Enabled = b;
            cboInstrumentatorID1.Enabled = b;
            cboInstrumentatorID2.Enabled = b;

            cboSRAnestesi.Enabled = b;
            cboSRProcedureCategory.Enabled = b;
            chkIsCito.Enabled = b;
            cboRoomID.Enabled = b;
        }

        private void PopulateCboOpNotesNo(string bookingNo, string anesthetist, bool isNew)
        {
            var bookNotesQ = new ServiceUnitBookingOperatingNotesQuery("a");
            var bookNotesColl = new ServiceUnitBookingOperatingNotesCollection();
            bookNotesQ.Where(bookNotesQ.BookingNo == bookingNo);
            if (isNew)
                bookNotesQ.Where(bookNotesQ.IsVoid == false);
            bookNotesQ.OrderBy(bookNotesQ.ParamedicID.Ascending, bookNotesQ.OpNotesSeqNo.Ascending);
            
            bookNotesColl.Load(bookNotesQ);

            cboOpNotesNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (!string.IsNullOrEmpty(anesthetist))
                cboOpNotesNo.Items.Add(new RadComboBoxItem("000 - " + anesthetist + " [Anesthetist]", "000"));

            foreach (var bn in bookNotesColl)
            {
                var par = new Paramedic();
                par.LoadByPrimaryKey(bn.ParamedicID);
                
                var regio = string.IsNullOrEmpty(bn.Regio) ? "-" : bn.Regio;

                cboOpNotesNo.Items.Add(new RadComboBoxItem(bn.OpNotesSeqNo + " - " + par.ParamedicName + " [Regio: " + regio + "]", bn.OpNotesSeqNo));
            }
        }

        private void PopulateTxtOperatingNotes(string notesNp)
        {
            if (notesNp == string.Empty)
                return;

            if (notesNp == "000")
            {
                var sub = new ServiceUnitBooking();
                if (sub.LoadByPrimaryKey(cboBookingNo.SelectedValue))
                    txtOperatinNotes.Text = sub.AnestesyNotes;
                lblRegion.Text = "Anesthetist Notes";

                return;
            }

            var notes = new ServiceUnitBookingOperatingNotes();
            if (notes.LoadByPrimaryKey(cboBookingNo.SelectedValue, notesNp))
            {
                var regio = string.IsNullOrEmpty(notes.Regio) ? "-" : notes.Regio;
                lblRegion.Text = "Operating Notes [Regio: " + regio + "]";
                txtOperatinNotes.Text = notes.OperatingNotes;
            }
            else
                txtOperatinNotes.Text = string.Empty;
        }

        protected void cboOpNotesNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string notesNp = e.Value;
            if (notesNp == string.Empty)
            {
                txtOperatinNotes.Text = string.Empty;
                return;
            }

            PopulateTxtOperatingNotes(notesNp);
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["EpisodeProcedure:MergeRegistration" + Request.UserHostName] == null)
                ViewState["EpisodeProcedure:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetFullMergeRegistration(TxtRegistrationNo.Text);

            return (string[])ViewState["EpisodeProcedure:MergeRegistration" + Request.UserHostName];
        }

        protected void txtProcedureDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtIncisionDate.SelectedDate = txtProcedureDate.SelectedDate;
        }

        protected void txtProcedureTime_TextChanged(object sender, EventArgs e)
        {
            txtIncisionTime.Text = txtProcedureTime.Text;
        }
    }
}