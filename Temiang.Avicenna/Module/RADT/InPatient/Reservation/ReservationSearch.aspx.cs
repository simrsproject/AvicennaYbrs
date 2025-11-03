using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class ReservationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Reservation;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var qa = new ReservationQuery("a");
            var qb = new PatientQuery("b");
            var qc = new ServiceUnitQuery("c");
            var qd = new ServiceRoomQuery("d");
            var qe = new ClassQuery("e");
            var std = new AppStandardReferenceItemQuery("f");
            var qsal = new AppStandardReferenceItemQuery("sal");


            qa.LeftJoin(qb).On(qa.PatientID == qb.PatientID);
            qa.InnerJoin(qc).On(qa.ServiceUnitID == qc.ServiceUnitID);
            qa.LeftJoin(qd).On(qa.RoomID == qd.RoomID);
            qa.LeftJoin(qe).On(qa.ClassID == qe.ClassID);
            qa.InnerJoin(std).On(
                qa.SRReservationStatus == std.ItemID &&
                std.StandardReferenceID == AppEnum.StandardReference.AppointmentStatus
                );
            qa.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qsal.ItemID == qb.SRSalutation);
            
            qa.es.Top = AppSession.Parameter.MaxResultRecord;


            qa.Select
            (
                qa.ReservationNo,
                qa.ReservationDate,
                qb.MedicalNo,
                qb.PatientName,
                qb.Address,
                qc.ServiceUnitName,
                qd.RoomName,
                qe.ClassName,
                qa.BedID,
                qa.Notes,
                std.ItemName,
                qsal.ItemName.As("SalutationName")
            );

            if (!txtReservationDate.SelectedDate.ToString().Trim().Equals(string.Empty))               
            {
                qa.Where(qa.ReservationDate >= txtReservationDate.SelectedDate, qa.ReservationDate < txtReservationDate.SelectedDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(txtReservationNo.Text))
            {
                if (cboFilterReservationNo.SelectedIndex == 1)
                    qa.Where(qa.ReservationNo == txtReservationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReservationNo.Text);
                    qa.Where(qa.ReservationNo.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            {
                if (cboFilterMedicalNo.SelectedIndex == 1)
                    qa.Where(qb.MedicalNo == txtMedicalNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMedicalNo.Text);
                    qa.Where(qb.MedicalNo.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtPatientName.Text))
            {
                if (cboFilterPatientName.SelectedIndex == 1)
                {
                    //string searchTextContain = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    //qa.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchTextContain)
                    //    );
                    qa.Where(qb.PatientName == txtPatientName.Text);
                }
                else
                {
                    string searchTextContain = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qa.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchTextContain)
                        );
                    //string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
                    //qa.Where(qb.PatientName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = qa;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
