using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class InosInfectionMonitoringList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.INOSInfectionMonitoring;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
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
                var room = new ServiceRoomCollection();
                room.Query.Where
                    (
                        room.Query.IsActive == true
                    );
                room.Query.OrderBy(room.Query.ServiceUnitID.Ascending, room.Query.RoomID.Ascending);
                room.LoadAll();

                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceRoom entity in room)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceRooms;
        }

        private DataTable ServiceRooms
        {
            get
            {
                var query = new ServiceUnitQuery("a");
                var room = new ServiceRoomQuery("b");
                var reg = new RegistrationQuery("c");
                var pat = new PatientQuery("d");
                query.InnerJoin(room).On(room.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(reg).On(reg.RoomID == room.RoomID);
                query.InnerJoin(pat).On(pat.PatientID == reg.PatientID);

                query.Select(query.ServiceUnitID, query.ServiceUnitName, room.RoomID, room.RoomName);

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient, 
                    query.IsActive == true, 
                    room.IsActive == true,
                    reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    reg.IsVoid == false,
                    reg.IsClosed == false,
                    query.Or(reg.DischargeDate.IsNull(), reg.SRDischargeMethod == string.Empty));

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(room.RoomID == cboRoomID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            reg.RegistrationNo == searchReg,
                            pat.MedicalNo == searchReg,
                            pat.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(d.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(d.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                         string.Format("<RTRIM(d.FirstName+' '+d.MiddleName)+' '+d.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                query.es.Distinct = true;

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detail":
                    {
                        gridListLoadDetail(e);
                        break;
                    }
                case "detailInos":
                    {
                        gridListLoadDetailInos(e);
                        break;
                    }
            }
        }

        private void gridListLoadDetail(GridDetailTableDataBindEventArgs e)
        {
            var query = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var par = new ParamedicQuery("c");
            var guar = new GuarantorQuery("d");
            var sal = new AppStandardReferenceItemQuery("e");
            
            query.Select
                (
                    query.RegistrationNo,
                    query.RegistrationDate,
                    query.RegistrationTime,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.Sex,
                    query.RoomID,
                    query.BedID,
                    query.ParamedicID,
                    par.ParamedicName,
                    query.GuarantorID,
                    guar.GuarantorName,
                    sal.ItemName.As("SalutationName")
                );

            query.InnerJoin(patient).On(patient.PatientID == query.PatientID);
            query.LeftJoin(par).On(par.ParamedicID == query.ParamedicID);
            query.LeftJoin(guar).On(guar.GuarantorID == query.GuarantorID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" && sal.ItemID == patient.SRSalutation);
            query.Where
                (
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.RoomID == e.DetailTableView.ParentItem.GetDataKeyValue("RoomID").ToString(),
                    query.IsVoid == false,
                    query.IsClosed == false,
                    query.Or(query.DischargeDate.IsNull(), query.SRDischargeMethod == string.Empty)
                );
            
            query.OrderBy(query.RegistrationNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private void gridListLoadDetailInos(GridDetailTableDataBindEventArgs e)
        {
            var query = new INOSInfectionMonitoringQuery("a");
            var reg = new RegistrationQuery("b");
            query.InnerJoin(reg).On(reg.RegistrationNo == query.RegistrationNo);
            query.Select(query, reg.RoomID);
            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString()
                );

            query.OrderBy(query.MonitoringDate.Ascending);

            DataTable dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }
    }
}