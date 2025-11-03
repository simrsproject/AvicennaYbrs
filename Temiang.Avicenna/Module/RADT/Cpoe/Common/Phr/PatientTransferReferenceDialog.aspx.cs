using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientTransferReferenceDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Select Patient Transfer Record";
                txtTransferDate.SelectedDate = DateTime.Today;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var trf = new PatientTransferHistoryQuery("a");
            
            var su = new ServiceUnitQuery("s");
            trf.InnerJoin(su).On(trf.ServiceUnitID == su.ServiceUnitID);

            var room = new ServiceRoomQuery("b");
            trf.InnerJoin(room).On(trf.ServiceUnitID == room.ServiceUnitID & trf.RoomID == room.RoomID);

            var trfBy = new AppUserQuery("tby");
            trf.LeftJoin(trfBy).On(trf.TransferredByUserID == trfBy.UserID);

            var rcBy = new AppUserQuery("rby");
            trf.LeftJoin(rcBy).On(trf.ReceivedByUserID == rcBy.UserID);

            trf.Select(
                "<CASE WHEN a.TransferNo='' THEN '-' ELSE a.TransferNo END as TransferNo>",
                trf.DateOfEntry,
                trf.TimeOfEntry,
                trf.ArrivedDateTime,
                su.ServiceUnitName,
                room.RoomName,
                trf.BedID,
                trfBy.UserName.As("TransferByUserName"),
                rcBy.UserName.As("ReceivedByUserName")
                );

            if (!txtTransferDate.IsEmpty)
                trf.Where(trf.DateOfEntry == txtTransferDate.SelectedDate.Value.Date);

            trf.Where(trf.RegistrationNo == Request.QueryString["regno"]);

            trf.OrderBy(trf.TransferNo.Descending);

            var dtb = trf.LoadDataTable();
            grdList.DataSource = dtb;
        }

        public override bool OnButtonOkClicked()
        {
            var qs = string.Empty;
            foreach (var key in Request.QueryString.AllKeys)  
            {  
                if (key!="refno")
                    qs = string.Concat(qs, "&", key, "=", Request.QueryString[key]);  
            }  

            Response.Redirect(string.Format("PatientHealthRecordDetail.aspx?refno={0}{1}", grdList.SelectedValue ?? string.Empty, qs));
            return true;
        }
    }
}
