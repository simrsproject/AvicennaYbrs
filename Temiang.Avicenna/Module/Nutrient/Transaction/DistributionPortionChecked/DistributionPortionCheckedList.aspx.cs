using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using DevExpress.XtraRichEdit.API.Native;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DistributionPortionCheckedList : BasePage
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

            ProgramID = AppConstant.Program.DistributionPortionChecked;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

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
                if (!string.IsNullOrEmpty(Request.QueryString["unitid"]))
                    cboServiceUnitID.SelectedValue = Request.QueryString["unitid"].ToString();

                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                StandardReference.Initialize(cboSRMealSet, AppEnum.StandardReference.MealSet, true);
                if (!string.IsNullOrEmpty(Request.QueryString["mealset"]))
                    cboSRMealSet.SelectedValue = Request.QueryString["mealset"].ToString();
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

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var srInvalidReason = dataItem["SRInvalidReason"].Text;
            if (!string.IsNullOrEmpty(srInvalidReason))
            {
                var std = (dataItem["OrderNo"].FindControl("cboSRInvalidReason") as RadComboBox);

                if (!std.Items.Any())
                {
                    std.Items.Clear();
                    std.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    if (ViewState["invalidreason"] == null) PopulateInvalidReason();

                    var table = ((DataTable)ViewState["invalidreason"]);
                    foreach (DataRow row in table.Rows)
                    {
                        std.Items.Add(new RadComboBoxItem((string)row["ItemName"], (string)row["ItemID"]));
                    }
                }

                var item = std.Items.Cast<RadComboBoxItem>().SingleOrDefault(s => s.Value == srInvalidReason);
                if (item != null) std.SelectedValue = item.Value;
            }
        }

        private void PopulateInvalidReason()
        {
            if (ViewState["invalidreason"] != null)
                return;

            var query = new AppStandardReferenceItemQuery("b");
            query.Select(
                query.ItemID,
                query.ItemName
                );
            query.Where(query.StandardReferenceID == "InvalidReason", query.IsActive == true);

            ViewState["invalidreason"] = query.LoadDataTable();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = MealOrders;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        private DataTable MealOrders
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Distribution Portion")) return null;

                var query = new MealOrderQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var bed = new BedQuery("d");
                var room = new ServiceRoomQuery("e");
                var unit = new ServiceUnitQuery("f");
                var diet = new DietQuery("g");
                var menu = new MenuQuery("h");
                var distPortion = new DistributionPortionQuery("j");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.OrderNo,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        room.RoomName,
                        reg.BedID,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.FastingTime,
                        menu.MenuName,
                        query.DietID,
                        diet.DietName,
                        @"<CASE WHEN j.OrderNo IS NOT NULL AND j.IsVoid = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsDistributed'>",
                        @"<CAST(b.AgeInYear AS VARCHAR) + 'y ' + CAST(b.AgeInMonth AS VARCHAR) + 'm ' + CAST(b.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        sal.ItemName.As("SalutationName"),
                        query.FastingTime,
                        query.DietPatientNo,
                        @"<ISNULL(j.IsInvalid, 0) AS 'IsInvalid'>",
                        @"<ISNULL(j.SRInvalidReason, '') AS 'SRInvalidReason'>"
                    );

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(bed).On(reg.BedID == bed.BedID);
                query.LeftJoin(room).On(reg.RoomID == room.RoomID);
                query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(diet).On(query.DietID == diet.DietID);
                query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                query.InnerJoin(distPortion).On(query.OrderNo == distPortion.OrderNo &&
                                               distPortion.SRMealSet == cboSRMealSet.SelectedValue);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue, reg.DischargeDate.IsNull());
                query.Where(query.IsApproved == true, query.EffectiveDate == txtOrderDate.SelectedDate,
                            reg.IsClosed == false, reg.DischargeDate.IsNull(), distPortion.IsVoid == false);
                if (AppSession.Parameter.IsUsingMealOrderVerification)
                    query.Where(query.IsVerified == true);

                query.OrderBy(unit.ServiceUnitName.Ascending, query.BedID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    bool isFasting = false;
                    if (!string.IsNullOrEmpty(row["FastingTime"].ToString()))
                    {
                        switch (cboSRMealSet.SelectedValue)
                        {
                            case "01": //pagi
                                isFasting = row["FastingTime"].ToString().Contains("1");
                                break;
                            case "02": //siang
                                isFasting = row["FastingTime"].ToString().Contains("2");
                                break;
                            case "03": //sore
                                isFasting = row["FastingTime"].ToString().Contains("3");
                                break;
                        }
                        
                    }
                    if (isFasting)
                        row.Delete();
                    else 
                    {
                        var moicoll = new MealOrderItemCollection();
                        moicoll.Query.Where(moicoll.Query.OrderNo == row["OrderNo"].ToString(), moicoll.Query.SRMealSet == cboSRMealSet.SelectedValue);
                        moicoll.Query.es.Top = 1;
                        moicoll.LoadAll();

                        var dietDetailName = string.Empty;
                        var menuDetailName = string.Empty;
                        foreach (var moi in moicoll)
                        {
                            if (!string.IsNullOrEmpty(moi.DietID))
                            {
                                var d = new Diet();
                                if (d.LoadByPrimaryKey(moi.DietID))
                                    dietDetailName = d.DietName;

                            }
                            if (!string.IsNullOrEmpty(moi.MenuID))
                            {
                                var m = new Menu();
                                if (m.LoadByPrimaryKey(moi.MenuID))
                                    menuDetailName = m.MenuName;
                            }
                        }

                        if (dietDetailName != string.Empty)
                            row["DietName"] = dietDetailName;
                        if (menuDetailName != string.Empty)
                            row["MenuName"] = menuDetailName;
                    }
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSRMealSet_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    pnlInfo.Visible = false;
                    grdList.Rebind();

                    break;

                default:
                    if (eventArgument.Contains("invalid|"))
                    {
                        var param = eventArgument.Split('|');
                        string orderNo = param[1];

                        foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                        {
                            if (dataItem.GetDataKeyValue("OrderNo").ToString() == orderNo)
                            {
                                if (string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboSRInvalidReason")).SelectedValue))
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "Reason required.";
                                }
                                else {
                                    pnlInfo.Visible = false;

                                    var mealOrder = new MealOrder();
                                    if (mealOrder.LoadByPrimaryKey(orderNo))
                                    {
                                        var entity = new DistributionPortion();
                                        if (entity.LoadByPrimaryKey(orderNo, cboSRMealSet.SelectedValue))
                                        {
                                            entity.IsInvalid = true;
                                            entity.SRInvalidReason = ((RadComboBox)dataItem.FindControl("cboSRInvalidReason")).SelectedValue;
                                            entity.CheckedByUserID = AppSession.UserLogin.UserID;
                                            entity.CheckedDateTime = (new DateTime()).NowAtSqlServer();

                                            entity.Save();
                                        }
                                    }

                                    grdList.Rebind();

                                    break;
                                }
                            }
                        }
                    }

                    break;
            }
        }

        protected void cboSRInvalidReason_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRInvalidReason_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery();
            query.Where(query.StandardReferenceID == "InvalidReason", query.ItemName.Like("%" + e.Text + "%"),
                        query.IsActive == true);
            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);

            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }
    }
}