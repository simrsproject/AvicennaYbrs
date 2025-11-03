using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class MotherRegistrationList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Hak akses ikut program parentnya
            //ProgramID = AppConstant.Program.Admitting;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.regno = '" + grdRegisteredList.SelectedValue + "'";
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

                qr.Where
                    (
                        qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        qr.IsVoid == false,
                        qp.Sex == "F",
                        qr.IsParturition == true
                    );
                if (!chkIncludeCheckedOut.Checked)
                {
                    qr.Where(qr.DischargeDate.IsNull(), qr.IsClosed == false);
                }

                if (!txtPatientSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtPatientSearch.Text);
                    qr.Where
                        (
                            qp.Or
                                (
                                    qp.FirstName.Like(searchTextContain),
                                    qp.MiddleName.Like(searchTextContain),
                                    qp.LastName.Like(searchTextContain)
                                )
                        );
                }

                if (!txtMedicalNo.Text.Trim().Equals(string.Empty))
                    qr.Where(qp.MedicalNo == txtMedicalNo.Text);

                qr.Select
                    (
                        qr.PatientID,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qr.RegistrationNo,
                        qr.IsClosed,
                        qr.IsVoid,
                        qr.IsFromDispensary,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        cq.ClassName,
                        qr.BedID,
                        qr.IsConsul,
                        qr.SRRegistrationType
                    );

                return qr.LoadDataTable();
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegisteredList.DataSource = Registrations;
        }

        protected void btnSearchPatient_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.Rebind();
        }
    }
}
