using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using System.Web.Script.Serialization;

namespace Temiang.Avicenna.Module.Kiosk
{
    public partial class PrescQueueDisplay3Col : Page //BasePageBootstrap
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "";
        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string GetBrandName() {
            var b = new Brand();
            return b.Name;
        }

        public string GetPrescriptionDisplayColsDefJS() {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(AppSession.Parameter.PrescriptionDisplayColumnsDef);
        }
    }
}