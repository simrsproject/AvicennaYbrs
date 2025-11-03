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
    // Encounter History -> Module/RADT/MedicalRecordHistory/MedicalRecordHistoryList.aspx
    public partial class PatientEpisodeSOAPEDetail : BasePageDialog
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
            var regno = e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString();
            //Load record
            EpisodeSOAPEQuery query = new EpisodeSOAPEQuery("a");
            ParamedicQuery param = new ParamedicQuery("b");

            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

            query.Select
                (
                    query.RegistrationNo,
                    query.SequenceNo,
                    param.ParamedicName,
                    query.SOAPEDate,
                    query.SOAPETime,
                    query.Subjective,
                    query.Objective,
                    query.Assesment,
                    query.Planning
                );

            query.Where
                (
                    query.RegistrationNo == regno,
                    query.IsVoid == false,
                    query.Or(query.Imported.IsNull(), query.Imported == false)
                    );
            var dtbSoape = query.LoadDataTable();

            // Add from RegistrationInfoMedic
            var rimQr = new RegistrationInfoMedicQuery("rim");
            var medicQr2 = new ParamedicQuery("d");
            rimQr.LeftJoin(medicQr2).On(rimQr.ParamedicID == medicQr2.ParamedicID);

            rimQr.Where(rimQr.RegistrationNo == regno, rimQr.Or(rimQr.IsDeleted.IsNull(), rimQr.IsDeleted == false));
            rimQr.Select(rimQr, medicQr2.ParamedicName);

            var dtbRim = rimQr.LoadDataTable();


            foreach (DataRow rim in dtbRim.Rows)
            {
                var row = dtbSoape.NewRow();

                row["RegistrationNo"] = rim["RegistrationNo"];
                row["SequenceNo"] = string.Empty;
                row["SOAPEDate"] = rim["DateTimeInfo"];
                row["SOAPETime"] = Convert.ToDateTime(rim["DateTimeInfo"]).ToString("HH:mm");

                row["Subjective"] = rim["Info1"];
                row["Objective"] = rim["Info2"];
                row["Assesment"] = rim["Info3"];
                row["Planning"] = rim["Info4"];

                row["ParamedicName"] = rim["ParamedicName"];

                dtbSoape.Rows.Add(row);
            }

            // return sorted
            dtbSoape.DefaultView.Sort = "RegistrationNo asc,SequenceNo asc";
            e.DetailTableView.DataSource = dtbSoape.DefaultView.ToTable();
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
                //qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                //ParamedicQuery qm = new ParamedicQuery("m");
                //qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                //ServiceUnitQuery unit = new ServiceUnitQuery("s");
                //qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                //ServiceRoomQuery room = new ServiceRoomQuery("d");
                //qr.LeftJoin(room).On(qr.RoomID == room.RoomID);

                //qr.Where
                //    (
                //        qr.PatientID == Page.Request.QueryString["patientID"],
                //        qr.IsVoid == false
                //    );

                //qr.Select
                //    (
                //        qr.PatientID,
                //        qr.RegistrationDate,
                //        qr.RegistrationNo,
                //        qp.PatientName,
                //        qp.Sex,
                //        qm.ParamedicName,
                //        unit.ServiceUnitName,
                //        room.RoomName,
                //        qr.BedID
                //    );

                //qr.OrderBy(qr.RegistrationDate.Ascending);

                //return qr.LoadDataTable();
            }
        }
    }
}