using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientSOAPEDetail : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        private DataTable Registrations
        {
            get
            {
                RegistrationQuery qr = new RegistrationQuery("r");

                PatientQuery qp = new PatientQuery("p");
                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                ParamedicQuery qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                ServiceUnitQuery unit = new ServiceUnitQuery("s");
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                ServiceRoomQuery room = new ServiceRoomQuery("d");
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);

                ClassQuery cq = new ClassQuery("e");
                qr.LeftJoin(cq).On(qr.ClassID == cq.ClassID);

                AppStandardReferenceItemQuery qregtype = new AppStandardReferenceItemQuery("f");
                qr.LeftJoin(qregtype).On
                    (
                        qregtype.ItemID == qr.SRRegistrationType & 
                        qregtype.StandardReferenceID == "RegistrationType"
                    );

                qr.Where(qr.PatientID == Page.Request.QueryString["patientID"]);

                qr.Select
                    (
                        qr.PatientID,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qr.RegistrationNo,
                        qr.IsClosed,
                        qr.IsVoid,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        cq.ClassName,
                        qr.BedID,
                        qr.SRRegistrationType,
                        qregtype.ItemName.As("RegistrationTypeName")
                    );
                qr.OrderBy(qr.RegistrationDate.Ascending);

                return qr.LoadDataTable();
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegisteredList.DataSource = Registrations;
        }
    }
}