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

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class MealAttendanceItem : System.Web.UI.UserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtOpenDate.SelectedDate = DateTime.Now.Date;
            txtOpenTime.Text = DateTime.Now.ToString("HH:mm");

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var datetime = Convert.ToDateTime(DataBinder.Eval(DataItem, MealAttendanceDetailMetadata.ColumnNames.Datetime));
            txtOpenDate.SelectedDate = datetime.Date;
            txtOpenTime.Text = datetime.ToString("HH:mm");

            var personId = (int)DataBinder.Eval(DataItem, MealAttendanceDetailMetadata.ColumnNames.PersonID);
            var pq = new VwEmployeeTableQuery();
            pq.Where(pq.PersonID == personId);
            cboPersonID.DataSource = pq.LoadDataTable();
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = DataBinder.Eval(DataItem, MealAttendanceDetailMetadata.ColumnNames.PersonID).ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (MealAttendanceDetailCollection)Session["collMealAttendanceDetailCollection"];

                //TODO: Betulkan cara pengecekannya
                //string pid = cboPersonID.SelectedValue;
                //string sc = cboSalaryComponetID.SelectedValue;
                //bool isExist = false;
                //foreach (EmployeeOvertimeItem item in coll)
                //{
                //    if (item.PersonID.ToString().Equals(pid) && item.SalaryComponentID.ToString().Equals(sc))
                //    {
                //        isExist = true;
                //        break;
                //    }
                //}
                //if (isExist)
                //{
                //    args.IsValid = false;
                //    ((CustomValidator)source).ErrorMessage = string.Format("Employee: {0} with {1} has exist", cboPersonID.Text, cboSalaryComponetID.Text);
                //}
            }
        }

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
                query.EmployeeNumber.Like(searchTextContain),
                query.EmployeeName.Like(searchTextContain)
            );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        public DateTime Datetime
        {
            get
            {
                DateTime.TryParseExact(
                    txtOpenDate.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + txtOpenTime.TextWithLiterals,
                    "MM/dd/yyyy HH:mm", null,
                    DateTimeStyles.None, out var parsedOpen);
                return parsedOpen;
            }
        }

        public int PersonID
        {
            get
            {
                return cboPersonID.SelectedValue.ToInt();
            }
        }

        public string EmployeeName
        {
            get
            {
                return cboPersonID.Text;
            }
        }

        public string ServiceUnitName
        {
            get
            {
                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.PersonID == PersonID);
                emp.Query.Load();

                if (emp == null || string.IsNullOrEmpty(emp.ServiceUnitID)) return string.Empty;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(emp.ServiceUnitID);
                return unit.ServiceUnitName;
            }
        }
    }
}