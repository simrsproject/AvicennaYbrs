using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsMeetingItem : BaseUserControl
    {
        public object DataItem { get; set; }

        private RadTextBox TxtTransactionNo
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtTransactionNo");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (EmployeeSafetyCultureIncidentReportsMeetingCollection)Session["collEmployeeSafetyCultureIncidentReportsMeeting" + Request.UserHostName];
                if (coll.Count == 0)
                    hdnSequenceNo.Value = "001";
                else
                {
                    var seqNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int id = Convert.ToInt32(seqNo.Single()) + 1;

                    hdnSequenceNo.Value = string.Format("{0:000}", id);
                }

                grdParticipant.Rebind();

                return;
            }
            ViewState["IsNewRecord"] = false;
            hdnSequenceNo.Value = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.SequenceNo).ToString();
            txtMeetingDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.MeetingDateTime));
            txtMeetingTime.Text = Convert.ToDateTime(DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.MeetingDateTime)).ToString("HH:mm");
            txtMeetingSummary.Text = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.MeetingSummary).ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public string SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public DateTime MeetingDateTime
        {
            get { return DateTime.Parse(txtMeetingDate.SelectedDate.Value.ToShortDateString() + " " + txtMeetingTime.TextWithLiterals); }
        }

        public string MeetingSummary
        {
            get { return txtMeetingSummary.Text; }
        }

        public string Participants
        {
            get
            {
                var str = string.Empty;

                var ds = EmployeeSafetyCultureIncidentReportsMeetingParticipants.Where(p => p.SequenceNo == hdnSequenceNo.Value);

                foreach (var e in ds)
                {
                    str += string.Format("{0} <br />", e.ParticipantName);
                }
                return str;
            }
        }

        protected void cboParticipantPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboParticipantPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery("a");
            var participant = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery("b");
            vw.InnerJoin(participant).On(participant.TransactionNo == TxtTransactionNo.Text && participant.ParticipantPersonID == vw.PersonID);
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));

            if ((o as RadComboBox).ID == cboParticipantPersonID.ID && EmployeeSafetyCultureIncidentReportsMeetingParticipants.Any())
            {
                var ds = EmployeeSafetyCultureIncidentReportsMeetingParticipants.Where(p => p.SequenceNo == hdnSequenceNo.Value);
                if (ds.Any())
                    vw.Where(vw.PersonID.NotIn(ds.Select(p => p.ParticipantPersonID)));
            }

            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        private EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection EmployeeSafetyCultureIncidentReportsMeetingParticipants
        {
            get
            {
                var obj = Session["collEmployeeSafetyCultureIncidentReportsMeetingParticipant" + Request.UserHostName];
                var coll = ((EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection)(obj));
                coll.Where(c => c.TransactionNo == TxtTransactionNo.Text && c.SequenceNo == hdnSequenceNo.Value);
                return coll;
            }
        }

        protected void grdParticipant_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in EmployeeSafetyCultureIncidentReportsMeetingParticipants
                     where d.TransactionNo == TxtTransactionNo.Text && d.SequenceNo == hdnSequenceNo.Value
                     select d;
            grdParticipant.DataSource = ds;
        }

        protected void grdParticipant_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var empId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.ParticipantPersonID]);
            var entity = EmployeeSafetyCultureIncidentReportsMeetingParticipants.FirstOrDefault(rec => rec.TransactionNo == TxtTransactionNo.Text && rec.SequenceNo == hdnSequenceNo.Value && rec.ParticipantPersonID.Equals(empId.ToInt()));
            if (entity != null)
            {
                entity.MarkAsDeleted();
                EmployeeSafetyCultureIncidentReportsMeetingParticipants.Save();
            }
        }

        protected void btnAddParticipant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboParticipantPersonID.SelectedValue)) return;

            var entity = EmployeeSafetyCultureIncidentReportsMeetingParticipants.Where(rec => rec.TransactionNo == TxtTransactionNo.Text && rec.SequenceNo == hdnSequenceNo.Value && rec.ParticipantPersonID.Equals(cboParticipantPersonID.SelectedValue.ToInt())).SingleOrDefault();
            if (entity != null) return;
            entity = EmployeeSafetyCultureIncidentReportsMeetingParticipants.AddNew();
            entity.TransactionNo = TxtTransactionNo.Text;
            entity.SequenceNo = hdnSequenceNo.Value;
            entity.ParticipantPersonID = cboParticipantPersonID.SelectedValue.ToInt();
            entity.ParticipantName = cboParticipantPersonID.Text;

            grdParticipant.Rebind();

            cboParticipantPersonID.Text = string.Empty;
            cboParticipantPersonID.SelectedValue = string.Empty;
            cboParticipantPersonID.DataSource = null;
            cboParticipantPersonID.DataBind();
        }
    }
}