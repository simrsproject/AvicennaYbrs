using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PatientTransferInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.RssaVerificationFinalizeBilling;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Patient Transfer Information for : " + pat.PatientName + " [MRN: " + pat.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PatientTransferHistorys();
        }

        private DataTable PatientTransferHistorys()
        {
            var query = new PatientTransferQuery("a");
            var rq = new RegistrationQuery("b");
            var fsu = new ServiceUnitQuery("c");
            var fsrq = new ServiceRoomQuery("d");
            var fbq = new BedQuery("f");
            var fcls = new ClassQuery("g");
            var fasriq = new AppStandardReferenceItemQuery("m");
            var tsu = new ServiceUnitQuery("e");
            var tsrq = new ServiceRoomQuery("i");
            var tbq = new BedQuery("k");
            var tcls = new ClassQuery("h");
            var tasriq = new AppStandardReferenceItemQuery("n");
            var fcc = new ClassQuery("fcc");
            var tcc = new ClassQuery("tcc");

            query.InnerJoin(rq).On(query.RegistrationNo == rq.RegistrationNo);
            query.InnerJoin(fsu).On(query.FromServiceUnitID == fsu.ServiceUnitID);
            query.InnerJoin(fsrq).On(query.FromRoomID == fsrq.RoomID);
            query.InnerJoin(fbq).On(query.FromBedID == fbq.BedID);
            query.InnerJoin(fcls).On(query.FromClassID == fcls.ClassID);
            query.LeftJoin(fasriq).On
                (
                    query.FromSpecialtyID == fasriq.ItemID &
                    fasriq.StandardReferenceID == "Specialty"
                );
            query.InnerJoin(tsu).On(query.ToServiceUnitID == tsu.ServiceUnitID);
            query.InnerJoin(tsrq).On(query.ToRoomID == tsrq.RoomID);
            query.InnerJoin(tbq).On(query.ToBedID == tbq.BedID);
            query.InnerJoin(tcls).On(query.ToClassID == tcls.ClassID);
            query.LeftJoin(tasriq).On
                (
                    query.ToSpecialtyID == tasriq.ItemID &
                    tasriq.StandardReferenceID == "Specialty"
                );
            query.InnerJoin(fcc).On(fcc.ClassID == query.FromChargeClassID);
            query.InnerJoin(tcc).On(tcc.ClassID == query.ToChargeClassID);
            query.Select
                (
                    query.TransferNo,
                    query.TransferDate,
                    query.TransferTime,
                    fsu.ServiceUnitName.As("FromUnit"),
                    fsrq.RoomName.As("FromRoomName"),
                    fbq.BedID.As("FromBedID"),
                    fcls.ClassName.As("FromClass"),
                    fasriq.ItemName.As("FromSpecialtyName"),
                    tsu.ServiceUnitName.As("ToUnit"),
                    tsrq.RoomName.As("ToRoomName"),
                    tbq.BedID.As("ToBedID"),
                    tcls.ClassName.As("ToClass"),
                    tasriq.ItemName.As("ToSpecialtyName"),
                    fcc.ClassName.As("FromChargeClass"),
                    tcc.ClassName.As("ToChargeClass"),
                    query.LastUpdateByUserID
                );
            query.Where(query.RegistrationNo == Request.QueryString["regNo"], query.IsApprove == true);

            query.OrderBy(query.TransferNo.Ascending);
            
            var dtb = query.LoadDataTable();

            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
