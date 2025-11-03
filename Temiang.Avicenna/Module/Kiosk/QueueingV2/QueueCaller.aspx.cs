using System;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Kiosk.QueueingV2
{
    public partial class QueueCaller : BasePage
    {
        public string getUserID()
        {
            return AppSession.UserLogin.UserID;
        }
        public string BaseURL()
        {
            return Helper.UrlRoot2();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.QueueList;

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {

            }
        }

        public void Page_Load()
        {
            if (!IsPostBack)
            {

            }
        }
    }
}