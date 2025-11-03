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
    public partial class SafetyCultureIncidentReportsChronologyItem : BaseUserControl
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

                var coll = (EmployeeSafetyCultureIncidentReportsChronologyCollection)Session["collEmployeeSafetyCultureIncidentReportsChronology" + Request.UserHostName];
                if (coll.Count == 0)
                    hdnSequenceNo.Value = "001";
                else
                {
                    var seqNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int id = Convert.ToInt32(seqNo.Single()) + 1;

                    hdnSequenceNo.Value = string.Format("{0:000}", id);
                }

                grdSubject.Rebind();

                return;
            }
            ViewState["IsNewRecord"] = false;
            hdnSequenceNo.Value = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.SequenceNo).ToString();
            txtChronologyDate.SelectedDate = Convert.ToDateTime(DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.ChronologyDateTime));
            txtChronologyTime.Text = Convert.ToDateTime(DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.ChronologyDateTime)).ToString("HH:mm");
            txtChronologyDescription.Text = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.ChronologyDescription).ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        public string SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public DateTime ChronologyDateTime
        {
            get {return DateTime.Parse(txtChronologyDate.SelectedDate.Value.ToShortDateString() + " " + txtChronologyTime.TextWithLiterals); }
        }

        public string ChronologyDescription
        {
            get { return txtChronologyDescription.Text; }
        }

        public string Subjects
        {
            get
            {
                var str = string.Empty;

                var ds = EmployeeSafetyCultureIncidentReportsChronologySubjects.Where(p => p.SequenceNo == hdnSequenceNo.Value);

                foreach (var e in ds)
                {
                    str += string.Format("{0} <br />", e.SubjectName);
                }
                return str;
            }
        }

        protected void cboSubjectPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSubjectPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery("a");
            var subject = new EmployeeSafetyCultureIncidentReportsSubjectQuery("b");
            vw.InnerJoin(subject).On(subject.TransactionNo == TxtTransactionNo.Text && subject.SubjectPersonID == vw.PersonID);
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));
           
            if ((o as RadComboBox).ID == cboSubjectPersonID.ID && EmployeeSafetyCultureIncidentReportsChronologySubjects.Any())
            {
                var ds = EmployeeSafetyCultureIncidentReportsChronologySubjects.Where(p => p.SequenceNo == hdnSequenceNo.Value);
                if (ds.Any())
                    vw.Where(vw.PersonID.NotIn(ds.Select(p => p.SubjectPersonID)));
            }

            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        private EmployeeSafetyCultureIncidentReportsChronologySubjectCollection EmployeeSafetyCultureIncidentReportsChronologySubjects
        {
            get
            {
                var obj = Session["collEmployeeSafetyCultureIncidentReportsChronologySubject" + Request.UserHostName];
                var coll = ((EmployeeSafetyCultureIncidentReportsChronologySubjectCollection)(obj));
                coll.Where(c => c.TransactionNo == TxtTransactionNo.Text && c.SequenceNo == hdnSequenceNo.Value);
                return coll;
            }
        }

        protected void grdSubject_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in EmployeeSafetyCultureIncidentReportsChronologySubjects
                     where d.TransactionNo == TxtTransactionNo.Text && d.SequenceNo == hdnSequenceNo.Value
                     select d;
            grdSubject.DataSource = ds;
        }

        protected void grdSubject_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var empId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsChronologySubjectMetadata.ColumnNames.SubjectPersonID]);
            var entity = EmployeeSafetyCultureIncidentReportsChronologySubjects.FirstOrDefault(rec => rec.TransactionNo == TxtTransactionNo.Text && rec.SequenceNo == hdnSequenceNo.Value && rec.SubjectPersonID.Equals(empId.ToInt()));
            if (entity != null)
            {
                entity.MarkAsDeleted();
                EmployeeSafetyCultureIncidentReportsChronologySubjects.Save();
            }
        }

        protected void btnAddSubject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboSubjectPersonID.SelectedValue)) return;

            var entity = EmployeeSafetyCultureIncidentReportsChronologySubjects.Where(rec => rec.TransactionNo == TxtTransactionNo.Text && rec.SequenceNo == hdnSequenceNo.Value && rec.SubjectPersonID.Equals(cboSubjectPersonID.SelectedValue.ToInt())).SingleOrDefault();
            if (entity != null) return;
            entity = EmployeeSafetyCultureIncidentReportsChronologySubjects.AddNew();
            entity.TransactionNo = TxtTransactionNo.Text;
            entity.SequenceNo = hdnSequenceNo.Value;
            entity.SubjectPersonID = cboSubjectPersonID.SelectedValue.ToInt();
            entity.SubjectName = cboSubjectPersonID.Text;
            
            grdSubject.Rebind();

            cboSubjectPersonID.Text = string.Empty;
            cboSubjectPersonID.SelectedValue = string.Empty;
            cboSubjectPersonID.DataSource = null;
            cboSubjectPersonID.DataBind();
        }
    }
}