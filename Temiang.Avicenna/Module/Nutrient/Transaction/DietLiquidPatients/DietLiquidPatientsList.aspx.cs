using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietLiquidPatientsList : BasePage
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

            ProgramID = AppConstant.Program.DietLiquidPatients;

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

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
                    cboServiceUnitID.SelectedValue = Request.QueryString["uid"];
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = TransCharges;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }            
        }

        private DataTable TransCharges
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) 
                    && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Diet Liquid")) return null;

                DataTable dtb = TransChargesInPatient;

                return dtb.DefaultView.ToTable(true, "RoomName", "RegistrationDate", "QueNo", "ServiceUnitID", "ParamedicID", "ParamedicName", "RegistrationNo",
                    "MedicalNo", "PatientName", "Sex", "GuarantorName", "Group", "PatientID", "IsConsul", "BedID", "NoteCount", "DietName", "DietCompName", "IsLiquidDiet", "SalutationName", "Age");
            }
        }

        private DataTable TransChargesInPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var nc = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        query.BedID,
                        nc.NoteCount,
                        @"< '' AS DietName>",
                        @"< '' AS DietCompName>",
                        @"<CAST(0 AS BIT) AS IsLiquidDiet>",
                        sal.ItemName.As("SalutationName"),
                         @"<CAST(e.AgeInYear AS VARCHAR) + 'y ' + CAST(e.AgeInMonth AS VARCHAR) + 'm ' + CAST(e.AgeInDay AS VARCHAR) + 'd' AS Age>"
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(nc).On(query.RegistrationNo == nc.RegistrationNo & nc.NoteCount > 0);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.MedicalNo == searchReg,
                            patient.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.DischargeDate.IsNull(),
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsVoid == false
                    );
                query.OrderBy(query.RoomID.Ascending, query.BedID.Ascending);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    var bed = new Bed();
                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                    {
                        if (bed.IsNeedConfirmation == true & bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            row.Delete();
                        else
                        {
                            var isLiquidDiet = false;

                            var dpColl = new DietPatientCollection();
                            dpColl.Query.es.Top = 1;
                            dpColl.Query.Where(dpColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                               dpColl.Query.EffectiveStartDate.Date() <= DateTime.Now.Date,
                                               dpColl.Query.IsVoid == false);
                            dpColl.Query.Where(dpColl.Query.Or(dpColl.Query.EffectiveEndDate.IsNull(),
                                                               dpColl.Query.EffectiveEndDate.Date() >= DateTime.Now.Date));
                            dpColl.Query.OrderBy(dpColl.Query.EffectiveStartDate.Ascending, dpColl.Query.EffectiveStartTime.Ascending);
                            dpColl.LoadAll();

                            if (dpColl.Count > 0)
                            {
                                var dietName = string.Empty;
                                var dietCompName = string.Empty;
                                var i = 0;
                                var ii = 0;
                                foreach (var dp in dpColl)
                                {
                                    var dpiColl = new DietPatientItemCollection();
                                    dpiColl.Query.es.Top = 1;
                                    dpiColl.Query.Where(dpiColl.Query.TransactionNo == dp.TransactionNo);
                                    dpiColl.LoadAll();

                                    if (dpiColl.Count > 0)
                                    {
                                        foreach (var dpi in dpiColl)
                                        {
                                            if (isLiquidDiet == false)
                                                isLiquidDiet = (dp.FormOfFood == "1" || dp.FormOfFood == "7");

                                            var diet = new Diet();
                                            if (diet.LoadByPrimaryKey(dpi.DietID))
                                            {
                                                if (i > 0)
                                                    dietName += ", ";

                                                dietName += diet.DietName;

                                                i += 1;

                                                var dpicColl = new DietComplicationPatientCollection();
                                                dpicColl.Query.Where(dpicColl.Query.TransactionNo == dp.TransactionNo,
                                                                     dpicColl.Query.DietID == dpi.DietID);
                                                dpicColl.LoadAll();
                                                if (dpicColl.Count > 0)
                                                {
                                                    foreach (var x in dpicColl)
                                                    {
                                                        diet = new Diet();
                                                        if (diet.LoadByPrimaryKey(x.DietComplicationID))
                                                        {
                                                            if (ii > 0)
                                                                dietCompName += ", ";

                                                            dietCompName += diet.DietName;

                                                            ii += 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                row["DietName"] = dietName;
                                row["DietCompName"] = dietCompName;
                            }

                            row["IsLiquidDiet"] = isLiquidDiet;
                        }
                    }
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detail":
                    var query = new DietLiquidPatientQuery("a");

                    query.Select
                        (
                            query.RegistrationNo,
                            query.TransactionNo,
                            @"<CAST(CONVERT(VARCHAR(10), a.EffectiveStartDate, 112) + ' ' + a.EffectiveStartTime AS DATETIME) AS 'EffectiveStartDate'>",
                            query.Notes,
                            query.IsVoid,
                            query.LastUpdateByUserID
                        );

                    query.Where
                        (
                            query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                            query.IsVoid == false
                        );

                    query.OrderBy(query.TransactionNo.Ascending);
                    DataTable dtb = query.LoadDataTable();

                    e.DetailTableView.DataSource = dtb;

                    break;

                case "detailItem":
                    string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();

                    var query2 = new DietLiquidPatientTimeQuery("a");
                    var foodq = new FoodQuery("b");

                    query2.Select
                        (
                            query2.TransactionNo,
                            query2.DietTime,
                            query2.FoodID,
                            foodq.FoodName,
                            query2.Measure,
                            query2.AmountOfWater,
                            query2.Etc,
                            query2.Total,
                            query2.Notes
                        );

                    query2.LeftJoin(foodq).On(query2.FoodID == foodq.FoodID);
                    query2.Where(query2.TransactionNo == transNo);

                    query2.OrderBy(query2.DietTime.Ascending);

                    DataTable dtb2 = query2.LoadDataTable();

                    e.DetailTableView.DataSource = dtb2;
                    break;
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if ((sourceControl is RadGrid) && (eventArgument == "rebind"))
                grdList.Rebind();
        }
    }
}
