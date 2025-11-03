using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderOprList : BasePage
    {       
        protected void Page_Init(object sender, EventArgs e)
        {            
            ProgramID = AppConstant.Program.MealOrderOutpatient;

            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                if (!this.IsUserCrossUnitAble)
                {
                    var usrq = new AppUserServiceUnitQuery("b");
                    query.InnerJoin(usrq).On(usrq.ServiceUnitID == query.ServiceUnitID &&
                                             usrq.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp), query.IsActive == true);
                query.OrderBy(query.DepartmentID.Ascending);

                var coll = new ServiceUnitCollection();
                coll.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                txtRegDate1.SelectedDate = DateTime.Now;
                txtRegDate2.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }       

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }           
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "neworder")
            {
                var regNo = e.CommandArgument.ToString();

                InsertToMealOrder(regNo);

                grdList.Rebind();
            }
        }

        protected void grdLMealOrderList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdLMealOrderList.DataSource = new String[] { };
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

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdLMealOrderList.Rebind();
        }

        private DataTable Registrations
        {
            get
            {               
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("c");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        grr.GuarantorName,
                        sal.ItemName.As("SalutationName")
                    );

                qr.InnerJoin(qp).On(qp.PatientID == qr.PatientID);
                qr.LeftJoin(qm).On(qm.ParamedicID == qr.ParamedicID);
                qr.LeftJoin(unit).On(unit.ServiceUnitID == qr.ServiceUnitID);
                qr.LeftJoin(room).On(room.RoomID == qr.RoomID);
                qr.InnerJoin(grr).On(grr.GuarantorID == qr.GuarantorID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == qp.SRSalutation);

                if (!this.IsUserCrossUnitAble)
                {
                    var usrunit = new AppUserServiceUnitQuery("un");
                    qr.InnerJoin(usrunit).On(usrunit.UserID == AppSession.UserLogin.UserID &
                                         usrunit.ServiceUnitID == qr.ServiceUnitID);
                }

                if (!txtRegDate1.IsEmpty && !txtRegDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate.Date().Between(txtRegDate1.SelectedDate, txtRegDate2.SelectedDate));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where
                    //    (qr.Or
                    //         (
                    //             string.Format("<r.RegistrationNo = '{0}' OR >", searchReg),
                    //             string.Format("<p.MedicalNo = '{0}' OR >", searchReg),
                    //             string.Format("<p.OldMedicalNo = '{0}'>", searchReg),
                    //             string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //             string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //         )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp),
                    qr.IsClosed == false,
                    qr.IsVoid == false,
                    qr.IsNonPatient == false);

                qr.OrderBy(unit.ServiceUnitName.Ascending, qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        private DataTable MealOrders
        {
            get
            {               
                var query = new MealOrderQuery("q");
                var reg = new RegistrationQuery("a");
                var patient = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");
                var medic = new ParamedicQuery("e");
                var grr = new GuarantorQuery("f");
                var cls = new ClassQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.OrderNo,
                        query.OrderDate,
                        query.RegistrationNo,
                        reg.RegistrationDate,
                        reg.ServiceUnitID,
                        unit.ServiceUnitName,
                        reg.RoomID,
                        room.RoomName,
                        reg.ParamedicID,
                        medic.ParamedicName,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        reg.ChargeClassID.As("ClassID"),
                        cls.ClassName,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(a.AgeInYear AS VARCHAR) + 'y ' + CAST(a.AgeInMonth AS VARCHAR) + 'm ' + CAST(a.AgeInDay AS VARCHAR) + 'd' AS Age>",
                        query.IsApproved,
                        query.IsVoid
                    );
                query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
                query.InnerJoin(patient).On(patient.PatientID == reg.PatientID);
                query.InnerJoin(grr).On(grr.GuarantorID == reg.GuarantorID);
                query.LeftJoin(room).On(room.RoomID == reg.RoomID);
                query.LeftJoin(medic).On(medic.ParamedicID == reg.ParamedicID);
                query.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
                query.InnerJoin(cls).On(cls.ClassID == reg.ChargeClassID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("ap");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID && qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (!txtRegDate1.IsEmpty && !txtRegDate2.IsEmpty)
                    query.Where(reg.RegistrationDate.Date().Between(txtRegDate1.SelectedDate, txtRegDate2.SelectedDate));

                if (!txtOrderDate.IsEmpty)
                    query.Where(query.OrderDate.Date() == txtOrderDate.SelectedDate);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                query.RegistrationNo == searchReg,
                                patient.MedicalNo == searchReg,
                                patient.OldMedicalNo == searchReg,
                                string.Format("< OR b.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR b.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where
                    (
                        query.IsOpr == true
                    );


                query.OrderBy(unit.ServiceUnitName.Ascending, query.RegistrationNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        private void InsertToMealOrder(string regNo)
        {
            using (var trans = new esTransactionScope())
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);

                var mealOrder = new MealOrder();
                var number = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.MealOrderOprNo);
                mealOrder.OrderNo = number.LastCompleteNumber;
               
                //--number--
                number.LastCompleteNumber = mealOrder.OrderNo;
                number.Save();

                //--header--
                mealOrder.OrderDate = (new DateTime()).NowAtSqlServer().Date;
                mealOrder.EffectiveDate = mealOrder.OrderDate;
                mealOrder.RegistrationNo = regNo;
                mealOrder.ServiceUnitID = reg.ServiceUnitID;
                mealOrder.ClassID = reg.ChargeClassID;
                mealOrder.BedID = reg.BedID;
                
                mealOrder.DietPatientNo = string.Empty;
                mealOrder.DietID = string.Empty;
                mealOrder.MenuID = string.Empty;
                mealOrder.MenuItemID = string.Empty;
                mealOrder.VersionID = string.Empty;
                mealOrder.SeqNo = string.Empty;
                mealOrder.FastingTime = string.Empty;
                mealOrder.IsAdditional = false;
                mealOrder.IsApproved = false;
                mealOrder.IsVoid = false;
                mealOrder.IsVerified = false;
                mealOrder.IsOpr = true;
                mealOrder.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                mealOrder.LastUpdateByUserID = AppSession.UserLogin.UserID;

                mealOrder.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
    }
}