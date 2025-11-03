using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class ReservationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ReservationSearch.aspx";
            UrlPageDetail = "ReservationDetail.aspx";

            ProgramID = AppConstant.Program.Reservation;
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
            grdList.DataSource = Reservations;
        }

        private DataTable Reservations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ReservationQuery qa;
                if (Session[SessionNameForQuery] != null)
                    qa = (ReservationQuery)Session[SessionNameForQuery];
                else
                {
                    qa = new ReservationQuery("a");
                    var qc = new ServiceUnitQuery("c");
                    var qd = new ServiceRoomQuery("d");
                    var qe = new ClassQuery("e");
                    var std = new AppStandardReferenceItemQuery("f");
                    

                    qa.InnerJoin(qc).On(qa.ServiceUnitID == qc.ServiceUnitID);
                    qa.LeftJoin(qd).On(qa.RoomID == qd.RoomID);
                    qa.LeftJoin(qe).On(qa.ClassID == qe.ClassID);
                    qa.InnerJoin(std).On(
                        qa.SRReservationStatus == std.ItemID &&
                        std.StandardReferenceID == AppEnum.StandardReference.AppointmentStatus
                        );

                    qa.es.Top = AppSession.Parameter.MaxResultRecord;


                    var qb = new PatientQuery("b");
                    qa.LeftJoin(qb).On(qa.PatientID == qb.PatientID);
                    var qsal = new AppStandardReferenceItemQuery("sal");
                    qa.LeftJoin(qsal).On(qsal.StandardReferenceID == "Salutation" & qb.SRSalutation == qsal.ItemID);

                    qa.Select
                    (
                        qa.ReservationNo,
                        qa.ReservationDate.As("ReservationDateTime"),
                        qb.MedicalNo,
                        qb.PatientName,
                       //"<ISNULL(sal.ItemName, '') + (((a.FirstName + '' + a.MiddleName).LTrim()).RTrim() + ' ' + a.LastName) As 'PatientName' >",
                       //"<ISNULL(sal.ItemName, '') + RTRIM(a.FirstName + ' ' + RTRIM(a.MiddleName) + ' ' + RTRIM(a.LastName)) As 'PatientName'  >",
                        qa.StreetName.As("Address"),
                        qc.ServiceUnitName,
                        qd.RoomName,
                        qe.ClassName,
                        qa.BedID,
                        qa.Notes,
                        std.ItemName,
                        "<ISNULL(sal.ItemName, '') As 'SalutationName'>"
                    );
                }

                DataTable dtb = qa.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

