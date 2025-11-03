using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class LocalistViewer : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }

        private string AssessmentType
        {
            get
            {
                return Request.QueryString["astp"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";


        }

        protected void imgGal_OnNeedDataSource(object sender, ImageGalleryNeedDataSourceEventArgs e)
        {
            var bd = new RegistrationInfoMedicBodyDiagramQuery("bd");
            bd.Where(bd.RegistrationInfoMedicID == RegistrationInfoMedicID);
            var dtb = bd.LoadDataTable();
            imgGal.DataSource = dtb;
        }
    }
}