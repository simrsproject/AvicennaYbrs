using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Upload;

namespace Temiang.Avicenna.Module.RADT.PCare
{
    public partial class ProcessProgress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Do not display SelectedFilesCount progress indicator.
                progressArea.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;
            }

            progressArea.Localization.Uploaded = "Total Progress";
            progressArea.Localization.UploadedFiles = "Progress";
            progressArea.Localization.CurrentFileName = "Custom progress in action: ";
        }
    }
}