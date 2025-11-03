using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public class BaseMainContentCtl : System.Web.UI.UserControl
    {
        #region Field informasi registrasi yg sedang dibuka
        public bool IsNewPatient
        {
            get { return Convert.ToBoolean(ViewState["_mc_isNewPatient"]); }
            set { ViewState["_mc_isNewPatient"] = value; }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
 
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        public string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }
        public bool IsClosed
        {
            get { return Convert.ToBoolean(ViewState["_mc_isclosed"]); }
            set { ViewState["_mc_isclosed"] = value; }
        }

        protected virtual List<string> MergeRegistrations
        {
            get
            {
                return AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            }
        }
        #endregion

        protected bool CheckAccess
        {
            get
            {
                if (!IsUserEditAble)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "unauthorized", "alert('Unauthorized access');", true);
                    return false;
                }
                return true;
            }
        }
        public bool IsUserEditAble
        {
            get { return Convert.ToBoolean(ViewState["_mc_isueditable"]); }
            set { ViewState["_mc_isueditable"] = value; }
        }
        public bool IsUserAddAble
        {
            get { return Convert.ToBoolean(ViewState["_mc_isuaddable"]); }
            set { ViewState["_mc_isuaddable"] = value; }
        }


        protected bool IsUserInParamedicTeam(string registrationNo, bool isPostBack, string serviceUnitID)
        {
            return BasePage.IsUserInParamedicTeam(registrationNo, isPostBack, serviceUnitID, RegistrationType);
        }

        protected string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }

        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }

                return _patientRelateds;
            }
        }


        protected void ShowPrintPreview()
        {
            var winPrintPreview = (RadWindow)Helper.FindControlRecursive(this.Page, "winPrintPreview");
            var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
            Helper.ShowRadWindowAfterPostback(winPrintPreview, url, "preview", true);
        }
    }
}