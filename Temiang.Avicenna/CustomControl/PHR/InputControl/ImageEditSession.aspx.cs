using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.CustomControl.PHR.InputControl
{
    /// <summary>
    /// Store to session for open in editor image -> ImageEdit.aspx
    /// Sub program ImageEdit
    /// </summary>
    /// <example>
    /// See on PhrCtl.ascx
    /// </example>
    /// ----------------------------------------
    /// Created By: Handono
    /// ----------------------------------------

    public partial class ImageEditSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.InputStream.Length > 0) // Call from POST Method
                {
                    //called page form json for creating imgBase64 in image
                    var reader = new StreamReader(Request.InputStream);
                    var data = Server.UrlDecode(reader.ReadToEnd());
                    reader.Close();

                    // Remove parameter name and store to session for open in editor image
                    HttpContext.Current.Session["editedImage"] = data.Replace("imgBase64=",String.Empty);
                }
                else
                {
                    Session["editedImage"] = string.Empty;
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string Dummy()
        {
            // Nothing to process, just for calling from javascript
            return "OK";
        }
    }
}