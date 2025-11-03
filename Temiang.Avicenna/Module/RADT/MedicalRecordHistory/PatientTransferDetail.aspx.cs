using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientTransferDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var transfer = new PatientTransferQuery("b");

            var unit1 = new ServiceUnitQuery("c");
            var room1 = new ServiceRoomQuery("d");
            var chcl1 = new ClassQuery("cc1");

            var unit2 = new ServiceUnitQuery("e");
            var room2 = new ServiceRoomQuery("f");
            var chcl2 = new ClassQuery("cc2");

            reg.Select
                (
                    reg.RegistrationNo,
                    transfer.TransferNo,
                    transfer.TransferDate,
                    transfer.TransferTime,
                    unit1.ServiceUnitName.As("FromServiceUnitName"),
                    room1.RoomName.As("FromRoomName"),
                    transfer.FromBedID,
                    unit2.ServiceUnitName.As("ToServiceUnitName"),
                    room2.RoomName.As("ToRoomName"),
                    transfer.ToBedID,
                    transfer.IsApprove,
                    transfer.IsVoid,
                    chcl1.ClassName.As("FromChargeClassName"),
                    chcl2.ClassName.As("ToChargeClassName")
                );
            reg.InnerJoin(transfer).On(reg.RegistrationNo == transfer.RegistrationNo);

            reg.InnerJoin(unit1).On(transfer.FromServiceUnitID == unit1.ServiceUnitID);
            reg.InnerJoin(room1).On(transfer.FromRoomID == room1.RoomID);
            reg.LeftJoin(chcl1).On(transfer.FromChargeClassID == chcl1.ClassID);

            reg.InnerJoin(unit2).On(transfer.ToServiceUnitID == unit2.ServiceUnitID);
            reg.InnerJoin(room2).On(transfer.ToRoomID == room2.RoomID);
            reg.LeftJoin(chcl2).On(transfer.ToChargeClassID == chcl2.ClassID);

            reg.Where(reg.PatientID == Request.QueryString["patientID"]);

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                reg.Where(reg.RegistrationNo == txtRegistrationNo.Text);

            reg.OrderBy
                (
                    transfer.RegistrationNo.Descending,
                    transfer.TransferDate.Ascending,
                    transfer.TransferTime.Ascending
                );

            grdList.DataSource = reg.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}
