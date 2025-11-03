using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class OvertimeItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboSupervisorId
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboSupervisorID");
            }
        }

        private RadDatePicker txtTransactionDate
        {
            get
            {
                return (RadDatePicker)Helper.FindControlRecursive(Page, "txtTransactionDate");
            }
        }

        private RadComboBox cboPayrollPeriodID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboPayrollPeriodID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var hours = new WorkingHourCollection();
            hours.Query.Where(hours.Query.IsActive == true);
            hours.Query.Load();

            cboWorkingHour.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var hour in hours)
            {
                cboWorkingHour.Items.Add(new RadComboBoxItem(hour.WorkingHourName, (hour.WorkingHourID ?? -1).ToString()));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboPersonID.Enabled = false;
            cboSalaryComponetID.Enabled = false;

            var personId = (int)DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.PersonID);
            var pq = new VwEmployeeTableQuery();
            pq.Where(pq.PersonID == personId);
            cboPersonID.DataSource = pq.LoadDataTable();
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.PersonID).ToString();

            var salaryCompId = (int)DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.SalaryComponentID);
            var sc = new SalaryComponentQuery();
            sc.Where(sc.SalaryComponentID == salaryCompId);
            cboSalaryComponetID.DataSource = sc.LoadDataTable();
            cboSalaryComponetID.DataBind();
            cboSalaryComponetID.SelectedValue = DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.SalaryComponentID).ToString();

            txtAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.Amount));
            txtNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.Notes));

            if (DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.WorkingHourID) != DBNull.Value)
            {
                try {
                    trWorkingHour.Visible = true;
                    cboWorkingHour.SelectedValue = DataBinder.Eval(DataItem, EmployeeOvertimeItemMetadata.ColumnNames.WorkingHourID).ToString();
                }
                catch {
                    trWorkingHour.Visible = false;
                }
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Employee Name required.");
                return;
            }

            if (string.IsNullOrEmpty(cboSalaryComponetID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Salary Componet Name required.");
                return;
            }

            if (txtAmount.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0.");
                return;
            }

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EmployeeOvertimeItemCollection)Session["collEmployeeOvertimeItem"];

                //TODO: Betulkan cara pengecekannya
                string pid = cboPersonID.SelectedValue;
                string sc = cboSalaryComponetID.SelectedValue;
                bool isExist = false;
                foreach (EmployeeOvertimeItem item in coll)
                {
                    if (item.PersonID.ToString().Equals(pid) && item.SalaryComponentID.ToString().Equals(sc))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Employee: {0} with {1} has exist", cboPersonID.Text, cboSalaryComponetID.Text);
                }
            }
        }

        #region Properties for return entry value
        public String PersonID
        {
            get { return cboPersonID.SelectedValue; }
        }
        public String PersonName
        {
            get { return cboPersonID.Text; }
        }
        public String SalaryComponetID
        {
            get { return cboSalaryComponetID.SelectedValue; }
        }
        public String SalaryComponetName
        {
            get { return cboSalaryComponetID.Text; }
        }
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }
        public int WorkingHourID
        {
            get { return trWorkingHour.Visible ? cboWorkingHour.SelectedValue.ToInt() : -1; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
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
                    query.Or(query.SupervisorId == CboSupervisorId.SelectedValue.ToInt(), query.ManagerID == CboSupervisorId.SelectedValue.ToInt()),
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.SRSalaryType == AppSession.Parameter.SalaryTypeOvertime,
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.SalaryComponentCode.Ascending);

            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }
        #endregion

        protected void cboPersonID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) trWorkingHour.Visible = false;
            else
            {
                var hour = GetWorkingHour(e.Value.ToInt(), cboPayrollPeriodID.SelectedValue.ToInt(), txtTransactionDate.SelectedDate.Value);
                trWorkingHour.Visible = hour != null;
            }
        }

        private int GetWorkingHourID(int day, Temiang.Avicenna.BusinessObject.WorkingScheduleDetail workingScheduleDetail)
        {
            if (day == 1)
                return workingScheduleDetail.WorkingHourIDDay1 ?? -1;
            else if (day == 2)
                return workingScheduleDetail.WorkingHourIDDay2 ?? -1;
            else if (day == 3)
                return workingScheduleDetail.WorkingHourIDDay3 ?? -1;
            else if (day == 4)
                return workingScheduleDetail.WorkingHourIDDay4 ?? -1;
            else if (day == 5)
                return workingScheduleDetail.WorkingHourIDDay5 ?? -1;
            else if (day == 6)
                return workingScheduleDetail.WorkingHourIDDay6 ?? -1;
            else if (day == 7)
                return workingScheduleDetail.WorkingHourIDDay7 ?? -1;
            else if (day == 8)
                return workingScheduleDetail.WorkingHourIDDay8 ?? -1;
            else if (day == 9)
                return workingScheduleDetail.WorkingHourIDDay9 ?? -1;
            else if (day == 10)
                return workingScheduleDetail.WorkingHourIDDay10 ?? -1;
            else if (day == 11)
                return workingScheduleDetail.WorkingHourIDDay11 ?? -1;
            else if (day == 12)
                return workingScheduleDetail.WorkingHourIDDay12 ?? -1;
            else if (day == 13)
                return workingScheduleDetail.WorkingHourIDDay13 ?? -1;
            else if (day == 14)
                return workingScheduleDetail.WorkingHourIDDay14 ?? -1;
            else if (day == 15)
                return workingScheduleDetail.WorkingHourIDDay15 ?? -1;
            else if (day == 16)
                return workingScheduleDetail.WorkingHourIDDay16 ?? -1;
            else if (day == 17)
                return workingScheduleDetail.WorkingHourIDDay17 ?? -1;
            else if (day == 18)
                return workingScheduleDetail.WorkingHourIDDay18 ?? -1;
            else if (day == 19)
                return workingScheduleDetail.WorkingHourIDDay19 ?? -1;
            else if (day == 20)
                return workingScheduleDetail.WorkingHourIDDay20 ?? -1;
            else if (day == 21)
                return workingScheduleDetail.WorkingHourIDDay21 ?? -1;
            else if (day == 22)
                return workingScheduleDetail.WorkingHourIDDay22 ?? -1;
            else if (day == 23)
                return workingScheduleDetail.WorkingHourIDDay23 ?? -1;
            else if (day == 24)
                return workingScheduleDetail.WorkingHourIDDay24 ?? -1;
            else if (day == 25)
                return workingScheduleDetail.WorkingHourIDDay25 ?? -1;
            else if (day == 26)
                return workingScheduleDetail.WorkingHourIDDay26 ?? -1;
            else if (day == 27)
                return workingScheduleDetail.WorkingHourIDDay27 ?? -1;
            else if (day == 28)
                return workingScheduleDetail.WorkingHourIDDay28 ?? -1;
            else if (day == 29)
                return workingScheduleDetail.WorkingHourIDDay29 ?? -1;
            else if (day == 30)
                return workingScheduleDetail.WorkingHourIDDay30 ?? -1;
            else if (day == 31)
                return workingScheduleDetail.WorkingHourIDDay31 ?? -1;

            return -1;
        }

        private int GetWorkingHourID(int day, Temiang.Avicenna.BusinessObject.WorkingSchduleInterventionDetail workingScheduleDetail)
        {
            if (day == 1)
                return workingScheduleDetail.WorkingHourIDDay1 ?? -1;
            else if (day == 2)
                return workingScheduleDetail.WorkingHourIDDay2 ?? -1;
            else if (day == 3)
                return workingScheduleDetail.WorkingHourIDDay3 ?? -1;
            else if (day == 4)
                return workingScheduleDetail.WorkingHourIDDay4 ?? -1;
            else if (day == 5)
                return workingScheduleDetail.WorkingHourIDDay5 ?? -1;
            else if (day == 6)
                return workingScheduleDetail.WorkingHourIDDay6 ?? -1;
            else if (day == 7)
                return workingScheduleDetail.WorkingHourIDDay7 ?? -1;
            else if (day == 8)
                return workingScheduleDetail.WorkingHourIDDay8 ?? -1;
            else if (day == 9)
                return workingScheduleDetail.WorkingHourIDDay9 ?? -1;
            else if (day == 10)
                return workingScheduleDetail.WorkingHourIDDay10 ?? -1;
            else if (day == 11)
                return workingScheduleDetail.WorkingHourIDDay11 ?? -1;
            else if (day == 12)
                return workingScheduleDetail.WorkingHourIDDay12 ?? -1;
            else if (day == 13)
                return workingScheduleDetail.WorkingHourIDDay13 ?? -1;
            else if (day == 14)
                return workingScheduleDetail.WorkingHourIDDay14 ?? -1;
            else if (day == 15)
                return workingScheduleDetail.WorkingHourIDDay15 ?? -1;
            else if (day == 16)
                return workingScheduleDetail.WorkingHourIDDay16 ?? -1;
            else if (day == 17)
                return workingScheduleDetail.WorkingHourIDDay17 ?? -1;
            else if (day == 18)
                return workingScheduleDetail.WorkingHourIDDay18 ?? -1;
            else if (day == 19)
                return workingScheduleDetail.WorkingHourIDDay19 ?? -1;
            else if (day == 20)
                return workingScheduleDetail.WorkingHourIDDay20 ?? -1;
            else if (day == 21)
                return workingScheduleDetail.WorkingHourIDDay21 ?? -1;
            else if (day == 22)
                return workingScheduleDetail.WorkingHourIDDay22 ?? -1;
            else if (day == 23)
                return workingScheduleDetail.WorkingHourIDDay23 ?? -1;
            else if (day == 24)
                return workingScheduleDetail.WorkingHourIDDay24 ?? -1;
            else if (day == 25)
                return workingScheduleDetail.WorkingHourIDDay25 ?? -1;
            else if (day == 26)
                return workingScheduleDetail.WorkingHourIDDay26 ?? -1;
            else if (day == 27)
                return workingScheduleDetail.WorkingHourIDDay27 ?? -1;
            else if (day == 28)
                return workingScheduleDetail.WorkingHourIDDay28 ?? -1;
            else if (day == 29)
                return workingScheduleDetail.WorkingHourIDDay29 ?? -1;
            else if (day == 30)
                return workingScheduleDetail.WorkingHourIDDay30 ?? -1;
            else if (day == 31)
                return workingScheduleDetail.WorkingHourIDDay31 ?? -1;

            return -1;
        }

        private WorkingHour GetWorkingHour(int personID, int payrollPeriodID, DateTime now)
        {
            var wh = new WorkingHour();

            var wsdq = new BusinessObject.WorkingScheduleDetailQuery("a");
            var wsq = new WorkingScheduleQuery("b");

            wsdq.es.Top = 1;
            wsdq.InnerJoin(wsq).On(wsdq.WorkingScheduleID == wsq.WorkingScheduleID && wsq.PayrollPeriodID == payrollPeriodID && wsq.IsApproved == true);
            wsdq.Where(wsdq.PersonID == personID);
            wsdq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
            wsdq.OrderBy(wsdq.LastUpdateDateTime.Descending);

            var wsd = new BusinessObject.WorkingScheduleDetail();
            if (!wsd.Load(wsdq)) return null; // tidak punya jadwal

            var wsidq = new WorkingSchduleInterventionDetailQuery("a");
            var wsiq = new WorkingSchduleInterventionQuery("b");

            wsidq.es.Top = 1;
            wsidq.InnerJoin(wsiq).On(wsidq.WorkingSchduleInterventionID == wsiq.WorkingSchduleInterventionID && wsiq.WorkingScheduleID == wsd.WorkingScheduleID && wsiq.IsApproved == true);
            wsidq.Where(wsidq.PersonID == personID);
            wsidq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
            wsidq.OrderBy(wsidq.LastUpdateDateTime.Descending);

            var wsidc = new WorkingSchduleInterventionDetailCollection();
            if (!wsidc.Load(wsidq))
            {
                wh = new WorkingHour();
                return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
            }
            else
            {
                foreach (var wsid in wsidc)
                {
                    if (GetWorkingHourID(now.Day, wsid) == -1)
                    {
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
                    }
                    else
                    {
                        wh = new WorkingHour();
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsid)) ? wh : null;
                    }
                }
            }

            return null;
        }
    }
}