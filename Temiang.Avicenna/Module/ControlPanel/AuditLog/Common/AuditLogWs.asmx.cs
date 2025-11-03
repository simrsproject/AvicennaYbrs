
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.Module.RADT.Emr.MainContent;
using Temiang.Dal.DynamicQuery;
using DevExpress.Xpo.DB.Helpers;
using System.Web;

namespace Temiang.Avicenna.ControlPanel
{
    /// <summary>
    /// Summary description for EmrWebService
    /// </summary>
    ///

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AuditLogWs : System.Web.Services.WebService
    {
        #region Status untuk EMR List

        [WebMethod]
        public string GetStateCppt(string itype, string astp, string rimid, string fnm)
        {
            string fieldName = fnm;
            if (astp == "PHR") return string.Empty;

            if ((itype == "SOAP" || itype == "SBAR" || itype == "ADIME" || itype == "Notes") && astp == "") //Progress NOte
                return AuditLogLinkMenu("RegistrationInfoMedic", fieldName, string.Format("RegistrationInfoMedicID='{0}'", rimid));

            if (astp == "NurseNotes") // PPA Notes
            {
                if (fieldName == "Info1") fieldName = "S";
                else if (fieldName == "Info2") fieldName = "O";
                else if (fieldName == "Info3") fieldName = "A";
                else if (fieldName == "Info4") fieldName = "P";
                else if (fieldName == "Info5") fieldName = "Info5";

                return AuditLogLinkMenu("NursingDiagnosaTransDT", fieldName, string.Format("ID='{0}'", rimid));
            }

            if (itype == "SOAP" && astp != "")  // asesmen
                return AuditLogLinkMenu("RegistrationInfoMedic", fieldName, string.Format("RegistrationInfoMedicID='{0}'", rimid));

            if (itype == "CON" || itype == "REF")
            {
                if (fieldName == "Answer")
                    return AuditLogLinkMenu("ParamedicConsultRefer", "Answer", string.Format("ConsultReferNo='{0}'", rimid));
                else
                    return AuditLogLinkMenu("RegistrationInfoMedic", fieldName, string.Format("RegistrationInfoMedicID='{0}'", rimid));
            }

            if (itype == "MDS")
                return string.Empty;

            return string.Empty;
        }

        #region audit log link menu
        private string AuditLogLinkMenu(string tableName, string fieldName, string pkeyData)
        {
            var au = new AuditLogQuery("a");
            var aud = new AuditLogDataQuery("b");

            au.InnerJoin(aud).On(au.AuditLogID == aud.AuditLogID);
            au.Where(au.TableName == tableName, au.AuditActionType == "U", au.PrimaryKeyData == pkeyData, aud.ColumnName == fieldName);
            au.Select(au.ActionByUserID);
            au.es.Top = 1;
            var dtbEdited = au.LoadDataTable();

            if (dtbEdited.Rows.Count == 0) return string.Empty;


            var pkeyDataEncd = HttpUtility.UrlEncode(pkeyData);
            var linkMenu = string.Format(" <a href=\"javascript:void(0);\" title=\"Edit History\" onclick=\"javascript:openAuditLogView('{1}','{2}','{3}')\"><sub><img src='{0}/Images/Toolbar/history16.png'/></sub></a>",
                          Helper.UrlRoot(), tableName, fieldName, pkeyDataEncd);
            return linkMenu;
        }
        #endregion #region audit log link menu

        #endregion

    }
}
