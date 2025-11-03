using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public partial class DeceasedNotificationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var style = Request.Cookies["themes_" + AppSession.UserLogin.UserID]["themes"];
                fw_RadSkinManager.Skin = style != null ? style : System.Configuration.ConfigurationSettings.AppSettings["DefaultStyle"];
            }
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");
            var room = new ServiceRoomQuery("d");

            reg.Select(reg.RegistrationNo, pat.PatientName, unit.ServiceUnitName, room.RoomName, reg.BedID, pat.MedicalNo, pat.DeceasedDateTime);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
            reg.Where(reg.DischargeDate.IsNull(), reg.IsClosed == false, reg.SRRegistrationType.In(AppConstant.RegistrationType.InPatient), pat.IsAlive == false);
            reg.OrderBy(pat.DeceasedDateTime.Descending);

            grdList.DataSource = reg.LoadDataTable();
        }
    }
}