using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for NotificationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NotificationService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDeceasedPatientContent(object context)
        {
            try
            {
                if (AppSession.UserLogin == null) return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
                if (string.IsNullOrEmpty(AppSession.UserLogin.UserID)) return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var grp = new AppUserUserGroupCollection();
                grp.Query.Where(grp.Query.UserID == AppSession.UserLogin.UserID);
                if (!grp.Query.Load()) JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var prg = new AppUserGroupProgramCollection();
                prg.Query.Where(prg.Query.UserGroupID.In(grp.Select(g => g.UserGroupID)));
                prg.Query.Where("<SUBSTRING(ProgramID, 1, 2) IN ('01', '02')>");
                if (!prg.Query.Load()) JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var reg = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");

                reg.Select("<COUNT(*) AS [JML]>");
                reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
                reg.Where(reg.DischargeDate.IsNull(), reg.IsClosed == false, reg.SRRegistrationType.In(AppConstant.RegistrationType.InPatient), pat.IsAlive == false);

                var table = reg.LoadDataTable();

                var img = new HtmlGenericControl("img");
                img.Attributes.Add("src", "@");
                img.Attributes.Add("alt", string.Empty);

                var td1 = new HtmlGenericControl("td");
                td1.Controls.Add(img);

                var td2 = new HtmlGenericControl("td");

                var link = new HtmlGenericControl("a");
                link.Attributes.Add("href", "#");
                link.Attributes.Add("onclick", "DeceasedPatientList();return false;");
                link.InnerText = string.Format("{0} patient is deceased.", table.Rows[0]["JML"].ToString());

                var td3 = new HtmlGenericControl("td");
                td3.Controls.Add(link);

                var tr = new HtmlGenericControl("tr");
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tr.Controls.Add(td3);

                var tbl = new HtmlGenericControl("table");
                tbl.Attributes.Add("align", "center");
                tbl.Controls.Add(tr);

                var wrapper = new HtmlGenericControl("div");
                wrapper.Attributes.Add("width", "100%");
                wrapper.Controls.Add(tbl);

                if (table.Rows[0]["JML"].ToInt() > 0)
                {
                    var sw = new StringWriter();
                    var writer = new HtmlTextWriter(sw);
                    wrapper.RenderControl(writer);

                    var json = JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "1", Value = sw.ToString() });
                    return json;
                }
                else return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetDeceasedPatientCount(object context)
        {
            try
            {
                if (AppSession.UserLogin == null) return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
                if (string.IsNullOrEmpty(AppSession.UserLogin.UserID)) return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var grp = new AppUserUserGroupCollection();
                grp.Query.Where(grp.Query.UserID == AppSession.UserLogin.UserID);
                if (!grp.Query.Load()) JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var prg = new AppUserGroupProgramCollection();
                prg.Query.Where(prg.Query.UserGroupID.In(grp.Select(g => g.UserGroupID)));
                prg.Query.Where("<SUBSTRING(ProgramID, 1, 2) IN ('01', '02')>");
                if (!prg.Query.Load()) JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });

                var reg = new RegistrationQuery("a");
                var pat = new PatientQuery("b");
                //var unit = new ServiceUnitQuery("c"); // Remark by Handono 20-05-11 ...Cost compare 67 : 33
                //var room = new ServiceRoomQuery("d");

                reg.Select("<COUNT(*) AS [JML]>");
                reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                //reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                //reg.InnerJoin(room).On(reg.RoomID == room.RoomID);
                reg.Where(reg.DischargeDate.IsNull(), reg.IsClosed == false, reg.SRRegistrationType.In(AppConstant.RegistrationType.InPatient), pat.IsAlive == false);

                var table = reg.LoadDataTable();


                if (table.Rows[0]["JML"].ToInt() > 0) return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "1", Value = string.Empty });
                else return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new NotificationServiceResponse() { Status = "0", Value = string.Empty });
            }
        }
    }

    public class NotificationServiceResponse
    {
        public string Status { get; set; }
        public string Value { get; set; }
    }
}
