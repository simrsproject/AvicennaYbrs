using System;
using System.Data;
using System.Web.UI;
// Remark by Handono, 
// Ada perubahan design Questionaire, tunggu entrian selesai dulu
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientHealthRecordDetailList : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.MedicalRecordHistory;

            if (!IsPostBack)
            {
                ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Registrations;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //PatientHealthRecordQuery query = new PatientHealthRecordQuery("a");
            //EmployeeQuery qe = new EmployeeQuery("e");
            //RegistrationQuery qr = new RegistrationQuery("r");

            //query.Select
            //    (
            //            query.RecordDate,
            //            query.RecordTime,
            //            query.SequenceNo,
            //            query.EmployeeID,
            //            qe.EmployeeName,
            //            query.RegistrationNo);

            //query.InnerJoin(qr).On(query.RegistrationNo == qr.RegistrationNo);
            //query.LeftJoin(qe).On(query.EmployeeID == qe.EmployeeID);

            //query.Where
            //    (
            //        query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
            //        qr.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient
            //    );
            //query.OrderBy(query.RecordDate.Descending);

            //e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private DataTable Registrations
        {
            get
            {
                string patientID = Page.Request.QueryString["patientID"].ToString();

                DataTable dtdRegistration = (new RegistrationCollection()).RegistrationHistoryForSOAP(patientID);

                return dtdRegistration;

                //RegistrationQuery qr = new RegistrationQuery("r");
                //PatientQuery qp = new PatientQuery("p");
                //ParamedicQuery qm = new ParamedicQuery("m");
                //ServiceUnitQuery unit = new ServiceUnitQuery("s");
                //ServiceRoomQuery room = new ServiceRoomQuery("d");
                //AppStandardReferenceItemQuery std = new AppStandardReferenceItemQuery("t");

                //qr.es.Top = AppSession.Parameter.MaxResultRecord;

                //qr.Select
                //    (
                //        qr.PatientID,
                //        qr.RegistrationDate,
                //        qr.RegistrationTime,
                //        qr.RegistrationNo,
                //        qp.MedicalNo,
                //        qp.PatientName,
                //        qp.Sex,
                //        qm.ParamedicName,
                //        unit.ServiceUnitName,
                //        room.RoomName,
                //        std.ItemName
                //    );

                //qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                //qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                //qr.InnerJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                //qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                //qr.LeftJoin(std).On(qr.SRTriage == std.ItemID);

                //qr.Where
                //    (
                //        qr.PatientID == Page.Request.QueryString["patientID"],
                //        qr.IsVoid == false
                //    );

                //qr.OrderBy(qr.RegistrationDate.Descending);

                //return qr.LoadDataTable();

            }
        }
    }
}
