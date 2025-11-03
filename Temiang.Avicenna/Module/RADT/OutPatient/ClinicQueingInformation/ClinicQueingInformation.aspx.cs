using System;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class ClinicQueingInformation : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] == AppConstant.RegistrationType.ClusterPatient)
                ProgramID = AppConstant.Program.ClusterQueingInformationStatus;
            else if (Request.QueryString["type"] == AppConstant.RegistrationType.OutPatient)
                ProgramID = AppConstant.Program.ClinicQueingInformationStatus;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);

                if (Request.QueryString["type"] == AppConstant.RegistrationType.ClusterPatient)
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient);
                else if (Request.QueryString["type"] == AppConstant.RegistrationType.OutPatient)
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient);

                query.Where(
                        query.IsActive == true,
                        qusr.UserID == AppSession.UserLogin.UserID
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

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PatientList;
        }

        private DataTable PatientList
        {
            get
            {
                var reg = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var que = new ServiceUnitQueQuery("c");
                var medic = new ParamedicQuery("d");
                var unit = new ServiceUnitQuery("e");

                reg.Select
                    (
                        que.ServiceUnitID,
                        unit.ServiceUnitName,
                        que.ParamedicID,
                        medic.ParamedicName,
                        que.QueDate,
                        que.QueNo,
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        reg.RegistrationDate,
                        reg.RegistrationTime,
                        @"<CASE WHEN ISNULL(c.IsStopped, CAST(0 AS BIT)) = 1 AND c.EndTime IS NULL THEN '0:00:00'
                                WHEN ISNULL(c.IsStopped, CAST(0 AS BIT)) = 1 AND c.EndTime IS NOT NULL THEN dbo.fnSeconds2Time(DATEDIFF(SECOND, c.QueDate, c.EndTime))
                                ELSE dbo.fnSeconds2Time(DATEDIFF(SECOND, c.QueDate, GETDATE()))
                           END AS Que>",
                        que.IsClosed,
                        que.IsStopped,
                        que.ParentNo,
                        que.Notes
                    );
                reg.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                reg.InnerJoin(que).On(reg.RegistrationNo == que.RegistrationNo);
                reg.InnerJoin(medic).On(que.ParamedicID == medic.ParamedicID);
                reg.InnerJoin(unit).On(que.ServiceUnitID == unit.ServiceUnitID);

                switch (Request.QueryString["type"])
                {
                    case AppConstant.RegistrationType.ClusterPatient:
                        reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient);
                        break;
                    case AppConstant.RegistrationType.OutPatient:
                        reg.Where(reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient);
                        break;
                }

                reg.Where
                    (
                        reg.RegistrationDate == DateTime.Now.Date,
                        reg.IsHoldTransactionEntry == false,
                        reg.IsClosed == false
                    );
                reg.OrderBy(reg.RegistrationNo.Ascending);

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchPatient = "%" + txtPatientSearch.Text + "%";
                    reg.Where(string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient));
                }

                if (!txtMedicalNo.Text.Trim().Equals(string.Empty))
                {
                    string searchText = string.Format("%{0}%", txtMedicalNo.Text);
                    reg.Where(patient.MedicalNo.Like(searchText));
                }

                if (cboParamedicID.SelectedValue != string.Empty)
                    reg.Where(que.ParamedicID == cboParamedicID.SelectedValue);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    reg.Where(que.ServiceUnitID == cboServiceUnitID.SelectedValue);

                var table = reg.LoadDataTable();

                //MergeWithJobOrderList(ref table);

                return table;
            }
        }

        private void MergeWithJobOrderList(ref DataTable patientList)
        {
            var hd = new TransChargesQuery("a");
            var dt = new TransChargesItemQuery("b");
            var it = new ItemQuery("c");
            var unit = new ServiceUnitQuery("d");
            var reg = new RegistrationQuery("e");
            var patient = new PatientQuery("f");
            var medic = new ParamedicQuery("g");

            hd.Select(
                hd.TransactionNo,
                hd.RegistrationNo,
                hd.ToServiceUnitID.As("ServiceUnitID"),
                unit.ServiceUnitName,
                dt.ItemID,
                it.ItemName,
                patient.MedicalNo,
                patient.PatientName,
                hd.TransactionDate,
                reg.ParamedicID,
                medic.ParamedicName
                );
            hd.InnerJoin(unit).On(hd.ToServiceUnitID == unit.ServiceUnitID);
            hd.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);
            hd.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            hd.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            hd.InnerJoin(dt).On(hd.TransactionNo == dt.TransactionNo);
            hd.InnerJoin(it).On(dt.ItemID == it.ItemID);
            hd.Where(
                hd.IsOrder == true,
                hd.IsApproved == true
                );

            var table = hd.LoadDataTable();

            if (!table.AsEnumerable().Any())
                return;

            var row = patientList.NewRow();
            row["ServiceUnitID"] = table.Rows[0]["ServiceUnitID"];
            row["ServiceUnitName"] = table.Rows[0]["ServiceUnitName"];
            row["ParamedicID"] = table.Rows[0]["ParamedicID"];
            row["ParamedicName"] = table.Rows[0]["ParamedicName"];
            row["QueDate"] = table.Rows[0]["TransactionDate"];
            row["QueNo"] = table.Rows[0][""];
            row["RegistrationNo"] = table.Rows[0]["TransactionNo"];
            row["MedicalNo"] = table.Rows[0]["MedicalNo"];
            row["PatientName"] = table.Rows[0]["PatientName"];
            row["RegistrationDate"] = Convert.ToDateTime(table.Rows[0]["TransactionDate"]).Date;
            row["RegistrationTime"] = Convert.ToDateTime(table.Rows[0]["TransactionDate"]).ToString("HH:mm");
            row["Que"] = table.Rows[0][""];
            row["IsClosed"] = false;
            row["IsStopped"] = true;
            row["ParentNo"] = table.Rows[0]["RegistrationNo"];
            row["Notes"] = table.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, entity) => current + (entity["ItemName"] + "\n"));

            patientList.Rows.Add(row);
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value != string.Empty)
            {
                var medic = new ParamedicQuery("a");
                var unit = new ServiceUnitParamedicQuery("b");

                medic.InnerJoin(unit).On(medic.ParamedicID == unit.ParamedicID);
                medic.Where
                    (
                        medic.IsActive == true,
                        unit.ServiceUnitID == e.Value);

                medic.OrderBy(medic.ParamedicName.Ascending);

                var param = new ParamedicCollection();
                param.Load(medic);

                cboParamedicID.Items.Clear();
                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }
            }
            else
                cboParamedicID.Items.Clear();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected string GetCommandTemplateQueStatus(GridItem container)
        {
            if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsClosed")))
                return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" alt=\"Finished\" /> Finished";
            return Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsStopped")) ?
                string.Format("<a href=\"#\" onclick=\"StartQue('{0}', '{1}', '{2}', '{3}'); return false;\"><img src=\"../../../../Images/Toolbar/insert16.png\" border=\"0\" alt=\"Start\" /> Waiting</a>",
                    DataBinder.Eval(container.DataItem, "ServiceUnitID"),
                    DataBinder.Eval(container.DataItem, "ParamedicID"),
                    DataBinder.Eval(container.DataItem, "QueDate"),
                    DataBinder.Eval(container.DataItem, "QueNo")) :
                string.Format("<a href=\"#\" onclick=\"FinishQue('{0}', '{1}', '{2}', '{3}'); return false;\"><img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" alt=\"Finish\" /> Counting</a>",
                    DataBinder.Eval(container.DataItem, "ServiceUnitID"),
                    DataBinder.Eval(container.DataItem, "ParamedicID"),
                    DataBinder.Eval(container.DataItem, "QueDate"),
                    DataBinder.Eval(container.DataItem, "QueNo"));
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            var command = eventArgument.Split(';');
            var param = command[1].Split('|');

            var entity = new ServiceUnitQue();
            if (entity.LoadByPrimaryKey(param[0], param[1], DateTime.Parse(param[2]), int.Parse(param[3])))
            {
                switch (command[0])
                {
                    case "reset":
                    case "start":
                        entity.IsClosed = false;
                        entity.str.EndTime = string.Empty;
                        entity.IsStopped = false;
                        break;
                    case "finish":
                        entity.IsClosed = true;
                        entity.EndTime = DateTime.Now;
                        entity.IsStopped = true;
                        break;
                }
                entity.Save();
            }

            grdList.Rebind();
        }

        protected void grdList_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column is GridExpandColumn)
                e.Column.Visible = false;
            else if (e.Column is GridBoundColumn)
                e.Column.HeaderStyle.Width = Unit.Pixel(100);
        }

        public void Page_PreRenderComplete(object sender, EventArgs e)
        {
            HideExpandColumnRecursive(grdList.MasterTableView);
        }

        public void HideExpandColumnRecursive(GridTableView tableView)
        {
            var nestedViewItems = tableView.GetItems(GridItemType.NestedView);
            foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
            {
                foreach (var nestedView in nestedViewItem.NestedTableViews)
                {
                    nestedView.Style["border"] = "0";

                    var MyExpandCollapseButton = (Button)nestedView.ParentItem.FindControl("MyExpandCollapseButton");
                    if (nestedView.Items.Count == 0)
                    {
                        if (MyExpandCollapseButton != null)
                            MyExpandCollapseButton.Style["visibility"] = "hidden";
                        nestedViewItem.Visible = false;
                    }
                    else
                    {
                        if (MyExpandCollapseButton != null)
                            MyExpandCollapseButton.Style.Remove("visibility");
                    }

                    if (nestedView.HasDetailTables)
                        HideExpandColumnRecursive(nestedView);
                }
            }
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            CreateExpandCollapseButton(e.Item, "RegistrationNo");

            if (e.Item is GridHeaderItem && e.Item.OwnerTableView != grdList.MasterTableView)
                e.Item.Style["display"] = "none";

            if (e.Item is GridNestedViewItem)
                e.Item.Cells[0].Visible = false;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            CreateExpandCollapseButton(e.Item, "RegistrationNo");
        }

        public void CreateExpandCollapseButton(GridItem item, string columnUniqueName)
        {
            var gridDataItem = item as GridDataItem;
            if (gridDataItem != null)
            {
                if (item.FindControl("MyExpandCollapseButton") == null)
                {
                    var button = new Button();
                    button.Click += button_Click;
                    button.CommandName = "ExpandCollapse";
                    button.CssClass = (item.Expanded) ? "rgCollapse" : "rgExpand";
                    button.ID = "MyExpandCollapseButton";

                    if (item.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
                    {
                        var script = String.Format(@"$find(""{0}"")._toggleExpand(this, event); return false;", item.Parent.Parent.ClientID);
                        button.OnClientClick = script;
                    }

                    var level = item.ItemIndexHierarchical.Split(':').Length;
                    if (level > 1)
                        button.Style["margin-left"] = level + 10 + "px";

                    var cell = (gridDataItem)[columnUniqueName];
                    cell.Controls.Add(button);
                    cell.Controls.Add(new LiteralControl("&nbsp;"));
                    cell.Controls.Add(new LiteralControl((gridDataItem).GetDataKeyValue(columnUniqueName).ToString()));
                }
            }
        }

        void button_Click(object sender, EventArgs e)
        {
            ((Button)sender).CssClass = (((Button)sender).CssClass == "rgExpand") ? "rgCollapse" : "rgExpand";
        }
    }
}