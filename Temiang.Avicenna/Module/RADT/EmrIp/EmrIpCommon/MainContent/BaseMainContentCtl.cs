using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent
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
 
        public virtual string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
            set
            {
                var dummy = value;
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
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
        #endregion

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

        protected bool IsUserParamedicCanNotEdit(string registrationNo, string recordParamedicID)
        {
            if (IsUserEditAble.Equals(false)) return true;

            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                return true;
            }
            if (AppSession.UserLogin.ParamedicID.Equals(recordParamedicID))
                return false;

            return !IsUserInParamedicTeam(registrationNo);
        }

        protected bool IsUserParamedicCanNotAdd()
        {
            if (this.IsUserAddAble.Equals(false)) return true;

            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                // User selain dokter bisa tambah record dan hak aksesnya diset di entriannya page yg dipanggil
                return false;
            }
            else
            {
                if (AppSession.UserLogin.ParamedicID.Equals(ParamedicID))
                    return false;
            }


            return !IsUserInParamedicTeam(RegistrationNo);
        }

        private bool IsUserInParamedicTeam(string registrationNo)
        {
            // Jika user paramedic cek apakah termasuk Paramedic Team nya
            if (IsPostBack)
            {
                if (Session["IsUserInParamedicTeam"] != null)
                    return (bool) Session["IsUserInParamedicTeam"];
            }

            var qrPt = new ParamedicTeamQuery("pt");
            qrPt.Where(qrPt.RegistrationNo == registrationNo && qrPt.ParamedicID == AppSession.UserLogin.ParamedicID &&
                       (qrPt.EndDate.IsNull() || qrPt.EndDate >= DateTime.Today));
            var dtbPt = qrPt.LoadDataTable();
            bool retval = dtbPt != null && dtbPt.Rows.Count > 0;

            Session["IsUserInParamedicTeam"] = retval;
            return retval;
        }

        protected void ShowPrintPreview()
        {
            var winPrintPreview = (RadWindow)Helper.FindControlRecursive(this.Page, "winPrintPreview");
            var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
            Helper.ShowRadWindowAfterPostback(winPrintPreview, url, "preview", true);
        }

    }
}