using System;
using System.Data;
using System.Web.UI.WebControls;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class ReservationListByDate : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ReservationListDetail.aspx";
            UrlPageDetail = "ReservationDetail.aspx";

            ProgramID = AppConstant.Program.Reservation; //TODO: Isi ProgramID
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(ReservationMetadata.ColumnNames.ReservationNo).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            Session["Reservation"] = null;
            grdList.DataSource = Reservations;
        }

        private DataTable Reservations
        {
            get
            {
                DataTable dtdReservation = (new ReservationCollection()).GetJadualBed();

                return dtdReservation;

            }
        }

    }
}

