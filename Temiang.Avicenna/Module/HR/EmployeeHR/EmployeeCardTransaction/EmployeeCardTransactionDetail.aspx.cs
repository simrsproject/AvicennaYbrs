using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeeCardTransactionDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EmployeeCardTransactionSearch.aspx";
            UrlPageList = "EmployeeCardTransactionList.aspx";

            ProgramID = AppConstant.Program.EmployeeCardTransaction;
        }

        protected override void OnMenuNewClick()
        {
            ViewState["id"] = 0;

            txtDate.SelectedDate = DateTime.Now.Date;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeCardTransaction();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeCardTransaction();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeCardTransactionID='{0}'", ViewState["id"].ToString());
            auditLogFilter.TableName = "EmployeeCardTransaction";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeCardTransaction();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString()));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wh = (EmployeeCardTransaction)entity;
            if (wh == null)
            {
                ViewState["id"] = 0;
                return;
            }
            ViewState["id"] = wh.EmployeeCardTransactionID;

            var personId = wh.PersonID;
            var pq = new VwEmployeeTableQuery();
            pq.Where(pq.PersonID == personId);
            cboPersonID.DataSource = pq.LoadDataTable();
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = wh.PersonID.ToString();

            txtDate.SelectedDate= wh.Datetime;
            txtOldCardID.Text= wh.OldCardID;
            txtNewCardID.Text= wh.NewCardID;
            txtNotes.Text = wh.Notes;

            if (wh == null || wh.PersonID == null)
                grdList.DataSource = GetHistory(0);
            else
            {
                grdList.DataSource = GetHistory(wh.PersonID ?? 0);
            }
            grdList.Rebind();
        }

        private void SetEntityValue(EmployeeCardTransaction entity)
        {
            entity.PersonID = cboPersonID.SelectedValue.ToInt();
            entity.Datetime = txtDate.SelectedDate;
            entity.OldCardID = txtOldCardID.Text;
            entity.NewCardID = txtNewCardID.Text;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeCardTransaction entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                ViewState["id"] = entity.EmployeeCardTransactionID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeCardTransactionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeCardTransactionID > (int)ViewState["id"]);
                que.OrderBy(que.EmployeeCardTransactionID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeCardTransactionID < (int)ViewState["id"]);
                que.OrderBy(que.EmployeeCardTransactionID.Descending);
            }

            var entity = new EmployeeCardTransaction();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
            (
                query.PersonID,
                query.EmployeeNumber,
                query.EmployeeName
            );

            query.Where
            (
                query.EmployeeNumber.Like(string.Format("%{0}%", e.Text)),
                query.EmployeeName.Like(string.Format("%{0}%", e.Text))
            );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtOldCardID.Text = string.Empty;
                grdList.DataSource = GetHistory(0);
            }
            else
            {
                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == e.Value.ToInt());
                emp.Query.Load();
                txtOldCardID.Text = emp.AbsenceCardNo;
                grdList.DataSource = GetHistory(e.Value.ToInt());
            }
            grdList.DataBind();
        }

        private DataTable GetHistory(int personID)
        {
            var cards = new EmployeeCardTransactionQuery();
            cards.Where(cards.PersonID == personID && cards.EmployeeCardTransactionID != ViewState["id"].ToInt());
            cards.OrderBy(cards.Datetime.Descending);
            return cards.LoadDataTable();
        }
    }
}