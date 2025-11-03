using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class BedBookingInfoDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Admitting;
            var btkOk = (Button)Helper.FindControlRecursive(Master, "btnOk");
            var btkCancel = (Button)Helper.FindControlRecursive(Master, "btnCancel");
            btkOk.Visible = false;
            btkCancel.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var bed = new Bed();
                bed.LoadByPrimaryKey(Request.QueryString["id"]);

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(bed.RoomID);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(room.ServiceUnitID);

                txtBedID.Text = bed.BedID;
                txtServiceUnitName.Text = unit.ServiceUnitName;
                txtRoomName.Text = room.RoomName;
            }
        }

        protected void grdBedManagement_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = BedManagements();
        }

        private DataTable BedManagements()
        {
            var qa = new BedManagementQuery("a");
            var qb = new PatientQuery("b");
            var std = new AppStandardReferenceItemQuery("c");

            qa.LeftJoin(qb).On(qa.PatientID == qb.PatientID);
            qa.InnerJoin(std).On(
                qa.SRBedStatus == std.ItemID &&
                std.StandardReferenceID == AppEnum.StandardReference.BedStatus
                );

            qa.es.Top = AppSession.Parameter.MaxResultRecord;
            qa.Select
            (
                qa.BedManagementID,
                qa.TransactionDate,
                qb.MedicalNo,
                qa.RegistrationNo,
                qb.PatientName,
                @"<CASE WHEN b.FirstName IS NULL THEN RTRIM(RTRIM(a.FirstName + ' ' + a.MiddleName) + ' ' + a.LastName) ELSE RTRIM(RTRIM(b.FirstName + ' ' + b.MiddleName) + ' ' + b.LastName) END AS PatientName>",
                @"<CASE WHEN b.StreetName IS NULL THEN RTRIM(RTRIM(a.StreetName + ' ' + a.City) + ' ' + a.County) ELSE RTRIM(RTRIM(b.StreetName + ' ' + b.City) + ' ' + b.County) END AS Address>",
                qa.BedID,
                std.ItemName.As("BedStatusName")
            );

            qa.Where(qa.BedID == txtBedID.Text, qa.IsVoid == false, qa.IsReleased == false);
            qa.OrderBy(qa.TransactionDate.Ascending);

            var dtb = qa.LoadDataTable();

            return dtb;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
