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

namespace Temiang.Avicenna.Module.Emr.Phr
{
    public partial class BookingReferenceDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var booking = new ServiceUnitBookingQuery("a");
            var room = new ServiceRoomQuery("b");
            var md = new ParamedicQuery("c");
            var an = new ParamedicQuery("d");

            booking.Select(
                booking.BookingNo,
                booking.BookingDateTimeFrom,
                booking.BookingDateTimeTo,
                booking.RealizationDateTimeFrom,
                booking.RealizationDateTimeTo,
                md.ParamedicName,
                an.ParamedicName.As("AnesthesiologistName"),
                booking.Notes
                );

            booking.InnerJoin(room).On(booking.ServiceUnitID == room.ServiceUnitID && booking.RoomID == room.RoomID);
            booking.LeftJoin(md).On(booking.ParamedicID == md.ParamedicID);
            booking.LeftJoin(an).On(booking.ParamedicIDAnestesi == an.ParamedicID);

            if (!txtBookingDate.IsEmpty) booking.Where(booking.BookingDateTimeFrom.Date() == txtBookingDate.SelectedDate.Value.Date);

            booking.Where(
                booking.RegistrationNo == Request.QueryString["regno"],
                booking.Or(booking.IsVoid.IsNull(), booking.IsVoid == false)
                );

            booking.OrderBy(booking.BookingNo.Descending);

            DataTable dtb = booking.LoadDataTable();

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            booking = new ServiceUnitBookingQuery("a");
            room = new ServiceRoomQuery("b");
            md = new ParamedicQuery("c");
            an = new ParamedicQuery("d");

            booking.Select(
                booking.BookingNo,
                booking.BookingDateTimeFrom,
                booking.BookingDateTimeTo,
                booking.RealizationDateTimeFrom,
                booking.RealizationDateTimeTo,
                md.ParamedicName,
                an.ParamedicName.As("AnesthesiologistName"),
                booking.Notes
                );

            booking.InnerJoin(room).On(booking.ServiceUnitID == room.ServiceUnitID && booking.RoomID == room.RoomID);
            booking.LeftJoin(md).On(booking.ParamedicID == md.ParamedicID);
            booking.LeftJoin(an).On(booking.ParamedicIDAnestesi == an.ParamedicID);

            if (!txtBookingDate.IsEmpty) booking.Where(booking.BookingDateTimeFrom.Date() == txtBookingDate.SelectedDate.Value.Date);

            booking.Where(
                booking.PatientID == reg.PatientID,
                booking.RegistrationNo == string.Empty, 
                booking.IsVoid == false,
                booking.IsExtendedSurgery == false
                );

            booking.OrderBy(booking.BookingNo.Descending);

            DataTable dtb2 = booking.LoadDataTable();
            dtb.Merge(dtb2);

            grdList.DataSource = dtb;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument.id = '{0}'", (grdList.SelectedValue ?? string.Empty));
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
