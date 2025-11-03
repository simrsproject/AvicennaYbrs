using System;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl
{
    public partial class VitalSignInfoCtl : System.Web.UI.UserControl
    {
        public string RegistrationNo => Request.QueryString["regno"];

        private DateTime? _vitalSignDateTime = null;
        public DateTime? VitalSignDateTime
        {
            get
            {
                return _vitalSignDateTime;
            }
            set
            {
                _vitalSignDateTime = value;
            }
        }

        public bool IsShowHeader
        {
            get
            {
                if (ViewState["ishd"] == null)
                    return true;

                return Convert.ToBoolean(ViewState["ishd"]);
            }
            set
            {
                ViewState["ishd"] = value;
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            grdVitalSign.ShowHeader = IsShowHeader;
        }

    }
}