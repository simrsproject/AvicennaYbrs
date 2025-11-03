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
    public partial class PatientEpisodeDiagnosisDetail : BasePageDialog
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
            //Load record
            EpisodeDiagnoseQuery query = new EpisodeDiagnoseQuery("a");
            DiagnoseQuery diag = new DiagnoseQuery("b");
            AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("e");
            ParamedicQuery param = new ParamedicQuery("d");

            query.InnerJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
            query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID);
            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

            query.Select
                (
                    query.RegistrationNo,
                    query.SequenceNo,
                    query.DiagnoseID,
                    diag.DiagnoseName,
                    item.ItemName,
                    param.ParamedicName,
                    query.IsChronicDisease,
                    query.IsOldCase,
                    query.IsConfirmed
                );

            query.Where
                (
                    query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
                    query.IsVoid == false
                );

            query.OrderBy(query.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
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