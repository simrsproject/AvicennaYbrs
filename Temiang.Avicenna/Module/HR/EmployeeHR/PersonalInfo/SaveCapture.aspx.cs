using System;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeePersonalInformation
{
    public partial class SaveCapture : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Save to file
            Image tmpImage = Image.FromStream(Request.InputStream);
            tmpImage.Save(Server.MapPath("Image" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".jpg"));
            
        }
    }
}